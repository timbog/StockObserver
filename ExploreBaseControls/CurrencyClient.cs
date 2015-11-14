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
    public class CurrencyClient:Client
    {
        private const string url = "http://www.bloomberg.com/markets/currencies";

        protected override String GetUrl()
        {
            return url;
        }

        protected override void UpdatePage()
        {
            page.UpdateCurrency();
        }

        public CurrencyClient(MainPage page): base(page)
        {
        }
    }
}
