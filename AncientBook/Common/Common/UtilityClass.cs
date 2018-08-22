using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace Common
{
    public static class UtilityClass
    {
        #region 将手机号隐藏中间四位为*
        /// <summary>
        /// 将手机号隐藏中间四位为*
        /// </summary>
        /// <param name="aMobilePhone">手机号码</param>
        /// <param name="aMask">代替字符串</param>
        /// <returns></returns>
        public static string MaskMobilePhone(this string aMoilePhone, string aMask = "****")
        {
            try
            {
                string str1 = aMoilePhone.Substring(0, 3);
                string str2 = aMoilePhone.Substring(7, 4);
                return str1 + aMask + str2;
            }
            catch (Exception)
            {
                return aMoilePhone;
            }
        }
        #endregion

        #region 文件名添加字符串
        /// <summary>
        /// 文件名添加字符串
        /// </summary>
        /// <param name="filenName">文件名(含扩展名)</param>
        /// <param name="addStr">添加的字符串</param>
        /// <returns></returns>
        public static string FilaNameAdd(this string filenName, string addStr)
        {
            try
            {
                string newFileName = string.Empty;
                var ext = filenName.Substring(filenName.LastIndexOf("."));
                newFileName = filenName.Replace(ext, "");
                newFileName = newFileName + addStr;
                newFileName = newFileName + ext;
                return newFileName;
            }
            catch (Exception)
            {
                return filenName;
            }
        }
        #endregion

        #region 判断对象是否为空或NULL,如果是空或NULL返回true，否则返回false
        /// <summary>
        /// 判断对象是否为空或NULL,如果是空或NULL返回true，否则返回false
        /// </summary>
        /// <param name="comeStr">要检测的字符串</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(object comeStr)
        {
            if (comeStr == null)
                return true;
            return (comeStr.ToString().Trim().Length == 0);
        }
        #endregion

        #region 验证手机号是否正确 13,15,18
        /// <summary>
        /// 验证手机号是否正确 13,15,18
        /// </summary>
        /// <returns>返回布尔值</returns>
        public static bool IsMobile(string source)
        {
            return Regex.IsMatch(source, @"^1[358]\d{9}$", RegexOptions.IgnoreCase);
        }
        #endregion

        #region 验证邮箱
        /// <summary>
        /// 验证邮箱
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsEmail(string source)
        {
            return Regex.IsMatch(source, @"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", RegexOptions.IgnoreCase);
        }
        #endregion

        #region 验证网址
        /// <summary>
        /// 验证网址
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsUrl(string source)
        {
            return Regex.IsMatch(source, @"^(((file|gopher|news|nntp|telnet|http|ftp|https|ftps|sftp)://)|(www\.))+(([a-zA-Z0-9\._-]+\.[a-zA-Z]{2,6})|([0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}))(/[a-zA-Z0-9\&amp;%_\./-~-]*)?$", RegexOptions.IgnoreCase);
        }
        #endregion

        #region MD5加密,返回32位的字符串
        /// <summary>
        /// MD5加密,返回32位的字符串
        /// </summary>
        /// <param name="comeStr">要加密的字符串</param>
        /// <returns></returns>
        public static string MD5(string comeStr)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(comeStr, "MD5");
        }
        #endregion

        #region =====DES加密/解密类=======

        #region ========加密========
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static string EncryptDES(string Text)
        {
            return Encrypt(Text, "litianping");
        }
        /// <summary> 
        /// 加密数据 
        /// </summary> 
        /// <param name="Text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string Encrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(Text);
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }

        #endregion

        #region ========解密========


        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static string DecryptDES(string Text)
        {
            return Decrypt(Text, "litianping");
        }
        /// <summary> 
        /// 解密数据 
        /// </summary> 
        /// <param name="Text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string Decrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            int len;
            len = Text.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(Text.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }

        #endregion

        #endregion

        #region 把字符串的第一个字符变为大写
        /// <summary>
        /// 把字符串的第一个字符变为大写
        /// </summary>
        /// <param name="value">要转换的字符串</param>
        /// <returns></returns>
        public static string FirstCharToUpper(string value)
        {
            if (value.Length > 1)
                return value.Substring(0, 1).ToUpper() + value.Substring(1);
            return value.ToUpper();
        }
        #endregion

        #region 判断一个字符串是否是数字
        /// <summary>
        /// 判断一个字符串是否是数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsInt(string str)
        {
            int n;
            if (int.TryParse(str, out n))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 判断一个字符串是否是时间
        /// <summary>
        /// 判断一个字符串是否是时间
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsTime(string str)
        {
            DateTime n;
            if (DateTime.TryParse(str, out n))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 判断一个字符串是否是decimal类型
        /// <summary>
        /// 判断一个字符串是否是decimal类型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool Isdecimal(string value)
        {
            decimal db;
            if (decimal.TryParse(value, out db))
                return true;
            else
                return false;
        }
        #endregion

        #region 生成随机数方法 小于9位
        /// <summary>
        /// 生成随机数方法 小于9位
        /// </summary>
        /// <returns></returns>
        public static string CreateRandom(int length)
        {
            string rndNumber = "1";
            int tempNumber = 0;
            rndNumber = rndNumber.PadRight(length + 1, '0');
            tempNumber = int.Parse(rndNumber);
            Random rnd = new Random();
            rndNumber = rnd.Next(1, tempNumber).ToString();
            rndNumber = rndNumber.PadLeft(length, '0');
            return rndNumber;
        }
        #endregion

        #region 获取网站配置ConfigurationManager_AppSettings键值
        /// <summary>
        /// 获取网站配置ConfigurationManager_AppSettings键值 
        /// </summary>
        /// <param name="configKeyName">AppSettings_keyName</param>
        /// <returns>AppSettings_keyValue</returns>
        public static string GetConfigAppSettings(string configKeyName)
        {
            string configKeyValue = string.Empty;
            if (!string.IsNullOrEmpty(configKeyName))
            {
                configKeyValue = ConfigurationManager.AppSettings[configKeyName].ToString();
            }
            return configKeyValue;
        }
        #endregion

        #region 检查某个文件是否存在于磁盘上，存在--true，不存在--false
        /// <summary>
        /// 检查某个文件是否存在于磁盘上，存在--true，不存在--false
        /// </summary>
        /// <param name="fileName">要检查的文件相对路径，如：/XML/MyFile.xml</param>
        /// <returns>返回bool值</returns>
        public static bool IsExistsFile(string fileName)
        {
            return File.Exists(UtilityClass.ConvertToPhysicalPath(fileName));
        }
        #endregion

        #region 在服务器上创建文件夹
        /// <summary>
        /// 在服务器上创建文件夹
        /// </summary>
        /// <param name="fileName">创建路径，形如：xml/myxml.config,如果只给出文件夹路径则形式：xml/myexcel/,注意后面一定要有“/”</param>
        /// <returns></returns>/
        public static void CreateDirectory(string fileName)
        {
            fileName = UtilityClass.ConvertToPhysicalPath(fileName);
            int position = fileName.LastIndexOf('\\');//准备在web服务器上创建文件夹
            string directory = fileName.Substring(0, position);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);//web服务器不存在该文件夹就创建
            }
        }
        #endregion

        #region 转换相对路径为物理路径
        /// <summary>
        /// 转换相对路径为物理路径
        /// </summary>
        /// <param name="fileName">文件或目录的根路径，如：/xml/myxml.config或/xml/</param>
        /// <returns></returns>
        public static string ConvertToPhysicalPath(string fileName)
        {
            string virtualRootPath = string.Empty;
            if (fileName.StartsWith("/"))
            {
                virtualRootPath = HttpContext.Current.Request.ApplicationPath;
                if (virtualRootPath.Equals("/"))
                    virtualRootPath = "";
            }
            //由于很多地方没有加根路径，而目的是使用相对于根路径的文件路径，所以这里加上根路径前缀
            if (!fileName.StartsWith("/"))
                fileName = "/" + fileName;
            return HttpContext.Current.Server.MapPath(virtualRootPath + fileName);
        }
        #endregion

        #region 两个值的百分比例
        /// <summary>
        /// 两个值的百分比例
        /// </summary>
        /// <param name="numerator">分子</param>
        /// <param name="denominator">分母</param>
        /// <param name="denominator">小数点后的位数</param>
        /// <returns></returns>
        public static string ChangeToPercent(string numerator, string denominator, int number)
        {
            decimal c = 0;
            string percent = string.Empty;
            if (!IsNullOrEmpty(numerator) && !IsNullOrEmpty(denominator))
            {
                decimal a = decimal.Parse(numerator);
                decimal b = decimal.Parse(denominator);
                if (b > 0)
                {
                    c = a / b * 100;
                    percent = string.Format("{0:f" + number + "}", c) + "%";
                }
                else
                {
                    percent = "分母为零";
                }
            }
            else
            {
                percent = "空值";
            }
            return percent;
        }
        #endregion

        #region 截取字符枚举值
        /// <summary>
        /// 截取字符枚举值,Varchar--英文一个字节，中文两个字节，NVarchar--无论中英文都是两个字节
        /// </summary>
        public enum CutType
        {
            Varchar,
            NVarchar
        }
        #endregion

        #region 获取指定长度的字符串
        /// <summary>
        /// 要截取的字节数
        /// </summary>
        /// <param name="value">输入的字符串</param>
        /// <param name="length">限定长度</param>
        /// <param name="ellipsis">是否需要省略号,true--需要，false--不需要</param>
        /// <param name="cuttype">截取类型 Varchar,NVarchar</param>
        /// <returns>截取后的字符串，如果是NVarchar--则20个字节就会有10个字符，Varchar--20个字节会有>=10个字符</returns>
        public static string CutString(string value, int length, bool ellipsis, CutType cuttype)
        {
            if (length < 0)
                length = 0;
            if (value == null)
                return "";
            value = value.Trim();
            if (value.Length == 0)
                return string.Empty;
            if (cuttype == CutType.NVarchar)
            {
                if (value.Length > length / 2)
                {
                    value = value.Substring(0, length / 2);
                    if (ellipsis)
                        return value + "..";
                }
            }
            else
            {
                //没想到这种方法是最高效的
                int len = value.Length;
                int i = 0;
                for (; i < length && i < len; ++i)
                {
                    if ((int)(value[i]) > 0xFF)
                        --length;
                }
                if (length < i && i > 0)
                {
                    length = i - 1;
                    if (ellipsis)
                        return value.Substring(0, length) + "..";
                    return value.Substring(0, length);
                }
                else if (length > len)
                {
                    length = len;
                }
                return value.Substring(0, length);
            }
            return value;
        }
        #endregion

        #region 地址栏传值加密
        /// <summary>
        /// 地址栏传值加密
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string EncryptFilename(string value)
        {
            if (!UtilityClass.IsNullOrEmpty(value))
            {
                byte[] buffer = HttpContext.Current.Request.ContentEncoding.GetBytes(value);
                value = Convert.ToBase64String(buffer).Replace('+', '@').Replace('/', '*');
                int length = value.Length;
                if (length > 3)
                {
                    //确定需要分组的组数，按每3位分一组
                    int group = length / 3;
                    for (int i = 0, j = 3; i < group; i++, j = j + 3)
                    {
                        value = value.Insert(j + i, ",");
                    }
                    StringBuilder rebuildvalue = new StringBuilder(length);
                    string[] everygroup = value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = everygroup.Length - 1; i >= 0; i--)
                        rebuildvalue.Append(everygroup[i]);
                    return rebuildvalue.ToString();
                }
            }
            return value;
        }
        #endregion

        #region 地址栏传值解密
        /// <summary>
        /// 地址栏传值解密
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string DecryptFilename(string value)
        {
            if (!UtilityClass.IsNullOrEmpty(value))
            {
                int length = value.Length;
                if (length > 3)
                {
                    int modvaluelength = length % 3;
                    int group = length / 3;
                    StringBuilder rebuildvalue = new StringBuilder(length);
                    string modvalue = value.Substring(0, modvaluelength);
                    value = value.Substring(modvaluelength);
                    for (int i = 0, j = 3; i < group; i++, j = j + 3)
                    {
                        value = value.Insert(j + i, ",");
                    }
                    string[] everygroup = value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = everygroup.Length - 1; i >= 0; i--)
                        rebuildvalue.Append(everygroup[i]);
                    rebuildvalue.Append(modvalue);
                    value = rebuildvalue.ToString();
                    byte[] buffer = Convert.FromBase64String(value.ToString().Replace('@', '+').Replace('*', '/'));
                    value = HttpContext.Current.Request.ContentEncoding.GetString(buffer);
                    return value;
                }
            }
            return value;
        }
        #endregion

        #region 获得当前页面客户端的IP
        /// <summary>
        /// 获得当前页面客户端的IP
        /// </summary>
        /// <returns>当前页面客户端的IP</returns>
        public static string GetClientIP()
        {
            string result = String.Empty;
            result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }

            if (IsNullOrEmpty(result) || !IsIP(result))
            {
                return "0.0.0.0";
            }
            return result;
        }
        /// <summary>
        /// 是否为ip
        /// </summary>
        /// <param name="ip">参数名称</param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }
        #endregion

        #region 格式化要显示的内容，主要用于在网页上显示由textarea产生的内容
        /// <summary>
        /// 格式化要显示的内容，主要用于在网页上显示由textarea产生的内容
        /// </summary>
        /// <param name="value">传入字符串</param>
        /// <param name="labelType">要把换行符转换的标签类型，1-- 标签 br 换行符,2--标签 p 段落换行</param>
        /// <returns></returns>
        public static string FormatHtml(string value, int labelType)
        {
            if (value.Trim().Length == 0)
                return string.Empty;
            if (labelType.Equals(1))
            {
                value = value.Replace("\r\n", "<br/>");
                value = value.Replace("\n", "<br/>");
            }
            else
            {
                value = value.Replace("\r\n", "<p></p>");
                value = value.Replace("\n", "<p></p>");
            }
            return value;
        }
        #endregion

        #region 判断当前访问是否来自浏览器软件
        /// <summary>
        /// 判断当前访问是否来自浏览器软件
        /// </summary>
        /// <returns></returns>
        public static bool IsBrowser()
        {
            string[] BrowserName = { "ie", "opera", "netscape", "mozilla", "konqueror", "firefox" };
            string curBrowser = HttpContext.Current.Request.Browser.Type.ToLower();
            for (int i = 0; i < BrowserName.Length; i++)
            {
                if (curBrowser.IndexOf(BrowserName[i]) >= 0)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region 判断当前访问是否来自非IE浏览器软件
        /// <summary>
        /// 判断当前访问是否来自非IE浏览器软件
        /// </summary>
        /// <returns></returns>
        public static bool IsIEBrowser()
        {
            string curBrowser = HttpContext.Current.Request.Browser.Type.ToLower();
            if (curBrowser == "ie")
            {
                return true;
            }
            return false;
        }
        #endregion

        #region 后退
        /// <summary>
        /// 后退
        /// </summary>
        /// <param name="WebPage"></param>
        public static void GoBack(System.Web.UI.Page WebPage)
        {
            string str_script = "\n<script>history.go(-1)</script>\n";
            WebPage.Response.Write(str_script);
        }
        #endregion
    }
}
