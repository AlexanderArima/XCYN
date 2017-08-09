using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XCYN.EasyUI.ViewModel
{
    public class NavViewModel
    {
        /// <summary>
        /// id唯一标识
        /// </summary>
        public int id { get; set; }
        
        /// <summary>
        /// 标题内容
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string iconCls { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        public List<NavViewModel> children { get; set; }

        /// <summary>
        /// 是否展开
        /// </summary>
        public string state { get; set; }
    }

    public enum TREE_STATE
    {
        open = 1,
        closed = 2,
    }
}