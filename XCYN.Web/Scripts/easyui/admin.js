$(function () {

    //获取查询字符串
    function GetQueryString(name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return unescape(r[2]); return null;
    }

    var user_name = GetQueryString("user_name");
    if (user_name == null | user_name.length <= 0)
    {
        //如果未登录，则跳转到登录页面
        location.href = "login.html";
    }
    $("#label_user_name").html(user_name);

    $("#tab").tabs({
        fit: true,
        border:false,
    })
     
    $("#nav").tree({
        "url": "../ashx/TreeHandler.ashx",
        "lines": true,
        onClick:function(node)
        {
            $(this).tree('expand', node.target);
            //console.log(node.target);
            //for (var i = 0; i < $(node)[0].children.length; i++) {
                
            //    var child = $(node)[0].children[i];
            //    console.log(child);
                
            //}
            //console.log($(node)[0].children);
            //$(node).each(function (index,item) {
            //    console.log(item);
            //})
            //console.log(node);
        }
    })

});