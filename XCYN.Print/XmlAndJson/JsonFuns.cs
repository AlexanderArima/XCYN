using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.XmlAndJson
{
    public class JsonFuns
    {
        public void Fun1()
        {
            JObject obj = new JObject();
            obj["Name"] = "戴尔笔记本";
            obj["ID"] = 1;
            obj["C_ID"] = 11;
            obj["Price"] = 4000;

            JObject obj2 = new JObject();
            obj2["Name"] = "联想笔记本";
            obj2["ID"] = 2;
            obj2["C_ID"] = 11;
            obj2["Price"] = 3000;

            JArray array = new JArray();
            array.Add(obj);
            array.Add(obj2);

            Console.WriteLine(array);
            Console.Read();
        }

        public void Fun2()
        {
            JsonSerializer serializer = JsonSerializer.Create();
        }
    }
}
