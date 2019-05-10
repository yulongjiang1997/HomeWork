using Timor.HomeWork.IService;
using Timor.HomeWork.Model;
using Timor.HomeWork.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timor.HomeWork.Service
{
    public class DbService : IDbService
    {
        private string SqlConnctionString = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T QueryById<T>(int id) where T : BaseModel
        {
            Type type = typeof(T);
            string sql = $"{SqlStringCache<T>.GetQuerySql()}where id={id}";
            return Execute(sql, i =>
            {
                SqlDataReader reader = i.ExecuteReader();
                if (reader.Read())
                {
                    return GetDate<T>(reader);
                }
                return null;
            });
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> QueryAll<T>() where T : BaseModel
        {
            Type type = typeof(T);
            string sql = SqlStringCache<T>.GetQuerySql();
            return Execute(sql, i =>
            {
                List<T> result = new List<T>();
                SqlDataReader reader = i.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(GetDate<T>(reader));
                }
                return result;
            });
        }

        /// <summary>
        /// 查询数据拼装数据用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        private T GetDate<T>(SqlDataReader reader) where T : BaseModel
        {
            Type type = typeof(T);
            var tempModel = (T)Activator.CreateInstance(type);
            foreach (var item in type.GetProperties())
            {
                item.SetValue(tempModel, reader[item.GetPropertyMappingName()] is DBNull ? null : reader[item.GetPropertyMappingName()]);
            }
            return tempModel;
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Insert<T>(T model) where T : BaseModel
        {
            Type type = model.GetType();
            string sql = SqlStringCache<T>.GetInsertSql();
            SqlParameter[] sqlParameters = type.GetNoKeyProperties().Select(i => new SqlParameter($"@{i.Name}", i.GetValue(model) == null ? DBNull.Value : i.GetValue(model))).ToArray();
            return Execute(sql, i =>
            {
                return i.ExecuteNonQuery() > 0;
            }, sqlParameters);
        }

        /// <summary>
        /// 根据ID删除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteById<T>(int id) where T : BaseModel
        {
            Type type = typeof(T);
            string sql = SqlStringCache<T>.GetDelteSql() + id;
            return Execute(sql, i =>
            {
                return i.ExecuteNonQuery() > 0;
            });
        }

        /// <summary>
        /// 根据ID修改数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateById<T>(int id, T model) where T : BaseModel
        {
            Type type = typeof(T);
            string sql = SqlStringCache<T>.GetUpdateSql() + id;
            SqlParameter[] sqlParameters = type.GetNoKeyProperties().Select(i => new SqlParameter($"@{i.Name}", i.GetValue(model) == null ? DBNull.Value : i.GetValue(model))).ToArray();

            return Execute(sql, i =>
            {
                return i.ExecuteNonQuery() > 0;
            }, sqlParameters);
        }

        /// <summary>
        /// ADO执行sql操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="func"></param>
        /// <param name="sqlParameters"></param>
        /// <returns></returns>
        private T Execute<T>(string sql, Func<SqlCommand, T> func, SqlParameter[] sqlParameters = null)
        {
            using (SqlConnection conn = new SqlConnection(SqlConnctionString))
            {
                SqlTransaction transaction = null;
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    if (sqlParameters != null)
                    {
                        cmd.Parameters.AddRange(sqlParameters);
                    }
                    return func.Invoke(cmd);
                }
                catch (Exception ex)
                {
                    if (transaction != null)
                    {
                        transaction.Rollback();
                    }
                    throw ex;
                }

            }
        }
    }
}
