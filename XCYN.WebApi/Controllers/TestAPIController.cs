using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XCYN.WebApi.Models;

namespace XCYN.WebApi.Controllers
{
    public class TestAPIController : ApiController
    {

        public UserInfoRestule SetUserInfo(UserInfo dto)
        {
            UserInfoRestule info = new UserInfoRestule();
            info.UserId = dto.UserId;
            info.UserName = dto.UserName;
            info.Age = 18;
            info.Email = "rice@153.com";
            return info;
        }

        /// <summary>
        /// 删除用户id
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("TestAPI/DeleteByID")]
        public ReceiveObject DeleteByID(string id)
        {
            return new ReceiveObject
            {
                code = 0,
                msg = "",
            };
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="obj">用户对象</param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("TestAPI/Save")]
        public ReceiveObject Save([FromBody]UserInfoRestule obj)
        {
            return new ReceiveObject
            {
                code = 0,
                msg = "",
            };
        }

        //// POST TestAPI/post
        //public ReceiveObject Post([FromBody]UserInfoRestule obj)
        //{
        //    UserInfoRestule info = obj;
        //    return new ReceiveObject
        //    {
        //        code = 999999,
        //        msg = "数据异常",
        //    };
        //}

        // GET: api/User
        public IEnumerable<string> Get(int id)
        {
            return new string[] { "value1", "value2" };
        }
    }
}