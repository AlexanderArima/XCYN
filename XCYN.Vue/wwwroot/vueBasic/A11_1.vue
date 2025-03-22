<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title></title>
</head>
<body>
    <template>
        <div id="MainDiv">
            {{ count }}
        </div>
    </template>
    <script src="/lib/vue/vue.global.js"></script>
    <script>
        var { createApp } = Vue;
        var app = createApp({
            data() {
                return {
                    count: 0
                };
            }
        })

        app.mount("#MainDiv");
    </script>
</body>
</html>