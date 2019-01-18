using System;
using System.Collections.Generic;
using System.Web;
namespace bscy.App_Code.Models.Table.BranchList
{
    /// <summary>
    /// BranchListQuery 的摘要说明
    /// </summary>
    public class BranchListQueryParm
    {
        public BranchListQueryParm()
        {

        }

        private string _nsrmc;
        private string _nsrsbh;
        private string _jydz;
        private string _zgswjg;
        private int _sfnszt = -1;
        private string _kysj_starttime;
        private string _kysj_endtime;
        private string _zxsj_starttime;
        private string _zxsj_endtime;

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
        public int SFNSZT
        {
            set { _sfnszt = value; }
            get { return _sfnszt; }
        }
        /// <summary>
        /// 开业时间（起始时间）
        /// </summary>
        public string KYSJ_StartTime
        {
            set { _kysj_starttime = value; }
            get { return _kysj_starttime; }
        }

        /// <summary>
        /// 开业时间（结束时间）
        /// </summary>
        public string KYSJ_EndTime
        {
            set { _kysj_endtime = value; }
            get { return _kysj_endtime; }
        }

        /// <summary>
        /// 注销时间（起始时间）
        /// </summary>
        public string ZXSJ_StartTime
        {
            set { _zxsj_starttime = value; }
            get { return _zxsj_starttime; }
        }

        /// <summary>
        /// 注销时间（结束时间）
        /// </summary>
        public string ZXSJ_EndTime
        {
            set { _zxsj_endtime = value; }
            get { return _zxsj_endtime; }
        }
    }
}