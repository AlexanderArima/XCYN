<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArrayTest2.aspx.cs" Inherits="XCYN.Web.Pages.test.ArrayTest2" %>

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
    <script>
        var colors = ["red", "orange", "yellow"];
        console.log("------------------every()方法-------------------");
        //every()用于查询函数中的项全部满足条件，就返回true
        var everyResult = colors.every(function (value, index, array) {
            if (index >= 1) {
                return true;
            }
        });
        console.log(everyResult);

        
        //some()用于查询函数中的项只要有一项满足，就返回true
        console.log("------------------some()方法-------------------");
        var someResult = colors.some(function (value, index, array) {
            if (index >= 1) {
                return true;
            }
        });
        console.log(someResult);

        console.log("------------------filter()方法-------------------");
        //返回该函数会返回true的项组成的数组，这个方法对查询符合某些条件的所有数组项非常有用。
        var filterResult = colors.filter(function (value, index, array) {
            if (index % 2 == 1) {
                return true;
            }
        });
        console.log(filterResult);

        console.log("------------------forEach()方法-------------------");
        //遍历数组，本质是for循环
        colors.forEach(function (value, index, array) {
            //执行某些操作
            console.log(value);
        });

        console.log("------------------map()方法-------------------");
        //map()也是返回一个数组，而这个数组的每一项都是在原始数组中的对应项上运行传入函数的结果。
        var mapResult = colors.map(function (value, index, array) {
            if (index % 2 == 1) {
                return value;
            }
            else {
                return;
            }
        });
        console.log(mapResult);

    </script>
</body>
</html>
