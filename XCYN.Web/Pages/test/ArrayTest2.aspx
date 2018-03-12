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
        //如果该函数对每一项都返回true，则返回true
        var everyResult = colors.every(function (value, index, array) {
            if (index >= 1) {
                return true;
            }
        });
        console.log(everyResult);
        console.log("------------------filter()方法-------------------");
        //返回该函数会返回true的项组成的数组
        var filterResult = colors.filter(function (value, index, array) {
            if (index % 2 == 1) {
                return true;
            }
        });
        console.log(filterResult);
        console.log("------------------forEach()方法-------------------");
        //遍历数组，没有返回值
        colors.forEach(function (value, index, array) {
            //执行某些操作
            console.log(value);
        });

        console.log("------------------map()方法-------------------");
        //返回每次函数调用的结果组成的数组
        var mapResult = colors.map(function (value, index, array) {
            if (index % 2 == 1) {
                return value;
            }
            else {
                return;
            }
        });
        console.log(mapResult);

        console.log("------------------some()方法-------------------");
        var someResult = colors.some(function (value, index, array) {
            if (index >= 1) {
                return true;
            }
        });
        console.log(someResult);
    </script>
</body>
</html>
