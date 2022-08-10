using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Service.Library.Model
{
    public class AtomicInteger
    {
        public int count { get; set; }

        /// <summary>
        /// 设置次数.
        /// </summary>
        /// <param name="count"></param>
        public void Set(int count)
        {
            this.count = count;
        }

        /// <summary>
        /// 自增并返回次数.
        /// </summary>
        /// <returns></returns>
        public int IncrementAndGet()
        {
            return count++;
        }
    }
}
