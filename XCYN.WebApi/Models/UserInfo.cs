using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XCYN.WebApi.Models
{
    public class UserInfo
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
    }
    
    public class UserInfoRestule : UserInfo
    {
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 电子邮箱地址
        /// </summary>
        public string Email { get; set; }
    }
}