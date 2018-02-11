/*!
 * =====================================================
 * SUI Mobile - http://m.sui.taobao.org/
 *
 * =====================================================
 */
 
 
 //var iyear = 2017;
//var iperiods = 77;
//var dataqs = [
//  {
//      'iyear': '2016', 'iperiod': [{ "name": "1期" }, { "name": "2期" }, { "name": "3期" }, { "name": "4期" }, { "name": "5期" }]

//  },
//  {
//      'iyear': '2017', 'iperiod': [{"name":"1期"},{"name":"2期"}, {"name":"3期"}]
//  }
//]

//var longstr = '';
//for (var i = 0; i < year_qs.length; i++) {
//    var name = year_qs[i];
//    var iyearstring = name.substring(0, 4) + '年';

//    var names = name.substr(name.indexOf('+') + 1, name.length - 1);

//    var arr = [];
//    for (var j = 1; j <= parseInt(names) ; j++) {
//        var str = '"'+'第' + j + '期' +'"';

    
//        var str2 ="{"+'"'+'name' + '"'+ ":" + str + "}"

//        arr.push(str2)
  
//    }

//    var iyearstr = '"name"' + ":" + '"' + iyearstring.toString() + '"';
//     console.log(iyearstr)
//    var iyeararr ='"'+"sub"+'"'+":"+ "[" + arr.toString() + "]"
//      console.log(iyeararr)
//    var str3 = '"' + "type" + '"' + ':' + '0';
//    console.log(iyearstr + "," + iyeararr)

//    longstr +="{"+ iyearstr + "," + iyeararr+","+str3+"},"
//}

//console.log(longstr)



// jshint ignore: start
+function($){

    $.smConfig.rawCitiesData = [
        {
            "name":"2016年",  //一级
            "sub": [
                {
                    "name":"请选择"
                },
                {
                    "name":"第01期"
                },
                {
                    "name":"第02期"
                },
                {
                    "name":"第03期"
                }
            ],
            "type":0
        },
        {
            "name":"2017年",  //一级
            "sub":[
                {
                    "name":"请选择"
                },
                {
                    "name":"第03期"
                },
                {
                    "name":"第04期"
                },
                {
                    "name":"第05期"
                }
            ],
            "type":0
        }
    ];

}(Zepto);
// jshint ignore: end

/* jshint unused:false*/

+ function($) {
    "use strict";
    var format = function(data) {
        var result = [];
        for(var i=0;i<data.length;i++) {
            var d = data[i];
            if(d.name === "请选择") continue;
            result.push(d.name);
        }
        if(result.length) return result;
        return [""];
    };

    var sub = function(data) {
        if(!data.sub) return [""];
        return format(data.sub);
    };

    var getCities = function(d) {
        for(var i=0;i< raw.length;i++) {
            if(raw[i].name === d) return sub(raw[i]);
        }
        return [""];
    };

    var raw = $.smConfig.rawCitiesData;
    var provinces = raw.map(function(d) {
        return d.name;
    });
    var initCities = sub(raw[0]);

    var currentProvince = provinces[0];
    var currentCity = initCities[0];


    var provinces2 = raw.map(function(d) {
        return d.name;
    });
    var initCities2 = sub(raw[0]);
    var currentProvince2 = provinces2[0];
    var currentCity2 = initCities2[0];


    var t;
    var defaults = {

        cssClass: "periods-picker",
        rotateEffect: false,  //为了性能

        onChange: function (picker, values, displayValues) {
            var newProvince = picker.cols[0].value;
            var newCity;
            if(newProvince !== currentProvince) {
                // 如果Province变化，节流以提高reRender性能
                clearTimeout(t);

                t = setTimeout(function(){
                    var newCities = getCities(newProvince);
                    newCity = newCities[0];
                    picker.cols[1].replaceValues(newCities);
                    currentProvince = newProvince;
                    currentCity = newCity;
                    picker.updateValue();
                }, 200);
                return;
            }

            var newProvince2 = picker.cols[3].value;
            var newCity2;
            if(newProvince2 !== currentProvince2) {
                // 如果Province变化，节流以提高reRender性能
                clearTimeout(t);

                t = setTimeout(function(){
                    var newCities = getCities(newProvince2);
                    newCity2 = newCities[0];
                    picker.cols[4].replaceValues(newCities);
                    currentProvince2 = newProvince2;
                    currentCity2 = newCity2;
                    picker.updateValue();
                }, 200);
                return;
            }
            //console.log(newProvince2)
        },

        cols: [
            {
                textAlign: 'center',
                values: provinces,
                cssClass: "col-province"
            },
            {
                textAlign: 'center',
                values: initCities,
                cssClass: "col-city"
            },
            {
                textAlign: 'center',
                values: ['至'],
                cssClass: "col-w"
            },
            {
                textAlign: 'center',
                values: provinces2,
                cssClass: "col-province"
            },
            {
                textAlign: 'center',
                values: initCities2,
                cssClass: "col-city"
            }
        ]
    };

    $.fn.PeriodsPicker = function(params) {
        return this.each(function() {
            if(!this) return;
            var p = $.extend(defaults, params);
            //计算value
            if (p.value) {
                $(this).val(p.value.join(' '));
            } else {
                var val = $(this).val();
                val && (p.value = val.split(' '));
            }

            if (p.value) {
                //p.value = val.split(" ");
                if(p.value[0]) {
                    currentProvince = p.value[0];
                    currentProvince2 = p.value[0];
                    p.cols[1].values = getCities(p.value[0]);
                }
            }
            $(this).picker(p);
        });
    };

}(Zepto);

