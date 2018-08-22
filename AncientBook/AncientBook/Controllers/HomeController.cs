using AncientBook.Data;
using AncientBook.Service;
using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AncientBook.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ICmsStatesService _cmsService;
        public HomeController(IBookService bookService, ICmsStatesService cmsService)
        {
            this._bookService = bookService;
            this._cmsService = cmsService;
        }

        public ActionResult Index()
        {
            //IQueryable<TB_Book> bookList = _bookService.GetBookList().OrderBy(p=>p.CreateTime).Skip(22000).Take(10000);
            //List<TB_CmsStatus> cmsList = new List<TB_CmsStatus>();
            //foreach (var item in bookList)
            //{
            //    TB_CmsStatus cms = new TB_CmsStatus();
            //    cms.CmsUid = item.BookUid;
            //    cms.StatusId = 1;
            //    cmsList.Add(cms);
            //}
            //_cmsService.InsertInfo(cmsList);
            //List<TB_Book> bookList = new List<TB_Book>();
            //for (int i = 0; i < 3; i++)
            //{
            //    TB_Book book = new TB_Book();
            //    book.BookUid = Guid.NewGuid();
            //    book.CategoryId = 10;
            //    book.CreateTime = DateTime.Now;
            //    book.Dynasty = "晚唐";
            //    book.FromAF49 = "啦啦啦啦";
            //    book.FromBF49 = "啊啊啊啊啊";
            //    book.Functionary = "一百卷 年表一卷";
            //    book.ImageUris = "/Uploads/" + book.BookUid;
            //    book.Notice = "ss";
            //    book.Publisher = "李白";
            //    book.Title = "你猜";
            //    book.Title2 = "你猜";
            //    book.Volume = "四十卷 你猜 校勘記一卷";
            //    book.Version = "清光緒刻本";

            //    bookList.Add(book);
            //}
            //_bookService.InsertBookInfo(bookList);
            //int a = 1;
            return View();
        }
        public ActionResult CategoryTree() 
        {
            var categoryList = _bookService.GetCategoryList();
            var aa = _bookService.GetCategoryAllList();
            var a =   categoryList.OrderBy(p => p.CategoryId).ToList();
            var modelList = Totree(a);
            Session["treeList"] = modelList;
            return View();
        }
        public List<TB_Category> Totree(List<TB_Category> list)
        {
            var treeList = list;
            var dic = new Dictionary<int, TB_Category>(treeList.Count);
            foreach (var tree in treeList)
            {
                dic.Add(tree.CategoryId, tree);
            }
            foreach (var tree in dic.Values)
            {
                if (dic.ContainsKey(tree.PId))
                {
                    if (dic[tree.PId].Children == null)
                    {
                        dic[tree.PId].Children = new List<TB_Category>();
                    }
                    dic[tree.PId].Children.Add(tree);
                }
            }
            return dic.Values.Where(t => t.PId == 0).ToList();
        }
    }
}
