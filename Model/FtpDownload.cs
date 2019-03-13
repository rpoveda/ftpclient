using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtpClient.Model
{
    public class FtpDownload
    {
        public FtpConfig FtpConfig { get; private set; }
        public string FileName { get; private set; }

        public FtpDownload(FtpConfig ftpConfig, string fileName)
        {
            this.FtpConfig = FtpConfig;
            this.FileName = FileName;
        }
    }
}
