window.onload = function () {

    //请求数据
    var GetIndex = function(category_id) {
        $.get("http://localhost:55898/Lottery/BBS/GetIndex",
                {
                    category_id: category_id
                },
                function (data) {
                    var obj = eval("(" + data + ")");
                    //console.log(obj.obj)
                    if (obj.result == 1) {
                        //论坛类别
                        var list_category = obj.obj.list_category;
                        _.forEach(list_category, function (value, key) {
                            value.is_active = value.is_active == 1 ? "is_active" : "";
                        })
                        v.$data.list_category = list_category;
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
                            value.detail_href = "detail.html?id=" + value.id;
                        });
                    }
                    else {
                        console.log(obj.message);
                    }
                    layer.closeAll();
                });
    }

    //绑定数据
    var v = new Vue({
        el: "#div_container",
        data: {
            list_category: null,
            list_hot: null,
            list_top: null,
            Controller: GetController(),
            category_id:0,
        },
        methods: {
            GetCategory: function (category_id) {
                //按照类别区分
                console.log(this);
                //将当前的选中项赋值active类
                v.$data.category_id = category_id;
                GetIndex(category_id);
            }
        }
    })

    //加载动画
    layer.load(2, { time: 10 * 1000 });

    GetIndex(0);
}
