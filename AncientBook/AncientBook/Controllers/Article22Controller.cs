//using AncientBook.Data;
//using AncientBook.Service;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using Common;
//using AncientBook.Models;
//using System.Net.Http;
//using Newtonsoft.Json;
//using System.Configuration;
//using Webdiyer.WebControls.Mvc;
//using System.IO;
//using AncientBook.common;

//namespace AncientBook.Controllers
//{
//    public class Article22Controller : Controller
//    {
//        private readonly IBookService _bookService;
//        private readonly int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"].ToString());
//        private readonly string domain = ConfigurationManager.AppSettings["Domain"];
//        public CommonInfo commonInfo = new CommonInfo();
//        private static string s_keyWord = "";
//        private static int s_categoryId = 0;
//        private static  int r_categoryId = 0;
//        private static  string r_title = "";
//        private static  int r_categoryId2 = 0;
//        private static  string r_title2 = "";
//        private static  int r_searchType = 1;  //1:与 2：或
//        public Article22Controller(IBookService bookService) 
//        {
//            this._bookService = bookService;
//        }

//        public ActionResult Index()
//        {
//            return View();
//        }

//        public ActionResult ListFromIndex3(string keyWord, int pageIndex = 1)
//        {
//            commonInfo.WirteLog("ListFromIndex开始时间");
//            int categoryId = 0;
//            BookQueryData query = new BookQueryData();
//            query.CategoryIdOne = (TypeEnum)categoryId;
//            query.KeyWordOneSimp = keyWord;
//            query.KeyWordOneOrig = FontTypeConvert.VBConvertOrig(keyWord);
//            commonInfo.WirteLog("ListFromIndex简繁转换");
//            int count = 0;
//            IQueryable<TB_Book> bookList = _bookService.GetBookListByQueryData(query, out count,0,0);
//            commonInfo.WirteLog("ListFromIndex书籍Query");
//            List<TB_Book> tbBookList = bookList.ToList();
//            commonInfo.WirteLog("ListFromIndex书籍List");
//            List<BookViewModel> models = new List<BookViewModel>();
//            models.O2D(tbBookList, true, null, "TB_Category");
//            commonInfo.WirteLog("ListFromIndex书籍O2D");
//            int i = 0;
//            foreach (var item in tbBookList.Skip((pageIndex - 1) * pageSize).Take(pageSize))
//            {
//                if (i <= pageSize)
//                {
//                    var model = models.Where(p => p.BookUid == item.BookUid).FirstOrDefault();
//                    model.Category = item.Category;
//                    model.CategoryName = item.Category.CategoryName;
//                    i++;
//                }
//                else
//                {
//                    break;
//                }
//            }
//            commonInfo.WirteLog("ListFromIndex循环赋值");
//            PagedList<BookViewModel> p_list = models.ToPagedList(Convert.ToInt32(pageIndex), pageSize);
//            commonInfo.WirteLog("ListFromIndex列表分页");
//            s_keyWord = keyWord;
//            s_categoryId = 0;
//            ViewData["keyWord"] = keyWord;
//            ViewData["KeyJian"] = keyWord;
//            ViewData["KeyFan"] = FontTypeConvert.VBConvertOrig(keyWord);
//            commonInfo.WirteLog("ListFromIndex简体转繁");
//            ViewData["CategoryId"] = 0;
//            return View("List", p_list);
//        }
//        public ActionResult ListFromIndex(string keyWord, int pageIndex = 1)
//        {
//            commonInfo.WirteLog("ListFromIndex开始时间");
//            int categoryId = 0;
//            BookQueryData query = new BookQueryData();
//            query.CategoryIdOne = (TypeEnum)categoryId;
//            query.KeyWordOneSimp = keyWord;
//            query.KeyWordOneOrig = FontTypeConvert.VBConvertOrig(keyWord);
//            commonInfo.WirteLog("ListFromIndex简繁转换");
//            int count = 0;
//            IQueryable<TB_Book> bookList = _bookService.GetBookListByQueryData(query, out count, 0, 0);
//            commonInfo.WirteLog("ListFromIndex书籍Query");
//            List<TB_Book> tbBookList = bookList.ToList();
//            commonInfo.WirteLog("ListFromIndex书籍List");
//            List<BookViewModel> models = new List<BookViewModel>();
//            models.O2D(tbBookList, true, null, "TB_Category");
//            commonInfo.WirteLog("ListFromIndex书籍O2D");
//            int i = 0;
//            foreach (var item in tbBookList.Skip((pageIndex - 1) * pageSize).Take(pageSize))
//            {
//                if (i <= pageSize)
//                {
//                    var model = models.Where(p => p.BookUid == item.BookUid).FirstOrDefault();
//                    model.Category = item.Category;
//                    model.CategoryName = item.Category.CategoryName;
//                    i++;
//                }
//                else
//                {
//                    break;
//                }
//            }
//            commonInfo.WirteLog("ListFromIndex循环赋值");
//            PagedList<BookViewModel> p_list = models.ToPagedList(Convert.ToInt32(pageIndex), pageSize);
//            commonInfo.WirteLog("ListFromIndex列表分页");
//            s_keyWord = keyWord;
//            s_categoryId = 0;
//            ViewData["keyWord"] = keyWord;
//            ViewData["KeyJian"] = keyWord;
//            ViewData["KeyFan"] = FontTypeConvert.VBConvertOrig(keyWord);
//            commonInfo.WirteLog("ListFromIndex简体转繁");
//            ViewData["CategoryId"] = 0;
//            return View("List", p_list);
//        }
//        public ActionResult List(int? pageIndex = 1,int? categoryId = 0) 
//        {
//            commonInfo.WirteLog("List开始时间");
//            var keyWord = Request.Form["title"];
//            categoryId = categoryId == null ? 0 : categoryId;   //书的目录id
//            if (categoryId != 0)
//            {
//                s_categoryId = Convert.ToInt32(categoryId);
//            }
//            if (categoryId == 0 && s_categoryId != 0 && !string.IsNullOrEmpty(keyWord))
//            {
//                categoryId = s_categoryId;
//            }
//            s_categoryId = Convert.ToInt32(categoryId);
//            pageIndex = (pageIndex == 0 || pageIndex == null) ? 1 : pageIndex;
//            BookQueryData query = new BookQueryData();
//            query.CategoryIdOne = (TypeEnum)0;
//            query.KeyWordOneSimp = keyWord;
//            query.KeyWordOneOrig = FontTypeConvert.VBConvertOrig(keyWord);
//            commonInfo.WirteLog("List简繁转换");
//            int count = 0;
//            IQueryable<TB_Book> bookList = _bookService.GetBookListByQueryData(query, out count, 0, 0, Convert.ToInt32(categoryId));
//            commonInfo.WirteLog("List数据列表");
//            List<TB_Book> tbBookList = bookList.ToList();
//            commonInfo.WirteLog("List数据ToList");
//            List<BookViewModel> models = new List<BookViewModel>();
//            models.O2D(tbBookList, true, null, "TB_Category");
//            commonInfo.WirteLog("List书籍O2D");
//            int i = 0;
//            foreach (var item in tbBookList.Skip(((int)pageIndex - 1) * pageSize).Take(pageSize))
//            {
//                if (i <= pageSize)
//                {
//                    models.Where(p => p.BookUid == item.BookUid).FirstOrDefault().Category = item.Category;
//                    models.Where(p => p.BookUid == item.BookUid).FirstOrDefault().CategoryName = item.Category.CategoryName;
//                    i++;
//                }
//                else
//                {
//                    break;
//                }
//            }
//            commonInfo.WirteLog("List循环赋值");
//            PagedList<BookViewModel> p_list = models.ToPagedList(Convert.ToInt32(pageIndex), pageSize);
//            commonInfo.WirteLog("List分页数据");
//            ViewData["KeyJian"] = keyWord;
//            ViewData["KeyFan"] = FontTypeConvert.VBConvertOrig(keyWord);
//            ViewData["CategoryId"] = 0;
//            p_list.CurrentPageIndex = Convert.ToInt32(pageIndex);
//            if (Request.IsAjaxRequest())
//            {
//               // System.Threading.Thread.Sleep(2000);
//                return PartialView("_PartList", p_list);
//            }
//            return View(p_list);
//        }
//        public ActionResult Read(Guid id) 
//        {
//            BookViewModel bookViewModel = new BookViewModel();
//            var httpClient = new HttpClient();
//            var responseJson = httpClient.GetAsync(domain + "/Article/ReadEntity/" + id.ToString());
//            var sites = responseJson.Result.Content.ReadAsStringAsync().Result;
//            bookViewModel = JsonConvert.DeserializeObject<BookViewModel>(sites);
//            return View(bookViewModel);
//        }
//        public ActionResult MuchList(int pageIndex=1) 
//        {
//            return View();
//        }
//        public ActionResult MuchListTable(int pageIndex = 1)
//        {
//            commonInfo.WirteLog("MuchListTable开始");
//            #region 查询条件
//            ///0:全部 1：书名 2：责任者 3：出版者 4：朝代 5：其他名称
//            int categoryId = Convert.ToInt32(Request["categoryId1"]);
//            string title = Request["title1"];
//            int categoryId2 = Convert.ToInt32(Request["categoryId2"]);
//            string title2 = Request["title2"];
//            int searchType = Convert.ToInt32(Request["searchType"]);  //1:与 2：或
//            if (!string.IsNullOrEmpty(title))
//            {
//                r_categoryId = Convert.ToInt32(categoryId);
//                r_title = title;
//            }
//            if (!string.IsNullOrEmpty(title2))
//            {
//                r_categoryId2 = Convert.ToInt32(categoryId2);
//                r_title2 = title2;
//                r_searchType = searchType;
//            }
//            if (string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(r_title))
//            {
//                categoryId = r_categoryId;
//                title = r_title;
//            }
//            if (string.IsNullOrEmpty(title2) && !string.IsNullOrEmpty(r_title2))
//            {
//                categoryId2 = r_categoryId2;
//                title2 = r_title2;
//                searchType = r_searchType;
//            }
//            r_categoryId = Convert.ToInt32(categoryId);
//            r_categoryId2 = Convert.ToInt32(categoryId2);
//            r_title = title;
//            r_title2 = title2;
//            r_searchType = searchType;
//            #endregion
//            commonInfo.WirteLog("MuchListTable查询条件结束");
//            BookQueryData query = new BookQueryData();
//            query.CategoryIdOne = (TypeEnum)categoryId;
//            query.KeyWordOneSimp = title;
//            query.KeyWordOneOrig = FontTypeConvert.VBConvertOrig(title);
//            query.CategoryIdTwo = (TypeEnum)categoryId2;
//            query.KeyWordTwoSimp = title2;
//            query.KeyWordTwoOrig = FontTypeConvert.VBConvertOrig(title2);
//            query.SearchType = searchType;
//            int count = 0;
//            commonInfo.WirteLog("MuchListTable简繁转换");
//            IQueryable<TB_Book> bookList = _bookService.GetBookListByQueryData(query, out count);
//            commonInfo.WirteLog("MuchListTable数据Query");
//            List<TB_Book> tbBookList = bookList.ToList();
//            commonInfo.WirteLog("MuchListTable数据ToList");

