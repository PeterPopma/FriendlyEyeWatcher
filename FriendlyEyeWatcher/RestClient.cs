using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FriendlyEyeSender
{
    class RestClient
    {
        static HttpClient client = new HttpClient();

        public RestClient()
        {
            client.BaseAddress = new Uri("http://localhost:8000/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/xml"));
        }

        public void PostImage(byte[] image, string filename, int imageset_number, int sequence_number, string name, string telephone, string address)
        {
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("clientname", name);
            nvc.Add("telephone", telephone);
            nvc.Add("address", address);
            nvc.Add("imageset_number", imageset_number.ToString());
            nvc.Add("sequence_number", sequence_number.ToString());

            string url = "http://localhost:8000/newimage";
            Debug.Write(string.Format("Calling {0}", url));
            HttpUploadImage(url, filename, image, nvc);
        }

        public static Stream ToStream(Image image)
        {
            var stream = new System.IO.MemoryStream();
            image.Save(stream, ImageFormat.Bmp);
            stream.Position = 0;
            return stream;
        }

        public static void HttpUploadImage(string url, string imageName, byte[] image, NameValueCollection nvc) 
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";
            wr.KeepAlive = true;
            wr.Credentials = System.Net.CredentialCache.DefaultCredentials;
            Stream rs;

            try
            {
                rs = wr.GetRequestStream();
            } catch(Exception)
            {
                Console.WriteLine("Could not connect to server!");
                return;
            }

            rs.Write(boundarybytes, 0, boundarybytes.Length);
            string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n";
            string formitem = string.Format(formdataTemplate, "imageinfo");
            byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
            rs.Write(formitembytes, 0, formitembytes.Length);
            foreach (string key in nvc.AllKeys)
            {
                formitem = key + "=\"" + nvc[key] + "\"\r\n";
                formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                rs.Write(formitembytes, 0, formitembytes.Length);
            }
            rs.Write(boundarybytes, 0, boundarybytes.Length);

            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
            string header = string.Format(headerTemplate, imageName, imageName, "image/png");
            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);

            //var imageStream = ToStream(image);
            Stream imageStream = new MemoryStream(image);
            byte[] buffer = new byte[4096];
            int bytesRead = 0;
            while ((bytesRead = imageStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                rs.Write(buffer, 0, bytesRead);
            }
            imageStream.Close();

            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);
            rs.Close();

            WebResponse wresp = null;
            try
            {
                wresp = wr.GetResponse();
                Stream stream2 = wresp.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);
                Debug.Write(string.Format("File uploaded, server response is: {0}", reader2.ReadToEnd()));
            }
            catch (Exception ex)
            {
                Debug.Write("Error uploading file:" + ex.Message);
                if (wresp != null)
                {
                    wresp.Close();
                    wresp = null;
                }
            }
            finally
            {
                wr = null;
            }
        }

        public static void HttpUploadFile(string url, string file, string paramName, string contentType, NameValueCollection nvc)
        {
            Debug.Write(string.Format("Uploading {0} to {1}", file, url));
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";
            wr.KeepAlive = true;
            wr.Credentials = System.Net.CredentialCache.DefaultCredentials;

            Stream rs = wr.GetRequestStream();

            string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            foreach (string key in nvc.Keys)
            {
                rs.Write(boundarybytes, 0, boundarybytes.Length);
                string formitem = string.Format(formdataTemplate, key, nvc[key]);
                byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                rs.Write(formitembytes, 0, formitembytes.Length);
            }
            rs.Write(boundarybytes, 0, boundarybytes.Length);

            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
            string header = string.Format(headerTemplate, paramName, file, contentType);
            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);

            FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[4096];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                rs.Write(buffer, 0, bytesRead);
            }
            fileStream.Close();

            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);
            rs.Close();

            WebResponse wresp = null;
            try
            {
                wresp = wr.GetResponse();
                Stream stream2 = wresp.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);
                Debug.Write(string.Format("File uploaded, server response is: {0}", reader2.ReadToEnd()));
            }
            catch (Exception ex)
            {
                Debug.Write("Error uploading file:" + ex.Message);
                if (wresp != null)
                {
                    wresp.Close();
                    wresp = null;
                }
            }
            finally
            {
                wr = null;
            }
        }
    }
}
