using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XCYN.Winform.Demo
{
    public partial class MyThreadState : Form
    {
        public MyThreadState()
        {
            InitializeComponent();
        }

        int _index = 0;
        Thread _t = null;

        private void button1_Click(object sender, EventArgs e)
        {
            //启动线程
            _t = new Thread(new ThreadStart(()=> {
                while (true)
                {
                    try
                    {
                        Thread.Sleep(1000);
                        textBox1.Invoke(new Action(() => {
                            textBox1.AppendText(_index + ",");
                        }));
                        _index++;
                    }
                    catch(Exception ex)
                    {
                        _index++;
                        MessageBox.Show(ex.Message + ":"+_index);
                    }
                }
            }));
            _t.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(_t.ThreadState == ThreadState.Running || _t.ThreadState == ThreadState.WaitSleepJoin)
            {
                _t.Suspend();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(_t.ThreadState == ThreadState.Suspended)
            {
                _t.Resume();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _t.Interrupt();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            _t.Abort();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
        }
    }
}
