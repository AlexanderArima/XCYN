<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BoxTypeTest.aspx.cs" Inherits="XCYN.Web.Pages.test.BoxTypeTest" %>

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
        console.log("--------------基本包装类型String，Boolean，Number--------------");
        /**
         * 引用类型与基本包装类型的主要区别就是对象的生命周期。
         * 使用new操作符创建的引用类型的实例，在执行流离开当前作用域之前都一直保存在内存中。
         * 而自动创建的基本包装类型的对象，而只存在于一行代码的执行瞬间，然后立即销毁。
         */
        var s = "hello world";
        s.color = "red";//设置的color属性，只在这一行有效
        console.log(s);
        console.log(s.color);
        var v = new String("how are you?");
        v.color = "red";//设置的color属性，一直有效
        console.log(v.toString());
        console.log(v.color);

        console.log("--------------String类型--------------");
        console.log("--------------字符方法charAt,charCodeAt--------------");
        var str = "hello world";
        console.log(str.charAt(0));
        console.log(str.charCodeAt(0));
        console.log(str[0]);//还可以通过使用方括号，加索引的方法来访问字符串中的特定字符
    </script>
</body>
</html>
