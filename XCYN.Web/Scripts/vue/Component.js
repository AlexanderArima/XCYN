Vue.component('todo-item', {
    props: ['todo'],    //todo是传入这个组件的参数
    template: '<li>{{ todo }}</li>'   //模板
})

var app7 = new Vue({
    el: '#app-7',
    data: {
        groceryList: [ 
           '蔬菜' ,
           '奶酪' ,
           '随便其它什么人吃的东西',
        ]
    },
})