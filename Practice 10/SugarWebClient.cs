using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Practice_10
{
    internal class Sugar11Value
    {
        GraphicDrawer drawer;
        public WebClient Client { get; set; }
        public string Value { get; set; }

        string LastValue { get; set; }

        string url = "https://ru.investing.com/commodities/us-sugar-no11";

        bool started = false;

        public Sugar11Value()
        {
            Client = new WebClient();
            Client.DownloadStringCompleted += Client_DownloadStringCompleted;
            Client.DownloadStringAsync(new Uri(url));
        }

        public void Update()
        {
            Client.DownloadStringAsync(new Uri(url));
            drawer.Update(Value);
        }

        private void Client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            string text = e.Result;
            string[] parsedHtml = text.Split(new string[] { "<span class=\"text-2xl\" data-test=\"instrument-price-last\">" }, StringSplitOptions.None);
            parsedHtml = parsedHtml[1].Split(new string[] { "</span" }, StringSplitOptions.None);
            Value = parsedHtml[0];
            if (!started)
            {
                drawer = new GraphicDrawer(Value);
                started = true;
            }
            LastValue = drawer.GetLastValue();
        }

        public string GetValue()
        {
            Client.DownloadStringAsync(new Uri(url));
            string text = null;
            double lastV = double.Parse(LastValue);
            double v = double.Parse(Value);
            if (v < lastV)
            {
                text = $"{Value}  \u2193  {Math.Round(lastV - v, 2)}";
            }
            else if (v >= lastV)
            {
                text = $"{Value}  \u2191  {Math.Round(v - lastV, 2)}";
            }
            return text;
        }
    }
}
