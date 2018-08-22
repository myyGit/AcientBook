using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientBook.Data
{
    public interface IRepositoryUser<T> where T : BaseEntity
    {
        T GetById(object id);
        void Insert(T entity);
      //  int InsertReturnID(T entity);
        void Update(T entity);
        void Update(IEnumerable<T> entities);
        void Delete(T entity);
        void Delete(IEnumerable<T> entities);
        IQueryable<T> Table { get; }
        void Insert(IEnumerable<T> entities);
        /// <summary>
        /// 批量插入，sqlConnection方法
        /// </summary>
        /// <param name="entities"></param>
        void InsertMany(IEnumerable<T> entities);
        /// <summary>
        /// 暂未实现
        /// </summary>
        void UpdateMany();
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="entities"></param>
        void DeleteMany(IEnumerable<T> entities);
    }
}
