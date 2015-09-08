using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Net;
using System.IO;
using System.Text;
using Microsoft.Phone.Controls;

namespace ExploreBaseControls
{
    public class CurrencyClient
    {
        private static string[] codes = { "USD-JPY", "EUR-USD", "RUB", "USD-CHF", "GBP-USD" };
        private const string url = "http://www.bloomberg.com/markets/currencies";
        private string html {get; set;}
        private MainPage page;

        public CurrencyClient(MainPage page)
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
                    html = System.Text.Encoding.UTF8.GetString(stream.ToArray(), 0, 123763);
                    page.Update();
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
            int rubleInd = html.IndexOf(code);
            string substr = html.Substring(rubleInd);
            int valueStartInd = substr.IndexOf("value") + 7;
            int valueEndInd = substr.IndexOf('<', valueStartInd);
            string substr2 = substr.Substring(valueStartInd, valueEndInd - valueStartInd);
            float f;
            float.TryParse(substr2, NumberStyles.Any, new CultureInfo("en-US"), out f);
            return f;
        }
    }
}
