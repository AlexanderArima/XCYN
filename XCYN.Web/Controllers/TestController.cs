using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XCYN.Web.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return View(new Student() { ID = 1,Name = "jack",Age = 18 });
        }

        [HttpPost]
        public ActionResult Index([Bind(Exclude = "ID")]Student stu)
        {
            return View(stu);
        }
    }

    public class Student:IValidatableObject
    {
        public int ID { get; set; }

        public String Name { get; set; }
   
        public int Age { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(Age % 2 == 0)
            {
                //第一个参数显示的是ValidationSummary的总结提示
                //第二个参数只显示Age字段的验证提示信息
                //在 yield return 语句中，将计算 expression 并将结果以值的形式返回给枚举器对象
                var result = new ValidationResult("age是偶数",new string[] { "Age" });
                yield return result;
            }
            if(Name.Length < 2 || Name.Length > 50)
            {
                var result = new ValidationResult("名称长度在2-50之间", new string[] { "Name" });
                yield return result;
            }
        }
    }

    //自定义验证
    public class MyCostomAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            int num = new Random().Next(1, 100);
            if(num % 2 == 0)
            {
                return true;
            }
            else
            {
                ErrorMessage = "随机数为奇数";
                return false;
            }
        }
    }

}