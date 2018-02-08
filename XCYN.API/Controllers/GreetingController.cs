using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace XCYN.API.Controllers
{
    public class GreetingController : ApiController
    {

        public static List<Greeting> _list_greet = new List<Greeting>();

        [HttpGet]
        public string GetGreeting()
        {
            return "Hello World";
        }

        [HttpGet]
        public string GetGreeting(string id)
        {
            var greeting = _list_greet.FirstOrDefault(g => g.Name == id);
            if(greeting == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotAcceptable);
            }
            return greeting.Message;
        }

        [HttpPost]
        public HttpResponseMessage PostGreeting(Greeting greeting)
        {
            _list_greet.Add(greeting);
            var location = new Uri(this.Request.RequestUri, "greeting/" + greeting.Name);
            var response = this.Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Location = location;
            return response;
        }
    }

    public class Greeting
    {
        public string Name { get; set; }

        public string Message { get; set; }
    }
}
