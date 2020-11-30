
namespace StockAnalysis.Common
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Extensions;
    using Exceptions;

    public static class FileHandeling
    {
        private static int counter = 0;

        public static void DownloadKeyRatioFileAsync(string stockSymbol)
        {
            WebBrowser browser = new WebBrowser();
            browser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(Browser_DocumentCompleted);
            browser.ScriptErrorsSuppressed = true;
            browser.Navigate(new Uri(CreateDownloadCSVLink(stockSymbol)));
        }

        private static void Browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser browser = (WebBrowser)sender;
            if (browser.Url == e.Url)
            {
                var doc = browser.Document;
                var col = doc.GetElementById("financeWrap");
                HtmlElement downloadButton = doc.CreateElement("a");
                downloadButton.SetAttribute("className", "large_button");
                downloadButton.SetAttribute("href", "javascript:exportKeyStat2CSV();");
                col.AppendChild(downloadButton);
                downloadButton.InvokeMember("Click");
            }
            else 
            {
                throw new BrowserDidntNavigateException(browser.Url.AbsoluteUri, e.Url.AbsoluteUri);
            }
        }


        private static string CreateDownloadCSVLink(string stockSymbol)
        {
            return $"https://financials.morningstar.com/ratios/r.html?t={stockSymbol}&region=usa&culture=en-US";
        }
    }
}
