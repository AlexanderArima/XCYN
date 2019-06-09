using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using XCYN.Common;
using XCYN.Print.DesignPattern.Bridge;
using XCYN.Print.DesignPattern.ChainOfResponsibility;
using XCYN.Print.DesignPattern.Command;
using XCYN.Print.DesignPattern.Composite;
using XCYN.Print.DesignPattern.Factory;
using XCYN.Print.DesignPattern.Filter;
using XCYN.Print.DesignPattern.Flyweight;
using XCYN.Print.DesignPattern.Mediator;
using XCYN.Print.DesignPattern.Memento;
using XCYN.Print.DesignPattern.Observer;
using XCYN.Print.DesignPattern.Proxy;
using XCYN.Print.DesignPattern.State;
using XCYN.Print.DesignPattern.Strategy;
using XCYN.Print.FileSystem;
using XCYN.Print.Generics;
using XCYN.Print.MianShiTi;
using XCYN.Print.MultiThread;
using XCYN.Print.Operators;
using System.Linq;
using System.Text.RegularExpressions;
using XCYN.Print.delegates;
using XCYN.Print.Alg;
using XCYN.Print.WCF;
using XCYN.Print.MongoDB;
using XCYN.Print.Basic;
using System.Threading;
using XCYN.Print.MySQL;
using XCYN.Print.DesignPattern.Builder.Rest;
using XCYN.Print.DesignPattern.Adapter.Object;
using XCYN.Print.rabbitmq;
using XCYN.Print.Quartz;
using System.Collections;
using XCYN.Common.Sql.redis;

namespace XCYN.Print
{
    class Program
    {
        static void Main(string[] args)
        {
            
            RedisCommand command = new RedisCommand();
            var flag = command.KeyExistsAsync("city", 1).Result;
            if(flag == true)
            {
                command.KeyDelete("city", 1);
            }
            Console.Read();
        }

        static void Box()
        {
            object i = 10;
            Console.WriteLine(i);
        }

        static void Unbox()
        {
            object i = 10;
            int s = (int)i;
            Console.WriteLine(s);
        }

        static void DefEnum()
        {
            Fruit f = Fruit.Apple;
            int obj = Convert.ToInt32(Fruit.Apple);
            Console.WriteLine("Fruit:{0}",Enum.GetName(typeof(Fruit),f));   //Enum.GetName()获取枚举的名称
            Console.WriteLine("Fruit:{0}", obj);   
            Console.Read();
        }

        /// <summary>
        /// 定义一个结构体
        /// </summary>
        static void DefStruct()
        {
            Point point = new Point();
            point.X = 1.1M;
            point.Y = 2.2M;
            Console.WriteLine("X:{0},Y:{1}", point.X, point.Y);
            Console.Read();
        }

        private static void RefOut()
        {
            XCYN.Print.MianShiTi.Room room = new XCYN.Print.MianShiTi.Room();
            //int id = 0;
            //room.GetPrice(ref id);
            //Console.WriteLine(id);

            //int age;
            //room.GetAge(out age);
            //Console.WriteLine(age);
        }

        /// <summary>
        /// 索引器
        /// </summary>
        private static void Indexer()
        {
            Indexer indexer = new Indexer();
            indexer[0] = 1;
            indexer[1] = 2;
            indexer[2] = 3;
            indexer[3] = 4;
            indexer[4] = 5;
            Console.WriteLine(indexer[0].ToString() + indexer[1].ToString() + indexer[2].ToString() + 
                indexer[3].ToString() + indexer[4].ToString());

            Console.WriteLine(indexer["a"] + "\n" + indexer["b"]);
        }

        private static void HandleWCFService()
        {
            //BasicOperation basic = new BasicOperation();
            //basic.Fun2();
            //HandleBinding binding = new HandleBinding();
            //binding.Fun1();
            //HandleAllClient client = new HandleAllClient();
            //client.Fun1();
            //HandleOrderClient service = new HandleOrderClient();
            //service.Fun1();
            //HandleStockClient service = new HandleStockClient();
            //service.Fun3();
            //SortOrderTest.HeapSortVsQuick();
        }

