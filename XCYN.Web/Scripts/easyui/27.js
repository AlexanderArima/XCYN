
$(function () {

    var buttons = $.extend([], $.fn.datebox.defaults.buttons);
    buttons.splice(1, 0, {
        text: 'MyBtn',
        handler: function (target) {
            alert("MyBtn");
        }
    });
     
    $("#div_date").datebox({
        panelWidth: 200,
        panelHeight: 200,
        currentText: 'today',//今天按钮文字
        closeText: 'close',//关闭按钮文字
        //disabled:true,
        buttons: buttons,
        onSelect:function(date)
        {
            alert(date.getFullYear() + "-" + (date.getMonth() + 1) + "-" + date.getDate());
        }
    })

    $("#div_date").datebox("calendar").calendar({
        firstDay: 1,
        weeks: ['1', '2', '3', '4', '5', '6', '7', ],
    });

    $("#div_date").datebox("setValue",'2017-06-25')

})