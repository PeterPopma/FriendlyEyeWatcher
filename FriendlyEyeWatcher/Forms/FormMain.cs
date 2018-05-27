using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace FriendlyEyeWatcher.Forms
{
    public partial class FormMain : Form
    {
        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndIntertAfter, int X, int Y, int cx, int cy, int uFlags);
        [DllImport("user32.dll")]
        private static extern int GetSystemMetrics(int Which);

        static HttpClient client = new HttpClient();
        private static System.Windows.Forms.Timer updateScreenTimer;
        bool pendingRequest;
        const string HOST_URL = "http://localhost:8000/";

        string imageSetID;
        string reviewerID;
        int numFrames;
        int currentFrameNumber;
        string numVotesYes;
        string numVotesNo;

        Bitmap[] imageFrame = new Bitmap[10];

        public FormMain()
        {
            InitializeComponent();
            SetupTimer();
            outlineLabelStatus.Parent = pictureBoxImage;
            outlineLabelStatus.BackColor = Color.Transparent;
            outlineLabelStatus.Text = "Cannot connect to server!";
            outlineLabelStatus.ForeColor = Color.OrangeRed;
            outlineLabelHints.Parent = pictureBoxImage;
            outlineLabelHints.BackColor = Color.Transparent;
            outlineLabelHints.Text = "";
//            outlineLabelHints.MaximumSize = new Size(1500, 0);
//            outlineLabelHints.AutoSize = true;
            outlineLabelReal.Parent = pictureBoxImage;
            outlineLabelReal.BackColor = Color.Transparent;
            outlineLabelReal.Visible = false;
            buttonYes.Parent = pictureBoxImage;
            buttonYes.BackColor = Color.Transparent;
            buttonNo.Parent = pictureBoxImage;
            buttonNo.BackColor = Color.Transparent;
            outlineLabelPurpose.Parent = pictureBoxImage;
            outlineLabelPurpose.BackColor = Color.Transparent;
            UpdateButtons(false);

            reviewerID = new Random().Next(100000, 999999).ToString();

            // Go Fullscreen
            /*
            WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.None;
            TopMost = true;
            SetWindowPos(Handle, IntPtr.Zero, 0, 0, GetSystemMetrics(0), GetSystemMetrics(1), 64);
            */
            client.BaseAddress = new Uri(HOST_URL);
        }

        void UpdateButtons(bool show)
        {
            buttonYes.Visible = show;
            buttonNo.Visible = show;
            panelNavigation.Visible = show;
            outlineLabelReal.Visible = show;
            outlineLabelPurpose.Visible = show;
            outlineLabelHints.Visible = show;
        }

        void ResetFrames()
        {
            for(int k=0; k<imageFrame.Length; k++)
            {
                imageFrame[k] = null;
            }
            pictureBoxImage.Image = null;
            numFrames = 0;
            outlineLabelHints.Text = "";
            UpdateButtons(false);
            outlineLabelReal.Visible = false;
        }

        private void SetupTimer()
        {
            // Create a timer with a 100 msec interval.
            updateScreenTimer = new System.Windows.Forms.Timer();
            updateScreenTimer.Interval = 80;
            updateScreenTimer.Tick += new EventHandler(OnTimedEventUpdateScreen);
            updateScreenTimer.Start();
        }

        public static string SplitToLines(string text, char[] splitOnCharacters, int maxStringLength)
        {
            var sb = new StringBuilder();
            var index = 0;

            while (text.Length > index)
            {
                // start a new line, unless we've just started
                if (index != 0)
                    sb.AppendLine();

                // get the next substring, else the rest of the string if remainder is shorter than `maxStringLength`
                var splitAt = index + maxStringLength <= text.Length
                    ? text.Substring(index, maxStringLength).LastIndexOfAny(splitOnCharacters)
                    : text.Length - index;

                // if can't find split location, take `maxStringLength` characters
                splitAt = (splitAt == -1) ? maxStringLength : splitAt;

                // add result to collection & increment index
                sb.Append(text.Substring(index, splitAt).Trim());
                index += splitAt;
            }

            return sb.ToString();
        }

        private void OnTimedEventUpdateScreen(object sender, EventArgs eArgs)
        {
            if (numFrames>0 || pendingRequest)
            {
                return;
            }
            pendingRequest = true;
            HttpResponseMessage response = null;
            string request = String.Format("newwarning?reviewerid={0}", reviewerID);
            try
            {
                response = client.GetAsync(request).GetAwaiter().GetResult();
            }
            catch (HttpRequestException)
            {
                outlineLabelStatus.Text = "Cannot connect to server!";
                outlineLabelStatus.ForeColor = Color.OrangeRed;
                pendingRequest = false;
                ResetFrames();
                return;
            }
            if (response.IsSuccessStatusCode)
            {
                string responseString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                if (responseString.Equals("\"<Nothing\\/>\""))
                {
                    outlineLabelStatus.Text = "Everything safe for now..";
                    outlineLabelStatus.ForeColor = Color.LawnGreen;
                    pendingRequest = false;
                    return;
                }
                else
                {
                    outlineLabelStatus.Text = "Suspicious activity?";
                    outlineLabelStatus.ForeColor = Color.White;
                    UpdateButtons(true);

                    // Look for ID
                    Regex re = new Regex(@"(?<=id\=\\\"")(.*?)(?=\\\"")");
                    Match match = re.Match(responseString);
                    if (match.Success)
                    {
                        imageSetID = match.Value;
                    }
                    // Look for num frames
                    re = new Regex(@"(?<=num_frames\=\\\"")(.*?)(?=\\\"")");
                    match = re.Match(responseString);
                    if (match.Success)
                    {
                        numFrames = Convert.ToInt32(match.Value);
                    }
                    // Look for hints
                    re = new Regex(@"(?<=hints\=\\\"")(.*?)(?=\\\"")");
                    match = re.Match(responseString);
                    if (match.Success)
                    {
                        string hints = SplitToLines(match.Value, new char[]{' '}, 60);
                        outlineLabelHints.Text = "Hints: " + hints;
                    }
                    // Look for purpose
                    re = new Regex(@"(?<=purpose\=\\\"")(.*?)(?=\\\"")");
                    match = re.Match(responseString);
                    if (match.Success)
                    {
                        outlineLabelPurpose.Text = "Danger-level: " + match.Value;
                    }
                    currentFrameNumber = 1;
                    labelCurrentFrameNumber.Text = currentFrameNumber.ToString();
                    UpdateImage();
                }
            }
            pendingRequest = false;
        }

        private void UpdateImage()
        {
            if(imageFrame[currentFrameNumber-1] !=null)
            {
                pictureBoxImage.Image = imageFrame[currentFrameNumber - 1];
                return;     // image already uploaded
            }

            HttpResponseMessage response = null;
            try
            {
                string request = String.Format("warningimage?id={0}&frame_number={1}", imageSetID, currentFrameNumber);
                response = client.GetAsync(request).GetAwaiter().GetResult();
            }
            catch (HttpRequestException)
            {
                outlineLabelStatus.Text = "Cannot connect to server!";
                outlineLabelStatus.ForeColor = Color.OrangeRed;
                pendingRequest = false;
                ResetFrames();
                return;
            }
            string responseString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            responseString = responseString.Substring(1, responseString.Length-2).Replace(@"\", "");      // strip of double qoutes
            byte[] imageData = Convert.FromBase64String(responseString);
            using (var ms = new MemoryStream(imageData))
            {
                imageFrame[currentFrameNumber-1] = new Bitmap(ms);
            }
            pictureBoxImage.Image = imageFrame[currentFrameNumber-1];
        }

        static async Task HttpGetForLargeFile(int sequenceNumber)
        {
            using (HttpClient client = new HttpClient())
            {
                const string url = "https://github.com/tugberkugurlu/ASPNETWebAPISamples/archive/master.zip";
                using (HttpResponseMessage response = await client.GetAsync(HOST_URL+"", HttpCompletionOption.ResponseHeadersRead))
                using (Stream streamToReadFrom = await response.Content.ReadAsStreamAsync())
                {
                    string fileToWriteTo = Path.GetTempFileName();
                    using (Stream streamToWriteTo = File.Open(fileToWriteTo, FileMode.Create))
                    {
                        await streamToReadFrom.CopyToAsync(streamToWriteTo);
                    }
                }
            }
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            currentFrameNumber++;
            if(currentFrameNumber>numFrames)
            {
                currentFrameNumber = 1;
            }
            labelCurrentFrameNumber.Text = currentFrameNumber.ToString();
            UpdateImage();
        }

        private void buttonPrevious_Click(object sender, EventArgs e)
        {
            currentFrameNumber--;
            if (currentFrameNumber < 1)
            {
                currentFrameNumber = numFrames;
            }
            labelCurrentFrameNumber.Text = currentFrameNumber.ToString();
            UpdateImage();
        }

        private void buttonYes_Click(object sender, EventArgs e)
        {
            Vote(true);
            FormOverview formOverview = new FormOverview();
            formOverview.outlineLabelVotesYes.Text = "people voted YES: " + numVotesYes;
            formOverview.outlineLabelVotesNo.Text = "people voted NO: " + numVotesNo;
            formOverview.Show();
            ResetFrames();
        }

        private void buttonNo_Click(object sender, EventArgs e)
        {
            Vote(false);
            FormOverview formOverview = new FormOverview();
            formOverview.outlineLabelVotesYes.Text = "people voted YES: " + numVotesYes;
            formOverview.outlineLabelVotesNo.Text = "people voted NO: " + numVotesNo;
            formOverview.Show();
            ResetFrames();
        }

        private void Vote(bool yes)
        {
            HttpResponseMessage response = null;
            try
            {
                string request = String.Format("review?id={0}&reviewerid={1}&value={2}", imageSetID, reviewerID, yes ? 1 : 0);
                response = client.PostAsync(request, null).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    string responseString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    // Look for yes votes
                    Regex re = new Regex(@"(?<=yes_votes\=\\\"")(.*?)(?=\\\"")");
                    Match match = re.Match(responseString);
                    if (match.Success)
                    {
                        numVotesYes = match.Value;
                    }
                    // Look for no votes
                    re = new Regex(@"(?<=no_votes\=\\\"")(.*?)(?=\\\"")");
                    match = re.Match(responseString);
                    if (match.Success)
                    {
                        numVotesNo = match.Value;
                    }
                }
            }
            catch (HttpRequestException)
            {
                outlineLabelStatus.Text = "Cannot connect to server!";
                outlineLabelStatus.ForeColor = Color.OrangeRed;
                pendingRequest = false;
                ResetFrames();
                return;
            }
        }
    }
}
