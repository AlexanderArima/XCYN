
$(function () {
    $("#table_users").datagrid({
        width: 400,
        url: 'ashx/UserHandler.ashx',
        title: "用户列表",
        iconCls:"icon-add",
        columns: [[
            {
                field: 'id',
                title: '用户id',
                sortable: true,
                halign: "center",
                hidden: true,
            },
            {
                field: 'user_name',
                title: '用户名',
                sortable: true,
                width: 180,
                halign: "center",//头部标题居中
                formatter: function (value, row, index) {
                    //自定义每行的内容
                    return value + "(" + row.id + ")"
                },
                styler:function(value,row,index)
                {
                    //设置每行的样式
                    if (value.length <= 6)
                    {
                        return "color:red";
                    }
                },
            },
            {
                field: 'reg_time',
                title: '注册时间',
                sortable: true,
                width: 100,
                halign: "center",
            }
        ]],
        pagination: true,
        pageSize: 10,
        pageList:[10,20,30],
        pageNumber: 1,
        sortName: 'id',
        sortOrder: 'ASC',
        striped: true,//显示斑马线
        showHeader:true,//显示页头
        rownumbers: true,//显示行号
        showFooter: false,//显示页尾
        singleSelect: true,//只选中一行
        rowStyler:function(index,row)
        {
            //返回样式
            if(row.user_name.indexOf("@") > 0)
            {
                return "color:blue";
            }
        }
    })
})