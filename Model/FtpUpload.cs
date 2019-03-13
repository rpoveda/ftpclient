using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtpClient.Model
{
    public class FtpUpload
    {
        public FtpConfig FtpConfig { get; private set; }
        public byte[] Content { get; private set; }
        public String FileName { get; private set; }

        public FtpUpload(FtpConfig ftpConfig, byte[] content, string fileName)
        {
            this.FtpConfig = ftpConfig;
            this.Content = content;
            this.FileName = fileName;
        }
    }
}
