using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtpClient.Model
{
    public class FtpConfig
    {
        public Uri Uri { get; private set; }
        public String Login { get; private set; }
        public String Password { get; private set; }

        public FtpConfig(
            Uri uri,
            String login,
            String password)
        {
            this.Uri = uri;
            this.Login = login;
            this.Password = password;
        }
    }
}
