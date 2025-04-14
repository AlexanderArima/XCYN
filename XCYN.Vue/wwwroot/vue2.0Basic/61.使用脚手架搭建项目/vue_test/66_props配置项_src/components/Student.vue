<template>
    <div>
        <h2>学生姓名：{{Name}}</h2>
        <br/>
        <h2>学生年龄：{{Age}}</h2>
        <br/> 
    </div>
</template>

<script>
    export default {
        name:"Student",
        data(){
            return {
                studentName: "张三",
                studentAge: 18
            }
        },
        // 进行简单配置
        props:["Name","Age"]

        // 限制数据类型
        /* props:{
            Name:String,
            Age:Number
        } */

        // 限制数据类型，是否必填，默认值
        /* props:{
            Name:{
                type:String,
                required:true
            },
            Age:{
                type:Number,
                default:0
            }
        } */
    }
</script>
