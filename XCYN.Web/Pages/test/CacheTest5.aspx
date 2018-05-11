<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CacheTest5.aspx.cs" Inherits="XCYN.Web.Pages.test.CacheTest5" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            动态网页缓存加速
            <br />
            用ab做压力测试的命令：
            C:\>ab -n1000 -c100 -H "If-Modified-Since: 过期时间" xxx地址
        </div>
    </form>
</body>
</html>
