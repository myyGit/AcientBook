using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common
{
    public static class EncryptionHelper
    {
        #region MD5加密（不可逆）
        /// <summary>
        /// MD5的64位加密 不可逆
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string MD5Encrypt64(string password)
        {
            string cl = password;
            //string pwd = "";
            MD5 md5 = MD5.Create(); //实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            return Convert.ToBase64String(s);
        }
        /// <summary>
        /// MD5的64位加密 不可逆
        /// </summary>
        /// <param name="normalTxt"></param>
        /// <returns></returns>
        public static string MD5Encrypt(string normalTxt)
        {
            var bytes = Encoding.Default.GetBytes(normalTxt);//求Byte[]数组  
            var Md5 = new MD5CryptoServiceProvider().ComputeHash(bytes);//求哈希值  
            return Convert.ToBase64String(Md5);//将Byte[]数组转为净荷明文(其实就是字符串)  
        } 
        /// <summary>
        /// 32位MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string MD5Encrypt32(string password)
        {
            string cl = password;
            string pwd = "";
            MD5 md5 = MD5.Create(); //实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                //pwd = pwd + s[i].ToString("X");
                var a = s[i].ToString("x");
                var b = a.Length == 1 ? ("0"+a) : a;
                pwd = pwd + b;
            }
            return pwd;
        }
        #endregion
        #region RSA数据加密解密
        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="normaltxt"></param>
        /// <returns></returns>
        public static string RSAEncrypt(string normaltxt)
        {
            var bytes = Encoding.Default.GetBytes(normaltxt);
            var encryptBytes = new RSACryptoServiceProvider(new CspParameters()).Encrypt(bytes, false);
            return Convert.ToBase64String(encryptBytes);
        }
        /// <summary>
        /// RSA解密（暂时失败不可用）
        /// </summary>
        /// <param name="securityTxt"></param>
        /// <returns></returns>
        public static string RSADecrypt(string securityTxt)
        {
            try//必须使用Try catch,不然输入的字符串不是净荷明文程序就Gameover了  
            {
                var bytes = Convert.FromBase64String(securityTxt);
                var DecryptBytes = new RSACryptoServiceProvider(new CspParameters()).Decrypt(bytes, false);
                return Encoding.Default.GetString(DecryptBytes);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        } 
        #endregion

        #region AES加密
        /// <summary>  
        /// AES加密(无向量)  
        /// </summary>  
        /// <param name="plainBytes">被加密的明文</param>  
        /// <param name="key">密钥</param>  
        /// <returns>密文</returns>  
        public static string AesEncrypt(string encryptStr, string key)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(encryptStr);
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }


        /// <summary>  
        /// AES解密(无向量)  
        /// </summary>  
        /// <param name="encryptedBytes">被加密的明文</param>  
        /// <param name="key">密钥</param>  
        /// <returns>明文</returns>  
        public static string AesDecrypt(string str, string key)
        {
            if (string.IsNullOrEmpty(str)) return null;
            Byte[] toEncryptArray = Convert.FromBase64String(str);

            System.Security.Cryptography.RijndaelManaged rm = new System.Security.Cryptography.RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = System.Security.Cryptography.CipherMode.ECB,
                Padding = System.Security.Cryptography.PaddingMode.PKCS7
            };

            System.Security.Cryptography.ICryptoTransform cTransform = rm.CreateDecryptor();
            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Encoding.UTF8.GetString(resultArray);
        }
        #endregion
        #region DES加密解密
      //  private static string encryptKey = "Oyea";    //定义密钥 
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="normalTxt">密码字符串</param>
        /// <param name="encryptKey">定义的密钥，似乎只能4位，如：Oyea</param>
        /// <returns></returns>
        public static string DesEncrypt(string normalTxt, string encryptKey2)
        {
            DESCryptoServiceProvider descsp = new DESCryptoServiceProvider();   //实例化加/解密类对象   
            byte[] key = Encoding.Unicode.GetBytes(encryptKey2); //定义字节数组，用来存储密钥    
            byte[] data = Encoding.Unicode.GetBytes(normalTxt);//定义字节数组，用来存储要加密的字符串  
            MemoryStream MStream = new MemoryStream(); //实例化内存流对象      
            //使用内存流实例化加密流对象   
            CryptoStream CStream = new CryptoStream(MStream, descsp.CreateEncryptor(key, key), CryptoStreamMode.Write);
            CStream.Write(data, 0, data.Length);  //向加密流中写入数据      
            CStream.FlushFinalBlock();              //释放加密流      
            return Convert.ToBase64String(MStream.ToArray());//返回加密后的字符串  
        }
        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="securityTxt">加密后的字符串</param>
        /// <param name="encryptKey">定义的密钥，似乎只能4位，如：Oyea</param>
        /// <returns></returns>
        public static string DesDecrypt(string securityTxt, string encryptKey)//解密  
        {
            DESCryptoServiceProvider descsp = new DESCryptoServiceProvider();   //实例化加/解密类对象  
            byte[] key = Encoding.Unicode.GetBytes(encryptKey); //定义字节数组，用来存储密钥    
            byte[] data = Convert.FromBase64String(securityTxt);//定义字节数组，用来存储要解密的字符串  
            MemoryStream MStream = new MemoryStream(); //实例化内存流对象      
            //使用内存流实例化解密流对象       
            CryptoStream CStream = new CryptoStream(MStream, descsp.CreateDecryptor(key, key), CryptoStreamMode.Write);
            CStream.Write(data, 0, data.Length);      //向解密流中写入数据     
            CStream.FlushFinalBlock();               //释放解密流      
            return Encoding.Unicode.GetString(MStream.ToArray());       //返回解密后的字符串  
        } 
        #endregion
        #region SHA四种加密方式（不可逆）
        public static string SHA1Encrypt(string normalTxt)
        {
            var bytes = Encoding.Default.GetBytes(normalTxt);
            var SHA = new SHA1CryptoServiceProvider();
            var encryptbytes = SHA.ComputeHash(bytes);
            return Convert.ToBase64String(encryptbytes);
        }
        public static string SHA256Encrypt(string normalTxt)
        {
            var bytes = Encoding.Default.GetBytes(normalTxt);
            var SHA256 = new SHA256CryptoServiceProvider();
            var encryptbytes = SHA256.ComputeHash(bytes);
            return Convert.ToBase64String(encryptbytes);
        }
        public static string SHA384Encrypt(string normalTxt)
        {
            var bytes = Encoding.Default.GetBytes(normalTxt);
            var SHA384 = new SHA384CryptoServiceProvider();
            var encryptbytes = SHA384.ComputeHash(bytes);
            return Convert.ToBase64String(encryptbytes);
        }
        public static string SHA512Encrypt(string normalTxt)
        {
            var bytes = Encoding.Default.GetBytes(normalTxt);
            var SHA512 = new SHA512CryptoServiceProvider();
            var encryptbytes = SHA512.ComputeHash(bytes);
            return Convert.ToBase64String(encryptbytes);
        }
        #endregion
    }
}
