<template>
  <div>
      <!-- 将值传给子组件 -->
      <Student Name="张三" :Age="20"></Student>
  </div>
</template>

<script>

  import Student from "./components/Student.vue";

  export default {
      name:"App",
      components:{
        Student
      },
  }

</script>
