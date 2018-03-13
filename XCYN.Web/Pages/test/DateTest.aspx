<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DateTest.aspx.cs" Inherits="XCYN.Web.Pages.test.DateTest" %>

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
        console.log("--------------------------");
        var d = new Date();
        console.log("-------------toLocaleString()方法会按照与浏览器设置的地区相适应的格式返回日期和时间-------------");
        console.log(d.toLocaleString());
        console.log("-------------toString()方法通常返回带有时区信息的日期和事件-------------");
        console.log(d.toString());
        console.log("-------------toDateString()返回日期-------------");
        console.log(d.toDateString());
        console.log("-------------toDateString()返回时间-------------");
        console.log(d.toTimeString());
        console.log("-------------getTime()返回日期的毫秒数，与valueOf()方法返回的值相同-------------");
        console.log(d.getTime());
        console.log("-------------getFullYear()返回4位数的年份-------------");
        console.log(d.getFullYear());
        d.setUTCFullYear(2000);//设置日期的年份，传入的年份必须是4位数字。
        console.log(d.getFullYear());
        console.log("-------------getMonth()返回日期中的月份，其中0表示一月，11表示十二月-------------");
        console.log(d.getMonth());
        d.setMonth(11);//设置日期的月份。传入的月份比如大于0，超过11会增加年份
        console.log(d.getMonth());
        console.log("-------------getDate()返回月份中的天数-------------");
        console.log(d.getDate());
        d.setDate(31);//设置月份中的天数。如果传入的值超过了该月中应有的天数，则增加月份
        console.log(d.getDate());
        d = new Date();
        console.log("-------------getDay()返回日期中的星期几(其中0表示星期日，6表示星期六)-------------");
        console.log(d.getDay());
        console.log("-------------getHours()设置日期中的小时数(0到23)-------------");
        console.log(d.getHours());
        d.setHours(23);//设置日期中的小时数，传入的值超过了23则增加月份中的天数
        console.log(d.getHours());
    </script>
</body>
</html>
