using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web;

namespace bscy.App_Code.Models.Table.BranchList
{
    /// <summary>
    /// 类BranchRecord。
    /// </summary>
    [Serializable]
    public partial class BranchRecord
    {
        public BranchRecord()
        { }

        #region Model
        private int _id;
        private string _number;
        private string _nsrmc;
        private string _nsrsbh;
        private string _jydz;
        private string _zgswjg;
        private bool _sfnszt;
        private DateTime _kysj;
        private DateTime? _zxsj;
        private int _jgcj;
        private string _sjjgmc;
        private bool _isdelete = false;
        private DateTime? _deletetime;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 序号
        /// </summary>
        public string NUMBER
        {
            set { _number = value; }
            get { return _number; }
        }
        /// <summary>
        /// 纳税人名称
        /// </summary>
        public string NSRMC
        {
            set { _nsrmc = value; }
            get { return _nsrmc; }
        }
        /// <summary>
        /// 纳税人识别号
        /// </summary>
        public string NSRSBH
        {
            set { _nsrsbh = value; }
            get { return _nsrsbh; }
        }
        /// <summary>
        /// 经营地址
        /// </summary>
        public string JYDZ
        {
            set { _jydz = value; }
            get { return _jydz; }
        }
        /// <summary>
        /// 主管税务机关
        /// </summary>
        public string ZGSWJG
        {
            set { _zgswjg = value; }
            get { return _zgswjg; }
        }
        /// <summary>
        /// 是否纳税主体
        /// </summary>
        public bool SFNSZT
        {
            set { _sfnszt = value; }
            get { return _sfnszt; }
        }
        /// <summary>
        /// 开业时间
        /// </summary>
        public DateTime KYSJ
        {
            set { _kysj = value; }
            get { return _kysj; }
        }
        /// <summary>
        /// 注销时间
        /// </summary>
        public DateTime? ZXSJ
        {
            set { _zxsj = value; }
            get { return _zxsj; }
        }
        /// <summary>
        /// 机构层级
        /// </summary>
        public int JGCJ
        {
            set { _jgcj = value; }
            get { return _jgcj; }
        }
        /// <summary>
        /// 上级机构名称
        /// </summary>
        public string SJJGMC
        {
            set { _sjjgmc = value; }
            get { return _sjjgmc; }
        }

        /// <summary>
        /// 删除标记
        /// </summary>
        public bool ISDELETE
        {
            set { _isdelete = value; }
            get { return _isdelete; }
        }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DELETETIME
        {
            set { _deletetime = value; }
            get { return _deletetime; }
        }
        #endregion Model

