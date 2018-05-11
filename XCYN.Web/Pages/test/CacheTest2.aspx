<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CacheTest2.aspx.cs" Inherits="XCYN.Web.Pages.test.CacheTest2" %>

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
                    <td colspan="2">缓存依赖</td>
                </tr>
                <tr>
                    <td>
                        <input type="button" value="缓存1" onclick="ButtonClick(1)" />
                    </td>
                    <td>
                        <input type="button" value="缓存2" onclick="ButtonClick(2)"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        key1:
                        <input type="text" value="<%= key1 %>" id="text_key1"/>
                    </td>
                    <td>
                        Key2:
                        <input type="text" value="<%= key2 %>" id="text_key2"/>
                    </td>
                </tr>
            </table>
        </div>
    </form>
    <script src="../../Plugins/jquery/jquery-1.10.2.min.js"></script>
    <script type="text/javascript">
        function ButtonClick(id) {
            $.post("RequestHandler.ashx",
                {
                    action:"ClearCache",
                    id: id
                },
                function (data) {
                    var obj = eval("(" + data + ")");
                    $("#text_key1").val(obj.key1);
                    $("#text_key2").val(obj.key2);
                });
        }
    </script>
</body>
</html>
