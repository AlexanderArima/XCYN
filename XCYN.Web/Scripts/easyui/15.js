$(function () {
    $("#box").window({
		width:400,
		height:300,
		title:"标题",
		draggable:false,
		resizable:false,
		closable:false,
		collapsible:false,
		maximizable:false,
		minimizable:false,
		shadow:false,
		modal:true,
		fit:true
	});
	
	//window获取整个window对象
	//console.log($("#box").window("window"));
	
	//console.log($("#box").window("body"));
	
	$(document).click(function(){
		
	});
})