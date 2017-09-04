
$(function () {

    //var timestamp3 = "/Date(15213464464)/".replace("/Date(", "").replace(")/", "");
    //alert(timestamp3);
    //var newDate = new Date();
    //newDate.setTime(timestamp3 * 1000);
    //alert(newDate.toDateString());

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
        action:"",
        editable:undefined,
        search: function () {
            console.log($("#reg_from").datebox('getValue'))
            $("#table_users").datagrid("load",{
                "user_name": $(".textbox[name='user_name']").val(),
                "reg_from": $("#reg_from").datebox('getValue'),
                "reg_to": $("#reg_to").datebox('getValue'),
            });
        },
        add: function () {
            if (this.editable != undefined)
            {
                return;
            }
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
            $("#button_save,#button_cancel").show();
            $("#table_users").datagrid('selectRow', 0);//选中第一行
            this.editable = 0;
            this.action = "add";
        },
        save: function () {
            //结束编辑
            if (this.action == "add")
            {
                
                //添加，并获取那一行的数据
                //var user_name = $("#table_users").datagrid('getEditor', { index: 0, field: 'user_name' });
                //var reg_time = $("#table_users").datagrid('getEditor', { index: 0, field: 'reg_time' });
                //console.log(reg_time);
                //$.post("../ashx/UserHandler.ashx",
                //    {
                //        action: "add",
                //        user_name: user_name,
                //        reg_time: reg_time,
                //    },
                //    function (data) {
                //        if(data == "1")
                //        {
                //            $("#table_users").datagrid("reload")
                //        }
                //    })
                //console.log(row);
            }
            $("#table_users").datagrid("endEdit", this.editable);
        },
        cancel: function () {
            $("#button_save,#button_cancel").hide();
            this.editable = undefined;
            //回滚所有编辑
            $("#table_users").datagrid("rejectChanges");
        },
        update: function () {
            //获取选中的行
            //console.log($("#table_users").datagrid("getSelected"))
            var rows = $("#table_users").datagrid("getRows");
            var selected_row = $("#table_users").datagrid("getRowIndex", $("#table_users").datagrid("getSelected"));
            if (selected_row > -1)
            {
                for (var i = 0; i < rows.length; i++) {
                    $("#table_users").datagrid("endEdit", $("#table_users").datagrid("getRowIndex", rows[i]))
                }
                $("#table_users").datagrid("beginEdit", selected_row);
                $("#button_save,#button_cancel").show();
                this.editable = selected_row;
                this.action = "update";
            }
            else
            {
                $.messager.alert("提示", "请选择一行!", "info");
            }
        }
    }

    $("#table_users").datagrid({
        width: 600,
        url: 'ashx/UserHandler.ashx?action=query',
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
        singleSelect:true,
        onAfterEdit:function(rowIndex, rowData, changes)
        {
            var inserted_row = $("#table_users").datagrid('getChanges', 'inserted');
            $.post("../ashx/UserHandler.ashx",
                {
                    action: "add",
                    user_name: inserted_row[0].user_name,
                    reg_time: inserted_row[0].reg_time,
                },
                function (data) {
                    if(data == "1")
                    {
                        $("#table_users").datagrid("reload")
                    }
                    else
                    {
                        $("#table_users").datagrid('rejectChanges');
                    }
                })
            //编辑结束后
            $("#button_save,#button_cancel").hide();
            global.editable = undefined;
            global.action = "";
        },
        onDblClickRow: function (rowIndex, rowData)
        {
            //双击行
            global.update();
        },
    })
})