using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtpClient.Util
{
    public static class Find
    {
        public static string Href(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var anchor = doc.DocumentNode.SelectSingleNode("//a");
            if (anchor != null)
            {
                string link = anchor.Attributes["href"].Value;
                if (link.Contains("@"))
                    return link.Split('@')[1];

                return link;
            }

            return string.Empty;
        }
    }
}
