using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Factory
{
    /// <summary>
    /// 工厂对象
    /// </summary>
    public class DataBaseFactory
    {
        /// <summary>
        /// 创建对象
        /// </summary>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        public IDataBase GetDataBase(DataBaseName databaseName)
        {
            switch (databaseName)
            {
                case DataBaseName.SQLITE:
                    return new SqliteDataBase();
                case DataBaseName.SQLSERVER:
                    return new SqlServerDataBase();
                default:
                    return new SqlServerDataBase();
            }
        }
    }

    /// <summary>
    /// 数据库名称
    /// </summary>
    public enum DataBaseName{
        SQLITE,
        SQLSERVER
    }
}
