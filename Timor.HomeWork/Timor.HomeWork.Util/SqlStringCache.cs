using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timor.HomeWork.Util
{
    public static class SqlStringCache<T>
    {
        private static string InsertSql { get; set; }
        private static string DelteSql { get; set; }
        private static string QuerySql { get; set; }
        private static string UpdateSql { get; set; }
        static SqlStringCache()
        {
            Type type = typeof(T);
            InsertSql = $"insert into [{type.GetTableMappingName()}] ({string.Join(",", type.GetNoKeyProperties().Select(i => $"[{i.GetPropertyMappingName()}]"))}) values ({string.Join(",", type.GetNoKeyProperties().Select(i => $"@{i.Name}"))})";
            DelteSql = $"delete from [{type.GetTableMappingName()}] where id=";
            QuerySql = $"select { string.Join(",", type.GetProperties().Select(i => $"[{i.GetPropertyMappingName()}]"))} from [{type.GetTableMappingName()}]";
            UpdateSql = $"Update  [{type.GetTableMappingName()}] set {string.Join(",", type.GetNoKeyProperties().Select(i => $"[{i.GetPropertyMappingName()}]=@{i.GetPropertyMappingName()}"))} where id=";
        }
        public static string GetInsertSql()
        {
            return InsertSql;
        }
        public static string GetDelteSql()
        {
            return DelteSql;
        }
        public static string GetQuerySql()
        {
            return QuerySql;
        }
        public static string GetUpdateSql()
        {
            return UpdateSql;
        }
    }
}
