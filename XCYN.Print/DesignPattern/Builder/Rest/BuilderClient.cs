using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.DesignPattern.Builder.Rest
{
    /// <summary>
    ///  使用者
    /// </summary>
    public class BuilderClient
    {
        public void Fun1()
        {
            Meal meal = new Meal();
            meal.Add(new HongShaoPaiGu());
            meal.Add(new BanLiShaoJi());
            meal.Add(new YuXiangRouSi());
            meal.Show();
        }
    }
}
