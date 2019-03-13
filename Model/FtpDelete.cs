using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtpClient.Model
{
    public class FtpDelete
    {
        public FtpConfig FtpConfig { get; private set; }
        public String FileName { get; private set; }

        public FtpDelete(FtpConfig ftpConfig, string fileName)
        {
            this.FtpConfig = ftpConfig;
            this.FileName = fileName;
        }
    }
}
