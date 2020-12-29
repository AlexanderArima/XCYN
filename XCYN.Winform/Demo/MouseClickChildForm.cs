using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XCYN.Demo.Winform
{
    public partial class MouseClickChildForm : Form
    {
        public MouseClickChildForm()
        {
            InitializeComponent();

            Subscribe();

            GetSysProcessWithWindow();
        }

        List<Process> processWithWindow = new List<Process>();
        
        private void MouseClickChildForm_Load(object sender, EventArgs e)
        {
            
        }

        private void GetSysProcessWithWindow()
        {
            Process[] processes = Process.GetProcesses();
            processWithWindow.Clear();

            foreach (var p in processes)
            {
                if(p.MainWindowHandle == IntPtr.Zero)
                {
                    continue;
                }
                if(p.MainWindowTitle.Length == 0)
                {
                    continue;
                }
                processWithWindow.Add(p);
            }
        }

        private void MouseClickChildForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private IKeyboardMouseEvents m_GlobalHook;

        public void Subscribe()
        {
            // Note: for the application hook, use the Hook.AppEvents() instead
            m_GlobalHook = Hook.GlobalEvents();

            m_GlobalHook.MouseDownExt += GlobalHookMouseDownExt;
            m_GlobalHook.KeyPress += GlobalHookKeyPress;
        }

        private void GlobalHookKeyPress(object sender, KeyPressEventArgs e)
        {
            Console.WriteLine("KeyPress: \t{0}", e.KeyChar);
        }

        private void GlobalHookMouseDownExt(object sender, MouseEventExtArgs e)
        {
            Console.WriteLine("MouseDown: \t{0}; \t System Timestamp: \t{1}", e.Button, e.Timestamp);

            // uncommenting the following line will suppress the middle mouse button click
            // if (e.Buttons == MouseButtons.Middle) { e.Handled = true; }
        }

        public void Unsubscribe()
        {
            m_GlobalHook.MouseDownExt -= GlobalHookMouseDownExt;
            m_GlobalHook.KeyPress -= GlobalHookKeyPress;

            //It is recommened to dispose it
            m_GlobalHook.Dispose();
        }

    }
}
