/**
 * 浏览器检测
 * */
var client = function () {
    var engine = {
        //呈现引擎
        ie: 0,
        gecko: 0,
        webkit: 0,
        khtml: 0,
        opera: 0,
        //具体版本号
        ver: null
    };

    var browser = {
        //浏览器
        ie: 0,
        edge: 0,
        firefox: 0,
        chrome: 0,
        safari: 0,
        konq: 0,
        //具体版本
        ver: null
    };

    var system = {
        //操作系统
        win: false,
        mac: false,
        x11: false,

        //移动设备
        iphone: false,
        ipad: false,
        ipod: false,
        ios: false,
        android: false,
        nokiaN: false,
        winMobile: false,

        //游戏系统
        wii: false,
        ps: false
    }

    var ua = navigator.userAgent;

    if (/OPR\/(\S+)/.test(ua)) {
        //首先识别Opera引擎，因为它可以完全模仿其他浏览器
        engine.ver = client.browser.ver = RegExp["$1"];
        engine.opera = client.browser.opera = parseFloat(engine.ver);
    }
    else if (/AppleWebKit\/(\S+)/.test(ua)) {
        //然后识别WebKit引擎，包含特定的AppleWebKit字符串
        engine.ver = RegExp["$1"];
        engine.webkit = parseFloat(engine.ver);
        if (/Chrome\/(\S+)/.test(ua)) {
            //判断是Chrome还是Safari浏览器
            browser.ver = RegExp["$1"];
            browser.chrome = parseFloat(browser.ver);
        }
        else if (/Version\/(\S+)/.test(ua)) {
            browser.ver = RegExp["$1"];
            browser.safari = parseFloat(browser.ver);
        } else {
            //近似地确定版本号
            var safariVersion = 1;
            if (engine.webkit < 100) {
                safariVersion = 1;
            }
            else if (engine.webkit < 312) {
                safariVersion = 1.2;
            }
            else if (engine.webkit < 412) {
                safariVersion = 1.3;
            } else {
                safariVersion = 2;
            }
            browser.safari = browser.ver = safariVersion;
        }
    }
    else if (/KHTML\/(\S+)/.test(ua) || /Konqueror\/([^;]+)/.test(ua)) {
        //由于konqueror3.1及更早期的版本不包含KHTML的版本，因此要用Konqueror来代替
        engine.ver = browser.ver = RegExp["$1"];
        engine.khtml = browser.konq = parseFloat(engine.ver);
    }
    else if (/rv:(\S+\)) Gecko\/\d{8}/.test(ua)) {
        //匹配Gecko引擎
        engine.ver = RegExp["$1"];
        engine.gecko = parseFloat(engine.ver);
        if (/Firefox\/(\S+)/.test(ua)) {
            //判断是否为火狐浏览器
            browser.ver = RegExp["$1"];
            browser.firefox = parseFloat(browser.ver);
        }
    }
    else if (/MSIE([^;]+);/.test(ua) || /Edge\/(\S+)/.test(ua) || /Trident\/(7.0)/.test(ua)) {
        //匹配IE引擎
        if (/Trident\/(7.0)/.test(ua)) {
            //IE11浏览器
            engine.ver = browser.ver = "11";
            engine.ie = browser.ie = 11;
        }
        else if (/Edge\/(\S+)/.test(ua)) {
            //Edge浏览器
            engine.ver = browser.ver = RegExp["$1"];
            engine.ie = browser.edge = parseFloat(engine.ver);
        }
        else {
            engine.ver = browser.ver = RegExp["$1"];
            engine.ie = browser.ie = parseFloat(engine.ver);
        }
    }

    var p = navigator.platform;
    system.win = p.indexOf("Win") == 0;
    system.mac = p.indexOf("Mac") == 0;
    system.x11 = (p.indexOf("X11") == 0 || p.indexOf("Linux") == 0);

    if (system.win) {
        //判断windows操作系统
        if (/Win(?:dows )?([^do]{2})\s?(\d+\.\d+)?/.test(ua)) {
            if (RegExp["$1"] == "NT") {
                switch (RegExp["$2"]) {
                    case "5.0":
                        system.win = "2000";
                        break;
                    case "5.1":
                        system.win = "XP";
                        break;
                    case "6.0":
                        system.win = "Vista";
                        break;
                    case "6.1":
                        system.win = "Win7";
                        break;
                }
            }
            else if (RegExp["$1"] == "9x") {
                system.win = "ME";
            }
            else {
                system.win = RegExp["$1"];
            }
        }
    }

    system.iphone = ua.indexOf("iphone") > -1;
    system.ipad = ua.indexOf("ipad") > -1;
    system.ipod = ua.indexOf("ipod") > -1;
    if (system.mac && ua.indexOf("Mobile") > -1) {
        //检测ios版本
        if (/CPU (?:iPhone )?OS (\d+_\d+)/.test(ua)) {
            system.ios = parseFloat(RegExp["$1"].replace("_", "."));
        } else {
            system.ios = 2.0;
        }
    }
    if (/Android (\d+\.\d+)/.test(ua)) {
        //检测Android系统
        system.android = parseFloat(RegExp("$1"));
    }
    system.nokiaN = ua.indexOf("NokiaN") > -1;
    system.winMobile = (system.win == "CE");

    //识别游戏系统
    system.wii = ua.indexOf("Wii") > -1;
    system.ps = /playstation/i.test(ua);


    return {
        engine: engine,
        browser: browser,
        system: system
    };

}();

/*
 * 跨浏览器的事件处理程序
 * **/
var eventUtil = {
    addHandler: function (element, type, handler) {
        if (element.addEventListener) {
            element.addEventListener(type, handler, false);
        }
        else if (element.attachEvent) {
            element.attachEvent("on" + type, handler);
        }
        else {
            element["on" + type] = handler;
        }
    },
    removeHandler: function (element, type, handler) {
        if (element.removeEventListener) {
            element.removeEventListener(type, handler, false);
        }
        else if (element.detachEvent) {
            element.detachEvent("on" + type, handler);
        }
        else {
            element["on" + type] = null;
        }
    }
};

//获取查询字符串
function GetQueryString() {
    var qs = location.search.length > 0 ? location.search.substring(1) : "",
        args = {},//保存数据的对象
        items = qs.length > 0 ? qs.split("&") : [],
        item = null,
        name = null,
        value = null;
    for (var i = 0; i < items.length; i++) {
        item = items[i].split("=");
        name = decodeURIComponent(items[i].split("=")[0]),
            value = decodeURIComponent(items[i].split("=")[1]);
        args[name] = value;
    }
    return args;
}