<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="XCYN.Web.Pages.redis.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server" method="post" action="UserHandler.ashx">
        <div>
            <table>
                <tr>
                    <td>
                        用户名：
                    </td>
                    <td>
                        <input type="text" name="UserName" />
                    </td>
                </tr>
                <tr>
                    <td>
                        密码：
                    </td>
                    <td>
                        <input type="password" name="Password" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <input type="submit" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
