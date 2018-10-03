using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using XCYN.Common.Sql.redis;

namespace XCYN.Web.Model
{
    public class XSession : ISession
    {
        private static XSession _instance;
        private static object _lockMe = new object();

        private XSession()
        {

        }

        public static XSession GetInstance()
        {
            //双检锁
            if (_instance == null)
            {
                lock (_lockMe)
                {
                    if (_instance == null)
                    {
                         _instance = new XSession();
                        _instance.UserInfo = new XUser();
                    }
                }
            }
            return _instance;
        }
        
        public XUser UserInfo {
            get
            {
                //登陆未完成，省去验证过程，直接记录到缓存中
                RedisCommand command = new RedisCommand();
                if (!command.HashExists(session.SessionID, "UserName"))
                {
                    return null;
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

        public HttpSessionState session { get ; set ; }
    }
    
}