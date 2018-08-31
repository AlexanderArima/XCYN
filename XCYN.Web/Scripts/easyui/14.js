$(function () {
    $("#lay").layout({
		fit:true,
	});
	
	 
	//$("#lay").layout().css("display","block");
	
	//重置大小
	//$("#lay").layout("resize");
	
	//panel返回指定面板
	//console.log($("#lay").layout("panel","east"));
	
	//collapse折叠面板
	//$("#lay").layout("collapse","west")
	
	//展开面板
	//$("#lay").layout("expand","west")
	
	//删除面板
	$("#lay").layout("remove","west")
	
	//添加面板
	$("#lay").layout("add",{
		region:"west",
		title:"左西",
		width:100,
		iconCls:'icon-save',
	})
})