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

        //concat()方法可以基于当前数组中所有创建一个新数组。这个方法会创建当前数组的一个副本，然后将接收到的参数添加到这个副本的末尾
        console.log("----------------------concat()方法--------------------");
        color = ["red", "yellow", "green"];
        var color2 = color.concat(["blue", "black"]);
        console.log(color.join(","));
        console.log(color2.join(","));

        //slice()方法能够基于当前数组中的一个或多个项创建一个新数组。它接收一个或两个参数，即要返回项的起始和结束位置(不包含)。
        console.log("----------------------slice()方法--------------------");
        color = ["red", "yellow", "green", "blue", "black"];
        var color2 = color.slice(1);
        var color3 = color.slice(1, 4);
        console.log(color2.join(","));
        console.log(color3.join(","));

        //splice()方法是最强大的数组方法了，它有很多种用法。
        //1.删除，可以删除任意数量的项，只需指定2个参数：要删除的第一项的位置和要删除的项数。
        //2.插入，可以向任意位置插入任意数量的项，只需指定3个参数：起始位置，0(要删除的项数，这里始终为0)和要插入的项。如果要插入多个项，可以再插入第四，第五，以致任意多个项。
        //3.替换，可以向指定位置插入任意数量的项，且同时删除任意数量的项，只需指定3个参数：起始位置，要删除的项数和要插入的任意数量的项。
        //splice()方法会始终返回一个数组，该数组中包含从原始数组中删除的项。
        console.log("----------------------splice()方法--------------------");
        color = ["red", "yellow", "green"];
        var removed = color.splice(0, 1);//删除
        console.log("----删除----");
        console.log(removed);
        console.log(color);
        color = ["red", "yellow", "green"];
        var inserted = color.splice(0, 0, "blue", "black");//往首项添加两个元素
        console.log("----添加----");
        console.log(inserted);
        console.log(color);
        color = ["red", "yellow", "green"];
        console.log("----修改----")
        var updated = color.splice(0, 1, "blue");//将第一个项改成blue
        console.log(updated);
        console.log(color);

        //indexOf()和lastIndexOf()这两个方法都接受两个参数：要查找的项和(可选的)查找起点的索引位置。
        //indexOf()方法从数组开头开始向后查找，lastIndexOf()方法则从数组的末尾开始向前查找。
        console.log("----------------------indexOf()方法--------------------");
        color = ["red", "yellow", "green","yellow"];
        console.log(color.indexOf("yellow"));
        console.log(color.lastIndexOf("yellow"));
    </script>
</body>
</html>
