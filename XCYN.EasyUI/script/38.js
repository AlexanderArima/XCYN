
$(function () {

    $.extend($.fn.validatebox.defaults.rules, {
        date: {
            validator: function (value, param) {
                return RQcheck(value);
            },
            message: '请输入正确的日期'
        }
    });

    //验证日期
    function RQcheck(RQ) {
        var date = RQ;
        var result = date.match(/^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2})$/);

        if (result == null)
            return false;
        var d = new Date(result[1], result[3] - 1, result[4]);
        return (d.getFullYear() == result[1] && (d.getMonth() + 1) == result[3] && d.getDate() == result[4]);

    }

    //全局对象
    global = {
        editable:true,
        search: function () {
            console.log($("#reg_from").datebox('getValue'))
            $("#table_users").datagrid("load",{
                "user_name": $(".textbox[name='user_name']").val(),
                "reg_from": $("#reg_from").datebox('getValue'),
                "reg_to": $("#reg_to").datebox('getValue'),
            });
        },
        add: function () {
            if (!this.editable)
            {
                return;
            }
            //if ($("#button_save").css("display") != "none")
            //{
            //    //正在编辑，则返回
            //    return;
            //}
            //开启编辑状态
            $("#table_users").datagrid('insertRow', {
                index: 0,
                row: {
                    user_name: "",
                    reg_time :"",
                }
            })
            $("#table_users").datagrid('beginEdit', 0)
            //显示保存和取消按钮
            $("#button_save").show();
            $("#button_cancel").show();
            this.editable = false;
        },
        save: function () {
           //结束编辑
           $("#table_users").datagrid("endEdit",0);
        },
        cancel: function () {
            $("#button_save").hide();
            $("#button_cancel").hide();
            this.editable = true;
            //回滚所有编辑
            $("#table_users").datagrid("rejectChanges");
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
                hidden:true,
            },
            {
                field: 'user_name',
                title: '用户名',
                sortable: true,
                width: 100,
                editor: {
                    type: 'validatebox',
                    options: {
                        required:true,
                    }
                }
            },
            {
                field: 'reg_time',
                title: '注册时间',
                sortable: true,
                width: 100,
                editor: {
                    type: 'datebox',
                    options: {
                        required: true,
                        validType: ['date']
                    }
                }
            }
        ]],
        pagination: true,
        pageSize: 10,
        pageList:[10,20,30],
        pageNumber: 1,
        sortName: 'id',
        sortOrder: 'ASC',
        toolbar: "#tb",
        onAfterEdit:function(rowIndex, rowData, changes)
        {
            //编辑结束后
            $("#button_save").hide();
            $("#button_cancel").hide();
            global.editable = true;
            console.log(rowData);
        },
    })
})