<template>
    <div>
        <h2>学生姓名：{{studentName}}</h2>
        <br/>
        <h2>学生年龄：{{studentAge}}</h2>
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
        }
    }
</script>
