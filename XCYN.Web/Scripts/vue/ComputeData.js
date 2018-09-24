
var vm = new Vue({
    el: "#app",
    data: {
        UserName: "",
        Password: "",
        RePassword:"",
    },
    computed: {
        ValidUserName: function () {
            if (this.UserName.length <= 0) { 
                return {
                    state: 0,
                    font:"font-red",
                    msg : "用户名不能为空"
                };
            }
            else if (this.UserName.length >= 15) {
                return {
                    state: 0,
                    font: "font-red",
                    msg : "用户名长度不能超过15"
                };
            }
            else {
                return {
                    state: 1,
                    font: "font-green",
                    msg: "验证通过"
                };
            }
        },
        ValidPassword: function () {
            if (this.Password.length <= 0) {
                return {
                    state: 0,
                    font: "font-red",
                    msg: "密码不能为空"
                };
            }
            else if (/^\d{1}$/.test(this.Password[0])) {
                return {
                    state: 0,
                    font: "font-red",
                    msg: "密码首字母不能为数字"
                };
            }
            else {
                return {
                    state: 1,
                    font: "font-green",
                    msg: "验证通过"
                };
            }
        },
        ValidRePassword: function () {
            if (this.Password != this.RePassword) {
                return {
                    state: 0,
                    font: "font-red",
                    msg: "两次输入的密码必须一致"
                };
            }
            else if (this.RePassword.length <= 0) {
                return {
                    state: 0,
                    font: "font-red",
                    msg: "密码不能为空"
                };
            }
            else if (/^\d{1}$/.test(this.RePassword[0])) {
                return {
                    state: 0,
                    font: "font-red",
                    msg: "密码首字母不能为数字"
                };
            }
            else {
                return {
                    state: 1,
                    font: "font-green",
                    msg: "验证通过"
                };
            }
        },

    }
})