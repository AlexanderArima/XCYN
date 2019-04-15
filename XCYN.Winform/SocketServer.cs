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

namespace XCYN.Winform
{
    public partial class SocketServer : Form
    {

        Dictionary<string, Socket> _dict_socket = new Dictionary<string, Socket>();

        public SocketServer()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ip = textBox1.Text;
            string port = textBox2.Text;
            IPAddress address = IPAddress.Parse(ip);
            IPEndPoint point = new IPEndPoint(address, Convert.ToInt32(port));
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                //绑定地址
                socket.Bind(point);
                //同一时间监听10个客户端
                socket.Listen(10);
                Thread thread = new Thread(AcceptInfo);
                thread.IsBackground = true;
                thread.Start(socket);
            }
            catch
            {

            }
        }

        /// <summary>
        /// 监听客户端发来的消息
        /// </summary>
        /// <param name="obj"></param>
        private void AcceptInfo(object obj)
        {
            Socket socket = obj as Socket;
            while (true)
            {
                try
                {
                    //创建通信用的Socket
                    Socket tsocket = socket.Accept();
                    var point = tsocket.RemoteEndPoint.ToString();
                    ShowMsg(point + "连接成功!");
                    _dict_socket.Add(point, tsocket);
                    //接收消息
                    Thread thread = new Thread(ReceiveMsg);
                    thread.IsBackground = true;
                    thread.Start(tsocket);
                }
                catch(Exception ex)
                {
                    ShowMsg(ex.Message);
                    break;
                }
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
                    ShowMsg(client.RemoteEndPoint.ToString()+":"+word);
                }
                catch(Exception ex)
                {
                    ShowMsg(ex.Message);
                    break;
                }
            }
        }

        private void ShowMsg(string msg)
        {
            CrossThreadCalls.SafeInvoke(listBox1,() => {
                listBox1.Items.Insert(0, msg);
            });
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                ShowMsg(textBox3.Text);
                string ip = textBox1.Text;
                byte[] buffer = Encoding.UTF8.GetBytes(textBox3.Text);
                _dict_socket[ip].Send(buffer);
            }
           catch(Exception ex)
            {
                ShowMsg(ex.Message);
            }
        }
    }
}
