using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using XCYN.Common;

namespace XCYN.Service.WinClient
{
    public partial class SocketClient : Form
    {

        Socket _client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        public SocketClient()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IPAddress address = IPAddress.Parse(textBox1.Text);
            IPEndPoint point = new IPEndPoint(address, Convert.ToInt32(textBox2.Text));
            try
            {
                //连接服务器
                _client.Connect(point);
                ShowMsg("连接成功");
                ShowMsg("客户端:" + _client.LocalEndPoint.ToString());
                ShowMsg("服务端:" + _client.RemoteEndPoint.ToString());
                //连接成功后，就可以接收服务器发送的消息了
                Thread thread = new Thread(ReceiveMsg);
                thread.IsBackground = true;
                thread.Start(_client);
            }
            catch(Exception ex)
            {

            }
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        private void ReceiveMsg(object obj)
        {
            Socket client = obj as Socket;
            while (true)
            {
                try
                {
                    byte[] buffer = new byte[1024 * 1024];
                    //将接收到的消息存到byte数组中
                    int n = client.Receive(buffer);
                    string word = Encoding.UTF8.GetString(buffer, 0, n);
                    ShowMsg(client.RemoteEndPoint.ToString() + ":" + word);
                }
                catch (Exception ex)
                {
                    ShowMsg(ex.Message);
                    break;
                }
            }
        }

        private void ShowMsg(string msg)
        {
            CrossThreadCalls.SafeInvoke(listBox1, () => {
                listBox1.Items.Insert(0, msg);
            });
        }

        /// <summary>
        /// 发送消息到服务器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if(_client != null)
            {
                try
                {
                    ShowMsg(textBox3.Text);
                    byte[] buffer = Encoding.UTF8.GetBytes(textBox3.Text);
                    _client.Send(buffer);
                }
                catch(Exception ex)
                {
                    ShowMsg(ex.Message);
                }
            }
        }
    }
}
