using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.MianShiTi
{
    public class RefOut
    {
    }

    /// <summary>
    /// ref能出能进，out只出不进
    /// </summary>
    public class Room
    {
        public void GetPrice(ref int id)
        {
            id = 10;
            Console.WriteLine(id);
        }

        public void GetAge(out int age)
        {
            age = 10;
            Console.WriteLine(age);
        }
    }
}
