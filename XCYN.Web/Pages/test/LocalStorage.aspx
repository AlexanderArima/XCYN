<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LocalStorage.aspx.cs" Inherits="XCYN.Web.Pages.test.LocalStorage" %>

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

        function ResetCount() {
            localStorage.count = 0;
            location.reload();
        }
        
        if (localStorage.count) {
            var count = parseInt(localStorage.count);
            localStorage.count = count + 1;
        }
        else {
            localStorage.count = 1;
        }
        document.getElementById("text_count").setAttribute("value", localStorage.count);
    </script>
</body>
</html>
