using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
//using Microsoft.International.Converters.TraditionalChineseToSimplifiedConverter; //需要引用Nuget程序包ChineseConverter
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Word;
using System.Reflection; 

namespace Common
{
    public static class FontTypeConvert
    {
        [DllImport("kernel32.dll", EntryPoint = "LCMapStringA")]
        public static extern int LCMapString(int Locale, int dwMapFlags, byte[] lpSrcStr, int cchSrc, byte[] lpDestStr, int cchDest);
        const int LCMAP_SIMPLIFIED_CHINESE = 0x02000000;  //繁体转简体
        const int LCMAP_TRADITIONAL_CHINESE = 0x04000000; //简体转繁体
        #region VisualBasic：简体字和繁体字转换
        /// <summary>
        /// VisualBasic：简体字转繁体字
        /// 有些字体的繁体字转换不过来，例如：理发的发和发财的发分别是髮和發
        /// </summary>
        /// <param name="simpChara">简体字</param>
        /// <returns>繁体字</returns>
        public static string VBConvertOrig(string simpChara)
        {
            //理发的发和发财的发分别是髮和發
            //闹钟和一见钟情则分别是鬧鐘和一見鍾情
            var original = Strings.StrConv(simpChara, VbStrConv.TraditionalChinese, 0);
            return original;
        }
        /// <summary>
        /// VisualBasic：繁体字转简体字
        /// </summary>
        /// <param name="original">繁体字</param>
        /// <returns>简体字</returns>
        public static string VBConvertSimp(string original)
        {
            var simpChara = Strings.StrConv(original, VbStrConv.SimplifiedChinese, 0);
            return simpChara;
        }
        #endregion

        #region ChineseConverter：简体字和繁体字转换 --由于发布时出问题导致暂时先不用
        /// <summary>
        /// ChineseConverter：简体字转繁体字
        /// 缺点同VisualBasic
        /// </summary>
        /// <param name="simpChara">简体字</param>
        /// <returns></returns>
        //public static string ChineseConvertOrig(string simpChara)
        //{
        //    string original = ChineseConverter.Convert(simpChara, ChineseConversionDirection.SimplifiedToTraditional);
        //    return original;
        //}
        /// <summary>
        /// ChineseConverter：繁体字转简体字
        /// 缺点同VisualBasic
        /// </summary>
        /// <param name="original">繁体字</param>
        /// <returns></returns>
        //public static string ChineseConvertSimp(string original)
        //{
        //    string simpChara = ChineseConverter.Convert(original, ChineseConversionDirection.TraditionalToSimplified);
        //    return simpChara;
        //}
        #endregion

        #region Kernel32：简体字和繁体字转换
        /// <summary>
        /// Kernel32：简体字转繁体字
        /// </summary>
        /// <param name="simpChara">简体字</param>
        /// <returns>繁体字</returns>
        public static string Kernel32ConvertOrig(string simpChara)
        {
            string original = ToTraditional(simpChara, LCMAP_TRADITIONAL_CHINESE);
            return original;
        }
        /// <summary>
        /// Kernel32：繁体字转简体字
        /// </summary>
        /// <param name="simpChara">繁体字</param>
        /// <returns>简体字</returns>
        public static string Kernel32ConvertSimp(string original)
        {
            string simpChara = ToTraditional(original, LCMAP_SIMPLIFIED_CHINESE);
            return simpChara;
        }
        public static string ToTraditional(string source, int type)
        {
            byte[] srcByte2 = Encoding.Default.GetBytes(source);
            byte[] desByte2 = new byte[srcByte2.Length];
            LCMapString(2052, type, srcByte2, -1, desByte2, srcByte2.Length);
            string des2 = Encoding.Default.GetString(desByte2);
            return null;
        }
        #endregion

        #region Office：简体字和繁体字转换
        /// <summary>
        /// Office：简体字转繁体字
        /// 需要添加引用Microsoft.Office.Interop.Word
        /// </summary>
        /// <param name="simpChara"></param>
        /// <returns></returns>
        public static string OfficeConvertOrig(string simpChara)
        {
            string orignChara = "";
            _Application appWord = new Application();
            object template = Missing.Value;
            object newTemplate = Missing.Value;
            object docType = Missing.Value;
            object visible = true;
            Document doc = appWord.Documents.Add(ref template, ref newTemplate, ref docType, ref visible);
            appWord.Selection.TypeText(simpChara);
            appWord.Selection.Range.TCSCConverter(WdTCSCConverterDirection.wdTCSCConverterDirectionSCTC, true, true);
            appWord.ActiveDocument.Select();
            orignChara = appWord.Selection.Text;
            object saveChange = 0;
            object originalFormat = Missing.Value;
            object routeDocument = Missing.Value;
            appWord.Quit(ref saveChange, ref originalFormat, ref routeDocument);
            doc = null;
            appWord = null;
            GC.Collect();//进程资源释放
            return orignChara;
        }
        /// <summary>
        ///  Office：繁体字转简体字
        /// </summary>
        /// <param name="orignChara"></param>
        /// <returns></returns>
        public static string OfficeConvertSimp(string orignChara)
        {
            string simpChara = "";
            _Application appWord = new Microsoft.Office.Interop.Word.Application();
            object template = Missing.Value;
            object newTemplate = Missing.Value;
            object docType = Missing.Value;
            object visible = true;
            Document doc = appWord.Documents.Add(ref template, ref newTemplate, ref docType, ref visible);
            appWord.Selection.TypeText(orignChara);
            appWord.Selection.Range.TCSCConverter(WdTCSCConverterDirection.wdTCSCConverterDirectionTCSC, true, true);
            appWord.ActiveDocument.Select();
            simpChara = appWord.Selection.Text;
            object saveChange = 0;
            object originalFormat = Missing.Value;
            object routeDocument = Missing.Value;
            appWord.Quit(ref saveChange, ref originalFormat, ref routeDocument);
            doc = null;
            appWord = null;
            GC.Collect();//进程资源释放
            return simpChara;
        }
        #endregion
        
    }
}
