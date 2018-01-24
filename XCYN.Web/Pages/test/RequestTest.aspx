<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RequestTest.aspx.cs" Inherits="XCYN.Web.Pages.test.RequestTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
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
</body>
</html>
