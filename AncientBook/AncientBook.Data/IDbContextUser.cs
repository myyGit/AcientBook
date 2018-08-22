using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientBook.Data
{
    public interface IDbContextUser
    {
        IDbSet<T> Set<T>() where T : BaseEntity;
        int SaveChanges();
        IList<T> ExecuteStoredProcedureList<T>(string commandText, params object[] parameters) where T : BaseEntity, new();
        IEnumerable<T> SqlQuery<T>(string sql, params object[] parameters);
        int ExecuteSqlCommand(string sql, int? timeout = null, params object[] parameters);
    }
}
