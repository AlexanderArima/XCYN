
var app = new Vue({
    el: "#app",
    data: {
        country: [
            {
                name: "ENG",
                classObj: {
                    'border_red': true,
                    'flag_ENG':true,
                },
                message: "英国是一个高度发达的资本主义国家，欧洲四大经济体之一，其国民拥有较高的生活水平和良好的社会保障制度。作为英联邦元首国、八国集团成员国、北约创始会员国、英国同时也是联合国安全理事会五大常任理事国之一。",
            },
            {
                name: "FRA",
                classObj: {
                    'border_red': false,
                    'flag_FRA': true,
                },
                message: "法国是一个高度发达的资本主义国家。欧洲四大经济体之一，是欧盟和北约创始会员国、申根公约和八国集团成员国，和欧洲大陆主要的政治实体之一 。",
            },
            {
                name: "GER",
                classObj: {
                    'border_red': false,
                    'flag_GER': true,
                },
                message: "德国是欧洲第一大经济体，也是欧盟的创始会员国之一、还是北约、申根公约、八国集团、联合国等国际组织的重要成员国。",
            },
            {
                name: "ITA",
                classObj: {
                    'border_red': false,
                    'flag_ITA': true,
                },
                message:"意大利是一个高度发达的资本主义国家，欧洲四大经济体之一，也是欧盟和北约的创始会员国，还是申根公约、八国集团和联合国等重要国际组织的成员",
            },
            {
                name: "USA",
                classObj: {
                    'border_red': false,
                    'flag_USA': true,
                },
                message:"美国是一个高度发达的资本主义国家，在两次世界大战中，美国和其他盟国取得胜利，经历数十年的冷战，在苏联解体后，成为目前唯一的超级大国，在经济、文化、工业等领域都处于全世界的领先地位。",
            },
            {
                name: "SOV",
                classObj: {
                    'border_red': false,
                    'flag_SOV': true,
                },
                message:"苏联是一个联邦制国家，由15个权利平等的苏维埃社会主义共和国按照自愿联合的原则组成，并奉行社会主义制度及计划经济政策，由苏联共产党执政 [2]  。",
            },
            {
                name: "JAP",
                classObj: {
                    'border_red': false,
                    'flag_JAP': true,
                },
                message:"日本是一个高度发达的资本主义国家，也是世界第三大经济体。其资源匮乏并极端依赖进口，发达的制造业是国民经济的主要支柱。科研、航天、制造业、教育水平均居世界前列。",
            },
        ],
        message:""
    },
    methods: {
        SelectCountry: function (index) {
            //重置
            for (var i = 0; i < this.country.length; i++) {
                app.country[i].classObj.border_red = false;
            }
            //设置背景色，简介
            app.country[index].classObj.border_red = true;
            app.message = app.country[index].message;
        }
    }
});