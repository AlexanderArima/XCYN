using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using XCYN.EF.Model;

namespace XCYN.EF
{
    class Program
    {
        static void Main(string[] args)
        {
            //using(SchoolDBEntities entity = new SchoolDBEntities())
            //{
            //    entity.Database.Log = Console.WriteLine;
            //group，by，into的运行
            //var query = from item in entity.StudentAddresses
            //            group item by item.City
            //            into s
            //            select s;

            //foreach(var item in query)
            //{
            //    Console.WriteLine(item.Key);

            //    Console.WriteLine(item.ToList().Count);
            //}

            //join，on，equal，order的运用
            //var query = from item in entity.Students
            //            join address in entity.StudentAddresses
            //            on item.StudentID equals address.StudentID
            //            orderby item.StudentID descending
            //            select new { item, address };
            //foreach (var item in query)
            //{
            //    Console.WriteLine(item.item);

            //    Console.WriteLine(item.address);
            //}

            //let的运用
            //var query = from item in entity.Students
            //            let len = item.StudentName.Length
            //            select new { len = len, s = item };
            //foreach(var item in query)
            //{
            //    Console.WriteLine(item.len);
            //    Console.WriteLine(item.s);
            //}

            //lambda表达式select方法
            //var query = entity.Students.Select(i => i);
            //foreach(var item in query)
            //{
            //    Console.WriteLine(item.StudentID + ":" +item.StudentName);
            //}

            //lambda表达式groupby和groupjoin方法
            //var query = entity.StudentAddresses.GroupBy(i => i.City);
            //foreach (var item in query)
            //{
            //    Console.WriteLine(item.Key);

            //    Console.WriteLine(item.ToList());
            //}

            //GroupJoin相当于left join
            //var query = entity.Teachers.GroupJoin(entity.Courses, (Teacher t) => t.TeacherId, (Course c) => c.TeacherId, (t, list) => new
            //{
            //    t.TeacherName,
            //    list
            //});

            //foreach (var item in query)
            //{
            //    Console.WriteLine(item.TeacherName);

            //    Console.WriteLine(item.list);
            //}

            //lambda表达式join方法(相当于inner join)
            //var query = entity.Teachers.Join(entity.Courses, (Teacher t) => t.TeacherId, (Course c) => c.TeacherId, (t, list) => new
            //{
            //    t.TeacherName,
            //    list
            //});
            //foreach (var item in query)
            //{
            //    Console.WriteLine(item.TeacherName);

            //    Console.WriteLine(item.list);
            //}

            //使用Native SQL查询数据
            //var query = entity.Database.SqlQuery<TeachCourse>(@"select * from Course left join Teacher 
            //                                               on Course.TeacherId = Teacher.TeacherId").ToList();

            //entity.Database.Log = Console.WriteLine;

            //entity.Teachers.Add(new Teacher
            //{
            //    TeacherName = "谢老师",
            //});

            //entity.Courses.Add(new Course {
            //    CourseName = "计算机"
            //});

            //entity.SaveChanges();

            //开启事务，第一种方法
            //entity.Database.Log = Console.WriteLine;

            //entity.Database.BeginTransaction();

            //entity.Students.Add(new Student
            //{
            //    StudentName = "叶影",

            //});

            //entity.Students.Add(new Student
            //{
            //    StudentName = "莫言",

            //});

            //entity.SaveChanges();

            //entity.Database.CurrentTransaction.Commit();

            //开启事务第二种方法
            //entity.Database.Log = Console.WriteLine;

            //using (TransactionScope ts = new TransactionScope())
            //{

            //    entity.Students.Add(new Student
            //    {
            //        StudentName = "柯南",
            //    });

            //    entity.Students.Add(new Student
            //    {
            //        StudentName = "佐助",
            //    });

            //    entity.SaveChanges();

            //    ts.Complete();
            //}

            //EF的数据三种加载方式

            //Explicti Loading
            //entity.Configuration.LazyLoadingEnabled = false;//关闭延迟加载
            //entity.Database.Log = Console.WriteLine;
            //var stu = entity.Students.FirstOrDefault();
            //entity.Entry(stu).Reference(s => s.StudentAddress).Load();
            //var course = stu.Courses;

            //Eager Loading
            //entity.Configuration.LazyLoadingEnabled = false;//关闭延迟加载
            //entity.Database.Log = Console.WriteLine;
            //var stu = entity.Students.Include("Courses").Include("StudentAddress").FirstOrDefault();
            //var course = stu.Courses;

            //三种加载方式。Lazy Loading会生成大量的sql，Eager Loading生成的关联查询比较负责，
            //Explicit Loading同Lazy Loading一样生成很多的sql，但是有一些其他优点，比如：导航属性可以不用标注为virtual。
            //如果这几种关联都不能解决实际问题，可以直接使用sql查询。

            //映射存储过程

            //var stu = entity.Students.Add(new Student {
            //    StudentName = "虚空"
            //});
            //entity.SaveChanges();

            //映射枚举
            //var tea = entity.Teachers.Add(new Teacher {
            //    TeacherName = "毕老师",
            //    type = TeacherTypeEnum.One
            //});
            //entity.SaveChanges();

            //Entry跟踪
            //var str = entity.Students.FirstOrDefault();
            //str.StudentName = "小樱";
            //var entry1 = entity.Entry(str);
            //entity.Students.Add(str);
            //var entry2 = entity.Entry(str);

            //ChangeTracker跟踪
            //var str = entity.Students.FirstOrDefault();
            //str.StudentName = "小樱";

            //entity.Students.Add(new Student {
            //    StudentName = "鸣人"
            //});

            //var track = entity.ChangeTracker;
            //foreach(var item in track.Entries())
            //{
            //    Console.WriteLine("state:" + item.State);
            //    if (item.State != System.Data.Entity.EntityState.Added)
            //    {
            //        Console.WriteLine("OriginalValues:" + item.OriginalValues["StudentName"]);
            //    }
            //    Console.WriteLine("CurrentValues:"+item.CurrentValues["StudentName"]);
            //}

            //Local跟踪
            //var str = entity.Students.FirstOrDefault();
            //str.StudentName = "小樱";

            //entity.Students.Add(new Student
            //{
            //    StudentName = "鸣人"
            //});

            //entity.SaveChanges();

            //var local = entity.Students.Local;
            //foreach (var item in local)
            //{
            //    Console.WriteLine("StudentName:" + item.StudentName + " and StudentID:"+item.StudentID+" with state " + entity.Entry(item).State);
            //}

            //entity.Database.Connection.ConnectionString = "Data Source=.;Initial Catalog=SchoolDB;Integrated Security=True";
            //var student = entity.Students.FirstOrDefault();
            //student.StudentName = "test";
            //entity.SaveChanges();

            //Console.ReadKey();
            //}

            
            using (SchoolEntity entity = new SchoolEntity())
            {
                entity.Database.Log = Console.WriteLine;
                Persons person = new Persons() {
                    name = "jack",
                    age = 12,
                    state = 1,
                    add_time = DateTime.Now
                };
                entity.Persons.Add(person);
                entity.SaveChanges();
                Console.Read();

            }
        }

        
    }

    public class TeachCourse
    {
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public Nullable<int> StandardId { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public System.Data.Entity.Spatial.DbGeography Location { get; set; }
    }
}
