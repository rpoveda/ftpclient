using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtpClient.Model
{
    public class FtpMove
    {
        public FtpConfig FtpConfig { get; private set; }
        public String From { get; private set; }
        public String To { get; private set; }
        public String FileName { get; private set; }
    }
}
