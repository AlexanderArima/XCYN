using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using XCYN.Common.Sql.redis;

namespace XCYN.Web.Model
{
    class XSession
    {
        //Session外部调用
        private static HttpSessionState session;

        public static HttpSessionState GetSession()
        {
            return session;
        }

        public static void SetSession(HttpSessionState session)
        {
            XSession.session = session;
            //dict[this.session.SessionID] = session;
        }

        /// <summary>
        /// 存储所有用户的Session
        /// </summary>
        public static Dictionary<string, HttpSessionState> dict = new Dictionary<string, HttpSessionState>();

        private static XSession instance;

        private XSession() { }

        /// <summary>
        /// 单例
        /// </summary>
        /// <returns></returns>
        public static XSession GetInstance()
        {
            if(instance == null)
            {
                instance = new XSession();
            }
            return instance;
        }

        /// <summary>
        /// 单例
        /// </summary>
        /// <returns></returns>
        public static XSession GetInstance(HttpSessionState session)
        {
            if (instance == null)
            {
                XSession.instance = new XSession();
                XSession.session = session;
            }
            return instance;
        }

        public XUser UserInfo {
            get
            {
                //登陆未完成，省去验证过程，直接记录到缓存中
                RedisCommand command = new RedisCommand();
                try
                {
                    if (command.KeyExists(session.SessionID))
                    {
                        return null;
                    }
                }
                catch(Exception ex)
                {
                    throw;
                }
                var ID = Convert.ToInt32(command.HashGet(session.SessionID, "ID"));
                var UserName = command.HashGet(session.SessionID, "UserName");
                var Age = Convert.ToInt32(command.HashGet(session.SessionID, "Age"));
                XUser user = new XUser
                {
                    ID = ID,
                    UserName = UserName,
                    Age = Age
                };
                return user;
            }
            set
            {
                RedisCommand command = new RedisCommand();
                command.HashSet(session.SessionID, "ID", value.ID.ToString());
                command.HashSet(session.SessionID, "UserName", value.UserName);
                command.HashSet(session.SessionID, "Age", value.Age.ToString());
            }
        }

    }
    
}