        private static void Algorithm()
        {
            Algorithm a = new Algorithm();
            ChainTree<string> root = new ChainTree<string>();
            root.left = new ChainTree<string>();
            root.data = "根节点";
            root.left.data = "左子树";
            root.left.left = new ChainTree<string>();
            root.left.left.data = "左子树的左子树";
            root.right = new ChainTree<string>();
            root.right.data = "右子树";
            //a.TreeDRT(root);//先序遍历
            //a.TreeLRT(root);//中序遍历
            a.TreeLRD(root);//后序遍历
            Console.Read();
        }

        private static void XMLAndJson()
        {
            //MyXmlSerializer xml = new MyXmlSerializer();
            //xml.Fun1();

            //XmlFuns funs = new XmlFuns();
            //funs.Fun9();

            //LinqToXML linq = new LinqToXML();
            //linq.Fun2();

            //JsonFuns json = new JsonFuns();
            //json.Fun1();
        }

        private static void DirectoryDemo()
        {
            MyDirectory myDirectory = new MyDirectory();
            //myDirectory.Fun5(Path.Combine(@"D:\迅雷下载", @"ReadMe.txt"), Path.Combine(@"D:\迅雷下载", @"Copy.txt"));
            //Console.Read();
            //string[] array = {"中国第一","美国第一"};
            //myDirectory.Fun7(array);
            //myDirectory.Fun9(Path.Combine(@"D:\迅雷下载", "ReadMe.txt"), Path.Combine(@"D:\迅雷下载", "Compress.txt"));
            Console.Read();
        }

        private static void FileDemo()
        {
            MyFile demo = new MyFile();
            demo.Fun5();
            Console.Read();
            
        }

        private static void TanXin()
        {
            while(true)
            {
                Console.WriteLine("请输入找零金额:");
                var money = Convert.ToDecimal(Console.ReadLine());
                var dict = XCYN.Print.MianShiTi.Algorithm.ExChange(money);
                foreach (var item in dict)
                {
                    Console.WriteLine($"{item.Key}元纸币{item.Value}张");
                }
            }
        }

