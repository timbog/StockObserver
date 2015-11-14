using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Globalization;

namespace ExploreBaseControls
{
    public abstract class Client
    {
        protected MainPage page;
        protected string html { get; set; }

        protected abstract String GetUrl();

        protected abstract void UpdatePage();

        public Client(MainPage page)
        {
            this.page = page;
            UpdateInfo();
        }

        public void UpdateInfo()
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(GetUrl());
            request.BeginGetResponse(GetData, request);
        }

        public void GetData(IAsyncResult result) {
            HttpWebRequest request = result.AsyncState as HttpWebRequest;
            if (request != null)
            {
                try
                {
                    WebResponse response = request.EndGetResponse(result);
                    MemoryStream stream = new MemoryStream();
                    response.GetResponseStream().CopyTo(stream);
                    StreamReader reader = new StreamReader(stream);
                    int length = stream.ToArray().Length;
                    html = Encoding.UTF8.GetString(stream.ToArray(), 0, length);
                    UpdatePage();
                }
                catch (WebException e)
                {
                    page.ShowError();
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
