using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class ToolClass
    {
        /*生成验证码用法：
         * 后台：string code = ToolClass.CreateRandomCode(4);
           TempData["SecurityCode"] = code;
           return File(ToolClass.CreateValidateGraphic(code), "image/jpeg");
         * 前台： <img id="code" src="@Url.Action("SecurityCode")" onclick="this.src=this.src+'/'" /> 
           function changeCode(){
           document.getElementById("code").src = document.getElementById("code").src+'?';}
         */

        /// <summary>
        /// 生成验证码的随机字符串
        /// </summary>
        /// <param name="codeCount">生成字符串的长度，如：4位，6位</param>
        /// <returns></returns>
        public static string CreateRandomCode(int codeCount)
        {
            string allChar = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            string[] allCharArry = allChar.Split(',');  //在此只能用单引号
            string randomCode = "";
            int temp = -1;
            Random rand = new Random();
            for (int i = 0; i < codeCount; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
                }
                int t = rand.Next(36);    //allChar总共36个字符
                if (temp == t)
                {
                    return CreateRandomCode(codeCount);
                }
                temp = t;
                randomCode += allCharArry[t];
            }
            return randomCode;
        }
        /// <summary>
        /// 利用字符串创建验证码的图片[two]
        /// </summary>
        /// <param name="validateCode">验证码的字符串</param>
        /// <returns></returns>
        public static byte[] CreateValidateGraphic(string validateCode)
        {
            Bitmap image = new Bitmap((int)Math.Ceiling(validateCode.Length * 16.0), 27);
            Graphics g = Graphics.FromImage(image);
            try
            {
                //生成随机生成器
                Random random = new Random();
                //1画图片的干扰线
                g.Clear(Color.White);
                for (int i = 0; i < 25; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Width);
                    int y2 = random.Next(image.Width);
                    g.DrawLine(new Pen(Color.Khaki), x1, x2, y1, y2);
                }
                //3验证的字符串画到图片
                Font font = new Font("Arial", 13, FontStyle.Italic);
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Green, Color.GreenYellow, 1.2f, true);
                g.DrawString(validateCode, font, brush, 3, 2);
                //2画图片的干扰点[前景]
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }
                //4画图片的边框线
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                //5保存图片数据
                MemoryStream stream = new MemoryStream();
                image.Save(stream, ImageFormat.Jpeg);
                //6输出图片流
                return stream.ToArray();
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }
    }
}
