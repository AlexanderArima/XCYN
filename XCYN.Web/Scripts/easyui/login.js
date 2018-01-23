$(function () {



    $("#login").dialog({
        title: "欢迎登录",
        height: 200,
        width: 300,
        iconCls: "icon-login",
        buttons: "#btn",
        modal:true,
    })

    $("#username").validatebox({
        required: true,
        missingMessage: "请输入用户名",
        validType: "length[2,15]",
        invalidMessage: "用户名长度2-15",
    })

    $("#password").validatebox({
        required: true,
        missingMessage: "请输入密码",
    })

    $("#btn a").click(function ()
    {
        if(!$("#username").validatebox("isValid")){
            $("#username").validatebox("validate");
        }
        else if (!$("#password").validatebox("isValid")) {
            $("#password").validatebox("validate");
        }
        else {
            $.ajax({
                url: "../ashx/LoginHandler.ashx",
                data: {
                    action: "Login",
                    username: $("#username").val(),
                    password: $("#password").val(),
                },
                beforeSend: function () {
                    $.messager.progress({
                        text:"加载中..."
                    })
                },
                success: function (data) {
                    if(data == "1")
                    {
                        location.href = "admin.html?user_name=" + $("#username").val()
                    }
                    else
                    {
                        $.messager.alert("提示", "用户名或密码错误!", "info", function () {
                            $.messager.progress("close")
                            $("#password").select();
                        });
                    }
                },
            })
        }
    })
})