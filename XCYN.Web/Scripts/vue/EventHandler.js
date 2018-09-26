var app = new Vue({
    el: "#app",
    data: {

    },
    methods: {
        Submit: function (event) {
            console.log("提交 表单");
        },
        FormClick: function (event) {
            console.log("点击表单");
        },
        Valid: function () {
            console.log("验证");
        }
    }
});