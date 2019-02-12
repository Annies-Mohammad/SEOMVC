using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using SEO.WorkerService.Constants;
using SEO.WorkerService.Interfaces;

namespace SEO.WorkerService.SEOServiceLogic
{
    public class SEORequestService : ISEORequestService
    {
        public HttpWebRequest CreateRequest(string searchTerm)
        {
            string search = string.Format(ServiceConstants.UrlPrefix, HttpUtility.UrlEncode(searchTerm));

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(search);

            //If required
            request.Credentials = CredentialCache.DefaultCredentials;

            return request;
        }

        public string GetResponse(HttpWebRequest request)
        {
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.ASCII))
                {
                    string html = reader.ReadToEnd();
                    var uri = new Uri(ServiceConstants.SearchUrl);
                    return FindPosition(html, uri);
                }
            }
        }



        public static string FindPosition(string html, Uri url)
        {
            // string lookup = "(<a class=l href=\")(\\w+[a-zA-Z0-9.-?=/]*)";
            string lookup = "(<a href=\")(\\w+[a-zA-Z0-9.-?=/]*)\" class=l";

            StringBuilder matchers = new StringBuilder();
            MatchCollection matches = Regex.Matches(html, lookup);

            for (int i = 0; i < matches.Count; i++)
            {
                string match = matches[i].Groups[2].Value;
                if (match.Contains(url.Host))
                {
                    var pos = i + 1;
                    matchers.Append(pos.ToString());
                }
            }

            return matchers.ToString() ?? "0";
        }

    }
}
