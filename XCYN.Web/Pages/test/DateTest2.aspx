<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DateTest2.aspx.cs" Inherits="XCYN.Web.Pages.test.DateTest2" %>

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
        console.log("--------------------------");
        var d = new Date();
        console.log("-------------获取日期中的分钟数-------------");
        console.log(d.getMinutes());
        d.setMinutes(22);
        console.log(d.getMinutes());//设置日期中的分钟数，传入的值超过59则增加小时数
        console.log("-------------获取日期中的秒数-------------");
        console.log(d.getSeconds());
        d.setSeconds(22);//设置日期中的秒数，传入的值超过59则增加分钟数
        console.log(d.getSeconds());
        console.log("-------------获取日期中的毫秒数-------------");
        console.log(d.getMilliseconds());
        d.setMilliseconds(255);
        console.log(d.getMilliseconds());
        console.log("-------------返回本地实际与UTC时间相差的分钟数-------------")
        console.log(d.getTimezoneOffset());
    </script>
</body>
</html>
