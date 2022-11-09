using Fleck;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XCYN.Common;
using XCYN.Winform.WebSocket.Service;

namespace XCYN.Winform.WebSocket
{
    public partial class ServerForm : Form
    {
        List<IWebSocketConnection> _list = new List<IWebSocketConnection>();

        /// <summary>
        /// WCF宿主对象.
        /// </summary>
        WebServiceHost host;

        public ServerForm()
        {
            InitializeComponent();
        }

        private void ServerForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.host = new WebServiceHost(typeof(FleckService));
                this.host.Open();
            }
            catch(Exception ex)
            {
                Log4NetHelper.Error("FleckServer：ServerForm_Load 出错：" + ex.Message, ex);
                PrintLog("初始化服务失败：" + ex.Message);
            }

            FleckLog.Level = LogLevel.Debug;
            var server = new WebSocketServer("ws://0.0.0.0:7181");
            server.Start(socket =>
            {
                string client_id = socket.ConnectionInfo.Path;
                if (client_id.Contains("/"))
                {
                    client_id = client_id.Replace("/", "");
                }

                OnlineUser user = OnlineUser.Get(client_id);
                if(user == null)
                {
                    PrintLog("客户端连接失败，id=" + client_id);
                    return;
                }

                socket.OnOpen = () =>
                {
                    PrintLog(string.Format("{0}连接服务成功", user.Name));
                    _list.Add(socket);
                };
                socket.OnClose = () =>
                {
                    PrintLog(string.Format("{0}断开了连接", user.Name));
                    _list.Remove(socket);
                };
                socket.OnMessage = msg =>
                {
                    PrintLog(string.Format("{0}说：{1}", user.Name, msg));
                    _list.ForEach(m =>
                    {
                        m.Send(string.Format("{0}说：{1}", user.Name, msg));
                    });
                };
            });

            this.textBox2.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var item in _list)
            {
                item.Send(string.Format("管理员说：{0}", this.textBox2.Text));
            }

            PrintLog(string.Format("管理员说：{0}", this.textBox2.Text));
            this.textBox2.Text = string.Empty;
        }

        /// <summary>
        /// 打印日志.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="tabIndex">输出打印文字的页签下标.</param>
        private void PrintLog(string str)
        {
            this.textBox1.Invoke(new Action(() =>
            {
                this.textBox1.Text = DateTime.Now.ToString("HH:mm:ss") + " " + string.Format("{0} \r\n", str) + this.textBox1.Text;
            }));
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