//            List<BookViewModel> models = new List<BookViewModel>();
//            models = GetModels(tbBookList, pageIndex);
//            commonInfo.WirteLog("MuchListTable循环赋值");
//            PagedList<BookViewModel> p_list = models.ToPagedList(Convert.ToInt32(pageIndex), pageSize);
//            commonInfo.WirteLog("MuchListTable分页数据");

//            ViewData["Query"] = categoryId + "," + title + "," + categoryId2 + "," + title2;
//            ViewData["searchType"] = searchType;
//            ViewData["KeyJian"] = title;
//            ViewData["KeyFan"] = FontTypeConvert.VBConvertOrig(title);
//            ViewData["KeyJian2"] = title2;
//            ViewData["KeyFan2"] = FontTypeConvert.VBConvertOrig(title2);
//            if (Request.IsAjaxRequest())
//            {
//                return PartialView("_PartMuchTableList", p_list);
//            }
//            return View(p_list);
//        }

//        public List<BookViewModel> GetModels(List<TB_Book> tbBookList,int pageIndex)
//        {
//            List<BookViewModel> models = new List<BookViewModel>();
//            models.O2D(tbBookList, true, null, "TB_Category");
//            commonInfo.WirteLog("List书籍O2D");
//            int i = 0;
//            foreach (var item in tbBookList.Skip(((int)pageIndex - 1) * pageSize).Take(pageSize))
//            {
//                if (i <= pageSize)
//                {
//                    models.Where(p => p.BookUid == item.BookUid).FirstOrDefault().Category = item.Category;
//                    models.Where(p => p.BookUid == item.BookUid).FirstOrDefault().CategoryName = item.Category.CategoryName;
//                    i++;
//                }
//                else
//                {
//                    break;
//                }
//            }
//            return models;
//        }
//    }
//}
