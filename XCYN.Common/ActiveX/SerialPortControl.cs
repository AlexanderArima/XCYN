using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;
using mshtml;
using XCYN.Common.ActiveX;

namespace XCYN.Common.ActiveX
{
    //[ProgId("WebSerial")]//控件名称
    //[ClassInterface(ClassInterfaceType.AutoDual), ComSourceInterfaces(typeof(ControlEvents))]
    [ComSourceInterfaces(typeof(IComEvents))]
    [Guid("6C6A0DE4-193A-48f5-BA91-3C180558785B")]//控件的GUID，用于COM注册和HTML中Object对象classid引用
    [ComVisible(true)]
    [ToolboxItem(true)]
    public partial class SerialPortControl : UserControl, IObjectSafety
    {
        public Label label1;
        public Button button1;
        public Button button2;
        public TextBox txtPwd;
        private IHTMLWindow2 window2;
        private IHTMLDocument2 document2;
        private IHTMLDocument3 document3;

        public SerialPortControl()
        {
            InitializeComponent();

        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="window"></param>
        /// <param name="document"></param>
        [ComVisible(true)]
        public void Init(object window, object document)
        {
            this.window2 = window as IHTMLWindow2;
            this.document2 = document as IHTMLDocument2;
            this.document3 = document as IHTMLDocument3;
        }

        private void InitializeComponent()
        {

            this.txtPwd = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(94, 17);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.Size = new System.Drawing.Size(100, 21);
            this.txtPwd.TabIndex = 0;
            this.txtPwd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPwd_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "密码";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(28, 57);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(137, 57);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "调用JS";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // SerialPortControl
            // 
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPwd);
            this.Name = "SerialPortControl";
            this.Size = new System.Drawing.Size(370, 192);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        [ComVisible(true)]
        public string GetPwd()
        {
            return txtPwd.Text;
        }
        
        [ComVisible(true)]
        public  int Add(int x, int y)
        {
            return x + y;
        }

        #region JS监听ActiveX事件

        public delegate void NotifyHandler(string pwd);

        /// <summary>
        /// 在JS中监听该事件
        /// </summary>
        public event NotifyHandler OnNotify;

        [ComVisible(true)]
        public string SetPwd(string pwd)
        {
            this.txtPwd.Text = pwd; //此处修改txtPwd时，后台将无法调用，如果想返回pwd，请注释这一行
            return pwd;
        }

        private void txtPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Enter)
            {
                // 回车 触发事件
                if (this.OnNotify != null)
                {
                    this.OnNotify(this.txtPwd.Text);
                }
            }
        }

        #endregion

        public string Password { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            Password = this.txtPwd.Text;
            MessageBox.Show("密码修改成功");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RunJs("GetSeq", "2");
        }

        /// <summary>
        /// JS调用ActiveX，ActiveX调用JS的后台接收前台调用，并调用前台方法
        /// </summary>
        /// <param name="functionName">js函数名</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        [ComVisible(true)]
        public dynamic RunJs(string functionName, string args = "")
        {
            dynamic dy = null;// 第二种方式 反射调用 Js
            //return this.window2 != null;
            // 被调用的js函数必须有一个参数
            if (this.window2 != null)
            {
                dy = this.window2.GetType().InvokeMember(functionName,
                    BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.Public,
                    null, this.window2, new object[] { args });
            }

            return dy;
        }

        //public event ControlEventHandler OnReceive;
        //[ComVisible(true)]
        //private void Receive(string dataString)
        //{
        //    if (OnReceive != null)
        //    {
        //        OnReceive(dataString); //Calling event that will be catched in JS
        //    }
        //}

        #region IObjectSafety 成员
        public void GetInterfacceSafyOptions(int riid, out int pdwSupportedOptions, out int pdwEnabledOptions)
        {
            pdwSupportedOptions = 1;
            pdwEnabledOptions = 2;
        }
        public void SetInterfaceSafetyOptions(int riid, int dwOptionsSetMask, int dwEnabledOptions)
        {
            //throw new NotImplementedException();
        }
        #endregion

        #region IObjectSafety 成员

        private const string _IID_IDispatch = "{00020400-0000-0000-C000-000000000046}";
        private const string _IID_IDispatchEx = "{a6ef9860-c720-11d0-9337-00a0c90dcaa9}";
        private const string _IID_IPersistStorage = "{0000010A-0000-0000-C000-000000000046}";
        private const string _IID_IPersistStream = "{00000109-0000-0000-C000-000000000046}";
        private const string _IID_IPersistPropertyBag = "{37D84F60-42CB-11CE-8135-00AA004BB851}";

        private const int INTERFACESAFE_FOR_UNTRUSTED_CALLER = 0x00000001;
        private const int INTERFACESAFE_FOR_UNTRUSTED_DATA = 0x00000002;
        private const int S_OK = 0;
        private const int E_FAIL = unchecked((int)0x80004005);
        private const int E_NOINTERFACE = unchecked((int)0x80004002);

        private bool _fSafeForScripting = true;
        private bool _fSafeForInitializing = true;


        public int GetInterfaceSafetyOptions(ref Guid riid, ref int pdwSupportedOptions, ref int pdwEnabledOptions)
        {
            int Rslt = E_FAIL;

            string strGUID = riid.ToString("B");
            pdwSupportedOptions = INTERFACESAFE_FOR_UNTRUSTED_CALLER | INTERFACESAFE_FOR_UNTRUSTED_DATA;
            switch (strGUID)
            {
                case _IID_IDispatch:
                case _IID_IDispatchEx:
                    Rslt = S_OK;
                    pdwEnabledOptions = 0;
                    if (_fSafeForScripting == true)
                        pdwEnabledOptions = INTERFACESAFE_FOR_UNTRUSTED_CALLER;
                    break;
                case _IID_IPersistStorage:
                case _IID_IPersistStream:
                case _IID_IPersistPropertyBag:
                    Rslt = S_OK;
                    pdwEnabledOptions = 0;
                    if (_fSafeForInitializing == true)
                        pdwEnabledOptions = INTERFACESAFE_FOR_UNTRUSTED_DATA;
                    break;
                default:
                    Rslt = E_NOINTERFACE;
                    break;
            }

            return Rslt;
        }

        public int SetInterfaceSafetyOptions(ref Guid riid, int dwOptionSetMask, int dwEnabledOptions)
        {
            int Rslt = E_FAIL;

            string strGUID = riid.ToString("B");
            switch (strGUID)
            {
                case _IID_IDispatch:
                case _IID_IDispatchEx:
                    if (((dwEnabledOptions & dwOptionSetMask) == INTERFACESAFE_FOR_UNTRUSTED_CALLER) &&
                         (_fSafeForScripting == true))
                        Rslt = S_OK;
                    break;
                case _IID_IPersistStorage:
                case _IID_IPersistStream:
                case _IID_IPersistPropertyBag:
                    if (((dwEnabledOptions & dwOptionSetMask) == INTERFACESAFE_FOR_UNTRUSTED_DATA) &&
                         (_fSafeForInitializing == true))
                        Rslt = S_OK;
                    break;
                default:
                    Rslt = E_NOINTERFACE;
                    break;
            }

            return Rslt;
        }

        #endregion
    }

    [Guid("FDA0A081-3D3B-4EAB-AE01-6A40FDDA9A60")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface IComEvents
    {
        [DispId(0x00000001)]
        void OnNotify(string pwd);
    }
}
