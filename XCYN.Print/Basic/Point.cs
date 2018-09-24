using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.Basic
{
    public struct Point
    {
        public decimal X;
        public decimal Y;

        public Point(decimal X,decimal Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public Point(decimal X)
        {
            this.X = 0;
            this.Y = 0;
        }
    }

    /// <summary>
    /// 定义一个枚举
    /// </summary>
    public enum Fruit
    {
        Apple = 1,
        Banana = 2,
        Orange = 3,
        Peach = 4,  //桃子
        Grape = 5,  //葡萄
    }
}
