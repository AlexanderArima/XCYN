using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XCYN.Winform.Quartz.Model;
using XCYN.Winform.Quartz.ViewModel;

namespace XCYN.Winform.Quartz.Views
{
    public partial class ServiceForm : Form
    {
        public ServiceForm()
        {
            InitializeComponent();
        }

        private void ServiceForm_Load(object sender, EventArgs e)
        {
            ServiceFormViewModel service = new ServiceFormViewModel();
            var ds = service.GetList();
            gridControl1.DataSource = ds.Tables[0];
            //gridView1.DataSource = ds.Tables[0];
        }
    }
}
