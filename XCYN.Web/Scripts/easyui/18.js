$(function () {
    $(document).on("contextmenu", function (e) {
        e.preventDefault();
		$("#my_menu").menu('show',{
			left:e.pageX,
			top:e.pageY
		});
		
		//$("#my_menu").menu("hide");
		//$("#my_menu").menu("destroy");
    }) 
	
	//console.log($("#my_menu").menu("options"));
	//console.log($("#my_menu").menu("getItem","#div_sort"));
	
	// $("#my_menu").menu("setText",{
		// target:"#div_sort",
		// text:"排排"
	// })
	
	// $("#my_menu").menu("setIcon",{
		// target:"#div_sort",
		// iconCls:"icon-filter"
	// })
	
	//console.log($("#my_menu").menu("findItem","排排"))
	
	$("#my_menu").menu('appendItem',{
		text:'查看',
		iconCls:'icon-search'
	});
	
	$("#my_menu").menu({
		onClick:
		function(item)
		{
			console.log(item);
			window.location.href = item.href;
		},
		onShow:
		function()
		{
			console.log('显示菜单');
		},
		onHide:
		function()
		{
			console.log('隐藏菜单');
		}
		
	});
})