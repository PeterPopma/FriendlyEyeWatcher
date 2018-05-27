using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FriendlyEyeWatcher
{
    class POSTNLClient
    {
        static HttpClient client = new HttpClient();
        const string HOST_URL = "http://localhost:8000"; //"http://192.168.1.103:8000";
        const string POSTNL_KEY = "GAypxOjG1jG3lJENewTbxWC7aZnBMmJV";

        public POSTNLClient()
        {
            client.BaseAddress = new Uri(HOST_URL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public string getDeliveries(string postalCode, string housenumber)
        {
            string result = "";
            HttpResponseMessage response = null;

            client.DefaultRequestHeaders.Accept.Clear();
       //     client.DefaultRequestHeaders.Accept.Add(
       //         new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("apikey", POSTNL_KEY);

            try
            {
                string request = String.Format("https://api-sandbox.postnl.nl/shipment/v2_1/calculate/timeframes?AllowSundaySorting=false&StartDate=30-06-2017&EndDate=02-07-2017&PostalCode={0}&HouseNumber={1}&CountryCode=NL&Options=MyTime", postalCode, housenumber);
             //   client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
                response = client.GetAsync(request).GetAwaiter().GetResult();
            }
            catch (HttpRequestException)
            {
                MessageBox.Show("Cannot connect to KPN host!");
                return "";
            }

            string text = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            // Look for access token
        //    re = new Regex(@"(?<=classification\"": \"")(.*?)(?=\"")");
            MatchCollection fromMatches = Regex.Matches(text, @"(?<=From\"":\"")(.*?)(?=\"")");
            MatchCollection toMatches = Regex.Matches(text, @"(?<=To\"":\"")(.*?)(?=\"")");
            for (int k=0; k<fromMatches.Count; k++)
            {

                result += fromMatches[k].Value + "-" + toMatches[k].Value + ", ";
            }
            if(result.Length>2)
            {
                result = result.Substring(0, result.Length - 2);       // strip last comma
            }

            return result;
        }
    }
}
