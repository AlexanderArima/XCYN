<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="101.aspx.cs" Inherits="XCYN.WS._101" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>websocket的基本使用</title>
    <script src="Scripts/jquery-1.10.2.min.js"></script>
    <script src="Scripts/jquery.signalR-2.1.2.js"></script>
    <script>
        var conn = $.connection("/myConn","",true);
        conn.start();
        conn.received(function (data) {
            console.log(data);
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input type="text" id="text_msg" />
            <input type="button" onclick="sendMsg()" value="发送" />
        </div>
    </form>
    <script>
        function sendMsg()
        {
            var msg = $("#text_msg").val();
            conn.send(msg);
        }
    </script>
</body>
</html>
