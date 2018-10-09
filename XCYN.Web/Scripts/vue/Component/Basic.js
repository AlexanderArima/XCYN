
Vue.component("base-userregister",
    {
        data: function () {
            return {
                UserName: "111111",
                Password:"hello world"
            };
        },
        methods: {
            SubmitClick: function () {
                console.log("UserName:"+this.UserName);
            }
        },
        template: `<div>
                            <table>
                                <tr>
                                    <td><label>用户名：</label></td>
                                    <td><input type="text" v-model="UserName"/></td>
                                </tr>
                                <tr>
                                    <td><label>密码：</label></td>
                                    <td><input type="password" v-model="Password"/></td>
                                </tr>
                                <tr>
                                    <td colspan="2"><input type="submit" value="提交" v-on:click='SubmitClick'></td>
                                </tr>
                            </table>
                        </div>`
    });

Vue.component("base-bbspost", {
    data: function () {
        return {};
    },
    props: {
        title: String,
        message: String
    },
    template: `<div>
                        <h2>{{title}}</h2>
                        <div>{{message}}</div>
                    </div>`
});

Vue.component("base-changefont", {
    data: function () {
        return {
            fontSize: [10, 12, 14, 16, 18, 20],
            font:10
        };
    },
    methods: {
        fontChange: function (event) {
            //调用父控件的方法
            this.$emit('font-change', this.font);
        }
    },
    template: `
        <div>
            <label>字体大小：</label>
            <select v-on:change="fontChange" v-model="font">
                <option v-for='item in fontSize'>{{item}}</option>
            </select>
            <label>px</label>
        </div>
    `
});

new Vue({
    el: "#app",
    data: {
        fontSize:10
    },
    methods: {
        fontChange: function (size) {
            this.fontSize = size;
        }
    }
});