        /// <summary>
        /// 算法题
        /// </summary>
        private static void AlgroithmTest()
        {
            while (true)
            {
                Console.WriteLine("请输入一个求阶乘的整数");
                var n = Console.ReadLine();
                var num = XCYN.Print.MianShiTi.Algorithm.Fact(Convert.ToInt32(n));
                Console.WriteLine($"{n}的阶乘为:{num}");
            }
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        private static void InvokeLog4Net()
        {
            InitLog4Net();

            var logger = LogManager.GetLogger(typeof(Program));
            logger.Info("消息");
            logger.Warn("警告");
            logger.Error("异常");
            logger.Fatal("错误");
            logger.Debug("调试");
            GenericsFuncTest();
            Console.ReadLine();
        }

        private static void InitLog4Net()
        {
            var logCfg = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
        }

        private static void GenericsFuncTest3()
        {
            var account = new List<GnrAccount>
            {
                new GnrAccount("aaa",111),
                new GnrAccount("bbb",222),
                new GnrAccount("ccc",333),
                new GnrAccount("ddd",444),
                new GnrAccount("eee",555),
            };

            var sum = Algorithms.AccumlateSimple(account);
            Console.WriteLine($"sum:{sum}");
            Console.Read();
        }

        private static void GenericsFuncTest2()
        {
            var account = new List<Account>
            {
                new Account("aaa",111),
                new Account("bbb",222),
                new Account("ccc",333),
                new Account("ddd",444),
                new Account("eee",555),
            };

            var sum = Algorithms.AccumlateSimple(account);
            Console.WriteLine($"sum:{sum}");
            Console.Read();
        }

        /// <summary>
        /// 泛型方法
        /// </summary>
        private static void GenericsFuncTest()
        {
            //调用泛型方法
            GenericsFunc f = new GenericsFunc();
            int x = 0;
            int y = 100;
            f.Swap<int>(ref x,ref y);
            Console.WriteLine();
            Console.WriteLine($"x:{x},y:{y}");
            Console.Read();
        }

        /// <summary>
        /// 泛型类
        /// </summary>
        private static void GenericsTest()
        {
            /*
            LinkedList list = new LinkedList();
            list.AddList(2);
            list.AddList(4);
            list.AddList("6");//不会报错
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            Console.Read();
            */

            /*
            XCYN.Print.Generics.LinkedList<int> list = new XCYN.Print.Generics.LinkedList<int>();
            list.AddList(2);
            list.AddList(4);
            list.AddList("6");//在编码期间就会报错
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            Console.Read();
            */

            /*
            DocumentManager<Document> documentManager = new DocumentManager<Document>();
            documentManager.AddDocument(new Document("Java",""));
            documentManager.AddDocument(new Document("C#", ""));
            documentManager.DisplayAllDocument();
            Console.Read();
            */
            
        }

        private static void NullOperators()
        {
            NullOperators operators = new NullOperators();
            //operators.Fun3(null);
            //operators.Fun3(new XCYN.Print.Operators.Person() { Name = "王大锤" });
            operators.Fun4(null);
            operators.Fun4(new XCYN.Print.Operators.Person() { Age = 12 });
            Console.Read();
        }

        /// <summary>
        /// 测试冒泡排序
        /// </summary>
        private static void DelegateBubble()
        {
            //var array = DelegateTest.BubbleSorter(new int[5] {
            //    1,3,5,2,4
            //});
            //Console.WriteLine(string.Join(",",array));

            var array = Utils.BubbliSorter<delegates.Employee>(new delegates.Employee[5]
            {
                new delegates.Employee("a",1),
                new delegates.Employee("a",2),
                new delegates.Employee("a",5),
                new delegates.Employee("a",4),
                new delegates.Employee("a",3),
            }, delegates.Employee.CompareSalary);
            Console.WriteLine(string.Join("\n", array));
            Console.Read();
        }

        public static object sync = new object();
        
        private static void DemoMonitor()
        {
            for (int i = 0; i < 5; i++)
            {
                Task.Factory.StartNew(() => {
                    DemoMonitor demo = new MultiThread.DemoMonitor();
                    demo.Fun3();
                });
            }
            Console.Read();
        }

        private static void RelationOperators()
        {
            RelationOperators operators = new Operators.RelationOperators();
            operators.Fun6();
        }

        private static void DemoShiftOperators()
        {
            ShiftOperators operators = new ShiftOperators();
            operators.Fun2();
        }

        private static void DemoMultiplicationOperations()
        {
            MultiplicationOperators operators = new MultiplicationOperators();
            operators.Fun3();
        }

        private static void DemoCountdownEvent()
        {
            DemoCountdownEvent demo = new MultiThread.DemoCountdownEvent();
            demo.Fun1();
        }

        /// <summary>
        /// 读写锁
        /// </summary>
        private static void DemoReadWriteLock()
        {
            DemoReadWriteLock demo = new MultiThread.DemoReadWriteLock();
            //demo.Fun1();
            //demo.Fun2();
            demo.Fun3();
            Console.Read();
        }

        private static void DemoLock()
        {
            DemoLock demo = new DemoLock();
            
            for (int i = 0; i < 5; i++)
            {
                Task.Factory.StartNew(() => {
                    demo.Fun7();
                });
            }

            Console.Read();
        }

        /// <summary>
        /// WebAPI托管(Windows自托管)
        /// </summary>
        private static void WebAPIHost()
        {
            //宿主
            var config = new HttpSelfHostConfiguration(new Uri("http://localhost:55898"));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            var host = new HttpSelfHostServer(config);
            host.OpenAsync().Wait();
            Console.WriteLine("Press any key to exit");
            Console.Read();
            host.CloseAsync().Wait();
        }
        
        /// <summary>
        /// WebAPI客户端
        /// </summary>
        private static void WebAPIClient()
        {
            var greetingServiceAddress = new Uri("http://localhost:55898/api/greeting");

            var client = new HttpClient();

            var result = client.GetAsync(greetingServiceAddress).Result;

            var greeting = result.Content.ReadAsStringAsync().Result;

            Console.WriteLine(greeting);

            Console.Read();
        }

        private void RunTaskScheduler()
        {
            System.Windows.Forms.Application.Run(new DemoTaskScheduler());
        }

        /// <summary>
        /// 组合模式
        /// </summary>
        private static void Composite()
        {
            var root = new Composite("root");
            var net = new Composite("net");
            var program = new Composite("program");
            root.Add(net);
            root.Add(program);
            var c = new Composite("c#");
            var core = new Composite("core");
            net.Add(c);
            net.Add(core);
            var java = new Composite("java");
            program.Add(java);
            root.Display(1);
        }

        /// <summary>
        /// 享元模式
        /// </summary>
        private static void FlyWeightCommand()
        {
            var model = Factory.GetInstance(1);

            var model2 = Factory.GetInstance(1);

            var model3 = Factory.GetInstance(2);

            Console.WriteLine("model1 == model2?{0}",model.Equals(model2));

            Console.WriteLine("model2 == model3?{0}", model2.Equals(model3));
        }

        /// <summary>
        /// 桥接模式
        /// </summary>
        private static void BridgeCommand()
        {
            PhoneBrand xiaomi = new PhoneXiaoMi();
            Soft map = new Map();
            Soft game = new Game();
            xiaomi.AddSoft(map);
            xiaomi.AddSoft(game);
            xiaomi.run();
            Console.Read();
        }

        /// <summary>
        /// 责任链模式
        /// </summary>
        private static void ChainCommand()
        {
            AbstractHandler handler = new ConcreteHander1();
            AbstractHandler handler2 = new ConcreteHander2();
            AbstractHandler handler3 = new ConcreteHander3();
            //链式调用 handler1保存handler2的引用，handler2保存handler3的引用，这样就能逐级调用
            handler.SetHandler(handler2);
            handler2.SetHandler(handler3);
            //执行
            handler.Request(2);
            Console.Read();
        }

        /// <summary>
        /// 命令模式
        /// </summary>
        private static void HandleCommand()
        {
            Received recieved = new Received();
            //创建命令
            ICommand add = new AddCommand(recieved);
            ICommand remove = new RemoveCommand(recieved);
            //添加命令
            Invoker invoker = new Invoker();
            invoker.SetCommand(add);
            invoker.SetCommand(remove);
            invoker.SetCommand(add);
            invoker.SetCommand(add);

            //撤销
            invoker.Redo();
            //执行
            invoker.execute();
            Console.Read();
        }

        /// <summary>
        /// 状态模式
        /// </summary>
        private static void HandleState()
        {
            Context context = new Context(9, new MorningState());
            context.request();
            Console.Read();
        }

        /// <summary>
        /// 备忘录模式
        /// </summary>
        private static void HandleMemento()
        {
            //初始化
            Originator o = new Originator();
            o.msg = "Hello World";

            //创建一个备份
            Memento backup = o.CreateMemento();
            //将备份保存到Caretaker中去
            Caretaker c = new Caretaker()
            {
                memento = backup,
            };
            Console.WriteLine("备份中:"+o.msg);

            //修改
            o.msg = "Good Bye";
            Console.WriteLine("修改中:" + o.msg);

            //恢复
            o.RecoverMemento(c.memento);
            Console.WriteLine("恢复中:" + o.msg);
            Console.Read();
        }

        /// <summary>
        /// 工厂方法
        /// </summary>
        private static void HandleFactory()
        {
            //IFactory sqlserver = new SqlserverFactory();
            //sqlserver.CreateInstance().Create();

            //IFactory sqlite = new SqliteFactory();
            //sqlite.CreateInstance().Remove();

            //Console.Read();
        }

        /// <summary>
        /// 中介者模式
        /// </summary>
        private static void HandleMediator()
        {
            AbstractMediator mediator = new QQMediator();
            
            AbstractColleague colleague = new Colleague(mediator);
            colleague.UserName = "紫涵";

            AbstractColleague colleague2 = new Colleague(mediator);
            colleague2.UserName = "灵儿";

            mediator.Add(colleague);
            mediator.Add(colleague2);

            //发送消息
            colleague.Send("灵儿", "你好");
            colleague.Send("紫涵", "早上好~");
            Console.Read();
        }

        private static void HandleObserver()
        {
            ISubject subject = new ConcreteSubject();
            IObserver observer = new ConcreteObserver1(subject,"观察者1");
            IObserver observer2 = new ConcreteObserver1(subject, "观察者2");

            subject.Add(observer);
            subject.Add(observer2);

            subject.SutjectState = "服务器崩溃了!";
            subject.Notify();

            Console.Read();
        }

        private static void HandleStrategy()
        {
            //创建一个策略
            StrategyContext context = new StrategyContext(new FileLog());
            context.Write("aaaaaabbbbbb");

            //创建另一个策略
            context = new StrategyContext(new DBLog());
            context.Write("aaaaaabbbbbb");
            Console.Read();
        }

        private static void HandleFilter()
        {
            List<DesignPattern.Filter.Person> list_person = new List<DesignPattern.Filter.Person>() {
                new DesignPattern.Filter.Person()
                {
                   age = 19,
                   name = "cheng",
                   sex = 0
                },
                new DesignPattern.Filter.Person()
                {
                   age = 18,
                   name = "xheng",
                   sex = 0
                },
                new DesignPattern.Filter.Person()
                {
                   age = 19,
                   name = "xie",
                   sex = 0
                },
                new DesignPattern.Filter.Person()
                {
                   age = 19,
                   name = "cheng",
                   sex = 1
                },
            };
            //NameFilter namefilter = new NameFilter();
            //list_person = namefilter.Filter(list_person);
            //AndFilter andfilter = new AndFilter(new List<IFilter>()
            //{
            //     new NameFilter(),
            //     new AgeFilter()
            //});
            //list_person = andfilter.Filter(list_person);
            OrFilter orfilter = new OrFilter(new List<IFilter>()
            {
                 new NameFilter(),
                 new AgeFilter()
            });
            list_person = orfilter.Filter(list_person);

        }

        private static void HandleService()
        {
            ServiceHost host = new ServiceHost(typeof(DataService));
            host.Open();
            Console.WriteLine("服务启动");
            Console.Read();
        }

        private void HandleSignleton()
        {
            Console.WriteLine("主线程时间：{0}", DateTime.Now);
            //var db = XCYN.Print.DesignPattern.Singleton.hungry.DB.GetInstance();
            //Console.WriteLine("Show调用的时间：{0}", db.Show());
            //Console.WriteLine("Show调用的时间：{0}", XCYN.Print.DesignPattern.Singleton.hungry.DB.Show());

            //var db2 = XCYN.Print.DesignPattern.Singleton.hungry.DB.GetInstance();
            //Console.WriteLine("Show调用的时间：{0}", db2.Show());
            //Console.WriteLine("Show调用的时间：{0}", XCYN.Print.DesignPattern.Singleton.hungry.DB.Show());

            //多线程调用单例模式的例子
            Task.Factory.StartNew(() =>
            {
                var db = XCYN.Print.DesignPattern.Singleton.DCL.DB.GetInstance();
            });
            Task.Factory.StartNew(() =>
            {
                var db = XCYN.Print.DesignPattern.Singleton.DCL.DB.GetInstance();
            });
            Task.Factory.StartNew(() =>
            {
                var db = XCYN.Print.DesignPattern.Singleton.DCL.DB.GetInstance();
            });
            Task.Factory.StartNew(() =>
            {
                var db = XCYN.Print.DesignPattern.Singleton.DCL.DB.GetInstance();
            });
            Console.Read();
        }
    }
}
