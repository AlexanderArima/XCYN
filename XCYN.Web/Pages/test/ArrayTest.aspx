<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArrayTest.aspx.cs" Inherits="XCYN.Web.Pages.test.ArrayTest" %>

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
        var color = ["red", "yellow", "green"];
        //ECMAScript为数组专门提供了push()和pop()方法，以便实现类似栈的行为。
        //push()方法是像数组末端添加项的方法，pop()方法则是从数组末尾移除最后一项。

        console.log("----------------------栈---------------------");
        console.log(color.pop());//从栈顶取出元素
        console.log(color.join("丨"));
        console.log(color.push("blue"));//往栈顶添加元素
        console.log(color.join("丨"));
        
        //ECMAScript为数组专门提供了shift()和push()方法，以便实现类似栈的行为。
        //push()方法是像数组末端添加项的方法，shift()能够移除数组中第一个项并返回该项，同时数组长度减一。

        console.log("----------------------队列--------------------");
        color = ["red", "yellow", "green"];
        console.log(color.shift());
        console.log(color.join(","));
        color.push("blue");
        console.log(color.join(","));
        //还有一种方式可以反向模拟队列，unshift()和pop()方法
        //unshift()方法在数组首端添加项，pop()方法在数组末端移除元素

        console.log("----------------------队列(反向)--------------------");
        color = ["red", "yellow", "green"];
        console.log(color.pop());
        console.log(color.join(","));
        color.unshift("blue", "pink");
        console.log(color.join(","));
    </script>
</body>
</html>