/*+ function($) {
    "use strict";
    var format = function(data) {
        var result = [];
        for(var i=0;i<data.length;i++) {
            var d = data[i];
            if(d.name === "请选择") continue;
            result.push(d.name);
        }
        if(result.length) return result;
        return [""];
    };

    var sub = function(data) {
        if(!data.sub) return [""];
        return format(data.sub);
    };

    var getCities = function(d) {
        for(var i=0;i< raw.length;i++) {
            if(raw[i].name === d) return sub(raw[i]);
        }
        return [""];
    };

    var raw = $.smConfig.rawCitiesData;
    var provinces = raw.map(function(d) {
        return d.name;
    });
    var initCities = sub(raw[0]);

    var currentProvince = provinces[0];
    var currentCity = initCities[0];


    var t;
    var defaults = {

        cssClass: "periods-picker",
        rotateEffect: false,  //为了性能

        onChange: function (picker, values, displayValues) {
            var newProvince = picker.cols[0].value;
            var newCity;
            if(newProvince !== currentProvince) {
                // 如果Province变化，节流以提高reRender性能
                clearTimeout(t);

                t = setTimeout(function(){
                    var newCities = getCities(newProvince);
                    newCity = newCities[0];
                    picker.cols[1].replaceValues(newCities);
                    currentProvince = newProvince;
                    currentCity = newCity;
                    picker.updateValue();
                }, 200);
                return;
            }
            console.log(newProvince)
        },

        cols: [
            {
                textAlign: 'center',
                values: provinces,
                cssClass: "col-province"
            },
            {
                textAlign: 'center',
                values: initCities,
                cssClass: "col-city"
            },
            {
                textAlign: 'center',
                values: ['至'],
                cssClass: "col-w"
            },
            {
                textAlign: 'center',
                values: provinces,
                cssClass: "col-province"
            },
            {
                textAlign: 'center',
                values: initCities,
                cssClass: "col-city"
            }
        ]
    };

    $.fn.PeriodsPicker = function(params) {
        return this.each(function() {
            if(!this) return;
            var p = $.extend(defaults, params);
            //计算value
            if (p.value) {
                $(this).val(p.value.join(' '));
            } else {
                var val = $(this).val();
                val && (p.value = val.split(' '));
            }

            if (p.value) {
                //p.value = val.split(" ");
                if(p.value[0]) {
                    currentProvince = p.value[0];
                    p.cols[1].values = getCities(p.value[0]);
                }
            }
            $(this).picker(p);
        });
    };

}(Zepto);*/

