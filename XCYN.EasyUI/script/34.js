
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
                width: 100,
            },
            {
                field: 'user_name',
                title: '用户名',
                sortable: true,
                width: 180,
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
        sortOrder:'ASC',
    })
})