
$(function () {

    //属性与事件
    $("#box").slider({
        width: 300,
        height: 300,
        mode: 'h',
        rule: ["0", "|", "20", "|", "40", "|", "60", "|", "80", "|", "100"],
        showTip:true,
        step: 1,
        //reversed: true,
        //value: 100,
        //tipFormatter:function(value)
        //{
        //    return value + "%";
        //}
        //onChange:function(newValue,oldValue)
        //{
        //    $("#span_font").css("font-size", newValue);
        //    console.log("newValue:" + newValue);
        //    console.log("oldValue:" + oldValue);
        //},
        //onSlideStart:function(value)
        //{
        //    console.log(value);
        //},
        //onSlideEnd: function (value) {
        //    console.log(value);
        //},
    })

    //方法
    $("#box").slider("resize", {
        width: 400,
        height : 400,
    })
    
    $("#box").slider("setValue","10")
    console.log($("#box").slider("getValue"))
    $("#box").slider("clear")
    
})