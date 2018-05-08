<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CacheTest3.aspx.cs" Inherits="XCYN.Web.Pages.test.CacheTest3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>移动栏目的顺序，移动完成后要发送邮件到管理员</td>
                </tr>
                <tr>
                    <td>
                        <input type="button" value="↑" id="button_up" onclick="MoveClick(1)"/>
                        <input type="button" value="↓" id="button_down" onclick="MoveClick(2)"/>
                    </td>
                </tr>
            </table>
        </div>
    </form>
    <script src="../../Plugins/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript">

        var order = 0;

        function MoveClick(direct) {
            if (direct == 1) {
                order++;
            }
            else if (direct == 2) {
                order--;
            }
            $.post("RequestHandler.ashx",
                {
                    action: "MoveOrder",
                    direct: order
                },
                function (data) {
                    console.log(data);
                });
        }
    </script>
</body>
</html>
