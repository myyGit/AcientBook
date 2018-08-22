using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace AncientBook.Data
{
    public interface IDbContextBook
    {
        IDbSet<T> Set<T>() where T : BaseEntity;
        int SaveChanges();
        IList<T> ExecuteStoredProcedureList<T>(string commandText, params object[] parameters) where T : BaseEntity, new();
        IEnumerable<T> SqlQuery<T>(string sql, params object[] parameters);
        int ExecuteSqlCommand(string sql, int? timeout = null, params object[] parameters);
    }
}
