using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.EF
{
    public class SchoolEntity :DbContext
    {

        public SchoolEntity():base("data source=.;initial catalog=SchoolDB3;integrated security=True")
        {
            Database.SetInitializer<SchoolEntity>(new MyDropCreateDatabaseAlways());
        }

        public DbSet<Persons> Persons { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        
    }

    public class MyDropCreateDatabaseAlways : DropCreateDatabaseAlways<SchoolEntity>
    {
        public override void InitializeDatabase(SchoolEntity context)
        {
            base.InitializeDatabase(context);
        }

        protected override void Seed(SchoolEntity context)
        {
            for(int i = 0;i < 10;i++)
            {
                Persons person = new Persons()
                {
                    name = "jack" + i,
                    add_time = DateTime.Now
                };

                context.Persons.Add(person);
            }
            context.SaveChanges();
            base.Seed(context);
        }
    }

 }
