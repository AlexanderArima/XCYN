<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RequestTest.aspx.cs" Inherits="XCYN.Web.Pages.test.RequestTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
        <script src="RequestHandler.ashx?action=GetJS" defer="defer">
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <a href="http://localhost:4923/Pages/test/RequestTest.aspx" >跳转</a>
        <div>
            <input type="button" value="发送Get请求" onclick="IsGetTest()" />
        </div>
        <div>
            <input type="button" value="发送Post请求" onclick="IsPostTest()" />
        </div>
        <div>
            <input type="button" value="点我" onclick="Hello2()" />
        </div>
    </form>
    <script src="../../Plugins/jquery/jquery-1.10.2.min.js"></script>
    <script>

        function IsGetTest() {
            $.get("RequestHandler.ashx",
                {
                    action:"IsGetTest"
                });
        }

        function IsPostTest() {
            $.post("RequestHandler.ashx",
                {
                    action: "IsPostTest"
                });
        }

        
    </script>
    <script>
        var message = 0x79;
        //console.log(message);
        //console.log(typeof message);
        //console.log(message == undefined);

        console.log(Boolean(message))
        if(message)
        {
            console.log(message);
        }
        else
        {
            console.log("false");
        }

        var a = 0.1;
        var b = 0.2;
        console.log(a + b);
        if(a + b == 0.3)
        {
            console.log("hello");
        }

        var c = "1234";
       
        console.log(Number("aaa11"))
        console.log(Number.MAX_VALUE)

        console.log(isNaN(NaN));
        console.log(isNaN(10));
        console.log(isNaN("10"));
        console.log(isNaN("blue"));
        console.log(isNaN(true));
    </script>
</body>
</html>
