using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;

//HttpContext需要引用System.Web

namespace Common
{
    /// <summary>
    /// 公共方法下载文件
    /// 调用方式:DownFile.DownLoad("Images/ex.jpg", 2);
    /// </summary>
    public static class DownLoadFile
    {
        /// <summary>
        /// opt为0时，传绝对路径，例如：F:\学习\图片文件夹压缩\WebApplication1\aaa.rar
        /// opt为1时，传Server路径，例如：DownLoad/aaa.rar
        /// </summary>
        /// <param name="path"></param>
        /// <param name="opt">值为0-2</param>
        /// <param name="fileName"></param>
        /// <param name="contentType"></param>
        public static void DownLoad(string path, int opt, string fileName = "", string contentType = "application/x-zip-compressed")
        {
            switch (opt)
            {
                case 0:
                    TransmitFileWay(path, fileName, contentType);
                    break;
                case 1:
                    WriteFileWay(path, fileName, contentType);
                    break;
                case 2:
                    BinaryWriteWay(path, fileName, contentType);
                    break;
                case 3:
                    WriteFileBlockWay(path, fileName, contentType);   //下载的文件有问题
                    break;
            }
        }
        //**[TransmitFile方式]
        private static void TransmitFileWay(string path, string fileName = "", string contentType = "application/x-zip-compressed")
        {
            try
            {
                fileName = string.IsNullOrEmpty(fileName) ? path.Substring(path.LastIndexOf("\\") + 1) : fileName;
                //**指定编码,防止中文文件名乱码
                HttpContext.Current.Response.HeaderEncoding = System.Text.Encoding.GetEncoding("gb2312");
                HttpContext.Current.Response.ContentType = "application/x-zip-compressed";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
                HttpContext.Current.Response.TransmitFile(path);
            }
            catch { throw; }
        }

        //**[WriteFile方式]
        private static void WriteFileWay(string path, string fileName, string contentType = "application/octet-stream")
        {
            try
            {
                fileName = string.IsNullOrEmpty(fileName) ? path.Substring(path.LastIndexOf("\\") + 1).Substring(path.LastIndexOf("/") + 1) : fileName;
                path = HttpContext.Current.Server.MapPath(path);
                FileInfo fi = new FileInfo(path);
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
                HttpContext.Current.Response.AddHeader("Content-Length", fi.Length.ToString());
                HttpContext.Current.Response.AddHeader("Content-Transfer-Encoding", "binary");
                HttpContext.Current.Response.ContentType = contentType;
                HttpContext.Current.Response.HeaderEncoding = System.Text.Encoding.GetEncoding("gb2312");
                HttpContext.Current.Response.WriteFile(fi.FullName);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
            catch { throw; }
        }

        //**[WriteFile以块方式]
        private static void WriteFileBlockWay(string path, string fileName, string contentType = "application/octet-stream")
        {
            try
            {
                fileName = string.IsNullOrEmpty(fileName) ? path.Substring(path.LastIndexOf("\\") + 1).Substring(path.LastIndexOf("/") + 1) : fileName;
                path = HttpContext.Current.Server.MapPath(path);
                FileInfo fi = new FileInfo(path);
                if (fi.Exists == true)
                {
                    const long cs = 102400;//**每次读取文件，只读取100K
                    byte[] bf = new byte[cs];
                    HttpContext.Current.Response.Clear();
                    System.IO.FileStream istream = System.IO.File.OpenRead(path);
                    long dataLength = istream.Length;
                    HttpContext.Current.Response.ContentType = contentType;
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName));
                    //while (dataLength > 0 && HttpContext.Current.Response.IsClientConnected)
                    while (dataLength > 0)
                    {
                        int lthRead = istream.Read(bf, 0, Convert.ToInt32(cs));
                        HttpContext.Current.Response.OutputStream.Write(bf, 0, lthRead);
                        HttpContext.Current.Response.Flush();
                        dataLength = dataLength - lthRead;
                    }
                    HttpContext.Current.Response.Close();
                }
            }
            catch { throw; }
        }

        //**[流方式]
        private static void BinaryWriteWay(string path, string fileName, string contentType = "application/octet-stream")
        {
            try
            {
                fileName = string.IsNullOrEmpty(fileName) ? path.Substring(path.LastIndexOf("\\") + 1).Substring(path.LastIndexOf("/") + 1) : fileName;
                path = HttpContext.Current.Server.MapPath(path);
                FileStream fs = new FileStream(path, FileMode.Open);
                byte[] bt = new byte[(int)fs.Length];
                fs.Read(bt, 0, bt.Length);
                fs.Close();
                HttpContext.Current.Response.ContentType = contentType;
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
                HttpContext.Current.Response.BinaryWrite(bt);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
            catch { throw; }
        }
    }
}
