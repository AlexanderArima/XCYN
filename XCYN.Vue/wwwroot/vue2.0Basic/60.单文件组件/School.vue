
<template>
    <div>
        <h2>学校名称：{{schoolName}}</h2>
        <br/>
        <h2>学校地址：{{schoolAddress}}</h2>
        <br/>
    </div>
</template>

<script>
    // 暴露一个模块
    export default({
            name:"School",
            data(){
                return {
                    schoolName: "北京大学",
                    schoolAddress: "北京市海淀区"
                }
            }
        }
    );

/*     // 模块化的三种方式
    // 1、分别暴露
    export const school = Vue.extend({
        
    });

    // 2、统一暴露
    const school2 = Vue.extend({

    })
    export {school2}

    // 3、默认暴露
    const school3 = Vue.extend({

    })
    export default school3

    // 使用默认暴露时有一个优势，在引入时可以简写
    import XXX from "xxx"

    // 使用分别暴露和统一暴露时，需要这样写
    import {xxx} from "xxx"

    // 由于Vue.extend可以省略，可以直接写配置对象，所以就变成了上述的形式 */
</script>
