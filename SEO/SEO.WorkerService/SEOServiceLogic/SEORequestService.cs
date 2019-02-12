using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using SEO.WorkerService.Constants;
using SEO.WorkerService.Exceptions;
using SEO.WorkerService.Interfaces;

namespace SEO.WorkerService.SEOServiceLogic
{
    public class SEORequestService : ISEORequestService
    {
        public HttpWebRequest CreateRequest(string searchTerm)
        {
            string search = string.Format(ServiceConstants.UrlPrefix, HttpUtility.UrlEncode(searchTerm));

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(search);
            request.Method = "GET";
            request.ContentType = "application/json";

            //If required
            request.Credentials = CredentialCache.DefaultCredentials;

            return request;
        }

        public string GetResponse(HttpWebRequest request, string lookUp)
        {
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                if (response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    using (StreamReader reader = new StreamReader(
                        response.GetResponseStream() ??
                        throw new SEOValidationException(ServiceConstants.InvalidOperationMsg), Encoding.ASCII))
                    {
                        string html = reader.ReadToEnd();

                        var webresults = html.Split(new string[] { "ires" }, StringSplitOptions.None);

                        html = webresults[1] ?? html;

                        try
                        {
                            var uri = new Uri($"https://{lookUp}/"); //ServiceConstants.LookUpUrl;

                            return GetPositions(html, uri);
                        }
                        catch
                        {
                            throw new SEOValidationException(
                                $"Look Up value is invalid pass valid Uri : www.xxxxx.xxx or www.xxxxx.xxx.xx");
                        }
                    }
                }

                return "Bad Request";
            }
        }


        public string GetPositions(string html, Uri uri)
        {
            var positions = FindURLPosition(html, uri);

            var matchers = string.Join(",", positions);

            return matchers.Length > 0 ? matchers : "0";
        }

        private IList<int> FindURLPosition(string input, Uri uri)
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
                if (count > 100) break;

                //Get href attribute.
                Match m2 = Regex.Match(value, @"href=\""(.*?)\""",
                    RegexOptions.Singleline);
                if (m2.Success)
                {
                    i = m2.Groups[1].Value;
                }

                if (i.Contains(uri.Host))
                    listPositions.Add(count);

            }

            return listPositions;
        }
    }
}
