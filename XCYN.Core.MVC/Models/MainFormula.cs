using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XCYN.Core.MVC.Models
{
    public class MainFormula : IFormula
    {
        public int ID {
            get  {
                //从后台取值
                return 1;
            }
            set {
                //设置值
                ID = 1;
            }
        }

        public string kjqs {
            get {
                return "120";
            }
            set {
                kjqs = "120";
            }
        }
        public DateTime kjrq {
            get {
                return new DateTime(2017, 12, 12);
            }
            set {
                kjrq = new DateTime(2017, 12, 12);
            }
        }

        public int zm1 { get; set; }
        public int zm2 { get; set; }
        public int zm3 { get; set; }
        public int zm4 { get; set; }
        public int zm5 { get; set; }
        public int zm6 { get; set; }
        public int tm { get; set; }
    }
}
