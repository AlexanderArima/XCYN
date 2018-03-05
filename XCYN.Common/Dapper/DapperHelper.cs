#define Debug
//#undef Debug
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace XCYN.Common.Dapper
{
    /// <summary>
    /// 增删改查命令
    /// </summary>
    public static class DapperHelper
    {

        /// <summary>
        /// 添加操作
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static int Execute(string sql,object param = null,IDbTransaction transaction = null)
        {
            int count = 0;
            IDbConnection conn = null;
            try
            {
                conn = Dapper.DapperManager.GetConnection();
                conn.Open();
                count = conn.Execute(sql, param, transaction);
                return count;
            }
            catch(Exception ex)
            {
                //封装异常处理
#if Debug
#warning "Debug is define!"
                throw ex;
#else
                return 0;
#endif
            }
            finally
            {
                conn.Close();
            }
        }



        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static List<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null)
        {
            IDbConnection conn = null;
            try
            {
                conn = Dapper.DapperManager.GetConnection();
                conn.Open();
                var list = conn.Query<T>(sql, param, transaction);
                return list as List<T>;
            }
            catch(Exception ex)
            {
#if Debug
#warning "Debug is define!"
                throw ex;
#else
                return null;
#endif
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> Query(string sql, object param = null, IDbTransaction transaction = null)
        {
            IDbConnection conn = null;
            try
            {
                conn = Dapper.DapperManager.GetConnection();
                var list = conn.Query(sql, param, transaction);
                return list;
            }
            catch (Exception ex)
            {
#if Debug
#warning "Debug is define!"
                throw ex;
#else
                return null;
#endif
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static int ExecuteScalar(string sql, object param = null, IDbTransaction transaction = null)
        {
            IDbConnection conn = null;
            try
            {
                conn = Dapper.DapperManager.GetConnection();
                var obj = conn.ExecuteScalar<int>(sql, param, transaction);
                return obj;
            }
            catch (Exception ex)
            {
#if Debug
#warning "Debug is define!"
                throw ex;
#else
                return 0;
#endif
            }
            finally
            {
                conn.Close();
            }
        }

    }
}
