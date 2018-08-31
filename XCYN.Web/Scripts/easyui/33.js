
$(function () {
    $("#box").form({
        url: "RegisterHandler.ashx",
        onSubmit:function(param)
        {
            //param.user_name = "xc";
            return $(this).form("validate");
        },
        success:function(data)
        {
            alert(data);
        } 
    });

    //$("#box").form("load", {
    //    name: "xc",
    //    email:"cheng@qq.com"
    //})

    //$("#box").form("load","33_data.json")
})