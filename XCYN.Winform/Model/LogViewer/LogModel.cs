using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using Newtonsoft.Json;

namespace XCYN.Winform.Model.LogViewer
{
    public class LogModel
    {
        public static string LOGLOCATION = ConfigurationManager.AppSettings["LogLocation"];
        /// <summary>
        /// 异常时间
        /// </summary>
        public DateTime createTime { get; set; }

        /// <summary>
        /// 异常等级
        /// </summary>
        public string level { get; set; }

        /// <summary>
        /// 输出的日志消息
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// 引发日志请求的类的全名
        /// </summary>
        public string className { get; set; }

        /// <summary>
        /// 发生日志请求的方法名
        /// </summary>
        public string methodName { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        public string exception { get; set; }

        public void GetJson()
        {
            List<LogModel> list = new List<LogModel>();
            LogModel model = new LogModel()
            {
                className = "XCYN.Test.Log4NetTestCase",
                createTime = DateTime.Now,
                exception = "System.NullReferenceException: 名字不能为空",
                level = "INFO",
                message = "消息",
                methodName = "TestMethod1"
            };
            list.Add(model);
            list.Add(model);

            var json = JsonConvert.SerializeObject(list);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="level"></param>
        /// <param name="startTime">异常其时时间</param>
        /// <param name="endTime">异常结束时间</param>
        /// <returns></returns>
        public List<LogModel> GetList(string message = "", string level = "",DateTime? startTime = null,DateTime? endTime = null)
        {
            //确认文件是否存在
            if (!File.Exists(LogModel.LOGLOCATION))
            {
                throw new FileNotFoundException(string.Format("文件:{0}，不存在或没有访问权限", LogModel.LOGLOCATION));
            }
            try
            {
                var json = File.ReadAllText(LogModel.LOGLOCATION, Encoding.UTF8);
                json = "[" + json.Remove(json.Length - 1, 1) + "]";
                var jsonModel = JsonConvert.DeserializeObject<List<LogModel>>(json);
                jsonModel = jsonModel.FindAll(m => {
                    TimeSpan span = new TimeSpan();
                    if(message == null || m.message != message)
                    {
                        return false;
                    }
                    if(level == null || m.level != level)
                    {
                        return false;
                    }
                    if(createTime == null && endTime == null)
                    {
                        return true;
                    }
                    if(createTime == null && endTime != null && createTime > endTime)
                    {
                        return false;
                    }
                    if(createTime != null && createTime < startTime && endTime == null)
                    {
                        return false;
                    }
                    if(createTime != null && endTime != null && (createTime > endTime || createTime < startTime))
                    {
                        return false;
                    }
                    return true;
               });
                return jsonModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LogModel> GetList()
        {
            return GetList("", "", null, null);
        }
    }
}
