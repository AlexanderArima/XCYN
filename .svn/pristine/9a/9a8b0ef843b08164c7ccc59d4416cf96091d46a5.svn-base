<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="shop.aspx.cs" Inherits="XCYN.Knockout.pages.shop" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <div class="container">
        <div class=""></div>
        <div class="table-responsive ">
            <table class="table table-bordered">
                <thead>
                    <tr>
                        <th>编号</th>
                        <th>姓名</th>
                        <th>创建日期</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>1</td>
                        <td>2</td>
                        <td>3</td>
                    </tr>
                    <tr>
                        <td>1</td>
                        <td>2</td>
                        <td>3</td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    <script src="../Scripts/jquery-1.10.2.min.js"></script>
    <script src="../Scripts/knockout-3.4.2.js"></script>
    <script>

        //添加，删除，修改商品的功能。
        $.post("../ashx/ShopHandler.ashx",
            {
                action: "GetProductList"
            },
            function (data) {
                ko.applyBindings(data);
            })

        
    </script>
</body>
</html>
