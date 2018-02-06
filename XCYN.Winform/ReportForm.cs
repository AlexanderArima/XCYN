using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XCYN.Winform
{
    public partial class ReportForm : Form
    {

        SqlConnection sqlCon;
        SqlDataAdapter myda;
        DataSet myds;
        public string M_str_sql = @"Data Source=.\MSSQL2008R2;Initial Catalog=Book;User ID=sa;Password=111111";

        public ReportForm()
        {
            InitializeComponent();
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {
            sqlCon = new SqlConnection(M_str_sql);
            myda = new SqlDataAdapter(@"SELECT TOP 1 [ID]
                                      ,[Name]
                                      ,[Standard]
                                      ,[BatchNumnber]
                                      ,[VolumeNumber]
                                      ,[Brand]
                                      ,[Licence]
                                      ,[Size]
                                      ,[Weight]
                                      ,[PrintDate]
                                      ,[AddTime]
                                      ,[State] FROM T_Wire", sqlCon);
            myds = new DataSet();
            myda.Fill(myds, "Table1");
            CrystalReport1 c = new CrystalReport1();
            c.SetDataSource(myds.Tables[0]);
            crystalReportViewer1.ReportSource = c;
            this.crystalReportViewer1.Refresh();
        }
    }
}
