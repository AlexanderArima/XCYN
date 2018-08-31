$(function () {
	$("#div_panel").panel({
		title:"标题",
		width:400,
		height:200,
		iconCls:"icon-no",
		//left:200,
		//top:200,
		cls:"a",
		headerCls:"b",
		bodyCls:"c",
		style:{
			'min-height':'250px'
		}, 
		content:"<h1>我的面板，已刷新</h1>",
		//fit:true,
		//border:true,
		//doSize:false,
		//noheader:true,
		collapsible:true,//初始化时折叠窗口
		//minimizable:true,//初始化时最小化窗口
		//maximized:true
		closable:true,
		href: "Handler1.ashx",
		extractor: function (data)
		{
		    return data;
		},
		//loadingMessage:"....."
		//tools:"#tt",
		// tools:[
		// {
			// iconCls:"icon-help",
			// handler:function(){
				// alert("help");
			// }
		// },
		// {
			// iconCls:"icon-ok",
			// handler:function(){
				// alert("ok");
			// }
		// }
		// ]
		// onBeforeLoad:function(){
			// alert("加载之前触发");
		// },
		// onLoad:function(){
			// alert("加载之后触发");
		// },
		// onBeforeOpen:function(){
			// alert("打开之前触发");
		// },
		// onOpen:function(){
			// alert("打开之后触发");
		// },
		// onBeforeClose:function(){
			// alert("关闭之前触发");
			//return false;
		// },
		// onClose:function(){
			// alert("关闭之后触发");
		// },
		// onBeforeDestroy:function(){
			// alert("销毁之前触发");
		// },
		// onDestroy:function(){
			// alert("销毁之后触发");
		// },
		// onBeforeCollapse:function(){
			// alert("折叠之前触发");
			//return false;
		// },
		// onCollapse:function(){
			// alert("折叠之后触发");
		// },
		// onBeforeExpand:function(){
			// alert("展开之前触发");
		// },
		// onExpand:function(){
			// alert("展开之后触发");
		// },
		onResize:function(width,height){
			//alert("width:"+width+",height:"+height);
			//alert("改变面板大小");
		},
		onMove:function(left,top){
			alert("left:"+left+",top:"+top);
			alert("面板移动后触发");
		}
	});
	
	$("#div_panel").panel("move",{"left":"100","top":"100"});
	//$("#div_panel").panel("resize",{"height":"500","width":"600"});
	//$("#div_panel").panel("destroy");
	//$("#div_panel").panel("panel").css("position","absolute");
})