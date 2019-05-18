using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using bscy.App_Code.Models.Table.BranchList;

namespace XCYN.Test.cdbWeb
{
    /// <summary>
    /// BranchListTest 的摘要说明
    /// </summary>
    [TestClass]
    public class BranchListTest
    {
        public BranchListTest()
        {
            //
            //TODO:  在此处添加构造函数逻辑
            //
        }

        private TestContext testContextInstance;
        BranchListViewModel _model;

        /// <summary>
        ///获取或设置测试上下文，该上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        //
        // 编写测试时，可以使用以下附加特性: 
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        [TestInitialize()]
        public void MyTestInitialize() {
            _model = new BranchListViewModel();
        }

        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void GetList()
        {
            var actual = _model.GetList().Rows.Count;
            Assert.AreEqual(3, actual);
        }

        [TestMethod]
        public void GetList2()
        {
            BranchListQueryParm parm = new BranchListQueryParm();
            parm.NSRMC = " ";
            parm.NSRSBH = " ";
            parm.JYDZ = " ";
            parm.ZGSWJG = " ";
            parm.SFNSZT = -1;
            parm.KYSJ_StartTime = " ";
            parm.KYSJ_EndTime = " ";
            parm.ZXSJ_StartTime = " ";
            parm.ZXSJ_EndTime = " ";
            BranchListViewModel model = new BranchListViewModel();
            var grid =  model.GetList(parm);
            Assert.AreEqual(113, grid.Rows.Count);
        }

        [TestMethod]
        public void Count()
        {
            var flag = _model.Count("武汉1","");
            Assert.IsTrue(flag == 1);

            flag = _model.Count("武汉7","");
            Assert.IsTrue(flag ==2);

            flag = _model.Count("武汉s","");
            Assert.IsTrue(flag ==0);
        } 

        [TestMethod]
        public void Count2()
        {
            var flag = _model.Count( 2,"武汉1","");
            Assert.IsTrue(flag == 0);

            //如果大于0，就不能修改，因为已存在同名
            flag = _model.Count( 2,"武汉2","");
            Assert.IsTrue(flag == 1);

            flag = _model.Count(2,"武汉w","");
            Assert.IsTrue(flag == 0);
        }