        #region  Method

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public BranchRecord(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,NUMBER,NSRMC,NSRSBH,JYDZ,ZGSWJG,SFNSZT,KYSJ,ZXSJ,JGCJ,SJJGMC,ISDELETE,DELETETIME ");
            strSql.Append(" FROM [BranchRecord] ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
                        new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            DataTable dt = SqlHelper.GetTable(strSql.ToString(), parameters);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["id"] != null && dt.Rows[0]["id"].ToString() != "")
                {
                    this.id = int.Parse(dt.Rows[0]["id"].ToString());
                }
                if (dt.Rows[0]["NUMBER"] != null)
                {
                    this.NUMBER = dt.Rows[0]["NUMBER"].ToString();
                }
                if (dt.Rows[0]["NSRMC"] != null)
                {
                    this.NSRMC = dt.Rows[0]["NSRMC"].ToString();
                }
                if (dt.Rows[0]["NSRSBH"] != null)
                {
                    this.NSRSBH = dt.Rows[0]["NSRSBH"].ToString();
                }
                if (dt.Rows[0]["JYDZ"] != null)
                {
                    this.JYDZ = dt.Rows[0]["JYDZ"].ToString();
                }
                if (dt.Rows[0]["ZGSWJG"] != null)
                {
                    this.ZGSWJG = dt.Rows[0]["ZGSWJG"].ToString();
                }
                if (dt.Rows[0]["SFNSZT"] != null && dt.Rows[0]["SFNSZT"].ToString() != "")
                {
                    if ((dt.Rows[0]["SFNSZT"].ToString() == "1") || (dt.Rows[0]["SFNSZT"].ToString().ToLower() == "true"))
                    {
                        this.SFNSZT = true;
                    }
                    else
                    {
                        this.SFNSZT = false;
                    }
                }

                if (dt.Rows[0]["KYSJ"] != null && dt.Rows[0]["KYSJ"].ToString() != "")
                {
                    this.KYSJ = DateTime.Parse(dt.Rows[0]["KYSJ"].ToString());
                }
                if (dt.Rows[0]["ZXSJ"] != null && dt.Rows[0]["ZXSJ"].ToString() != "")
                {
                    this.ZXSJ = DateTime.Parse(dt.Rows[0]["ZXSJ"].ToString());
                }
                if (dt.Rows[0]["JGCJ"] != null && dt.Rows[0]["JGCJ"].ToString() != "")
                {
                    this.JGCJ = int.Parse(dt.Rows[0]["JGCJ"].ToString());
                }
                if (dt.Rows[0]["SJJGMC"] != null)
                {
                    this.SJJGMC = dt.Rows[0]["SJJGMC"].ToString();
                }
                if (dt.Rows[0]["ISDELETE"] != null && dt.Rows[0]["ISDELETE"].ToString() != "")
                {
                    if ((dt.Rows[0]["ISDELETE"].ToString() == "1") || (dt.Rows[0]["ISDELETE"].ToString().ToLower() == "true"))
                    {
                        this.ISDELETE = true;
                    }
                    else
                    {
                        this.ISDELETE = false;
                    }
                }

                if (dt.Rows[0]["DELETETIME"] != null && dt.Rows[0]["DELETETIME"].ToString() != "")
                {
                    this.DELETETIME = DateTime.Parse(dt.Rows[0]["DELETETIME"].ToString());
                }
            }
        }

