require.config({
    baseUrl: "../../plugin",
    paths: {
        "vue": "vue/vue",
        "layui": "layui/layui",
        "jquery": "jquery/jquery-1.11.1.min",
        "layer": "layer/layer",
    },
    waitSeconds: 15,
    map: {
        //在require.config配置map
        '*': {
            'css': 'require/css.min'
        }
    },
    shim: {
        'layui': {
            exports: 'layui',
            deps: [
                //layui依赖月layui.css文件
                'css!/plugin/layui/css/layui.css'
            ]
        }
    }
});

require(['vue', 'layui', 'jquery', "layer"],
    function (Vue, layui, $, layer) {

        $.get("http://localhost:55898/Lottery/BBS/GetIndex", function (data) {
            var obj = eval("(" + data + ")");
            if (obj.result == 1)
            {
                var list_category = obj.obj.list_category;
                for (var i = 0; i < list_category.length; i++) {
                    $("#div_category").append("<a href=\"\">  "+list_category[i].category_name+"  </a>|");
                }
               
                console.log(obj.obj);
            }
            else
            {
                console.log(obj.message);
            }
            
        });
        
    });