        [TestMethod]
        public void Add2()
        {
            //上级机构不存在时，无法录入
            Node node = new Node()
            {
                NSRMC = "武汉1",
                NSRSBH = "003",
                JYDZ = "武汉市洪山区",
                SFNSZT = 0,
                KYSJ = "2010-2-14",
                ZGSWJG = "",
                SJJGMC = "湖北1",
                ZXSJ = "2018-06-14",
                CREATEID = 1,
                CREATETIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
            };
            //上级机构名称: " + branch.SJJGMC + ", 不存在，请先录入该机构
            try
            {
                _model.Add(node);
            }
            catch(Exception ex)
            {
                Assert.AreEqual("上级机构名称: 湖北1, 不存在，请先录入该机构", ex.Message);
            }

            //上级机构不存在时，无法录入
            node = new Node()
            {
                NSRMC = "湖北1",
                NSRSBH = "002",
                JYDZ = "武汉市洪山区",
                SFNSZT = 0,
                KYSJ = "2010-2-14",
                ZGSWJG = "",
                SJJGMC = "",
                ZXSJ = "2018-06-14",
            };
            _model.Add(node);

            //上级机构不存在时，无法录入
            node = new Node()
            {
                NSRMC = "武汉1",
                NSRSBH = "003",
                JYDZ = "武汉市洪山区",
                SFNSZT = 0,
                KYSJ = "2010-2-14",
                ZGSWJG = "",
                SJJGMC = "湖北1",
                ZXSJ = "2018-06-14",
            };
            //上级机构名称: " + branch.SJJGMC + ", 不存在，请先录入该机构
            int actual =  _model.Add(node);
            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void Add()
        {
            Node node = new Node()
            {
                NSRMC = "湖北1",
                NSRSBH = "002",
                JYDZ = "武汉市洪山区",
                SFNSZT = 0,
                KYSJ = "2010-2-14",
                ZGSWJG = "",
                SJJGMC = "",
                ZXSJ = "2018-06-14",
                SJJGSFNSZT = 0,
                CREATEID = 1,
                CREATETIME = DateTime.Now.ToString("yyyy-MM-dd")
            };
            try
            {
                 int count = _model.Add(node);
                Assert.AreEqual(1, count);
            }
            catch(ArgumentException ex)
            {
                Assert.IsTrue(ex.Message.Contains("纳税人名称: 湖北1, 已存在"));
            }
            
        }

        [TestMethod]
        public void BatchAdd2()
        {
            List<Node> list = new List<Node>();
            for (int i = 1; i <= 10; i++)
            {
                Node node2 = new Node()
                {
                    NSRMC = "湖北" + i,
                    NSRSBH = "027",
                    JYDZ = "武汉市洪山区",
                    SFNSZT = 0,
                    KYSJ = "2010-2-14",
                    ZGSWJG = "",
                    SJJGMC = "",
                    ZXSJ = "",
                };
                list.Add(node2);
            }
            try
            {
                _model.BatchAdd(list);
            }
            catch(ArgumentException ex)
            {
                Assert.IsTrue(ex.Message.Contains("纳税人识别号: 027, 已存在"));
            }
        }

        [TestMethod]
        public void BatchAdd()
        {
            List<Node> list = new List<Node>();
            for (int i = 1; i <= 3; i++)
            {
                Node node2 = new Node()
                {
                    NSRMC = "湖北" + i,
                    NSRSBH = "A" + i.ToString().PadLeft(3,'0'),
                    JYDZ = "武汉市洪山区",
                    SFNSZT = 0,
                    KYSJ = "2010-2-14",
                    ZGSWJG = "1",
                    SJJGMC = "",
                    ZXSJ = "",
                    SJJGSFNSZT = 0,
                     CREATEID = 1,
                     CREATETIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                };
                list.Add(node2);
            }
            _model.BatchAdd(list);
            list.Clear();
            Random random = new Random();
            //二级菜单
            for (int i = 1; i <= 10; i++)
            {
                Node node2 = new Node()
                {
                    NSRMC = "武汉" + i ,
                    NSRSBH = "B" + i.ToString().PadLeft(3,'0'),
                    JYDZ = "武汉市洪山区",
                    SFNSZT = 0,
                    KYSJ = "2010-2-14",
                    ZGSWJG = "1",
                    SJJGMC = "湖北" + random.Next(1,4),
                    ZXSJ = "2018-01-11",
                    SJJGSFNSZT = 0,
                    CREATEID = 1,
                    CREATETIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                };
                list.Add(node2);
            }
            _model.BatchAdd(list);
            list.Clear();

            //三级菜单
            for (int i = 1; i <= 100; i++)
            {
                Node node2 = new Node()
                {
                    NSRMC = "洪山" + i,
                    NSRSBH = "C" + i.ToString().PadLeft(3, '0'),
                    JYDZ = "武汉市洪山区",
                    SFNSZT = 0,
                    KYSJ = "2010-2-14",
                    ZGSWJG = "1",
                    SJJGMC = "武汉" + random.Next(1, 11),
                    ZXSJ = "2018-01-11",
                    SJJGSFNSZT = 0,
                    CREATEID = 1,
                    CREATETIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                };
                list.Add(node2);
            }
            _model.BatchAdd(list);
            int count = _model.Count();
            Assert.AreEqual(113, count);
            
        }

        [TestMethod]
        public void GetModel()
        {
            var node = _model.GetModel(1);
            Assert.IsNotNull(node);
            Assert.AreEqual("湖北1", node.NSRMC);
            Assert.AreEqual("002", node.NSRSBH);
            Assert.AreEqual("武汉市洪山区", node.JYDZ);
            Assert.AreEqual("", node.ZGSWJG);
            Assert.AreEqual(0, node.SFNSZT);
            Assert.AreEqual("2010-02-14", node.KYSJ);
            Assert.AreEqual("2018-06-14", node.ZXSJ);
            Assert.AreEqual("", node.SJJGMC);
            Assert.AreEqual(0, node.ISDELETE);
            Assert.AreEqual(0, node.SJJGSFNSZT);
            Assert.AreEqual(1, node.CREATEID);
            Assert.AreEqual("2019-01-24 00:00:00", node.CREATETIME);
            Assert.IsTrue(node.DELETETIME == null);
        }

        [TestMethod]
        public void Edit()
        {
            var node = _model.GetModel(3);
            node.EDITID = 2;
            node.NSRSBH = "A003";
            var flag = 0;
            try
            {
                flag = _model.Edit(node);
            }
            catch(ArgumentException ex)
            {
                Assert.AreEqual("纳税人识别号:001,已存在", ex.Message);
            }
            
            node = _model.GetModel(3);
            Assert.AreEqual("A003", node.NSRSBH);

        }

        [TestMethod]
        public void DeleteAll()
        {
            var actual = _model.Delete(2,3);
            //actual = _model.Delete(468);
            //actual = _model.Delete(469);
            Assert.AreEqual(true, actual);
            
        }

        [TestMethod]
        public void Delete()
        {
            var actual= _model.Delete(7,3);
            Assert.AreEqual(true, actual);
        }
    }
}
