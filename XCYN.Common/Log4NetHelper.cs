using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Common
{
    public class Log4NetHelper
    {
        //定义信息的二次处理
        public static event Action<string> OutputMessage;
        //ILog对象
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region 定义信息二次处理方式
        private static void HandMessage(object Msg)
        {
            if (OutputMessage != null)
            {
                OutputMessage(Msg.ToString());
            }
        }
        private static void HandMessage(object Msg, Exception ex)
        {
            if (OutputMessage != null)
            {
                OutputMessage(string.Format("{0}:{1}", Msg.ToString(), ex.ToString()));
            }
        }
        private static void HandMessage(string format, params object[] args)
        {
            if (OutputMessage != null)
            {
                OutputMessage(string.Format(format, args));
            }
        }
        #endregion

        #region 封装Log4net
        public static void Debug(object message)
        {
            HandMessage(message);
            if (log.IsDebugEnabled)
            {
                log.Debug(message);
            }
        }
        public static void Debug(object message, Exception ex)
        {
            HandMessage(message, ex);
            if (log.IsDebugEnabled)
            {
                log.Debug(message, ex);
            }
        }
        public static void DebugFormat(string format, params object[] args)
        {
            HandMessage(format, args);
            if (log.IsDebugEnabled)
            {
                log.DebugFormat(format, args);
            }
        }
        public static void Error(object message)
        {
            HandMessage(message);
            if (log.IsErrorEnabled)
            {
                log.Error(message);
            }
        }
        public static void Error(object message, Exception ex)
        {
            HandMessage(message, ex);
            if (log.IsErrorEnabled)
            {
                log.Error(message, ex);
            }
        }
        public static void ErrorFormat(string format, params object[] args)
        {
            HandMessage(format, args);
            if (log.IsErrorEnabled)
            {
                log.ErrorFormat(format, args);
            }
        }
        public static void Fatal(object message)
        {
            HandMessage(message);
            if (log.IsFatalEnabled)
            {
                log.Fatal(message);
            }
        }
        public static void Fatal(object message, Exception ex)
        {
            HandMessage(message, ex);
            if (log.IsFatalEnabled)
            {
                log.Fatal(message, ex);
            }
        }
        public static void FatalFormat(string format, params object[] args)
        {
            HandMessage(format, args);
            if (log.IsFatalEnabled)
            {
                log.FatalFormat(format, args);
            }
        }
        public static void Info(object message)
        {
            HandMessage(message);
            if (log.IsInfoEnabled)
            {
                log.Info(message);
            }
        }
        public static void Info(object message, Exception ex)
        {
            HandMessage(message, ex);
            if (log.IsInfoEnabled)
            {
                log.Info(message, ex);
            }
        }
        public static void InfoFormat(string format, params object[] args)
        {
            HandMessage(format, args);
            if (log.IsInfoEnabled)
            {
                log.InfoFormat(format, args);
            }
        }
        public static void Warn(object message)
        {
            HandMessage(message);
            if (log.IsWarnEnabled)
            {
                log.Warn(message);
            }
        }
        public static void Warn(object message, Exception ex)
        {
            HandMessage(message, ex);
            if (log.IsWarnEnabled)
            {
                log.Warn(message, ex);
            }
        }
        public static void WarnFormat(string format, params object[] args)
        {
            HandMessage(format, args);
            if (log.IsWarnEnabled)
            {
                log.WarnFormat(format, args);
            }
        }
        #endregion
    }
}
