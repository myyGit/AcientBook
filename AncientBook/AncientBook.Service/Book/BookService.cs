using AncientBook.Data;
using AncientBook.Data.QueryBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientBook.Service
{
    public class BookService : IBookService
    {
        private readonly IRepositoryBook<TB_Book> _bookRep;
        private readonly IRepositoryBook<TB_Category> _categoryRep;
        
        public BookService(IRepositoryBook<TB_Book> bookRep, IRepositoryBook<TB_Category> categoryRep)
        {
            this._bookRep = bookRep;
            this._categoryRep = categoryRep;
        }
        public IQueryable<TB_Book> GetBookList()
        {
            return _bookRep.Table.Where(p => p.IsDelete == 1);
        }


        public IQueryable<TB_Book> GetBookByTypeOr(IQueryable<TB_Book> bookList, BookQueryData data)
        {
            string dataTwoSimp = data.KeyWordTwoSimp;
            string dataTwoOrig = data.KeyWordTwoOrig;
            string dataOneSimp = data.KeyWordOneSimp;
            string dataOneOrig = data.KeyWordOneOrig;
            switch (data.CategoryIdOne)
            {
                case TypeEnum.All:
                    if (data.CategoryIdTwo == TypeEnum.All)
                    {
                        bookList = bookList.Where(tt => tt.Title.Contains(dataOneSimp) || tt.Title.Contains(dataOneOrig) 
                                   || tt.Title.Contains(dataTwoOrig) || tt.Title.Contains(dataTwoSimp)
                                   || tt.Title2.Contains(dataOneSimp) || tt.Title2.Contains(dataOneOrig) 
                                   || tt.Title2.Contains(dataTwoOrig) || tt.Title2.Contains(dataTwoSimp)
                                   || tt.Publisher.Contains(dataOneSimp) || tt.Publisher.Contains(dataOneOrig) 
                                   || tt.Publisher.Contains(dataTwoOrig) || tt.Publisher.Contains(dataTwoSimp)
                                   || tt.Dynasty.Contains(dataOneSimp) || tt.Dynasty.Contains(dataOneOrig) 
                                   || tt.Dynasty.Contains(dataTwoOrig) || tt.Dynasty.Contains(dataTwoSimp)
                                   || tt.Functionary.Contains(dataOneSimp) || tt.Functionary.Contains(dataOneOrig)
                                   || tt.Functionary.Contains(dataTwoOrig) || tt.Functionary.Contains(dataTwoSimp));
                    }
                    else if (data.CategoryIdTwo == TypeEnum.BookName)
                    {
                        bookList = bookList.Where(tt => tt.Title.Contains(dataOneSimp) || tt.Title.Contains(dataOneOrig)
                                   || tt.Title2.Contains(dataOneSimp) || tt.Title2.Contains(dataOneOrig) 
                                   || tt.Title.Contains(dataTwoOrig) || tt.Title.Contains(dataTwoSimp)
                                   || tt.Publisher.Contains(dataOneSimp) || tt.Publisher.Contains(dataOneOrig)
                                   || tt.Dynasty.Contains(dataOneSimp) || tt.Dynasty.Contains(dataOneOrig)
                                   || tt.Functionary.Contains(dataOneSimp) || tt.Functionary.Contains(dataOneOrig));
                    }
                    else if (data.CategoryIdTwo == TypeEnum.Auther)
                    {
                        bookList = bookList.Where(tt => tt.Title.Contains(dataOneSimp) || tt.Title.Contains(dataOneOrig)
                                   || tt.Title2.Contains(dataOneSimp) || tt.Title2.Contains(dataOneOrig)
                                   || tt.Publisher.Contains(dataOneSimp) || tt.Publisher.Contains(dataOneOrig)
                                   || tt.Dynasty.Contains(dataOneSimp) || tt.Dynasty.Contains(dataOneOrig)
                                   || tt.Functionary.Contains(dataTwoOrig) || tt.Functionary.Contains(dataTwoSimp)
                                   || tt.Functionary.Contains(dataOneSimp) || tt.Functionary.Contains(dataOneOrig));
                    }
                    else if (data.CategoryIdTwo == TypeEnum.Publisher)
                    {
                        bookList = bookList.Where(tt => tt.Title.Contains(dataOneSimp) || tt.Title.Contains(dataOneOrig)
                                   || tt.Title2.Contains(dataOneSimp) || tt.Title2.Contains(dataOneOrig)
                                   || tt.Publisher.Contains(dataTwoOrig) || tt.Publisher.Contains(dataTwoSimp)
                                   || tt.Publisher.Contains(dataOneSimp) || tt.Publisher.Contains(dataOneOrig)
                                   || tt.Dynasty.Contains(dataOneSimp) || tt.Dynasty.Contains(dataOneOrig)
                                   || tt.Functionary.Contains(dataOneSimp) || tt.Functionary.Contains(dataOneOrig));
                    }
                    else if (data.CategoryIdTwo == TypeEnum.Dynasty)
                    {
                        bookList = bookList.Where(tt => tt.Title.Contains(dataOneSimp) || tt.Title.Contains(dataOneOrig)
                                   || tt.Title2.Contains(dataOneSimp) || tt.Title2.Contains(dataOneOrig) 
                                   || tt.Publisher.Contains(dataOneSimp) || tt.Publisher.Contains(dataOneOrig)
                                   || tt.Dynasty.Contains(dataTwoOrig) || tt.Dynasty.Contains(dataTwoSimp)
                                   || tt.Dynasty.Contains(dataOneSimp) || tt.Dynasty.Contains(dataOneOrig)
                                   || tt.Functionary.Contains(dataOneSimp) || tt.Functionary.Contains(dataOneOrig));
                    }
                    else if (data.CategoryIdTwo == TypeEnum.OtherName)
                    {
                        bookList = bookList.Where(tt => tt.Title.Contains(dataOneSimp) || tt.Title.Contains(dataOneOrig)
                                   || tt.Title2.Contains(dataOneSimp) || tt.Title2.Contains(dataOneOrig)
                                   || tt.Title2.Contains(dataTwoOrig) || tt.Title2.Contains(dataTwoSimp)
                                   || tt.Publisher.Contains(dataOneSimp) || tt.Publisher.Contains(dataOneOrig)
                                   || tt.Dynasty.Contains(dataOneSimp) || tt.Dynasty.Contains(dataOneOrig)
                                   || tt.Functionary.Contains(dataOneSimp) || tt.Functionary.Contains(dataOneOrig));
                    }
                    break;
                case TypeEnum.BookName:
                    #region 查询
                    if (data.CategoryIdTwo == TypeEnum.All)
                    {
                        bookList = bookList.Where(tt => tt.Title.Contains(dataTwoOrig) || tt.Title.Contains(dataTwoSimp)
                                   || tt.Title.Contains(dataOneOrig) || tt.Title.Contains(dataOneSimp)
                                   || tt.Title2.Contains(dataTwoOrig) || tt.Title2.Contains(dataTwoSimp)
                                   || tt.Publisher.Contains(dataTwoOrig) || tt.Publisher.Contains(dataTwoSimp)
                                   || tt.Dynasty.Contains(dataTwoOrig) || tt.Dynasty.Contains(dataTwoSimp)
                                   || tt.Functionary.Contains(dataTwoOrig) || tt.Functionary.Contains(dataTwoSimp));
                    }
                    else if (data.CategoryIdTwo == TypeEnum.BookName)
                    {
                        bookList = bookList.Where(tt => tt.Title.Contains(dataOneSimp) || tt.Title.Contains(dataOneOrig)
                                                        || tt.Title.Contains(dataTwoOrig) || tt.Title.Contains(dataTwoSimp));
                    }
                    else if (data.CategoryIdTwo == TypeEnum.Auther)
                    {
                        bookList = bookList.Where(tt => tt.Title.Contains(dataOneOrig) || tt.Title.Contains(dataOneSimp)
                                || tt.Functionary.Contains(dataTwoOrig) || tt.Functionary.Contains(dataTwoSimp));
                    }
                    else if (data.CategoryIdTwo == TypeEnum.Publisher)
                    {
                        bookList = bookList.Where(tt => tt.Title.Contains(dataOneSimp) || tt.Title.Contains(dataOneOrig)
                                   || tt.Publisher.Contains(dataTwoOrig) || tt.Publisher.Contains(dataTwoSimp));
                    }
                    else if (data.CategoryIdTwo == TypeEnum.Dynasty)
                    {
                        bookList = bookList.Where(tt => tt.Title.Contains(dataOneSimp) || tt.Title.Contains(dataOneOrig)
                                   || tt.Dynasty.Contains(dataTwoOrig) || tt.Dynasty.Contains(dataTwoSimp));
                    }
                    else if (data.CategoryIdTwo == TypeEnum.OtherName)
                    {
                        bookList = bookList.Where(tt => tt.Title.Contains(dataOneSimp) || tt.Title.Contains(dataOneOrig)
                                   || tt.Title2.Contains(dataTwoOrig) || tt.Title2.Contains(dataTwoSimp));
                    }
                    #endregion
                    
                    break;
                case TypeEnum.Auther:
                    #region 查询
                    if (data.CategoryIdTwo == TypeEnum.All)
                    {
                        bookList = bookList.Where(tt => tt.Title.Contains(dataOneOrig) || tt.Title.Contains(dataOneSimp)
                                   || tt.Title.Contains(dataTwoOrig) || tt.Title.Contains(dataTwoSimp)
                                   || tt.Title2.Contains(dataTwoOrig) || tt.Title2.Contains(dataTwoSimp)
                                   || tt.Publisher.Contains(dataTwoOrig) || tt.Publisher.Contains(dataTwoSimp)
                                   || tt.Dynasty.Contains(dataTwoOrig) || tt.Dynasty.Contains(dataTwoSimp)
                                   || tt.Functionary.Contains(dataTwoOrig) || tt.Functionary.Contains(dataTwoSimp));
                    }
                    else if (data.CategoryIdTwo == TypeEnum.BookName)
                    {
                        bookList = bookList.Where(tt => tt.Functionary.Contains(dataOneSimp) || tt.Functionary.Contains(dataOneOrig)
                                                        || tt.Title.Contains(dataTwoOrig) || tt.Title.Contains(dataTwoSimp));
                    }
                    else if (data.CategoryIdTwo == TypeEnum.Auther)
                    {
                        bookList = bookList.Where(tt => tt.Functionary.Contains(dataOneOrig) || tt.Functionary.Contains(dataOneSimp)
                                || tt.Functionary.Contains(dataTwoOrig) || tt.Functionary.Contains(dataTwoSimp));
                    }
                    else if (data.CategoryIdTwo == TypeEnum.Publisher)
                    {
                        bookList = bookList.Where(tt => tt.Functionary.Contains(dataOneSimp) || tt.Functionary.Contains(dataOneOrig)
                                   || tt.Publisher.Contains(dataTwoOrig) || tt.Publisher.Contains(dataTwoSimp));
                    }
                    else if (data.CategoryIdTwo == TypeEnum.Dynasty)
                    {
                        bookList = bookList.Where(tt => tt.Functionary.Contains(dataOneSimp) || tt.Functionary.Contains(dataOneOrig)
                                   || tt.Dynasty.Contains(dataTwoOrig) || tt.Dynasty.Contains(dataTwoSimp));
                    }
                    else if (data.CategoryIdTwo == TypeEnum.OtherName)
                    {
                        bookList = bookList.Where(tt => tt.Functionary.Contains(dataOneSimp) || tt.Functionary.Contains(dataOneOrig)
                                   || tt.Title2.Contains(dataTwoOrig) || tt.Title2.Contains(dataTwoSimp));
                    }
                    #endregion
                    
                    break;
                case TypeEnum.Publisher:
                    bookList = bookList.Where(p => p.Publisher.Contains(dataOneOrig) || p.Publisher.Contains(dataOneSimp));
                    #region 查询
                    if (data.CategoryIdTwo == TypeEnum.All)
                    {
                        bookList = bookList.Where(tt => tt.Title.Contains(dataTwoOrig) || tt.Title.Contains(dataTwoSimp)
                                   || tt.Title2.Contains(dataTwoOrig) || tt.Title2.Contains(dataTwoSimp)
                                   || tt.Publisher.Contains(dataTwoOrig) || tt.Publisher.Contains(dataTwoSimp)
                                   || tt.Dynasty.Contains(dataTwoOrig) || tt.Dynasty.Contains(dataTwoSimp)
                                   || tt.Functionary.Contains(dataTwoOrig) || tt.Functionary.Contains(dataTwoSimp)
                                   || tt.Publisher.Contains(dataOneOrig) || tt.Publisher.Contains(dataOneSimp));
                    }
                    else if (data.CategoryIdTwo == TypeEnum.BookName)
                    {
                        bookList = bookList.Where(tt => tt.Publisher.Contains(dataOneSimp) || tt.Publisher.Contains(dataOneOrig)
                                                        || tt.Title.Contains(dataTwoOrig) || tt.Title.Contains(dataTwoSimp));
                    }
                    else if (data.CategoryIdTwo == TypeEnum.Auther)
                    {
                        bookList = bookList.Where(tt => tt.Publisher.Contains(dataOneOrig) || tt.Publisher.Contains(dataOneSimp)
                                || tt.Functionary.Contains(dataTwoOrig) || tt.Functionary.Contains(dataTwoSimp));
                    }
                    else if (data.CategoryIdTwo == TypeEnum.Publisher)
                    {
                        bookList = bookList.Where(tt => tt.Publisher.Contains(dataOneSimp) || tt.Publisher.Contains(dataOneOrig)
                                   || tt.Publisher.Contains(dataTwoOrig) || tt.Publisher.Contains(dataTwoSimp));
                    }
                    else if (data.CategoryIdTwo == TypeEnum.Dynasty)
                    {
                        bookList = bookList.Where(tt => tt.Publisher.Contains(dataOneSimp) || tt.Publisher.Contains(dataOneOrig)
                                   || tt.Dynasty.Contains(dataTwoOrig) || tt.Dynasty.Contains(dataTwoSimp));
                    }
                    else if (data.CategoryIdTwo == TypeEnum.OtherName)
                    {
                        bookList = bookList.Where(tt => tt.Publisher.Contains(dataOneSimp) || tt.Publisher.Contains(dataOneOrig)
                                   || tt.Title2.Contains(dataTwoOrig) || tt.Title2.Contains(dataTwoSimp));
                    }
                    #endregion
                    
                    break;
                case TypeEnum.Dynasty:
                    //bookList = bookList.Where(p => p.Dynasty.Contains(dataOneOrig) || p.Dynasty.Contains(dataOneSimp));
                    #region 查询
                    if (data.CategoryIdTwo == TypeEnum.All)
                    {
                        bookList = bookList.Where(tt => tt.Title.Contains(dataTwoOrig) || tt.Title.Contains(dataTwoSimp)
                                   || tt.Title2.Contains(dataTwoOrig) || tt.Title2.Contains(dataTwoSimp)
                                   || tt.Publisher.Contains(dataTwoOrig) || tt.Publisher.Contains(dataTwoSimp)
                                   || tt.Dynasty.Contains(dataTwoOrig) || tt.Dynasty.Contains(dataTwoSimp)
                                   || tt.Functionary.Contains(dataTwoOrig) || tt.Functionary.Contains(dataTwoSimp)
                                   || tt.Dynasty.Contains(dataOneOrig) || tt.Dynasty.Contains(dataOneSimp));
                    }
                    else if (data.CategoryIdTwo == TypeEnum.BookName)
                    {
                        bookList = bookList.Where(tt => tt.Dynasty.Contains(dataOneSimp) || tt.Dynasty.Contains(dataOneOrig)
                                                        || tt.Title.Contains(dataTwoOrig) || tt.Title.Contains(dataTwoSimp));
                    }
                    else if (data.CategoryIdTwo == TypeEnum.Auther)
                    {
                        bookList = bookList.Where(tt => tt.Dynasty.Contains(dataOneOrig) || tt.Dynasty.Contains(dataOneSimp)
                                || tt.Functionary.Contains(dataTwoOrig) || tt.Functionary.Contains(dataTwoSimp));
                    }
                    else if (data.CategoryIdTwo == TypeEnum.Publisher)
                    {
                        bookList = bookList.Where(tt => tt.Dynasty.Contains(dataOneSimp) || tt.Dynasty.Contains(dataOneOrig)
                                   || tt.Publisher.Contains(dataTwoOrig) || tt.Publisher.Contains(dataTwoSimp));
                    }
                    else if (data.CategoryIdTwo == TypeEnum.Dynasty)
                    {
                        bookList = bookList.Where(tt => tt.Dynasty.Contains(dataOneSimp) || tt.Dynasty.Contains(dataOneOrig)
                                   || tt.Dynasty.Contains(dataTwoOrig) || tt.Dynasty.Contains(dataTwoSimp));
                    }
                    else if (data.CategoryIdTwo == TypeEnum.OtherName)
                    {
                        bookList = bookList.Where(tt => tt.Dynasty.Contains(dataOneSimp) || tt.Dynasty.Contains(dataOneOrig)
                                   || tt.Title2.Contains(dataTwoOrig) || tt.Title2.Contains(dataTwoSimp));
                    }
                    #endregion
                    break;
                case TypeEnum.OtherName:
                    //bookList = bookList.Where(p => p.Title2.Contains(dataOneOrig) || p.Title2.Contains(dataOneSimp));
                    #region 查询
                    if (data.CategoryIdTwo == TypeEnum.All)
                    {
                        bookList = bookList.Where(tt => tt.Title.Contains(dataTwoOrig) || tt.Title.Contains(dataTwoSimp)
                                   || tt.Title2.Contains(dataTwoOrig) || tt.Title2.Contains(dataTwoSimp)
                                   || tt.Publisher.Contains(dataTwoOrig) || tt.Publisher.Contains(dataTwoSimp)
                                   || tt.Dynasty.Contains(dataTwoOrig) || tt.Dynasty.Contains(dataTwoSimp)
                                   || tt.Functionary.Contains(dataTwoOrig) || tt.Functionary.Contains(dataTwoSimp)
                                   || tt.Title2.Contains(dataOneOrig) || tt.Title2.Contains(dataOneSimp));
                    }
                    else if (data.CategoryIdTwo == TypeEnum.BookName)
                    {
                        bookList = bookList.Where(tt => tt.Title2.Contains(dataOneSimp) || tt.Title2.Contains(dataOneOrig)
                                                        || tt.Title.Contains(dataTwoOrig) || tt.Title.Contains(dataTwoSimp));
                    }
                    else if (data.CategoryIdTwo == TypeEnum.Auther)
                    {
                        bookList = bookList.Where(tt => tt.Title2.Contains(dataOneOrig) || tt.Title2.Contains(dataOneSimp)
                                || tt.Functionary.Contains(dataTwoOrig) || tt.Functionary.Contains(dataTwoSimp));
                    }
                    else if (data.CategoryIdTwo == TypeEnum.Publisher)
                    {
                        bookList = bookList.Where(tt => tt.Title2.Contains(dataOneSimp) || tt.Title2.Contains(dataOneOrig)
                                   || tt.Publisher.Contains(dataTwoOrig) || tt.Publisher.Contains(dataTwoSimp));
                    }
                    else if (data.CategoryIdTwo == TypeEnum.Dynasty)
                    {
                        bookList = bookList.Where(tt => tt.Title2.Contains(dataOneSimp) || tt.Title2.Contains(dataOneOrig)
                                   || tt.Dynasty.Contains(dataTwoOrig) || tt.Dynasty.Contains(dataTwoSimp));
                    }
                    else if (data.CategoryIdTwo == TypeEnum.OtherName)
                    {
                        bookList = bookList.Where(tt => tt.Title2.Contains(dataOneSimp) || tt.Title2.Contains(dataOneOrig)
                                   || tt.Title2.Contains(dataTwoOrig) || tt.Title2.Contains(dataTwoSimp));
                    }
                    #endregion
                    break;
                default:
                    break;
            }
            return bookList;
        }

        public IQueryable<TB_Book> GetBookByType(IQueryable<TB_Book> bookList, TypeEnum type, string jian, string fan)
        {
            switch (type)
            {
                case TypeEnum.All:
                    bookList = bookList.Where(tt => tt.Title.Contains(jian) || tt.Title.Contains(fan)
                                   || tt.Title2.Contains(jian) || tt.Title2.Contains(fan)
                                   || tt.Publisher.Contains(jian) || tt.Publisher.Contains(fan)
                                   || tt.Dynasty.Contains(jian) || tt.Dynasty.Contains(fan)
                                   || tt.Functionary.Contains(jian) || tt.Functionary.Contains(fan));
                    break;
                case TypeEnum.BookName:
                    bookList = bookList.Where(p => p.Title.Contains(fan) || p.Title.Contains(jian));
                    break;
                case TypeEnum.Auther:
                    bookList = bookList.Where(p => p.Functionary.Contains(fan) || p.Functionary.Contains(jian));
                    break;
                case TypeEnum.Publisher:
                    bookList = bookList.Where(p => p.Publisher.Contains(fan) || p.Publisher.Contains(jian));
                    break;
                case TypeEnum.Dynasty:
                    bookList = bookList.Where(p => p.Dynasty.Contains(fan) || p.Dynasty.Contains(jian));
                    break;
                case TypeEnum.OtherName:
                    bookList = bookList.Where(p => p.Title2.Contains(fan) || p.Title2.Contains(jian));
                    break;
                default:
                    break;
            }
            return bookList;
        }
        public IQueryable<TB_Book> GetBookListByQueryData(BookQueryData queryData, out int count, int pageIndex = 0, int pageSize = 0, int categoryId = 0)
        {
            Common.LogFile.WriteLogNewInfo("Service查之前");
            var bookList = _bookRep.Table.Where(p => p.IsDelete == 1);
            Common.LogFile.WriteLogNewInfo("Service查之后");
            if (categoryId != 0 && categoryId != -1)
            {
                bookList = bookList.Where(p => p.CategoryId == categoryId);
            }
            Common.LogFile.WriteLogNewInfo("Service查目录");
            if (!string.IsNullOrEmpty(queryData.KeyWordOneSimp) || !string.IsNullOrEmpty(queryData.KeyWordTwoSimp))
            {
                bookList = GetBookByType(bookList, queryData.CategoryIdOne, queryData.KeyWordOneSimp, queryData.KeyWordOneOrig);
            }
            Common.LogFile.WriteLogNewInfo("Service查简繁1");
            if (!string.IsNullOrEmpty(queryData.KeyWordTwoSimp))
            {
                #region 与
                if (queryData.SearchType == 1)
                {
                    bookList = GetBookByType(bookList, queryData.CategoryIdTwo, queryData.KeyWordTwoSimp, queryData.KeyWordTwoOrig);
                    count = bookList.Count();
                    bookList = bookList.OrderByDescending(p => p.CreateTime);
                    if (pageIndex != 0 && pageSize != 0)
                    {
                        bookList = bookList.OrderByDescending(p => p.CreateTime).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    }

                    return bookList;
                }
                #endregion
                #region 或
                else if (queryData.SearchType == 2)
                {
                    Common.LogFile.WriteLogAddInfo("Service的或开始");
                    var bookList2 = _bookRep.Table.Where(p => p.IsDelete == 1);
                    bookList2 = GetBookByTypeOr(bookList2, queryData);
                    Common.LogFile.WriteLogAddInfo("Service的得到");
                    //var newBookList = bookList.Union(bookList2);
                    //bookList2.Distinct(new TB_BookCompareByBookUid());
                    count = bookList2.Count();
                    Common.LogFile.WriteLogAddInfo("Service获取去重数据");
                    bookList2 = bookList2.OrderByDescending(p => p.CreateTime);
                    if (pageIndex != 0 && pageSize != 0)
                    {
                        bookList2 = bookList2.OrderByDescending(p => p.CreateTime).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    }
                    Common.LogFile.WriteLogAddInfo("Service返回");
                    return bookList2;
                }
                count = bookList.Count();
                bookList = bookList.OrderByDescending(p => p.CreateTime);
                if (pageIndex != 0 && pageSize != 0)
                {
                    bookList = bookList.OrderByDescending(p => p.CreateTime).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                }
                return bookList;
                #endregion
            }
            count = bookList.Count();
            bookList = bookList.OrderByDescending(p => p.CreateTime);
            if (pageIndex != 0 && pageSize != 0)
            {
                bookList = bookList.OrderByDescending(p => p.CreateTime).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }
            return bookList;
        }

        public TB_Book GetBookByHGUID(string hguid)
        {
            Guid bookHguid = new Guid(hguid);
            return _bookRep.Table.Where(p => p.BookUid == bookHguid).FirstOrDefault();
        }
        public IQueryable<TB_Category> GetCategoryList()
        {
            return _categoryRep.Table;
        }
        public List<TB_Category> GetCategoryAllList()
        {
            return _categoryRep.Table.ToList();
        }

        public void InsertBookInfo(List<TB_Book> book)
        {
            _bookRep.Insert(book);
        }
    }

    public class TB_BookCompareByBookUid : IEqualityComparer<TB_Book>
    {
        public bool Equals(TB_Book x, TB_Book y)
        {
            if (x == null || y == null)
                return false;
            if (x.BookUid == y.BookUid)
                return true;
            else
                return false;
        }

        public int GetHashCode(TB_Book obj)
        {
            if (obj == null)
                return 0;
            else
                return obj.BookUid.GetHashCode();
        }
    }
    //public class BookQueryComparer : IEqualityComparer<TB_Book>
    //{
    //    public bool Equals(TB_Book x, TB_Book y)
    //    {
    //        if (Object.ReferenceEquals(x, y)) return true;
    //        if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
    //            return false;
    //        return x.BookUid == y.BookUid;
    //    }

    //    public int GetHashCode(TB_Book student)
    //    {
    //        if (Object.ReferenceEquals(student, null)) return 0;
    //        int hashStudentName = student.BookUid == null ? 0 : student.BookUid.GetHashCode();
    //        int hashStudentCode = student.BookUid.GetHashCode();
    //        return hashStudentName ^ hashStudentCode;
    //    }
    //} 
}
