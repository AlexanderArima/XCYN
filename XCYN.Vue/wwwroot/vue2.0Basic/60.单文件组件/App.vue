<template>
    <div>
        <school></school>
        <student></student>
    </div>
</template>

<script>

    import School from "./School.vue";
    import Student from "./Student.vue"

    export default {
        name:"App",
        components:{
            School,
            Student
        }
    }

</script>
