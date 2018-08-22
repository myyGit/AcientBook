using AncientBook.Service;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AncientBook.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserAccountService _userService;
        public AccountController(IUserAccountService userService)
        {
            this._userService = userService;
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public FileResult GetValidateCode()
        {
            string code = ToolClass.CreateRandomCode(4);
            TempData["SecurityCode"] = code;
            return File(ToolClass.CreateValidateGraphic(code), "image/jpeg");
        }
        [HttpPost]
        public ActionResult LoginDeal()
        
        {
            string account = Request["account"];
            string password = Request["password"];
            string validateCode = Request["validateCode"];
            int online = Convert.ToInt32(Request["online"]);

            if ((TempData["SecurityCode"] == null) || !TempData["SecurityCode"].ToString().ToUpper().Equals(validateCode.ToUpper()))
            {
                return Json("验证码错误！");
            }
            var user = _userService.GetUserByUserName(account);
            string md5Pwd = EncryptionHelper.MD5Encrypt32(password);
            if (user != null && user.Password.ToLower() == md5Pwd.ToLower())
            {
                //if (online == 1)   //存入缓存
                //{
                    //存缓存
                    Session["uid"] = user.UserUid;
                    Session["loginId"] = user.LoginId;
                    Session.Timeout = 120;
                    return Json("登录成功");
                //}
                //return Json("登录成功");
            }
            else if (user == null)
            {
                return Json("用户不存在");
            }
            else
            {
                return Json("用户名与密码不一致");
            }
        }
        public ActionResult LogOut() 
        {
            Session["uid"] = "";
            Session["loginId"] = "";
            return View("Login");
        }
    }
}
