using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Net;
using System.IO;
using Microsoft.Phone.Controls;

namespace ExploreBaseControls
{
    public class CrudeOilClient
    {
        private static string[] codes = { "CL1:COM", "CO1:COM" };
        private const string url = "http://www.bloomberg.com/energy";
        private string html { get; set; }
        private MainPage page;

        public CrudeOilClient(MainPage page)
        {
            this.page = page;
            UpdateInfo();
        }

        public void UpdateInfo()
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.BeginGetResponse(GetData, request);
        }

        public void GetData(IAsyncResult result)
        {
            HttpWebRequest request = result.AsyncState as HttpWebRequest;
            if (request != null)
            {
                try
                {
                    WebResponse response = request.EndGetResponse(result);
                    MemoryStream stream = new MemoryStream();
                    response.GetResponseStream().CopyTo(stream);
                    StreamReader reader = new StreamReader(stream);
                    html = Encoding.UTF8.GetString(stream.ToArray(), 0, 68081);
                    page.UpdateOil();
                }
                catch (WebException e)
                {
                    return;
                }
            }
        }

        public float Parse(String code)
        {
            int a = html.Length;
            int Ind = html.IndexOf(code);
            string substr = html.Substring(Ind);
            int valueStartInd = substr.IndexOf("value") + 7;
            int valueEndInd = substr.IndexOf('<', valueStartInd);
            string substr2 = substr.Substring(valueStartInd, valueEndInd - valueStartInd);
            float f;
            float.TryParse(substr2, NumberStyles.Any, new CultureInfo("en-US"), out f);
            return f;
        }
    }
}
