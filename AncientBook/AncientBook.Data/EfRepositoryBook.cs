using AncientBook.Data.QueryBook;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientBook.Data
{
    public partial class EfRepositoryBook<T> :IRepositoryBook<T> where T:BaseEntity
    {
        private readonly IDbContextBook _bookContext;
        private IDbSet<T> _bookEntities;
        public EfRepositoryBook(IDbContextBook context)
        {
            this._bookContext = context;
        }
        public T GetById(object id)
        {
            return this.BookEntities.Find(id);
        }
        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            this.BookEntities.Add(entity);
            this._bookContext.SaveChanges();
        }
        public IEnumerable<TB_BookQuery> GetBookListByCmsStatus()
        {
            return this._bookContext.SqlQuery<TB_BookQuery>("SELECT  b.BookUid,b.CategoryId,b.CreateTime,b.Dynasty,b.FromAF49,b.FromBF49,b.Functionary,b.ImageUris,b.Notice,b.Publisher,b.Title,b.Title2,b.Version,b.Volume,cate.CategoryId,cate.CategoryName"
                                                + " FROM [dbo].[TB_Book] as b,TB_CmsStatus as c,TB_Category as cate where b.BookUid = c.CmsUid and c.StatusId = 1  and cate.CategoryId = b.CategoryId");
        }
        public IEnumerable<TB_BookQuery> GetBookListBySql(string sql)
        {
            return this._bookContext.SqlQuery<TB_BookQuery>(sql);
        }
        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            this._bookContext.SaveChanges();
        }
        public void Update(IEnumerable<T> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }
            this._bookContext.SaveChanges();
        }
        public void Delete(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            this.BookEntities.Remove(entity);

            this._bookContext.SaveChanges();
        }
        public void Delete(IEnumerable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");

            foreach (var item in entities)
            {
                this.BookEntities.Remove(item);
            }

            this._bookContext.SaveChanges();
        }
        public virtual IQueryable<T> Table
        {
            get
            {
                return this.BookEntities;
            }
        }
        public void Insert(IEnumerable<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");

            foreach (var item in entities)
            {
                this.BookEntities.Add(item);
            }
            this._bookContext.SaveChanges();
        }
        /// <summary>
        /// 批量插入，sqlConnection方法
        /// </summary>
        /// <param name="entities"></param>
        public void InsertMany(IEnumerable<T> entities) 
        {
            
        }
        /// <summary>
        /// 暂未实现
        /// </summary>
        public void UpdateMany() 
        {
        
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="entities"></param>
        public void DeleteMany(IEnumerable<T> entities) 
        {
        
        }
        private IDbSet<T> BookEntities
        {
            get
            {
                if (_bookEntities == null)
                    _bookEntities = _bookContext.Set<T>();
                return _bookEntities;
            }
        }
    }
}