        /// <summary>
        /// 批量导入
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool BatchAdd(DataTable dt)
        {
            return SqlHelper.ExecuteNonQuery(dt);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into [BranchRecord] (");
            strSql.Append("NUMBER,NSRMC,NSRSBH,JYDZ,ZGSWJG,SFNSZT,KYSJ,ZXSJ,JGCJ,SJJGMC,ISDELETE,DELETETIME)");
            strSql.Append(" values (");
            strSql.Append("@NUMBER,@NSRMC,@NSRSBH,@JYDZ,@ZGSWJG,@SFNSZT,@KYSJ,@ZXSJ,@JGCJ,@SJJGMC,@ISDELETE,@DELETETIME)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@NUMBER", SqlDbType.NVarChar,200),
                    new SqlParameter("@NSRMC", SqlDbType.NVarChar,200),
                    new SqlParameter("@NSRSBH", SqlDbType.NVarChar,200),
                    new SqlParameter("@JYDZ", SqlDbType.NVarChar,200),
                    new SqlParameter("@ZGSWJG", SqlDbType.NVarChar,200),
                    new SqlParameter("@SFNSZT", SqlDbType.Bit,1),
                    new SqlParameter("@KYSJ", SqlDbType.DateTime),
                    new SqlParameter("@ZXSJ", SqlDbType.DateTime),
                    new SqlParameter("@JGCJ", SqlDbType.Int,4),
                    new SqlParameter("@SJJGMC", SqlDbType.NVarChar,200),
                    new SqlParameter("@ISDELETE", SqlDbType.Bit,1),
                    new SqlParameter("@DELETETIME", SqlDbType.DateTime)};
            parameters[0].Value = NUMBER;
            parameters[1].Value = NSRMC;
            parameters[2].Value = NSRSBH;
            parameters[3].Value = JYDZ;
            parameters[4].Value = ZGSWJG;
            parameters[5].Value = SFNSZT;
            parameters[6].Value = KYSJ;
            parameters[7].Value = ZXSJ;
            parameters[8].Value = JGCJ;
            parameters[9].Value = SJJGMC;
            parameters[10].Value = ISDELETE;
            parameters[11].Value = DELETETIME;

            object obj = SqlHelper.ExecuteNonQuery(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [BranchRecord] set ");
            strSql.Append("NUMBER=@NUMBER,");
            strSql.Append("NSRMC=@NSRMC,");
            strSql.Append("NSRSBH=@NSRSBH,");
            strSql.Append("JYDZ=@JYDZ,");
            strSql.Append("ZGSWJG=@ZGSWJG,");
            strSql.Append("SFNSZT=@SFNSZT,");
            strSql.Append("KYSJ=@KYSJ,");
            strSql.Append("ZXSJ=@ZXSJ,");
            strSql.Append("JGCJ=@JGCJ,");
            strSql.Append("SJJGMC=@SJJGMC,");
            strSql.Append("ISDELETE=@ISDELETE,");
            strSql.Append("DELETETIME=@DELETETIME");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
                        new SqlParameter("@NUMBER", SqlDbType.NVarChar,200),
                        new SqlParameter("@NSRMC", SqlDbType.NVarChar,200),
                        new SqlParameter("@NSRSBH", SqlDbType.NVarChar,200),
                        new SqlParameter("@JYDZ", SqlDbType.NVarChar,200),
                        new SqlParameter("@ZGSWJG", SqlDbType.NVarChar,200),
                        new SqlParameter("@SFNSZT", SqlDbType.Bit,1),
                        new SqlParameter("@KYSJ", SqlDbType.DateTime),
                        new SqlParameter("@ZXSJ", SqlDbType.DateTime),
                        new SqlParameter("@JGCJ", SqlDbType.Int,4),
                        new SqlParameter("@SJJGMC", SqlDbType.NVarChar,200),
                        new SqlParameter("@ISDELETE", SqlDbType.Bit,1),
                        new SqlParameter("@DELETETIME", SqlDbType.DateTime),
                        new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = NUMBER;
            parameters[1].Value = NSRMC;
            parameters[2].Value = NSRSBH;
            parameters[3].Value = JYDZ;
            parameters[4].Value = ZGSWJG;
            parameters[5].Value = SFNSZT;
            parameters[6].Value = KYSJ;
            parameters[7].Value = ZXSJ;
            parameters[8].Value = JGCJ;
            parameters[9].Value = SJJGMC;
            parameters[10].Value = ISDELETE;
            parameters[11].Value = DELETETIME;
            parameters[12].Value = id;

            int rows = SqlHelper.ExecuteNonQuery(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteAll(int id)
        {
            List<string> listSql = new List<string>();
            listSql.Add(string.Format("update [BranchRecord] set ISDELETE = 1 where id={0}", id));
            //查看是否有子节点
            this.GetModel(id);
            string NSRMC = this.NSRMC;
            if (this.CountSJJGMC(this.NSRMC) == 0)
            {
                //没有子节点执行SQL语句
                goto executeSql;
            }
            else
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select id,NSRMC ");
                strSql.Append(" FROM [BranchRecord] ");
                strSql.Append(string.Format(" where SJJGMC = '{0}' AND  ISDELETE = 0 ", NSRMC));
                DataTable dt = SqlHelper.GetTable(strSql.ToString());
                StringBuilder strId = new StringBuilder();
                StringBuilder strNSRMC = new StringBuilder();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    listSql.Add(string.Format("update [BranchRecord] set ISDELETE = 1 where id={0}", dt.Rows[i]["id"]));
                    strId.Append(dt.Rows[i]["id"].ToString() + ",");
                    strNSRMC.Append("'" + dt.Rows[i]["NSRMC"].ToString() + "',");
                }
                
                strId.Remove(strId.Length - 1, 1);
                strNSRMC.Remove(strNSRMC.Length - 1, 1);    //去掉最后一位逗号
                //查看是否还有子节点
                if (Count(string.Format("and SJJGMC in({0})", strNSRMC)) == 0)
                {
                    //没有子节点执行SQL语句
                    goto executeSql;
                }
                else
                {
                    strSql.Remove(0, strSql.Length);    //清空
                    strSql.Append("select id,NSRMC ");
                    strSql.Append(" FROM [BranchRecord] ");
                    strSql.Append(string.Format(" where SJJGMC in({0}) AND  ISDELETE = 0 ", strNSRMC));
                    dt = SqlHelper.GetTable(strSql.ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        listSql.Add(string.Format("update [BranchRecord] set ISDELETE = 1 where id={0}", dt.Rows[i]["id"]));
                    }
                }
            }


        executeSql:
            if (SqlHelper.ExecuteSqlTran(listSql) == listSql.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update [BranchRecord] ");
            strSql.Append("set ISDELETE = 1 ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
                        new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            int rows = SqlHelper.ExecuteNonQuery(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取数据总数
        /// </summary>
        public int Count()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) ");
            strSql.Append(" FROM [BranchRecord] ");
            strSql.Append(" where ISDELETE = 0 ");
            DataTable dt = SqlHelper.GetTable(strSql.ToString());
            return Convert.ToInt32(dt.Rows[0][0]);
        }

        /// <summary>
        /// 获取数据总数
        /// </summary>
        public int Count(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) ");
            strSql.Append(" FROM [BranchRecord] ");
            strSql.Append(" where ISDELETE = 0 ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }
            DataTable dt = SqlHelper.GetTable(strSql.ToString());
            return Convert.ToInt32(dt.Rows[0][0]);
        }

        /// <summary>
        /// 纳税人识别号是否存在
        /// </summary>
        public int CountNSRSBH(string name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) ");
            strSql.Append(" FROM [BranchRecord] ");
            strSql.Append(" where NSRSBH=@NSRSBH and ISDELETE = 0");
            SqlParameter[] parameters = {
                        new SqlParameter("@NSRSBH", SqlDbType.NVarChar,200)
            };
            parameters[0].Value = name;

            DataTable dt = SqlHelper.GetTable(strSql.ToString(), parameters);
            return Convert.ToInt32(dt.Rows[0][0]);
        }

        /// <summary>
        /// 纳税人名称是否存在
        /// </summary>
        public int CountNSRMC(string name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) ");
            strSql.Append(" FROM [BranchRecord] ");
            strSql.Append(" where NSRMC=@NSRMC and ISDELETE = 0");
            SqlParameter[] parameters = {
                        new SqlParameter("@NSRMC", SqlDbType.NVarChar,200)
            };
            parameters[0].Value = name;

            DataTable dt = SqlHelper.GetTable(strSql.ToString(), parameters);
            return Convert.ToInt32(dt.Rows[0][0]);
        }

        /// <summary>
        /// 上级机构名称是否存在
        /// </summary>
        public int CountSJJGMC(string name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) ");
            strSql.Append(" FROM [BranchRecord] ");
            strSql.Append(" where SJJGMC=@SJJGMC and ISDELETE = 0");
            SqlParameter[] parameters = {
                        new SqlParameter("@SJJGMC", SqlDbType.NVarChar,200)
            };
            parameters[0].Value = name;

            DataTable dt = SqlHelper.GetTable(strSql.ToString(), parameters);
            return Convert.ToInt32(dt.Rows[0][0]);
        }

