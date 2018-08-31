
$(function () {
    $("#search").searchbox({
        menu: '#box',
        width: 300,
        prompt: '请输入关键字',
        searcher: function (value, name) {
            //value表示搜索值，name表示下拉菜单的值
            //console.log(name);
            //$('#search').searchbox("clear")
            $('#search').searchbox("reset")
        },
        disabled:false,
    });
     
    //console.log($('#search').searchbox("options"));
    //console.log($('#search').searchbox("menu"));.
    //console.log($('#search').searchbox("textbox"));
    //console.log($('#search').searchbox("getValue"));
    //console.log($('#search').searchbox("setValue","123"));
    //console.log($('#search').searchbox("getName"));
    //console.log($('#search').searchbox("selectName"));
    //console.log($('#search').searchbox("destroy"));
    //console.log($('#search').searchbox("clear"));
})