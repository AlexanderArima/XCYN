using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;

namespace XCYN.Service.WCF
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“ServiceFly”。
    public class ServiceFly : IServiceFly
    {

        public Student fly(Student student)
        {
            //DataClasses1DataContext context = new DataClasses1DataContext();

            //context.zcp_info.InsertOnSubmit(new zcp_info() {
            //    user_id = 2,
            //    add_time = DateTime.Now,
            //    formula_name = "标准公式"
            //});

            //context.SubmitChanges();

            //DataClasses1DataContext context2 = new DataClasses1DataContext();

            //context2.zcp_info.InsertOnSubmit(new zcp_info()
            //{
            //    user_id = 3,
            //    add_time = DateTime.Now,
            //    formula_name = "标准公式"
            //});

            //context2.SubmitChanges();

            Console.WriteLine("方法执行成功!");

            //序列化
            //DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Student));
            DataContractSerializer serializer = new DataContractSerializer(typeof(Student));
            using (FileStream stream = new FileStream("123.json", FileMode.Create))
            {
                serializer.WriteObject(stream, student);
            }
            return new Student() {
                Name = "cheng",
                Age = 18
            };
        }
    }

    [DataContract]
    public class Student
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Age { get; set; }
    }
    
}
