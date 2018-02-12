window.onload = function () {

    var v = new Vue({
        el: "#div_container",
        data: {
            list_category: null,
            list_hot: null,
            list_top: null,
            Controller: GetController()
        },
    })

    layer.load(2, { time: 10 * 1000 });

    $.get("http://localhost:55898/Lottery/BBS/GetDetail",
        {
            id : GetQueryString("id")
        },
       function (data) {
            var obj = eval("(" + data + ")");
            console.log(obj.obj)
            if (obj.result == 1) {
                //论坛类别
                v.$data.list_category = obj.obj.list_category;
                //热门推荐
                var list_hot = obj.obj.list_hot;
                _.forEach(list_hot, function (value, key) {
                    value.avatar = "../../" + value.avatar;
                    value.detail_href = "detail.html?id=" + value.id
                });
                v.$data.list_hot = list_hot;
                //最新更新
                var list_top = obj.obj.list_top;
                v.$data.list_top = list_top;
                _.forEach(list_top, function (value, key) {
                    value.avatar = "../../" + value.avatar;
                });
            }
            else {
                console.log(obj.message);
            }
            layer.closeAll();
    });
}