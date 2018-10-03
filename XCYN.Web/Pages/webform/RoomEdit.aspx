<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoomEdit.aspx.cs" Inherits="XCYN.Web.Pages.webform.RoomEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <%= this.id %>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="类型名称"></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="Label2" runat="server" Text="房间价格"></asp:Label>
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="Label3" runat="server" Text="加床价格"></asp:Label>
            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="Label4" runat="server" Text="是否加床"></asp:Label>
            <asp:DropDownList ID="DropDownList1" runat="server">
            </asp:DropDownList>
            <br />
            <asp:Label ID="Label5" runat="server" Text="备&nbsp;&nbsp;注"></asp:Label>
            <asp:TextBox ID="TextBox4" runat="server" Height="100px" TextMode="MultiLine" Width="472px"></asp:TextBox>
        </div>
    </form>
</body>
</html>
