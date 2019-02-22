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
    public partial class EditServiceForm : Form
    {

        private int _id;

        public EditServiceForm(int id)
        {
            InitializeComponent();
            _id = id;
        }

        /// <summary>
        /// 加载画面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditServiceForm_Load(object sender, EventArgs e)
        {
            //绑定下拉菜单
            AddServiceFormViewModel model_lookup = new AddServiceFormViewModel();
            lookUpEdit1.Properties.NullText = "";
            lookUpEdit1.Properties.DataSource = model_lookup.GetAssemblyList();

            lookUpEdit2.Properties.NullText = "";
            lookUpEdit2.Properties.DataSource = model_lookup.GetNameSpaceList();

            EditServiceFormViewModel model = new EditServiceFormViewModel();
            model = model.GetModel(_id);
            textBox1.Text = model.ServiceName;
            lookUpEdit1.Text = model.AssemblyName;
            lookUpEdit2.Text = model.NameSpace;
            textBox2.Text = model.ClassName;
            textBox3.Text = model.MethodName;
        }

        /// <summary>
        /// 修改
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
            if (assemblyName.Length == 0)
            {
                MessageBox.Show("请输入程序集名称", "提示", MessageBoxButtons.OK);
                return;
            }
            if (nameSpace.Length == 0)
            {
                MessageBox.Show("请输入命名空间", "提示", MessageBoxButtons.OK);
                return;
            }
            EditServiceFormViewModel model = new EditServiceFormViewModel()
            {
                ID = _id,
                ServiceName = serviceName,
                AssemblyName = assemblyName,
                NameSpace = nameSpace,
                ClassName = className,
                MethodName = methodName,
            };
            
            var flag = model.Update();
            if (flag == true)
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

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void EditServiceForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            this.DialogResult = DialogResult.OK;
        }
    }
}
