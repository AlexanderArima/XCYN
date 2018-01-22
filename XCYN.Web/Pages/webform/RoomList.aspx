<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoomList.aspx.cs" Inherits="XCYN.Web.Pages.webform.PostList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>
        .menu{
            background-color:red;
        }
    </style>
</head>
<body>
    
    <form id="form1" runat="server">
        <div>
            <asp:Menu ID="Menu1" runat="server" BackColor="White" BorderColor="#660066" CssClass="menu" Orientation="Horizontal">
                <Items>
                    <asp:MenuItem Text="房屋管理" Value="房屋管理">
                        <asp:MenuItem Text="房屋列表" Value="房屋列表"></asp:MenuItem>
                        <asp:MenuItem Text="添加房屋" Value="添加房屋"></asp:MenuItem>
                    </asp:MenuItem>
                    <asp:MenuItem Text="类型管理" Value="类型管理">
                        <asp:MenuItem Text="类别列表" Value="类别列表"></asp:MenuItem>
                        <asp:MenuItem Text="添加类型" Value="添加类型"></asp:MenuItem>
                    </asp:MenuItem>
                </Items>
            </asp:Menu>
        </div>
        <div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="TypeName" HeaderText="房间名称" />
                    <asp:BoundField DataField="TypePrice" HeaderText="房间价格" />
                    <asp:BoundField DataField="AddBedPrice" HeaderText="加床价格" />
                    <asp:BoundField DataField="IsAddBed" HeaderText="是否加床" />
                    <asp:BoundField DataField="Remark" HeaderText="备注" />
                    <asp:HyperLinkField Text="修改" DataNavigateUrlFields="TypeID" DataNavigateUrlFormatString="RoomEdit.aspx?id={0}" />
                    <asp:HyperLinkField NavigateUrl="#" Text="删除" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
