<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FuncTest.aspx.cs" Inherits="XCYN.Web.Pages.test.FuncTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
    <script type="text/javascript">

        console.log("----------------------------");
        console.log("-------------函数的重载，后面的函数会覆盖前面的函数---------------");
        function fun1(num) {
            return num + 10;
        }

        function fun1(num)
        {
            return num + 20; 
        }

        /**
         * fun1与fun2是相等的
         * @param num
         */
        var fun2 = function (num) {
            return num + 10;
        }

        fun2 = function (num) {
            return num + 20;
        }

        console.log(fun2(10));

        console.log("--------------函数声明与函数表达式的区别--------------");
        fun3();
        function fun3() {
            console.log("调用fun3");
        }

        /*
         这里调用fun4会报错，因为解析器在向执行环境中加载数据时，对函数声明和函数表达式并非一视同仁。
         解析器会率先读取函数声明，并使其在执行任何代码之前可以访问；至于函数表达式，则必须等到解析器执行到他所在的代码行，才会真正被解释执行。
         */
        try {
            fun4();
        }
        catch (e) {
            console.log(e);
        }
        var fun4 = function () {
            console.log("调用fun4");
        }

        console.log("---------------函数作为参数和返回值-------------");

        function callSomeFunc(fun,args) {
            return fun(args);
        }

        function fun5(name) {
            return "hello " + name + "!";
        }

        console.log(callSomeFunc(fun5, "cheng"));

        /**
         * 数据排序规则，sort方法用于对数组的元素进行排序,并返回数组，他可以接收一个访问参数，里面可以编写排序规则。
         */
        var list_user = [
            { name: "cheng", age: 12 },
            { name: "wang", age: 15 },
            { name: "li", age: 20 }
        ];

        list_user.sort(sortedBy("age"));
        console.log(list_user);

        function sortedBy(prop) {
            return function (obj1, obj2) {
                if (obj1[prop] > obj2[prop]) {
                    return 1;
                }
                else if (obj1[prop] < obj2[prop]) {
                    return -1;
                }
                else {
                    return 0;
                }
            };
        }

        console.log("---------------函数内部属性arguments，this-------------");
        
        /**
         * 求阶乘，arguments.callee属性，指向这个函数对象
         */
        function fun6(num) {
            if (num <= 1) {
                return 1;
            }
            else {
                return num * arguments.callee(num - 1);
                //return num * fun6(num - 1);
            }
        }

        var fun7 = fun6;

        console.log(fun6(10));

        console.log("---------------函数的属性和方法-------------");

        function fun8(num, name) {

        }

        console.log("fun4的参数个数："+fun4.length);
        console.log("fun6的参数个数："+fun6.length);
        console.log("fun8的参数个数："+fun8.length);

    </script>
</body>
</html>
