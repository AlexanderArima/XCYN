using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace XCYN.Common
{
    public class SMSHelper
    {

        /// <summary>
        /// 行为
        /// </summary>
        public string ac { get; set; }

        /// <summary>
        /// 账户
        /// </summary>
        protected string u_id { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string pwd { get; set; }

        /// <summary>
        /// 模版ID
        /// </summary>
        public int template { get; set; }

        /// <summary>
        /// 要发送的手机号
        /// </summary>
        public string mobile { get; set; }

        /// <summary>
        /// 短信内容占位符
        /// </summary>
        public Dictionary<string,string> content { get; set; }
       
        public SMSHelper()
        {
            this.u_id = "lhcmutou";
        }

        public SMSHelper(string pwd,int template,string mobile,Dictionary<string,string> content)
        {
            this.u_id = "lhcmutou";
            this.pwd = pwd;
            this.template = template;
            this.mobile = mobile;
            this.content = content;
        }

        public void Send()
        {
            Send(this);
        }

        public void Send(SMSHelper helper)
        {
            //验证用户和游客是否达到今日最大短信次数
            StringBuilder url = new StringBuilder("http://api.sms.cn/sms/?ac=send&");
            url.Append("uid=" + helper.u_id + "&");
            url.Append("pwd=" + helper.pwd + "&");
            url.Append("template=" + helper.template + "&");
            url.Append("mobile=" + helper.mobile + "&");
            url.Append("content=" + HttpUtility.UrlEncode(Common.JsonHelper.ObjectToJSON(helper.content)));
            Common.Utils.HttpGet2(url.ToString());
        }
    }
}
