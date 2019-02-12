using System;
using System.Collections.Generic;
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
                using (StreamReader reader = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException(ServiceConstants.InvalidOperationMsg), Encoding.ASCII))
                {
                    string html = reader.ReadToEnd();
                    var uri = new Uri(ServiceConstants.SearchUrl);
                    return GetPositions(html, uri);
                }
            }
        }

        public string GetPositions(string html, Uri url)
        {
            var positions = FindURLPosition(html);

            var matchers = string.Join(",", positions);

            return matchers.Length > 0 ? matchers : "0";
        }

        private IList<int> FindURLPosition(string input)
        {
            List<int> listPositions = new List<int>();
            int count = 0;

            // 1. Get Anchor tags.
            MatchCollection m1 = Regex.Matches(input, @"(<a.*?>.*?</a>)", RegexOptions.Singleline);

            foreach (Match m in m1)
            {
                string value = m.Groups[1].Value;
                var i = "";
                count++;

                //Get href attribute.
                Match m2 = Regex.Match(value, @"href=\""(.*?)\""",
                    RegexOptions.Singleline);
                if (m2.Success)
                {
                    i = m2.Groups[1].Value;
                }

                if (i.Contains(ServiceConstants.SearchUrl))
                    listPositions.Add(count);

            }

            return listPositions;
        }
    }
}
