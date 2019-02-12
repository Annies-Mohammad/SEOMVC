using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Linq;
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
                    return GetPositions(html, uri);
                }
            }
        }



        public string GetPositions(string html, Uri url)
        {
            //  string lookup = "(<a class=l href=\")(\\w+[a-zA-Z0-9.-?=/]*)";
            string lookup = "(<a href=\")(\\w+[a-zA-Z0-9.-?=/]*)\" class=l";
            StringBuilder matchers = new StringBuilder();

           MatchCollection matches = Regex.Matches(html, lookup);


            FindURLPosition(html);


            for (int i = 0; i < matches.Count; i++)
            {
                string match = matches[i].Groups[2].Value;
                if (match.Contains(url.Host))
                {
                    var pos = i + 1;
                    matchers.Append(pos.ToString());
                }
            }

            return matchers.Length > 0 ? matchers.ToString() : "0";
        }



        private IList<int> FindURLPosition(string input)
        {
            List<int> listPositions = new List<int>();

            // 1.
            // Find all matches in html.
            MatchCollection m1 = Regex.Matches(input, @"(<a.*?>.*?</a>)",
                RegexOptions.Singleline);

            // 2.
            // Loop over each match.
            foreach (Match m in m1)
            {
                string value = m.Groups[1].Value;
                var i = "";

                // 3.
                // Get href attribute.
                Match m2 = Regex.Match(value, @"href=\""(.*?)\""",
                    RegexOptions.Singleline);
                if (m2.Success)
                {
                    i = m2.Groups[1].Value;
                }

                if (i.Contains(ServiceConstants.SearchUrl))
                    listPositions.Add(m.Index);

            }

            return listPositions;
        }


    }
}
