using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Service.Library.Model
{
    public class IPAddressRight
    {
        public IPAddressRight(string address, int right)
        {
            this.Address = address;
            this.Right = right;
        }

        /// <summary>
        /// 地址.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 权限级别.
        /// </summary>
        public int Right { get; set; }

        /// <summary>
        /// 调用次数.
        /// </summary>
        public int Count { get; set; }
    }
}
