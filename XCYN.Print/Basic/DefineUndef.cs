using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.Basic
{
    /// <summary>
    /// 预编译指令
    /// </summary>
    public class DefineUndef
    {
        [Conditional("Debug")]
        public static void FunDebug()
        {
            Console.WriteLine("执行FunDefine方法");
        }

        [Conditional("Error")]
        public static void FunError()
        {
            Console.WriteLine("执行FunError方法");
        }

        /// <summary>
        /// 预编译了Error或Debug
        /// </summary>
        [Conditional("Error"), Conditional("Debug")]
        public static void FunErrorOrDebug()
        {
            Console.WriteLine("执行FunErrorOrDebug方法");
        }

        /// <summary>
        /// 预编译了Error和Debug
        /// </summary>
        [Conditional("ErrorAndDebug")]
        public static void FunErrorAndDebug()
        {
            Console.WriteLine("执行ErrorAndDebug方法");
        }
        
    }
}
