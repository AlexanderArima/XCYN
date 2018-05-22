using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XCYN.Print.Calculator;

namespace XCYN.Test.Print
{
    /// <summary>
    /// CalculatorTest 的摘要说明
    /// </summary>
    [TestClass]
    public class CalculatorTestCase
    {
        public CalculatorTestCase()
        {
            //
            //TODO:  在此处添加构造函数逻辑
            //
        }

        private TestContext testContextInstance;

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
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Add()
        {
            IStrategy add = new StrategyAdd();
            Calculator calculator = new Calculator(add);
            decimal first = 1.1m;
            decimal second = 1.1m;
            var result = calculator.Exec(first, second);

            Assert.AreEqual(2.2m, result);
        }

        [TestMethod]
        public void Sub()
        {
            IStrategy Sub = new StrategySub();
            Calculator calculator = new Calculator(Sub);
            decimal first = 1.1m;
            decimal second = 1.1m;
            var result = calculator.Exec(first, second);

            Assert.AreEqual(0m, result);
        }

        [TestMethod]
        public void Mul()
        {
            IStrategy Mul = new StrategyMul();
            Calculator calculator = new Calculator(Mul);
            decimal first = 1.1m;
            decimal second = 1.1m;
            var result = calculator.Exec(first, second);

            Assert.AreEqual(1.21m, result);
        }

        [TestMethod]
        public void Div()
        {
            IStrategy Div = new StrategyDiv();
            Calculator calculator = new Calculator(Div);
            decimal first = 1.1m;
            decimal second = 1.1m;
            var result = calculator.Exec(first, second);

            Assert.AreEqual(1m, result);
        }

        [TestMethod]
        public void Add2()
        {
            IStrategy add = new StrategyAdd();
            Calculator calculator = new Calculator(add);
            decimal first = 1.1m;
            decimal second = 1.1m;
            var result = calculator.Exec(first, second);
            IStrategy sub = new StrategySub();
            calculator = new Calculator(sub);
            result = calculator.Exec(result, 2.2m);

            Assert.AreEqual(0m, result);
        }
    }
}
