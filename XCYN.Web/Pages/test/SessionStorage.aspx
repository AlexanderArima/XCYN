<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SessionStorage.aspx.cs" Inherits="XCYN.Web.Pages.test.SessionStorage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input type="text" id="text_count" />
            <input type="button" value="重置" onclick="ResetCount()" />
        </div>
    </form>
    <script type="text/javascript">

        //SessionStorage关闭浏览器即失效
        function ResetCount() {
            sessionStorage.count = 0;
            location.reload();
        }

        if (sessionStorage.count) {
            var count = parseInt(sessionStorage.count);
            sessionStorage.count = count + 1;
        }
        else {
            sessionStorage.count = 1;
        }
        document.getElementById("text_count").setAttribute("value", sessionStorage.count);
    </script>
</body>
</html>
