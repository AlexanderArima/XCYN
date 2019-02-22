using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XCYN.Winform.Quartz.ViewModel;

namespace XCYN.Winform.Quartz.Views
{
    public partial class AddServiceForm : Form
    {
        public AddServiceForm()
        {
            InitializeComponent();
        }
        
        private void AddServiceForm_Load(object sender, EventArgs e)
        {
            AddServiceFormViewModel model = new AddServiceFormViewModel();
            lookUpEdit1.Properties.NullText = "";
            lookUpEdit1.Properties.DataSource = model.GetAssemblyList();

            lookUpEdit2.Properties.NullText = "";
            lookUpEdit2.Properties.DataSource = model.GetNameSpaceList();
        }

        private void AddServiceForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string serviceName = textBox1.Text;
            string assemblyName = lookUpEdit1.Text;
            string nameSpace = lookUpEdit2.Text;
            string className = textBox2.Text;
            string methodName = textBox3.Text;
            if(assemblyName.Length == 0)
            {
                MessageBox.Show("请输入程序集名称", "提示", MessageBoxButtons.OK);
                return;
            }
            if (nameSpace.Length == 0)
            {
                MessageBox.Show("请输入命名空间", "提示", MessageBoxButtons.OK);
                return;
            }
            AddServiceFormViewModel model = new AddServiceFormViewModel()
            {
                ServiceName = serviceName,
                AssemblyName = assemblyName,
                NameSpace = nameSpace,
                ClassName = className,
                MethodName = methodName,
                IsDelete = false
            };
            int count = model.Add();
            if(count > 0)
            {
                var result = MessageBox.Show("添加成功!", "提示");
                if (result.Equals(DialogResult.OK))
                {
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("未添加成功，请修改数据后重试", "提示");
            }
        }

    }
}
