using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtpClient.Util
{
    public class File
    {
        public static string GetFileName(string uri)
        {
            var split = uri.Split('/');
            var count = split.Length;

            return split[count - 1];
        }
    }
}
