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
    public class CrudeOilClient:Client
    {
        private const string url = "http://www.bloomberg.com/energy";

        protected override String GetUrl()
        {
            return url;
        }

        protected override int GetLength()
        {
            return 68081;
        }

        protected override void UpdatePage()
        {
            page.UpdateOil();
        }

        public CrudeOilClient(MainPage page) : base(page)
        {
        }
    }
}
