window.onload = function () {

    var v = null;

    layer.load(2, { time: 10 * 1000 });

    $.get("http://localhost:55898/Lottery/BBS/GetReplyCount",
        {
            id: GetQueryString("id")
        },
        function (data) {
            var object = eval("(" + data + ")");
            if (object.result == 1) {
                //获取总数
                var count = object.obj;
                layui.use('laypage', function () {
                    var laypage = layui.laypage;
                    //执行一个laypage实例
                    laypage.render({
                        elem: 'test1' //注意，这里的 test1 是 ID，不用加 # 号
                      , count: count //数据总数，从服务端得到
                      , limit: 5
                      , jump: function (obj, first) {
                          //首次绑定Vue
                          if (first) {
                              v = new Vue({
                                  el: "#div_container",
                                  data: {
                                      post: null,
                                      list_reply: null,
                                      pageIndex: 0,
                                      pageSize:5,
                                      Controller: GetController()
                                  },
                              });
                          }
                          v.$data.pageIndex = obj.curr - 1;
                          $.get("http://localhost:55898/Lottery/BBS/GetDetail",
                             {
                                 id: GetQueryString("id"),
                                 pageIndex: obj.curr - 1,
                                 pageSize: obj.limit,
                             },
                            function (data) {
                                var object = eval("(" + data + ")");
                                //console.log(object.obj)
                                if (object.result == 1) {
                                    var post = object.obj.post;
                                    v.$data.post = post;
                                    v.$data.post.avatar = '..\\..\\' + post.avatar;
                                    var list_reply = object.obj.list_reply;
                                    for (var i = 0; i < list_reply.length; i++) {
                                        list_reply[i].avatar = "..\\..\\" + list_reply[i].avatar;
                                    }
                                    v.$data.list_reply = list_reply;
                                    
                                }
                                else {
                                    console.log(object.message);
                                }
                                layer.closeAll();
                            });
                        }
                    });
                });
            }
            else {
                console.log(object.message);
            }
        });
}