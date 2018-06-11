using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace XCYN.WebApi.Controllers
{
    public class UserController : ApiController
    {
        // GET: api/User
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/User/5
        public IDictionary<string,object> Get(User user)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("stateCode", 1);
            return dict;
        }
        
        // POST: api/User
        //public IDictionary<string,object> Post(string action,string UserName,string PassWord,
        //    string Sex,string Year,string Month,string Day)
        public IDictionary<string,object> Post(User user)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("stateCode", 1);
            return dict;
        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
        }
    }

    public class User
    {
        public string action { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Sex { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string Day { get; set; }
    }
}
