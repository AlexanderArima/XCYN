using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web;

namespace bscy.App_Code.Models.Table.BranchList
{

    /// <summary>
    /// BranchListViewModel 的摘要说明
    /// </summary>
    [Serializable]
    public class BranchListViewModel
    {

        public BranchListViewModel()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 遍历所有数据
        /// </summary>
        /// <returns></returns>
        public Grid GetList()
        {
            return GetList(null);
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <returns></returns>
        public Grid GetList(BranchListQueryParm parm)
        {
            BranchRecord model = new BranchRecord();
            StringBuilder builder = new StringBuilder();
            if (parm != null)
            {
                #region 拼接SQL筛选条件
                if (!string.IsNullOrEmpty(parm.NSRMC) && parm.NSRMC.Trim().Length > 0)
                {
                    builder.Append(string.Format(" AND NSRMC like '%{0}%' ", parm.NSRMC));
                }
                if (!string.IsNullOrEmpty(parm.NSRSBH) && parm.NSRSBH.Trim().Length > 0)
                {
                    builder.Append(string.Format(" AND NSRSBH like '%{0}%' ", parm.NSRSBH));
                }
                if (!string.IsNullOrEmpty(parm.JYDZ) && parm.JYDZ.Trim().Length > 0)
                {
                    builder.Append(string.Format(" AND JYDZ like '%{0}%' ", parm.JYDZ));
                }
                if (!string.IsNullOrEmpty(parm.ZGSWJG) && parm.ZGSWJG.Trim().Length > 0)
                {
                    builder.Append(string.Format(" AND ZGSWJG like '%{0}%' ", parm.ZGSWJG));
                }
                if (parm.SFNSZT == 0 || parm.SFNSZT == 1)
                {
                    builder.Append(string.Format(" AND SFNSZT = {0} ", parm.SFNSZT));
                }
                //开业时间
                if (!string.IsNullOrEmpty(parm.KYSJ_StartTime) && parm.KYSJ_StartTime.Trim().Length > 0 &&
                    !string.IsNullOrEmpty(parm.KYSJ_EndTime) && parm.KYSJ_EndTime.Trim().Length > 0)
                {
                    builder.Append(string.Format(" AND KYSJ > '{0}' AND KYSJ < '{1}' ", parm.KYSJ_StartTime, parm.KYSJ_EndTime));
                }
                if (string.IsNullOrEmpty(parm.KYSJ_StartTime) && !string.IsNullOrEmpty(parm.KYSJ_EndTime) &&
                    parm.KYSJ_EndTime.Trim().Length > 0)
                {
                    builder.Append(string.Format(" AND KYSJ < '{0}' ", parm.KYSJ_EndTime));
                }
                if (!string.IsNullOrEmpty(parm.KYSJ_StartTime) && parm.KYSJ_StartTime.Trim().Length > 0 &&
                    string.IsNullOrEmpty(parm.KYSJ_EndTime))
                {
                    builder.Append(string.Format(" AND KYSJ > '{0}' ", parm.KYSJ_StartTime));
                }
                //注销时间
                if (!string.IsNullOrEmpty(parm.ZXSJ_StartTime) && parm.ZXSJ_StartTime.Trim().Length > 0 &&
                    !string.IsNullOrEmpty(parm.ZXSJ_EndTime) && parm.ZXSJ_EndTime.Trim().Length > 0)
                {
                    builder.Append(string.Format(" AND ZXSJ > '{0}' AND ZXSJ < '{1}' ", parm.ZXSJ_StartTime, parm.ZXSJ_EndTime));
                }
                if (string.IsNullOrEmpty(parm.ZXSJ_StartTime) && !string.IsNullOrEmpty(parm.ZXSJ_EndTime) &&
                     parm.ZXSJ_EndTime.Trim().Length > 0)
                {
                    builder.Append(string.Format(" AND ZXSJ < '{0}' ", parm.ZXSJ_EndTime));
                }
                if (!string.IsNullOrEmpty(parm.ZXSJ_StartTime) && string.IsNullOrEmpty(parm.ZXSJ_EndTime) &&
                    parm.ZXSJ_StartTime.Trim().Length > 0)
                {
                    builder.Append(string.Format(" AND ZXSJ > '{0}' ", parm.ZXSJ_StartTime));
                }
                #endregion
            }
            Grid grid = new Grid();
            DataTable table = model.GetList(builder.ToString());
            foreach (DataRow row in table.Rows)
            {
                Node node = new Node();
                node.id = row["id"].ToString();
                node.NUMBER = row["NUMBER"].ToString();
                node.NSRMC = row["NSRMC"].ToString();
                node.NSRSBH = row["NSRSBH"].ToString();
                node.JYDZ = row["JYDZ"].ToString();
                node.ZGSWJG = row["ZGSWJG"].ToString();
                node.SFNSZT = ConvertHelper.ToBoolean(row["SFNSZT"]) ? 1 : 0;
                node.KYSJ = Convert.ToDateTime(row["KYSJ"]).ToString("yyyy-MM-dd");
                if (row["ZXSJ"] != DBNull.Value)
                {
                    node.ZXSJ = Convert.ToDateTime(row["ZXSJ"]).ToString("yyyy-MM-dd");
                }
                else
                {
                    node.ZXSJ = "";
                }
                node.JGCJ = ConvertHelper.ToInt(row["JGCJ"]);
                node.SJJGMC = row["SJJGMC"].ToString();
                grid.Rows.Add(node);
            }
            grid = GetTreeMap(grid);
            return grid;
        }

        /// <summary>
        /// 生成层级关系
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public Grid GetTreeMap(Grid grid)
        {
            Grid grid_result = new Grid();
            grid_result.Rows = new List<Node>();
            Node node_result = new Node();
            List<Node> temp_node = new List<Node>();
            //层级关系，SJJGMC 为空表示根，有值则查找上级菜单，只有三层
            for (int i = 0; i < grid.Rows.Count; i++)
            {
                if (grid.Rows[i].SJJGMC.Trim().Length == 0)
                {
                    //SJJGMC 为空表示根
                    //grid_result.Rows.Add(grid.Rows[i]);
                    //grid.Rows.Remove(grid.Rows[i]);
                    temp_node.Add(grid.Rows[i]);
                }
            }
            grid_result.Rows.AddRange(temp_node);
            foreach (Node item in temp_node)
            {
                grid.Rows.Remove(item);
            }

            //二级菜单
            for (int i = 0; i < grid_result.Rows.Count; i++)
            {
                temp_node.Clear();
                for (int j = 0; j < grid.Rows.Count; j++)
                {
                    if (grid.Rows[j].SJJGMC.Equals(grid_result.Rows[i].NSRMC))
                    {
                        //匹配成功
                        temp_node.Add(grid.Rows[j]);
                    }
                }
                if (temp_node.Count > 0)
                {
                    //将子节点挂到父节点上去
                    if (grid_result.Rows[i].children == null)
                    {
                        grid_result.Rows[i].children = new List<Node>();
                    }
                    grid_result.Rows[i].children = Clone<Node>(temp_node);
                    foreach (Node item in temp_node)
                    {
                        grid.Rows.Remove(item);
                    }
                }
            }

            //三级菜单
            for (int i = 0; i < grid_result.Rows.Count; i++)
            {
                List<Node> children = grid_result.Rows[i].children;
                if (children != null && children.Count > 0)
                {
                    for (int j = 0; j < children.Count; j++)
                    {
                        //循环子节点
                        for (int k = 0; k < grid.Rows.Count; k++)
                        {
                            if (grid.Rows[k].SJJGMC.Equals(children[j].NSRMC))
                            {
                                //匹配成功
                                temp_node.Add(grid.Rows[k]);
                            }
                        }
                        if (temp_node.Count > 0)
                        {
                            //将子节点挂到父节点上去
                            if (children[j].children == null)
                            {
                                children[j].children = new List<Node>();
                            }
                            children[j].children = Clone<Node>(temp_node);
                            foreach (Node item in temp_node)
                            {
                                grid.Rows.Remove(item);
                            }
                            temp_node.Clear();
                        }
                    }
                }
            }
            return grid_result;
        }

        /// <summary>
        /// 深拷贝数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="List">The list.</param>
        /// <returns>List{``0}.</returns>
        public static List<T> Clone<T>(object List)
        {
            using (Stream objectStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, List);
                objectStream.Seek(0, SeekOrigin.Begin);
                return formatter.Deserialize(objectStream) as List<T>;
            }
        }

