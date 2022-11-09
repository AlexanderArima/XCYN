using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCYN.Common;

namespace XCYN.Winform.WebSocket
{
    public class OnlineUser
    {
        /// <summary>
        /// 在线用户的集合.
        /// </summary>
        public static List<OnlineUser> List_OnlineUser = new List<OnlineUser>();

        /// <summary>
        /// 在线用户的唯一id
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 上次发言时间
        /// </summary>
        public DateTime? ActiveTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 创建一个新的连接
        /// </summary>
        public static OnlineUser Create()
        {
            try
            {
                OnlineUser result = new OnlineUser();
                var name = FleckViewModel.GetRandomName();
                var id = Guid.NewGuid().ToString();
                result.ID = id;
                result.Name = name;
                List_OnlineUser.Add(result);
                return result;
            }
            catch(Exception ex)
            {
                Log4NetHelper.Error("OnlineUser：Create 出错：" + ex.Message, ex);
                return null;
            }
        }

        public static OnlineUser Get(string id)
        {
            var result = List_OnlineUser.Find(m => m.ID.Equals(id));
            if(result == null)
            {
                return null;
            }
            else
            {
                return result;
            }
        }
    }
}
