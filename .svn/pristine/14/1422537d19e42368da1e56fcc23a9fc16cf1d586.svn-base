$(function () {
    $("#accord").accordion({
        width: 400,
        height: 300,
        onBeforeRemove:function(title,index)
        {
            //alert("title:" + title + ",index:" + index);
        },
        onRemove:function(title,index)
        {
            //alert("title:" + title + ",index:" + index);
        },
        onAdd:function(title,index)
        {
            //alert("title:" + title + ",index:" + index);
        }
    });
	
    //getPanel获取指定面板，第一个参数可以是panel的索引或者标题
    //console.log($("#accord").accordion("getPanel", "accordion1"));
    //console.log($("#accord").accordion("getPanel", 0));

    //getPanelIndex获取面板的索引，第一个参数是jquery的选择器
    //console.log($("#accord").accordion("getPanelIndex", "#accordion1"))

    //select选择指定面板，第一个参数可以是panel的索引或者标题
    //$("#accord").accordion("select", 1)

    //unselect取消选择指定面板，第一个参数可以是panel的索引或者标题
    //$("#accord").accordion("unselect", 0)

	//add新增面板
     $("#accord").accordion("add", {
         "title": "新标签",
         selected:false,
		 closable:true,
		 content:"<p>新内容</p>",
		 collapsible:true,
     });
	
	//remove移除面板
	//$("#accord").accordion("remove",0);
})