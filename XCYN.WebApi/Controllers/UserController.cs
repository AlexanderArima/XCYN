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
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/User
        public IDictionary<string,object> Post(string UserName,string PassWord,string Sex,string Year,string Month,string Day)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("stateCode", 1);
            //var json = "cb(" + JsonConvert.SerializeObject(dict) + ")";
            //return json;
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
}
