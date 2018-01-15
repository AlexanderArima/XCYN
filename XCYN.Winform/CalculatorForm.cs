using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XCYN.Common;
using XCYN.Print.Calculator;

namespace XCYN.Winform
{
    public partial class CalculatorForm : Form
    {

        private Stack _firstStack = new Stack();
        private Stack _secondStack = new Stack();
        private IStrategy _strategy = null;

        public CalculatorForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 数字0-9
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Number_Click(object sender, EventArgs e)
        {
            var button = sender as System.Windows.Forms.Button;
            if(_strategy == null)
            {
                //如果没有运算符合，往第一个栈中插入元素
                _firstStack.Push(button.Text);
            }
            else
            {
                //如果运算符合，往第二个栈中插入元素
                _secondStack.Push(button.Text);
            }
            //判断两个栈哪个是空值，就显示另一个
            if(_secondStack.Count == 0)
            {
                label1.Text = string.Join("", _firstStack.ToArray().Reverse());
            }
            else if()
            //if(_secondStack == null)
            //{
            //    label1.Text = _firstStack.ToArray();
            //}
        }

        /// <summary>
        /// 数字运算符
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberOperation_Click(object sender, EventArgs e)
        {
            var button = sender as System.Windows.Forms.Button;
            switch (button.Text)
            {
                case ("+"):
                    _strategy = new StrategyAdd();
                    break;
                case ("-"):
                    _strategy = new StrategySub();
                    break;
                case ("×"):
                    _strategy = new StrategyMul();
                    break;
                case ("÷"):
                    _strategy = new StrategyDiv();
                    break;
                default:
                    break;
            }
            //label2.Text = string.Join("", _firstStack.ToArray()) + button.Text;
        }

        /// <summary>
        /// 等于输出结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Equal_Click(object sender, EventArgs e)
        {
            var first = string.Join("", _firstStack.ToArray().Reverse());
            var second = string.Join("", _secondStack.ToArray().Reverse());
            var result = 0m;
            if (first.Length > 0 && second.Length > 0)
            {
                result = _strategy.Exec(Decimal.Parse(first), Decimal.Parse(second));
                StackHelper.Insert(_firstStack, result.ToString());
            }
            label1.Text = result.ToString();
        }

        /// <summary>
        /// 清除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clear_Click(object sender, EventArgs e)
        {
            _firstStack.Clear();
            _secondStack.Clear();
            _strategy = null;
        }
    }
}
