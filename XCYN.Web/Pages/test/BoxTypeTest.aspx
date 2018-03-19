<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BoxTypeTest.aspx.cs" Inherits="XCYN.Web.Pages.test.BoxTypeTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
    <script type="text/javascript">

        console.log("----------------------------");
        console.log("--------------基本包装类型String，Boolean，Number--------------");
        /**
         * 引用类型与基本包装类型的主要区别就是对象的生命周期。
         * 使用new操作符创建的引用类型的实例，在执行流离开当前作用域之前都一直保存在内存中。
         * 而自动创建的基本包装类型的对象，而只存在于一行代码的执行瞬间，然后立即销毁。
         */
        var s = "hello world";
        s.color = "red";//设置的color属性，只在这一行有效
        console.log(s);
        console.log(s.color);
        var v = new String("how are you?");
        v.color = "red";//设置的color属性，一直有效
        console.log(v.toString());
        console.log(v.color);

        console.log("--------------String类型--------------");
        console.log("--------------字符方法charAt,charCodeAt--------------");
        var str = "hello world";
        console.log(str.charAt(0));
        console.log(str.charCodeAt(0));
        console.log(str[0]);//还可以通过使用方括号，加索引的方法来访问字符串中的特定字符

        console.log("--------------字符串操作方法slice,substring,substr--------------");
        console.log("--------------slice()和substring()的第一个参数指定字符串的起始位置，第二个参数指定结束位置--------------");
        console.log("--------------substr()第一个参数也是起始位置，第二个参数则是返回的字符串长度--------------");
        console.log("--------------substr()第一个参数也是起始位置，第二个参数则是返回的字符串长度--------------");
        str = "hello world";
        var str1 = str.slice(0, 5);
        var str2 = str.substring(0, 5);
        var str3 = str.substr(6, 5);
        console.log(str1);
        console.log(str2);
        console.log(str3);

        console.log("---------------字符串位置方法indexOf(),lastIndexOf()-------------");
        str = "hello world,my name is Arima";
        
        String.prototype.NumberOf = 
        /**
        * 返回某一个字符串出现的次数
        * @param str 查询的字符串
        */
        function(str)
        {
            var pos = 0;
            var num = 0;
            while (1 == 1)
            {
                pos = this.toString().indexOf(str, pos);
                if (pos == -1)
                {
                    break;
                }
                else
                {
                    pos++;
                }
                num++;
            }
            return num;
        }

        var num = str.NumberOf("a");
        console.log(num);

        console.log("---------------去除空格trim()-------------");
        var schoolName = " Wu Han Gang Du Zhong Xue ";
        console.log(schoolName.trim());

        console.log("--------------字符串大小写转换toUpperCase(),toLowerCase()--------------");
        console.log(schoolName.toUpperCase());
        console.log(schoolName.toLowerCase());

        console.log("--------------字符串的模式匹配方法match(),search(),replace()--------------");
        var suffer = "jax,sax,jpg,avi";
        //match()方法返回一个数组；数组的第1项是与整个模式匹配的字符串，之后的每一项保存着与正则表达式相匹配的字符串
        var matches = suffer.match(/.ax/);
        console.log(matches);
        //search()方法返回字符串中第1个匹配项的索引；如果么有匹配项，则返回-1.而且search()方法始终是从字符串开头向后查找。
        var searches = suffer.search(/.ax/);
        console.log(searches);

        /*
         * replace()方法接收两个参数，第1个参数可以是一个RegExp对象或者一个字符串，第2个参数可以是一个字符串或者函数。
         * 如果第1个参数是字符串，那么只会替换第一个子字符串。要想替换所有字符串，唯一的办法就是提供一个正则表达式，而且要指定全局标志(g)。
         * **/
        suffer = suffer.replace(/ax/g, "ex");
        console.log(suffer);
        console.log("--------------Global对象--------------");
        console.log("--------------URI编码方法--------------")
        /*
         * Global对象的encodeURI()和encodeURIComponent()方法可以对URI进行UTF-8编码，他们用特殊的UTF-8编码替换所有无效的字符，从而让浏览器能够接受和理解
         * encodeURI()和encodeURIComponent()的主要区别在于，encodeURI不会对本身属于URI的特殊字符进行编码，例如冒号，正斜杠，问号和井号；
         * encodeURIComponent则会对所有非标准字符进行编码
         * **/
        var uri = "http://www.baidu.com value.htm#start";
        console.log(encodeURI(uri));
        console.log(encodeURIComponent(uri));

        var uri_enc = "http%3A%2F%2Fwww.baidu.com%20value.htm%23start";
        console.log(decodeURI(uri_enc));
        console.log(decodeURIComponent(uri_enc));
        console.log("----------------------------");
    </script>
</body>
</html>
