using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.delegates
{
    /// <summary>
    /// 事件Event
    /// </summary>
    public class EventTest
    {
        public void Fun1()
        {
            var dealer = new CarDealer();

            var ann = new Consumer("Ann");
            dealer.NewCarInfo += ann.NewCarIsHere;//Ann订阅了消息
            dealer.NewCar("Mercede");//发布了新的消息，Ann收到消息

            var John = new Consumer("John");
            dealer.NewCarInfo += John.NewCarIsHere;//John订阅了消息
            dealer.NewCar("Ferrari");//发布了新的消息，Ann和John都收到了消息

            dealer.NewCarInfo -= John.NewCarIsHere;//John取消了订阅
            dealer.NewCar("Red Bull");//发布了新的消息，Ann收到消息
        }
    }

    /// <summary>
    /// 消息发布者
    /// </summary>
    public class CarDealer
    {
        public event EventHandler<CarInfoEventArgs> NewCarInfo;

        public void NewCar(string car)
        {
            Console.WriteLine($"CarDealer,new car {car}");
            NewCarInfo?.Invoke(this, new CarInfoEventArgs(car));
        }
    }

    /// <summary>
    /// 消息监听器，给订阅者订阅消息的
    /// </summary>
    public class CarInfoEventArgs:EventArgs
    {

        public CarInfoEventArgs(string car)
        {
            Car = car;
        }

        public string Car { get; set; }
    }

    public class Consumer
    {
        private string _name;

        public Consumer(string name)
        {
            _name = name;
        }

        public void NewCarIsHere(object sender,CarInfoEventArgs e)
        {
            Console.WriteLine($"{_name}:car {e.Car} is new ");
        }
    }

}
