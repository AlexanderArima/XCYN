using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using XCYN.Print.Generics;

namespace XCYN.Winform
{
    public partial class FileSearch : Form
    {

        List<string> _list = new List<string>();
        CancellationTokenSource tks = null;

        public FileSearch()
        {
            InitializeComponent();
            //在多线程程序中，新创建的线程不能访问UI线程创建的窗口控件，
            //如果需要访问窗口中的控件，可以在窗口构造函数中将CheckForIllegalCrossThreadCalls设置为 false
            //也可以针对某一控件进行设置
            CheckForIllegalCrossThreadCalls = false;
            String[] drives = Environment.GetLogicalDrives();
        }

        /// <summary>
        /// 搜索按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            tks = new CancellationTokenSource();
            CancellationToken token = tks.Token;
            var text = textBox1.Text;
            Task t1 = Task.Factory.StartNew(() =>
            {
                button1.Enabled = false;
                while (true)
                {
                    if (!token.IsCancellationRequested)
                    {
                        LoopFile(text);
                    }
                    else
                    {
                        break;
                    }
                }
            },
            token);

            Task t2 = Task.Factory.StartNew(() => {
                t1.Wait();
                button1.Enabled = true;
            });
        }

        private void LoopFile(string target)
        {
            if (target.Contains("$RECYCLE.BIN") || target.Contains("Config.Msi"))
                return;
            if (target.Contains(textBox2.Text))
                listBox1.Items.Add(target);
            string[] list_dir = new string[0];
            //判断是否为文件夹
            if (File.Exists(target))
            {
                //listBox1.Items.Add(target);
                label3.Text = target;
            }
            else if(Directory.Exists(target))
            {
                try
                {
                    list_dir = Directory.GetDirectories(target);
                }
                catch(System.UnauthorizedAccessException ex)
                {
                    return;
                }
            }
            for (int i = 0; i < list_dir.Count(); i++)
            {
                label3.Text = list_dir[i];
                LoopFile(list_dir[i]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                var SelectedPath = folderBrowserDialog1.SelectedPath;
                textBox1.Text = SelectedPath;
            }
        }
        
    }
}
