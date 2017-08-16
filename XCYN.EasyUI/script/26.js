
$(function () {
    $("#div_date").calendar({
        width: 200,
        height: 200,
        //fit:true,
        //border:false,
        //firstDay:2,//起始星期
        //weeks: ['1', '2', '3', '4', '5', '6', '7',],//显示周列表内容
        //months: ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月'],//显示月的列表内容
        /*
        year: 2017,
        month: 10,
        formatter: function (date) {
            return '#' + date.getDate();
        },
        */
        onSelect: function (date) {
            alert(date);
        },//选择日期s
    });

    //移动日历到指定日期
    $("#div_date").calendar("moveTo",new Date(2017,9,7))
})