using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class HttpHelper
    {
        /// <summary>
        /// 将Key&Value的键值对List转化为string普通字符串
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string KeyValue2QueryStr(List<KeyValuePair<string, object>> list)
        {
            if (list != null)
            {
                return string.Join("&", list.Select(p => p.Key + "=" + (p.Value == null ? "" : p.Value.ToString())));
            }
            return null;
        }
        /// <summary>
        /// 将Key&Value的键值对List转化为string的JSON字符串
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string KeyValue2Json(List<KeyValuePair<string, object>> list)
        {
            if (list != null)
            {
                return "{" + string.Join(",", list.Select(p => "\"" + p.Key + "\":\"" + p.Value.ToString().Replace("\"", "\\\"") + "\"")) + "}";
            }
            return null;
        }
        /// <summary>
        /// 将post数据转换成application/x-www-form-urlencoded要求的content=的格式
        /// </summary>
        /// <param name="list">需要转换的post数据</param>
        /// <returns>返回content=Json格式的字符串</returns>
        public static string KeyValue2Content(List<KeyValuePair<string, object>> list)
        {
            return "content=" + (KeyValue2Json(list) == null ? "" : KeyValue2Json(list));
        }
        /// <summary>
        /// 将Key&Value的键值对List转化为表单字符串
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string KeyValue2form(List<KeyValuePair<string, object>> list)
        {
            return string.Join("&", list.Select(p => p.Key + "=" + p.Value));
        }
        /// <summary>
        /// Post请求
        /// </summary>
        /// <param name="URL">post请求地址</param>
        /// <param name="PostContent">请求的Post消息内容</param>
        /// <param name="QueryString">拼接在url后的字符串</param>
        /// <param name="Method">请求方法Get/Post</param>
        /// <param name="ContentType">请求头类型</param>
        /// <param name="IsContentMode"></param>
        /// <returns></returns>
        public static HttpWebResponse PostSync(string URL, string PostContent, string QueryString, string Method = "POST", string ContentType = "application/json", bool IsContentMode = false,bool isAllowRedirect = false)
        {

            string wholeURL = URL;
            if (!string.IsNullOrEmpty(QueryString))
            {
                wholeURL += "?" + QueryString;
            }
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(wholeURL);
            if (PostContent == null)
                req.Method = "GET";
            else
                req.Method = Method;
            req.ContentType = ContentType;
            req.Accept = "text/html, application/xhtml+xml, */*";
            req.AllowAutoRedirect = isAllowRedirect;    //是否自动跳转
            req.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; WOW64; Trident/6.0)";
            req.KeepAlive = true;
            req.ServicePoint.ConnectionLimit = int.MaxValue;
            if (!string.IsNullOrEmpty(PostContent))
            {
                byte[] postdatabyte = Encoding.UTF8.GetBytes(PostContent);
                req.ContentLength = postdatabyte.Length;
                //提交请求
                Stream stream;
                stream = req.GetRequestStream();
                stream.Write(postdatabyte, 0, postdatabyte.Length);
                stream.Close();
            }
            //接收响应
            return (HttpWebResponse)req.GetResponse();

        }
        private static string HttpResult(HttpWebResponse res)
        {
            Stream streamReceive = res.GetResponseStream();
            Encoding encoding = Encoding.UTF8;
            //Encoding encoding = Encoding.Default;
            StreamReader streamReader = new StreamReader(streamReceive, encoding);
            string strResult = streamReader.ReadToEnd();
            streamReceive.Dispose();
            streamReader.Dispose();
            return strResult;
        }
        public static string PostSyncStr(string URL, string PostContent, string QueryString, string Method = "POST", string ContentType = "application/json", bool IsContentMode = false)
        {
            return HttpResult(PostSync(URL, PostContent, QueryString, Method, ContentType, IsContentMode));
        }
        public static T PostSync<T>(string URL, string PostContent, string QueryString, string Method = "POST", string ContentType = "application/json", bool IsContentMode = false)
        {
            string Tstr = PostSyncStr(URL, PostContent, QueryString, Method, ContentType, IsContentMode);
            return JsonConvert.DeserializeObject<T>(Tstr);
        }
        public static HttpWebResponse PostSync(string URL, List<KeyValuePair<string, object>> PostContent, List<KeyValuePair<string, object>> QueryString, string Method = "POST", string ContentType = "application/json", bool IsContentMode = false)
        {
            string content = null;
            if (ContentType == "application/x-www-form-urlencoded")
            {
                if (IsContentMode)
                    content = KeyValue2Content(PostContent);
                else
                    content = KeyValue2form(PostContent);
            }
            else
            {
                content = KeyValue2Json(PostContent);
            }

            return PostSync(URL, content, KeyValue2QueryStr(QueryString), Method, ContentType, IsContentMode);
        }
        public static string PostSyncStr(string URL, List<KeyValuePair<string, object>> PostContent, List<KeyValuePair<string, object>> QueryString, string Method = "POST", string ContentType = "application/json", bool IsContentMode = false)
        {
            return HttpResult(PostSync(URL, PostContent, QueryString, Method, ContentType, IsContentMode));
        }
        public static T PostSyncStr<T>(string URL, string PostContent, string QueryString, string Method = "POST", string ContentType = "application/json", bool IsContentMode = false)
        {
            string Tstr = PostSyncStr(URL, PostContent, QueryString, Method, ContentType, IsContentMode);
            return JsonConvert.DeserializeObject<T>(Tstr);
        }
        public static T PostSync<T>(string URL, List<KeyValuePair<string, object>> PostContent, List<KeyValuePair<string, object>> QueryString, string Method = "POST", string ContentType = "application/json", bool IsContentMode = false)
        {
            string Tstr = PostSyncStr(URL, PostContent, QueryString, Method, ContentType, IsContentMode);
            return JsonConvert.DeserializeObject<T>(Tstr);
        }
        public static HttpWebResponse PostSync(string URL, Dictionary<string, object> PostContent, Dictionary<string, object> QueryString, string Method = "POST", string ContentType = "application/json", bool IsContentMode = false)
        {
            return PostSync(URL, PostContent.ToList(), QueryString != null ? QueryString.ToList() : null, Method, ContentType, IsContentMode);
        }
        public static string PostSyncStr(string URL, Dictionary<string, object> PostContent, Dictionary<string, object> QueryString, string Method = "POST", string ContentType = "application/json", bool IsContentMode = false)
        {
            return PostSyncStr(URL, PostContent.ToList(), QueryString != null ? QueryString.ToList() : null, Method, ContentType, IsContentMode);
        }
        public static T PostSync<T>(string URL, Dictionary<string, object> PostContent, Dictionary<string, object> QueryString, string Method = "POST", string ContentType = "application/json", bool IsContentMode = false)
        {
            return PostSync<T>(URL, PostContent.ToList(), QueryString.ToList(), Method, ContentType, IsContentMode);
        }
        public static HttpWebResponse GetSync(string URL, string QueryString, string ContentType = "application/json")
        {
            return PostSync(URL, null, QueryString, "GET", ContentType);
        }
        public static string GetSyncStr(string URL, string QueryString, string ContentType = "application/json")
        {
            return HttpResult(GetSync(URL, QueryString, ContentType));
        }
        public static T GetSync<T>(string URL, string QueryString, string ContentType = "application/json")
        {
            return JsonConvert.DeserializeObject<T>(GetSyncStr(URL, QueryString, ContentType));
        }
        public static HttpWebResponse GetSync(string URL, List<KeyValuePair<string, object>> QueryString, string ContentType = "application/json")
        {
            return PostSync(URL, null, QueryString, "GET", ContentType);
        }
        public static string GetSyncStr(string URL, List<KeyValuePair<string, object>> QueryString, string ContentType = "application/json")
        {
            return HttpResult(GetSync(URL, QueryString, ContentType));
        }
        public static T GetSync<T>(string URL, List<KeyValuePair<string, object>> QueryString, string ContentType = "application/json")
        {
            return JsonConvert.DeserializeObject<T>(GetSyncStr(URL, QueryString, ContentType));
        }
        public static HttpWebResponse GetSync(string URL, Dictionary<string, object> QueryString, string ContentType = "application/json")
        {
            return GetSync(URL, QueryString.ToList(), ContentType);
        }
        public static string GetSyncStr(string URL, Dictionary<string, object> QueryString, string ContentType = "application/json")
        {
            return GetSyncStr(URL, QueryString.ToList(), ContentType);
        }
        public static T GetSync<T>(string URL, Dictionary<string, object> QueryString, string ContentType = "application/json")
        {
            return GetSync<T>(URL, QueryString.ToList(), ContentType);
        }
        public static HttpWebResponse GetSync(string URL, string ContentType = "application/json")
        {
            List<KeyValuePair<string, object>> QueryString = null;
            return GetSync(URL, QueryString, ContentType);
        }
        public static string GetSyncStr(string URL, string ContentType = "application/json")
        {
            List<KeyValuePair<string, object>> QueryString = null;
            return GetSyncStr(URL, QueryString, ContentType);
        }
        public static T GetSync<T>(string URL, string ContentType = "application/json")
        {
            List<KeyValuePair<string, object>> QueryString = null;
            return GetSync<T>(URL, QueryString, ContentType);
        }
    }
}