        /// <summary>
        /// 单个查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Node GetModel(int id)
        {
            BranchRecord model = new BranchRecord();
            model.GetModel(id);
            Node node = new Node();
            node.id = model.id.ToString();
            node.NUMBER = model.NUMBER;
            node.NSRMC = model.NSRMC;
            node.NSRSBH = model.NSRSBH;
            node.JYDZ = model.JYDZ;
            node.ZGSWJG = model.ZGSWJG;
            node.SFNSZT = model.SFNSZT ? 1 : 0;
            node.KYSJ = model.KYSJ.ToString("yyyy-MM-dd");
            if (model.ZXSJ != null)
            {
                node.ZXSJ = Convert.ToDateTime(model.ZXSJ).ToString("yyyy-MM-dd");
            }
            else
            {
                node.ZXSJ = "";
            }
            node.JGCJ = model.JGCJ;
            node.SJJGMC = model.SJJGMC;
            return node;
        }

        /// <summary>
        /// 批量导入
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool BatchAdd(IList<Node> list)
        {
            //将IList<Node>转成DataTable
            DataTable dt = new DataTable("BranchRecord");
            dt.Columns.Add("id");
            dt.Columns.Add("NUMBER");
            dt.Columns.Add("NSRMC");
            dt.Columns.Add("NSRSBH");
            dt.Columns.Add("JYDZ");
            dt.Columns.Add("ZGSWJG");
            dt.Columns.Add("SFNSZT", typeof(bool));
            dt.Columns.Add("KYSJ", typeof(DateTime));
            dt.Columns.Add("ZXSJ", typeof(DateTime));
            dt.Columns.Add("JGCJ");
            dt.Columns.Add("SJJGMC");
            dt.Columns.Add("ISDELETE",typeof(bool));
            dt.Columns.Add("DELETETIME", typeof(DateTime));
            for (int i = 0; i < list.Count; i++)
            {
                DataRow row = dt.NewRow();
                row[1] = list[i].NUMBER;
                row[2] = list[i].NSRMC;
                row[3] = list[i].NSRSBH;
                row[4] = list[i].JYDZ;
                row[5] = list[i].ZGSWJG;
                row[6] = list[i].SFNSZT == 1 ? true : false;
                row[7] = list[i].KYSJ;
                DateTime Temp_ZXSJ = new DateTime();
                if(DateTime.TryParse(list[i].ZXSJ, out Temp_ZXSJ))
                {
                    row[8] = list[i].ZXSJ;
                }
                row[9] = list[i].JGCJ;
                row[10] = list[i].SJJGMC;
                row[11] = false;
                dt.Rows.Add(row);
            }
            BranchRecord branch = new BranchRecord();
            return branch.BatchAdd(dt);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public int Add(Node node)
        {
            BranchRecord branch = new BranchRecord();
            branch.NUMBER = node.NUMBER;
            branch.NSRMC = node.NSRMC;
            branch.NSRSBH = node.NSRSBH;
            branch.JYDZ = node.JYDZ;
            branch.ZGSWJG = node.ZGSWJG;
            branch.SFNSZT = node.SFNSZT == 1 ? true : false;
            branch.KYSJ = Convert.ToDateTime(node.KYSJ);
            if (string.IsNullOrEmpty(node.ZXSJ))
            {
                branch.ZXSJ = null;
            }
            else
            {
                branch.ZXSJ = Convert.ToDateTime(node.ZXSJ);
            }
            branch.JGCJ = node.JGCJ;
            branch.SJJGMC = node.SJJGMC;
            //检查纳税人名称是否不存在
            if (this.Count(node.NSRMC) != 0)
            {
                return -1;
            }
            int num = branch.Add();
            return num;
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public int Edit(Node node)
        {
            BranchRecord branch = new BranchRecord();
            branch.id = ConvertHelper.ToInt(node.id);
            branch.NUMBER = node.NUMBER;
            branch.NSRMC = node.NSRMC;
            branch.NSRSBH = node.NSRSBH;
            branch.JYDZ = node.JYDZ;
            branch.ZGSWJG = node.ZGSWJG;
            branch.SFNSZT = node.SFNSZT == 1 ? true : false;
            branch.KYSJ = Convert.ToDateTime(node.KYSJ);
            if (string.IsNullOrEmpty(node.ZXSJ))
            {
                branch.ZXSJ = null;
            }
            else
            {
                branch.ZXSJ = Convert.ToDateTime(node.ZXSJ);
            }
            branch.JGCJ = node.JGCJ;
            branch.SJJGMC = node.SJJGMC;
            if (this.Count(branch.NSRMC, branch.id) != 0)
            {
                return -1;
            }
            bool flag = branch.Update();
            if (flag == true)
            {
                return 1;
            }
            else
            {
                return 0;
            }
            //return flag;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            BranchRecord branch = new BranchRecord();
            branch.Delete(id);
            branch.GetModel(id);
            return branch.ISDELETE;
        }

        /// <summary>
        /// 纳税人名称是否唯一
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Count(string name)
        {
            BranchRecord branch = new BranchRecord();
            return branch.Count(name);
        }

        /// <summary>
        /// 纳税人名称是否唯一，除了某个ID之外，编辑时会用到
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Count(string name, int id)
        {
            BranchRecord branch = new BranchRecord();
            return branch.Count(name, id);
        }

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            BranchRecord branch = new BranchRecord();
            return branch.Count();
        }
    }

