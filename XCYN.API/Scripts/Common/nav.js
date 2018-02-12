//获取查询字符串
GetQueryString = function (name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}

//获取页面名
GetPageName = function () {
    var array = window.location.pathname.split('/');
    return array[array.length - 1];
}

//获取控制器名
GetController = function () {
    var array = window.location.pathname.split('/');
    return array[array.length - 2];
}

Vue.component('nav-component', {
    template: '<div>\
                    <ul class="layui-nav" lay-filter="">\
                        <template v-if="myMessage === \'Home\'">\
                            <li class="layui-nav-item layui-this"><a href="../Home/index.html">首页</a></li>\
                            <li class="layui-nav-item"><a href="../BBS/index.html">论坛</a></li>\
                            <li class="layui-nav-item" ><a href="">问题投票</a></li>\
                        </template>\
                        <template v-else-if="myMessage === \'BBS\'">\
                            <li class="layui-nav-item"><a href="../Home/index.html">首页</a></li>\
                            <li class="layui-nav-item layui-this"><a href="../BBS/index.html">论坛</a></li>\
                            <li class="layui-nav-item" ><a href="">问题投票</a></li>\
                        </template>\
                    </ul>\
                </div>',
    props: ['myMessage']
});