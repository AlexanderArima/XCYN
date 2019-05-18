using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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
        public DataTable GetExcelExportDataTable()
        {
            BranchRecord model = new BranchRecord();
            return model.GetExcelExportDataTable("");
        }

        /// <summary>
        /// Datable导出成Excel
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="file">导出路径(包括文件名与扩展名)</param>
        public byte[] TableToExcel(DataTable dt, string file)
        {
            IWorkbook workbook;
            string fileExt = Path.GetExtension(file).ToLower();
            if (fileExt == ".xlsx")
            {
                workbook = new XSSFWorkbook();
            }
            else if (fileExt == ".xls")
            {
                workbook = new HSSFWorkbook();
            }
            else
            {
                workbook = null;
            }
            if (workbook == null)
            {
                throw new NullReferenceException("workbook的值为空");
            }
            ISheet sheet = string.IsNullOrEmpty(dt.TableName) ? workbook.CreateSheet("Sheet1") : workbook.CreateSheet(dt.TableName);
            sheet.SetColumnWidth(0, 20 * 256);
            sheet.SetColumnWidth(1, 40 * 256);
            sheet.SetColumnWidth(2, 30 * 256);
            sheet.SetColumnWidth(3, 80 * 256);
            sheet.SetColumnWidth(4, 45 * 256);
            sheet.SetColumnWidth(5, 15 * 256);
            sheet.SetColumnWidth(6, 10 * 256);
            sheet.SetColumnWidth(7, 20 * 256);

            //表头样式
            ICellStyle style = workbook.CreateCellStyle();
            //IDataFormat format = workbook.CreateDataFormat();
            IFont f = workbook.CreateFont();
            f.Boldweight = (short)FontBoldWeight.Bold;  //加粗
            style.SetFont(f);
            style.WrapText = false; //不自动换行
            style.VerticalAlignment = VerticalAlignment.Center; //垂直水平居中
            style.Alignment = HorizontalAlignment.Center;

            //表头  
            IRow row = sheet.CreateRow(0);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ICell cell = row.CreateCell(i);
                cell.SetCellValue(dt.Columns[i].ColumnName);
                cell.CellStyle = style;
            }

            //数据样式
            style = workbook.CreateCellStyle();
            style.WrapText = false; //不自动换行
            style.VerticalAlignment = VerticalAlignment.Center; //垂直水平居中
            style.Alignment = HorizontalAlignment.Center;

            //数据  
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row1 = sheet.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell cell = row1.CreateCell(j);
                    cell.SetCellValue(dt.Rows[i][j].ToString());
                    cell.CellStyle = style;
                }
            }

            //转为字节数组  
            using (MemoryStream stream = new MemoryStream())
            {
                workbook.Write(stream);
                byte[] buf = stream.ToArray();
                //保存为Excel文件  
                using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(buf, 0, buf.Length);
                    fs.Flush();
                    return buf;
                }
            }

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
                if (parm.SFNSZT == 0 || parm.SFNSZT == 1)
                {
                    builder.Append(string.Format(" AND SFNSZT = {0} ", parm.SFNSZT));
                }
                #endregion
            }
            Grid grid = new Grid();
            DataTable table = model.GetList(builder.ToString());
            foreach (DataRow row in table.Rows)
            {
                Node node = new Node();
                node.id = row["id"].ToString();
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
                node.SJJGMC = row["SJJGMC"].ToString();
                node.SJJGSFNSZT = ConvertHelper.ToBoolean(row["SJJGSFNSZT"]) ? 1 : 0;
                grid.Rows.Add(node);
            }
            grid = GetTreeMap(grid);
            //按显示的数据导出Excel

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
                //SJJGMC 为空表示根节点
                if (grid.Rows[i].SJJGMC.Trim().Length == 0)
                {
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
                        temp_node.Clear();
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
            node.SJJGMC = model.SJJGMC;
            node.SJJGSFNSZT = model.SJJGSFNSZT ? 1 : 0;
            node.CREATEID = model.CREATEID;
            node.CREATETIME = model.CREATETIME.ToString("yyyy-MM-dd HH:mm:ss");
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
            dt.Columns.Add("NSRMC");
            dt.Columns.Add("NSRSBH");
            dt.Columns.Add("JYDZ");
            dt.Columns.Add("ZGSWJG");
            dt.Columns.Add("SFNSZT", typeof(bool));
            dt.Columns.Add("KYSJ", typeof(DateTime));
            dt.Columns.Add("ZXSJ", typeof(DateTime));
            dt.Columns.Add("SJJGMC");
            dt.Columns.Add("SJJGSFNSZT", typeof(bool));
            dt.Columns.Add("ISDELETE", typeof(bool));
            dt.Columns.Add("CREATEID", typeof(int));
            dt.Columns.Add("CREATETIME", typeof(DateTime));
            dt.Columns.Add("EDITID", typeof(int));
            dt.Columns.Add("EDITTIME", typeof(DateTime));
            dt.Columns.Add("DELETEID", typeof(int));
            dt.Columns.Add("DELETETIME", typeof(DateTime));
            for (int i = 0; i < list.Count; i++)
            {
                string NSRMC = list[i].NSRMC;
                string NSRSBH = list[i].NSRSBH;
                if (this.Count(NSRMC, string.Empty) != 0)
                {
                    throw new ArgumentException(string.Format("纳税人名称:{0}，已存在", NSRMC));
                }
                if (this.Count("", NSRSBH) != 0)
                {
                    throw new ArgumentException(string.Format("纳税人识别号:{0}，已存在", NSRSBH));
                }
                DataRow row = dt.NewRow();
                row[1] = list[i].NSRMC;
                row[2] = list[i].NSRSBH;
                row[3] = list[i].JYDZ;
                row[4] = list[i].ZGSWJG;
                row[5] = list[i].SFNSZT == 1 ? true : false;
                row[6] = list[i].KYSJ;
                DateTime Temp_ZXSJ = new DateTime();
                if (DateTime.TryParse(list[i].ZXSJ, out Temp_ZXSJ))
                {
                    row[7] = list[i].ZXSJ;
                }
                row[8] = list[i].SJJGMC;
                row[9] = list[i].SJJGSFNSZT;
                row[10] = false;
                row[11] = list[i].CREATEID;
                row[12] = list[i].CREATETIME;
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
            branch.SJJGMC = node.SJJGMC;
            branch.SJJGSFNSZT = node.SJJGSFNSZT == 1 ? true : false;
            branch.CREATEID = node.CREATEID;
            branch.CREATETIME = Convert.ToDateTime(node.CREATETIME);
            //验证上级机构是否存在
            if (node.SJJGMC.Length > 0 && Count(node.SJJGMC, string.Empty) == 0)
            {
                throw new ArgumentException("上级机构名称: " + branch.SJJGMC + ", 不存在，请先录入该机构");
            }
            //检查纳税人名称是否不存在
            if (this.Count(node.NSRMC, string.Empty) != 0)
            {
                throw new ArgumentException("纳税人名称: " + branch.NSRMC + ", 已存在");
            }
            if (this.Count("", node.NSRSBH) != 0)
            {
                throw new ArgumentException("纳税人识别号: " + branch.NSRSBH + ", 已存在");
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
            branch.SJJGMC = node.SJJGMC;
            branch.SJJGSFNSZT = node.SJJGSFNSZT == 1 ? true : false;
            branch.CREATEID = node.CREATEID;
            branch.CREATETIME = Convert.ToDateTime(node.CREATETIME);
            branch.EDITID = node.EDITID;
            branch.EDITTIME = DateTime.Now;
            //验证上级机构是否存在
            if (node.SJJGMC.Length > 0 && Count(node.SJJGMC, string.Empty) == 0)
            {
                throw new ArgumentException("上级机构名称: " + branch.SJJGMC + ", 不存在，请先录入该机构");
            }
            //检查纳税人名称是否不存在
            if (this.Count(branch.id, branch.NSRMC, string.Empty) != 0)
            {
                throw new ArgumentException("纳税人名称:" + branch.NSRMC + ",已存在");
            }
            if (this.Count(branch.id, string.Empty, branch.NSRSBH) != 0)
            {
                throw new ArgumentException("纳税人识别号:" + branch.NSRSBH + ",已存在");
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
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id,int userId)
        {
            BranchRecord branch = new BranchRecord();
            return branch.DeleteAll(id, userId);
        }

        /// <summary>
        /// 纳税人名称是否唯一
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Count(string NSRMC, string NSRSBH)
        {
            BranchRecord branch = new BranchRecord();
            if (NSRMC.Length == 0 && NSRSBH.Length > 0)
            {
                return branch.CountNSRSBH(NSRSBH);
            }
            else if (NSRMC.Length > 0 && NSRSBH.Length == 0)
            {
                return branch.CountNSRMC(NSRMC);
            }
            else
            {
                throw new Exception("纳税人名称与识别号不能为空，也不能同时有值");
            }
        }

        /// <summary>
        /// 判断上级机构名称是否存在
        /// </summary>
        /// <param name="SJJGMC"></param>
        /// <returns></returns>
        public int CountSJJGMC(string SJJGMC)
        {
            BranchRecord branch = new BranchRecord();
            return branch.CountNSRMC(SJJGMC);
        }

        /// <summary>
        /// 纳税人名称与纳税人识别号是否唯一，除了某个ID之外，编辑时会用到
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Count(int id, string NSRMC, string NSRSBH)
        {
            BranchRecord branch = new BranchRecord();
            if (NSRMC.Length == 0 && NSRSBH.Length > 0)
            {
                return branch.CountNSRSBH(NSRSBH, id);
            }
            else if (NSRMC.Length > 0 && NSRSBH.Length == 0)
            {
                return branch.CountNSRMC(NSRMC, id);
            }
            else
            {
                throw new Exception("纳税人名称与识别号不能为空，也不能同时有值");
            }
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
        private string _nsrmc;
        private string _nsrsbh;
        private string _jydz;
        private string _zgswjg;
        private int _sfnszt;
        private string _kysj;
        private string _zxsj;
        private string _sjjgmc;
        private int _sjjgsfnszt;
        private int _isdelete = 0;
        private int _createid;
        private string _createtime;
        private int? _editid;
        private string _edittime;
        private int? _deleteid;
        private string _deletetime;
        /// <summary>
		/// 
		/// </summary>
		public string id
        {
            set { _id = value; }
            get { return _id; }
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
        /// 上级机构名称
        /// </summary>
        public string SJJGMC
        {
            set { _sjjgmc = value; }
            get { return _sjjgmc; }
        }
        /// <summary>
        /// 上级机构是否纳税主体
        /// </summary>
        public int SJJGSFNSZT
        {
            set { _sjjgsfnszt = value; }
            get { return _sjjgsfnszt; }
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
        /// 创建人
        /// </summary>
        public int CREATEID
        {
            set { _createid = value; }
            get { return _createid; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CREATETIME
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 编辑人
        /// </summary>
        public int? EDITID
        {
            set { _editid = value; }
            get { return _editid; }
        }
        /// <summary>
        /// 编辑时间
        /// </summary>
        public string EDITTIME
        {
            set { _edittime = value; }
            get { return _edittime; }
        }
        /// <summary>
        /// 删除人
        /// </summary>
        public int? DELETEID
        {
            set { _deleteid = value; }
            get { return _deleteid; }
        }
        /// <summary>
        /// 删除时间
        /// </summary>
        public string DELETETIME
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