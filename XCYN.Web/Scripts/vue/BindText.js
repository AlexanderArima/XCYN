var app = new Vue({
    el: '#app',
    data: {
        c1r1: "",
        c2r1:"SQL",
        c3r1:"JavaScript",
        c4r1:"ASP.NET MVC",
        c5r1: "额外计划",
        rows: [
            { 
                c1: "查询优化",
            },
            {
                c1: "查询优化",
            },
            {
                 c4: "Linq拓展方法",
            },
            {
                c2: "性能优化",
                c3:"Razor引擎---模型对象，使用布局"
            },
            {
                c2: "学会使用JS压缩工具，JSCompress  ",
            }
        ],
        active: "active",
        normal:"normal",
        isActive:true,
    },
    methods: {
        GetFocus: function (event) {
            console.log(event);
            $("#app tr td").removeClass("td-active");
            $(event.target).addClass("td-active");
        }
    }
})

