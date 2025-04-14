<template>
  <div>
      <h2 ref="hello">你好啊</h2>
      <school ref="sch"></school>
      <button @click="ShowMsg">点我显示ref</button>
  </div>
</template>

<script>

  import School from "./components/School.vue";

  export default {
      name:"App",
      components:{
          School
      },
      methods:{
        ShowMsg(){
          // 显示id为hello的Dom元素
          console.log(this.$refs.hello)

          // 显示sch组件的实例对象，后期在需要获取组件的值的时候会用到
          console.log(this.$refs.sch)
        }
      }
  }

</script>
