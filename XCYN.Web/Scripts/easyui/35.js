
$(function () {

    //全局对象
    global = {
        search: function () {
            console.log($("#reg_from").datebox('getValue'))
            $("#table_users").datagrid("load",{
                "user_name": $(".textbox[name='user_name']").val(),
                "reg_from": $("#reg_from").datebox('getValue'),
                "reg_to": $("#reg_to").datebox('getValue'),
            });
        }
    }

    $("#table_users").datagrid({
        width: 600,
        url: 'ashx/UserHandler.ashx',
        title: "用户列表",
        iconCls: "icon-add",
        fitColumns:true,
        columns: [[
            {
                field: 'id',
                title: '用户id',
                sortable: true,
                width: 50,
            },
            {
                field: 'user_name',
                title: '用户名',
                sortable: true,
                width: 100,
            },
            {
                field: 'reg_time',
                title: '注册时间',
                sortable: true,
                width: 100,
            }
        ]],
        pagination: true,
        pageSize: 10,
        pageList:[10,20,30],
        pageNumber: 1,
        sortName: 'id',
        sortOrder: 'ASC',
        toolbar:"#tb",
    })
})