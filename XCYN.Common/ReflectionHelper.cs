using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Common
{
    public class ReflectionHelper
    {
        public static object InvokeMethod(string assemblyName, string nameSpace, string className, string methodName,object[] methodParams = null)
        {
            string path = nameSpace + "." + className + "," + assemblyName; //命名空间.类型名,程序集
            Type o = Type.GetType(path);//加载类型
            object obj = Activator.CreateInstance(o, true); //根据类型创建实例
            MethodInfo method = o.GetMethod(methodName);
            var result = method.Invoke(obj, methodParams);  //调用方法
            return result;
        }
    }
}