    [Serializable]
    public class Grid
    {
        public Grid()
        {
            _Rows = new List<Node>();
        }

        private List<Node> _Rows;

        public List<Node> Rows
        {
            get { return _Rows; }
            set { _Rows = value; }
        }
    }

    [Serializable]
    public class Node
    {
        private string _id;
        private string _number;
        private string _nsrmc;
        private string _nsrsbh;
        private string _jydz;
        private string _zgswjg;
        private int _sfnszt;
        private string _kysj;
        private string _zxsj;
        private int _jgcj;
        private string _sjjgmc;
        private int _isdelete = 0;
        private DateTime? _deletetime;
        /// <summary>
        /// 
        /// </summary>
        public string id
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
        public int SFNSZT
        {
            set { _sfnszt = value; }
            get { return _sfnszt; }
        }
        /// <summary>
        /// 开业时间
        /// </summary>
        public string KYSJ
        {
            set { _kysj = value; }
            get { return _kysj; }
        }
        /// <summary>
        /// 注销时间
        /// </summary>
        public string ZXSJ
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
        public int ISDELETE
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

        private List<Node> _children;

        public List<Node> children
        {
            get { return _children; }
            set { _children = value; }
        }

        /// <summary>
        /// 克隆实例
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            BinaryFormatter formatter = new BinaryFormatter(null, new System.Runtime.Serialization.StreamingContext(System.Runtime.Serialization.StreamingContextStates.Clone));
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, this);
            stream.Position = 0;
            object clonedObj = formatter.Deserialize(stream);
            stream.Close();
            return clonedObj;
        }

    }

}