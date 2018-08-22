using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class LogFile
    {
        /// <summary>
        /// 写日志文件
        /// </summary>
        /// <param name="sb">日志信息</param>
        /// <param name="Filepath">日志保存路径</param>
        /// <param name="fileName">日志保存的文件名</param>
        public static void WriteLogNewInfo(String sb, string Filepath = "C:\\myInfoLOG\\", string fileName = "")
        {
           // Filepath = "E:\\LOG\\";
            string path = AppDomain.CurrentDomain.BaseDirectory + "Log";
            if (string.IsNullOrEmpty(Filepath))
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            else
            {
                if (!Directory.Exists(Filepath))
                {
                    Directory.CreateDirectory(Filepath);
                }
            }

            string filePath = string.Empty;
            if (!string.IsNullOrEmpty(Filepath))
            {
                filePath = Filepath + "\\" + fileName + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            }
            else
            {
                filePath = path + "\\" + fileName + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            }

            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine("*********************日志记录" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss ffff") + "************************");
                sw.WriteLine(sb.ToString());
            }
        }

        /// <summary>
        /// 追加写写日志文件
        /// </summary>
        /// <param name="sb">日志信息</param>
        /// <param name="Filepath">日志保存路径</param>
        /// <param name="fileName">日志保存的文件名</param>
        public static void WriteLogAddInfo(String sb, string Filepath = "C:\\myInfoLOG\\", string fileName = "log")
        {
            // Filepath = "E:\\LOG\\";
            string path = AppDomain.CurrentDomain.BaseDirectory + "Log";
            if (string.IsNullOrEmpty(Filepath))
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            else
            {
                if (!Directory.Exists(Filepath))
                {
                    Directory.CreateDirectory(Filepath);
                }
            }

            string filePath = string.Empty;
            if (!string.IsNullOrEmpty(Filepath))
            {
                filePath = Filepath + "\\" + fileName + ".txt";
            }
            else
            {
                filePath = path + "\\" + fileName + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            }
            StreamWriter sw = File.AppendText(filePath);
            sw.WriteLine("*********************日志记录："+sb + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss ffff") + "************************");
            sw.Flush(); 
            sw.Close();
            //using (StreamWriter sw = new StreamWriter(filePath, true))
            //{
            //    sw.WriteLine("*********************日志记录" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss ffff") + "************************");
            //    sw.WriteLine(sb.ToString());
            //}
        }
    }
}
