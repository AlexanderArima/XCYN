<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="XCYN.Web.Pages.SSE.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <script type="text/javascript">
        window.onload = function ()
        {
            // 调用一个SSE服务
            if (typeof (EventSource) !== "undefined") {
                console.log("浏览器支持 Server-Sent事件");
                var source = new EventSource("SSEHandler.ashx");
                source.onmessage = function (event) {
                    console.log(event);
                };
            }
            else {
                console.log("浏览器不支持 Server-Sent事件");
            }
            
        };
    </script>
</body>
</html>
