
$(function () {

    //自定义验证规则
    $.extend($.fn.validatebox.defaults.rules, {
        minLength: {
            validator: function (value, param) {
                return value.length >= param[0];
            },
            message: "请输入长度不大于{0}的字符",
        },
    });

     
    $("#email").validatebox({
        required: true,
        validType:'minLength[8]',
        //validType:'email',
        //validType:'url',
        //validType:'length[2,10]',
        //validType:'remote["../Handler1.ashx","username"]',//服务器返回true则验证通过
        //missingMessage: '请填写电子邮件',
        //invalidMessage: '电子邮件验证失败',
    })

    $(document).click(function () {
        //返回验证框对象
        //console.log($("#email").validatebox("validate"));
        //返回验证结果(true或者false)
        //console.log($("#email").validatebox("isValid"));
        
    });

    //禁用验证控件
    $("#email").validatebox("disableValidation");
    //启用验证控件
    $("#email").validatebox("enableValidation");
})