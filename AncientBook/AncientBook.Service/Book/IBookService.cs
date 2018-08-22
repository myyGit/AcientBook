using AncientBook.Data;
using AncientBook.Data.QueryBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientBook.Service
{
    public interface IBookService
    {
        IQueryable<TB_Book> GetBookList();
        IQueryable<TB_Book> GetBookListByQueryData(BookQueryData queryData, out int count, int pageIndex = 0, int pageSize = 0, int categoryId = 0);
        TB_Book GetBookByHGUID(string hguid);
        IQueryable<TB_Category> GetCategoryList();
        List<TB_Category> GetCategoryAllList();
        void InsertBookInfo(List<TB_Book> book);
    }
}
