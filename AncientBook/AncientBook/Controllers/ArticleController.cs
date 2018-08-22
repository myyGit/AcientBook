using AncientBook.common;
using AncientBook.Data;
using AncientBook.Data.QueryBook;
using AncientBook.Models;
using AncientBook.Service;
using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace AncientBook.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ICmsStatesService _cmsStateService;
        private readonly int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"].ToString());
        private readonly string domain = ConfigurationManager.AppSettings["Domain"];
        public CommonInfo commonInfo = new CommonInfo();
        private static string s_keyWord = "";
        private static int s_categoryId = 0;
        private static int r_categoryId = 0;
        private static string r_title = "";
        private static int r_categoryId2 = 0;
        private static string r_title2 = "";
        private static int r_searchType = 1;  //1:与 2：或
        public ArticleController(IBookService bookService, ICmsStatesService cmsStateService)
        {
            this._bookService = bookService;
            this._cmsStateService = cmsStateService;
        }

        public ActionResult Index()
        {
            return View();
        }
        public List<BookViewModel> GetQueryList(List<TB_Book> bookList)
        {
            List<BookViewModel> models = new List<BookViewModel>();
            //new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("ImageUris", "") }
            models.O2D(bookList, true, null, "TB_Category");
            foreach (var item in bookList)
            {
                var model = models.Where(p => p.BookUid == item.BookUid).FirstOrDefault();
                model.CategoryName = item.Category.CategoryName;
            }
            return models;
        }
        public ActionResult Read(Guid id)
        {
            BookViewModel bookViewModel = new BookViewModel();
            var httpClient = new HttpClient();
            var responseJson = httpClient.GetAsync(domain + "/Article/ReadEntity/" + id.ToString());
            var sites = responseJson.Result.Content.ReadAsStringAsync().Result;
            bookViewModel = JsonConvert.DeserializeObject<BookViewModel>(sites);
            //bookViewModel.ImageUris = new string[] { "214833-120r51r92947.jpg", "302075_wx.jpg", "402066.jpg" };
            //bookViewModel.ImageNames = new string[] { "dd711513-8ad0-4818-ba35-7e375ff23cfd"};
            return View(bookViewModel);
        }
        public ActionResult ListFromIndex(string keyWord, int pageIndex = 1)
        {
            commonInfo.WirteLog("ListFromIndex开始时间");
            int categoryId = 0;
            BookQueryData query = new BookQueryData();
            query.CategoryIdOne = (TypeEnum)categoryId;
            query.KeyWordOneSimp = keyWord;
            query.KeyWordOneOrig = FontTypeConvert.VBConvertOrig(keyWord);
            commonInfo.WirteLog("ListFromIndex简繁转换");
            int count = 0;
            List<TB_Book> bookList = _bookService.GetBookListByQueryData(query, out count, pageIndex, pageSize).ToList();
            commonInfo.WirteLog("ListFromIndex书籍List");
            List<BookViewModel> models = GetQueryList(bookList);
            commonInfo.WirteLog("ListFromIndex列表分页");
            s_keyWord = keyWord;
            s_categoryId = 0;
            ArticleListModel articleModel = new ArticleListModel();
            articleModel.CategoryId = 0;
            articleModel.KeyFan = FontTypeConvert.VBConvertOrig(keyWord);
            articleModel.KeyJian = FontTypeConvert.VBConvertSimp(keyWord);
            articleModel.KeyWord = keyWord;
            articleModel.BooViewModelList = models;
            articleModel.CurrentPageIndex = pageIndex;
            articleModel.Count = count;
            articleModel.PageSize = pageSize;
            articleModel.PageTotalCount = count % pageSize == 0 ? count / pageSize : (int)Math.Ceiling((double)(count / pageSize))+1;
            commonInfo.WirteLog("ListFromIndex列表结束");
            return View("List", articleModel);
        }
        public ActionResult List(string keyWord="", int? pageIndex = 1, int? categoryId = 0)
        {
            commonInfo.WirteLog("List开始时间");
            //  var keyWord = Request.Form["title"];
            categoryId = categoryId == null ? 0 : categoryId;   //书的目录id
            if (categoryId != 0)
            {
                s_categoryId = Convert.ToInt32(categoryId);
            }
            if (categoryId == 0 && s_categoryId != 0 && !string.IsNullOrEmpty(keyWord))
            {
                categoryId = s_categoryId;
            }
            s_categoryId = Convert.ToInt32(categoryId);
            pageIndex = (pageIndex == 0 || pageIndex == null) ? 1 : pageIndex;
            BookQueryData query = new BookQueryData();
            query.CategoryIdOne = (TypeEnum)0;
            query.KeyWordOneSimp = keyWord;
            query.KeyWordOneOrig = FontTypeConvert.VBConvertOrig(keyWord);
            commonInfo.WirteLog("List简繁转换");
            int count = 0;
            List<TB_Book> bookList = _bookService.GetBookListByQueryData(query, out count, Convert.ToInt32(pageIndex), pageSize, Convert.ToInt32(categoryId)).ToList();
            commonInfo.WirteLog("List书籍List");
            List<BookViewModel> models = GetQueryList(bookList);
            commonInfo.WirteLog("List列表分页");
            s_keyWord = keyWord;
            s_categoryId = 0;
            ArticleListModel articleModel = new ArticleListModel();
            articleModel.CategoryId = Convert.ToInt32(categoryId);
            articleModel.KeyFan = FontTypeConvert.VBConvertOrig(keyWord);
            articleModel.KeyJian = FontTypeConvert.VBConvertSimp(keyWord);
            articleModel.KeyWord = keyWord;
            articleModel.BooViewModelList = models;
            articleModel.Count = count;
            articleModel.PageSize = pageSize;
            articleModel.PageTotalCount = count % pageSize == 0 ? count / pageSize : (int)Math.Ceiling((double)(count / pageSize))+1;
            articleModel.CurrentPageIndex = pageIndex > articleModel.PageTotalCount ? 1 : Convert.ToInt32(pageIndex);
            return View("List", articleModel);
        }
        public ActionResult MuchList(int pageIndex = 1)
        {
            return View();
        }
        public ActionResult MuchListTable(int pageIndex = 1)
        {
            commonInfo.WirteLog("MuchListTable开始");
            #region 查询条件
            ///0:全部 1：书名 2：责任者 3：出版者 4：朝代 5：其他名称
            int categoryId = Convert.ToInt32(Request["categoryId1"]);
            string title = Request["title1"];
            int categoryId2 = Convert.ToInt32(Request["categoryId2"]);
            string title2 = Request["title2"];
            int searchType = Convert.ToInt32(Request["searchType"]);  //1:与 2：或
            #endregion
            commonInfo.WirteLog("MuchListTable查询条件结束");
            BookQueryData query = new BookQueryData();
            query.CategoryIdOne = (TypeEnum)categoryId;
            query.KeyWordOneSimp = FontTypeConvert.VBConvertSimp(title);
            query.KeyWordOneOrig = FontTypeConvert.VBConvertOrig(title);
            query.CategoryIdTwo = (TypeEnum)categoryId2;
            query.KeyWordTwoSimp = FontTypeConvert.VBConvertSimp(title2);
            query.KeyWordTwoOrig = FontTypeConvert.VBConvertOrig(title2);
            query.SearchType = searchType;
            int count = 0;
            commonInfo.WirteLog("MuchListTable简繁转换");
            List<TB_Book> bookList = _bookService.GetBookListByQueryData(query, out count, Convert.ToInt32(pageIndex), pageSize).ToList();
            commonInfo.WirteLog("MuchListTable书籍List");
            List<BookViewModel> models = GetQueryList(bookList);
            MuchListTableModel listModel = new MuchListTableModel();
            listModel.BooViewModelList = models;
            listModel.Count = count;
            listModel.CurrentPageIndex = pageIndex;
            listModel.KeyFan = FontTypeConvert.VBConvertOrig(title);
            listModel.KeyFan2 = FontTypeConvert.VBConvertOrig(title2);
            listModel.KeyJian = FontTypeConvert.VBConvertSimp(title);
            listModel.KeyJian2 = FontTypeConvert.VBConvertSimp(title2);
            listModel.KeyWord = title;
            listModel.KeyWord2 = title2;
            listModel.PageSize = pageSize;
            listModel.PageTotalCount = count % pageSize == 0 ? count / pageSize : (int)Math.Ceiling((double)(count / pageSize))+1;
            listModel.Query = categoryId + "," + title + "," + categoryId2 + "," + title2;
            listModel.SearchType = searchType;
            commonInfo.WirteLog("MuchListTable分页数据");
            return View(listModel);
        }
        public ActionResult MuchListTablePage(string keyWord1, string keyWord2, int categoryId1 = 0, int categoryId2 = 0, int searchType = 1, int pageIndex = 1)
        {
            BookQueryData query = new BookQueryData();
            query.CategoryIdOne = (TypeEnum)categoryId1;
            query.KeyWordOneSimp = FontTypeConvert.VBConvertSimp(keyWord1);
            query.KeyWordOneOrig = FontTypeConvert.VBConvertOrig(keyWord1);
            query.CategoryIdTwo = (TypeEnum)categoryId2;
            query.KeyWordTwoSimp = FontTypeConvert.VBConvertSimp(keyWord2);
            query.KeyWordTwoOrig = FontTypeConvert.VBConvertOrig(keyWord2);
            query.SearchType = searchType;
            int count = 0;
            //IQueryable<TB_Book> bookList = _bookService.GetBookListByQueryData(query, out count, pageIndex, pageSize);
            List<TB_Book> bookList = _bookService.GetBookListByQueryData(query, out count, pageIndex, pageSize).ToList();
            commonInfo.WirteLog("MuchListTablePage书籍List");
            List<BookViewModel> models = GetQueryList(bookList);
            commonInfo.WirteLog("MuchListTablePage循环赋值");
            MuchListTableModel listModel = new MuchListTableModel();
            listModel.BooViewModelList = models;
            listModel.Count = count;
            listModel.CurrentPageIndex = pageIndex;
            listModel.KeyFan = FontTypeConvert.VBConvertOrig(keyWord1);
            listModel.KeyFan2 = FontTypeConvert.VBConvertOrig(keyWord2);
            listModel.KeyJian = FontTypeConvert.VBConvertSimp(keyWord1);
            listModel.KeyJian2 = FontTypeConvert.VBConvertSimp(keyWord2);
            listModel.KeyWord = keyWord1;
            listModel.KeyWord2 = keyWord2;
            listModel.PageSize = pageSize;
            listModel.PageTotalCount = count % pageSize == 0 ? count / pageSize : (int)Math.Ceiling((double)(count / pageSize))+1;
            listModel.Query = categoryId1 + "," + keyWord1 + "," + categoryId2 + "," + keyWord2;
            listModel.SearchType = searchType;
            commonInfo.WirteLog("MuchListTablePage返回页面");
            return View("MuchListTable", listModel);
        }
    }
}