        /// <summary>
        /// 纳税人名称是否存在，除了某个id之外
        /// </summary>
        public int CountNSRSBH(string name, int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) ");
            strSql.Append(" FROM [BranchRecord] ");
            strSql.Append(" where NSRSBH=@NSRSBH and id <> @id and ISDELETE = 0");
            SqlParameter[] parameters = {
                        new SqlParameter("@NSRSBH", SqlDbType.NVarChar,200),
                        new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = name;
            parameters[1].Value = id;
            DataTable dt = SqlHelper.GetTable(strSql.ToString(), parameters);
            return Convert.ToInt32(dt.Rows[0][0]);
        }

        /// <summary>
        /// 纳税人名称是否存在，除了某个id之外
        /// </summary>
        public int CountNSRMC(string name, int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) ");
            strSql.Append(" FROM [BranchRecord] ");
            strSql.Append(" where NSRMC=@NSRMC and id <> @id and ISDELETE = 0");
            SqlParameter[] parameters = {
                        new SqlParameter("@NSRMC", SqlDbType.NVarChar,200),
                        new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = name;
            parameters[1].Value = id;
            DataTable dt = SqlHelper.GetTable(strSql.ToString(), parameters);
            return Convert.ToInt32(dt.Rows[0][0]);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public void GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,NUMBER,NSRMC,NSRSBH,JYDZ,ZGSWJG,SFNSZT,KYSJ,ZXSJ,JGCJ,SJJGMC,ISDELETE,DELETETIME ");
            strSql.Append(" FROM [BranchRecord] ");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
                        new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            DataTable dt = SqlHelper.GetTable(strSql.ToString(), parameters);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["id"] != null && dt.Rows[0]["id"].ToString() != "")
                {
                    this.id = int.Parse(dt.Rows[0]["id"].ToString());
                }
                if (dt.Rows[0]["NUMBER"] != null)
                {
                    this.NUMBER = dt.Rows[0]["NUMBER"].ToString();
                }
                if (dt.Rows[0]["NSRMC"] != null)
                {
                    this.NSRMC = dt.Rows[0]["NSRMC"].ToString();
                }
                if (dt.Rows[0]["NSRSBH"] != null)
                {
                    this.NSRSBH = dt.Rows[0]["NSRSBH"].ToString();
                }
                if (dt.Rows[0]["JYDZ"] != null)
                {
                    this.JYDZ = dt.Rows[0]["JYDZ"].ToString();
                }
                if (dt.Rows[0]["ZGSWJG"] != null)
                {
                    this.ZGSWJG = dt.Rows[0]["ZGSWJG"].ToString();
                }
                if (dt.Rows[0]["SFNSZT"] != null && dt.Rows[0]["SFNSZT"].ToString() != "")
                {
                    if ((dt.Rows[0]["SFNSZT"].ToString() == "1") || (dt.Rows[0]["SFNSZT"].ToString().ToLower() == "true"))
                    {
                        this.SFNSZT = true;
                    }
                    else
                    {
                        this.SFNSZT = false;
                    }
                }
                if (dt.Rows[0]["KYSJ"] != null && dt.Rows[0]["KYSJ"].ToString() != "")
                {
                    this.KYSJ = DateTime.Parse(dt.Rows[0]["KYSJ"].ToString());
                }
                if (dt.Rows[0]["ZXSJ"] != null && dt.Rows[0]["ZXSJ"].ToString() != "")
                {
                    this.ZXSJ = DateTime.Parse(dt.Rows[0]["ZXSJ"].ToString());
                }
                if (dt.Rows[0]["JGCJ"] != null && dt.Rows[0]["JGCJ"].ToString() != "")
                {
                    this.JGCJ = int.Parse(dt.Rows[0]["JGCJ"].ToString());
                }
                if (dt.Rows[0]["SJJGMC"] != null)
                {
                    this.SJJGMC = dt.Rows[0]["SJJGMC"].ToString();
                }
                if (dt.Rows[0]["ISDELETE"] != null && dt.Rows[0]["ISDELETE"].ToString() != "")
                {
                    if ((dt.Rows[0]["ISDELETE"].ToString() == "1") || (dt.Rows[0]["ISDELETE"].ToString().ToLower() == "true"))
                    {
                        this.ISDELETE = true;
                    }
                    else
                    {
                        this.ISDELETE = false;
                    }
                }
                if (dt.Rows[0]["DELETETIME"] != null && dt.Rows[0]["DELETETIME"].ToString() != "")
                {
                    this.DELETETIME = DateTime.Parse(dt.Rows[0]["DELETETIME"].ToString());
                }
            }
        }

        /// <summary>
        /// 获得数据列表，Excel中需要导出中文表头和一部分字段
        /// </summary>
        public DataTable GetExcelExportDataTable(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT ROW_NUMBER() over(order by id) AS '序号' ,
                                                     [NSRMC] AS '纳税人名称'
                                                    ,[NSRSBH] AS '纳税人识别号'
                                                    ,[JYDZ] AS '经营地址'
                                                    ,[ZGSWJG] AS '主管税务机关'
                                                    , CASE[SFNSZT] WHEN 1 THEN '是' ELSE '否' END AS '是否纳税主体'
                                                    ,CONVERT(varchar(100), [KYSJ], 23) AS '开业时间'
                                                    ,CONVERT(varchar(100), [ZXSJ], 23) AS '注销时间' ");
            strSql.Append(" FROM [BranchRecord] ");
            strSql.Append(" WHERE ISDELETE = 0 ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" ORDER BY JGCJ ASC ");
            return SqlHelper.GetTable(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM [BranchRecord] ");
            strSql.Append(" where ISDELETE = 0 ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" ORDER BY id ASC ");
            return SqlHelper.GetTable(strSql.ToString());
        }

        #endregion  Method
    }
}