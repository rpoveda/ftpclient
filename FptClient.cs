using FtpClient.Model;
using FtpClient.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace FtpClient
{
    public static class FptClient
    {
        public static List<string> GetFiles(FtpConfig ftpConfig)
        {
            var files = new List<string>();

            FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(ftpConfig.Uri);
            ftpWebRequest.Method = WebRequestMethods.Ftp.ListDirectory;
            ftpWebRequest.UseBinary = true;
            ftpWebRequest.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequested;
            ftpWebRequest.Credentials = new NetworkCredential(ftpConfig.Login, ftpConfig.Password);

            StreamReader streamReader = new StreamReader(ftpWebRequest.GetResponse().GetResponseStream());

            var line = streamReader.ReadLine();
            while(line != null)
            {
                if (line.Contains(".CSV"))
                {
                    if (line.Contains("href"))
                    {
                        var href = Find.Href(line);

                        if (!string.IsNullOrEmpty(href))
                            files.Add(FtpClient.Util.File.GetFileName(href));
                    }
                    else
                        files.Add(line);
                }

                line = streamReader.ReadLine();
            }

            return files;
        }

        public static byte[] DownloadFile(FtpDownload ftpDownload)
        {
            byte[] bytes = null;

            WebClient wc = new WebClient();
            wc.Credentials = new NetworkCredential(ftpDownload.FtpConfig.Login, ftpDownload.FtpConfig.Password);

            bytes = wc.DownloadData(string.Format(@"{0}/{1}", 
                ftpDownload.FtpConfig.Uri.ToString(), 
                ftpDownload.FileName));

            return bytes;
        }

        public static void UploadFile(FtpUpload ftpUpload)
        {
            FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create($"{ftpUpload.FtpConfig.Uri}/{ftpUpload.FileName}");
            ftpWebRequest.Method = WebRequestMethods.Ftp.UploadFile;
            ftpWebRequest.UseBinary = true;
            ftpWebRequest.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequested;
            ftpWebRequest.Credentials = new NetworkCredential(ftpUpload.FtpConfig.Login, ftpUpload.FtpConfig.Password);

            ftpWebRequest.ContentLength = ftpUpload.Content.Length;

            using(Stream stream = ftpWebRequest.GetRequestStream())
            {
                stream.Write(ftpUpload.Content, 0, ftpUpload.Content.Length);
                stream.Close();
            }
        }

        public static void MoveFile(FtpMove ftpMove)
        {
            var ftpConfigDownload = new FtpDownload(new FtpConfig(new Uri($"{ftpMove.FtpConfig.Uri}/{ftpMove.From}"), ftpMove.FtpConfig.Login, ftpMove.FtpConfig.Password), ftpMove.FileName);
            var downloadContet = DownloadFile(ftpConfigDownload);

            var ftpConfigMove = new FtpUpload(new FtpConfig(new Uri($"{ftpMove.FtpConfig.Uri}/{ftpMove.To}"), ftpMove.FtpConfig.Login, ftpMove.FtpConfig.Password), downloadContet, ftpMove.FileName);
            var ftpDelete = new FtpDelete(new FtpConfig(new Uri($"{ftpMove.FtpConfig.Uri}/{ftpMove.From}"), ftpMove.FtpConfig.Login, ftpMove.FtpConfig.Password), ftpMove.FileName);
            DeleteFile(ftpDelete);

            UploadFile(ftpConfigMove);
        }

        public static void DeleteFile(FtpDelete ftpDelete)
        {
            FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(string.Format($"{ftpDelete.FtpConfig.Uri}/{ftpDelete.FileName}"));
            ftpWebRequest.Method = WebRequestMethods.Ftp.DeleteFile;
            ftpWebRequest.Credentials = new NetworkCredential(ftpDelete.FtpConfig.Login, ftpDelete.FtpConfig.Password);
            ftpWebRequest.GetResponse();
        }
    }
}
