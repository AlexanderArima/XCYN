<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegExpTest.aspx.cs" Inherits="XCYN.Web.Pages.test.RegExpTest" %>

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
        /**
         * 字面量模式，匹配第一个“bat”或“cat”，不区分大小写
         */
        var pattern1 = /\[bc\]at/gi;

        /**
         * 构造函数模式，与pattern1相同，只不过是使用构造函数创建的
         */
        var pattern2 = new RegExp("\\[bc\\]at", "i");

        
        /*
         * 由于RegExp构造函数的模式的参数是字符串，所以在某些情况下要对字符串进行双重转义。所有元字符都必须双重转义，那些已经转义过得字符也是如此，
         * 例如：字符“\”在字符串中通常被转义为“\\”，而在正则表达式字符串中国就变成了了“\\\\”
         */

        /*
         * 正则表达式的元字符包括：( [ { \ ^ $ | ? * . + } ] )
         * /

        console.log(pattern1.global);//是否设置了g标志
        console.log(pattern1.ignoreCase);//是否设置了i标志
        console.log(pattern1.multiline);//是否设置了m标志
        console.log(pattern1.lastIndex);
        console.log(pattern1.flags);
        console.log(pattern1.source);//正则表达式的字符串表示，按照字面量形式而非传入构造函数中的字符串模式返回

        console.log("----------RegExp实例方法-----------");
        console.log("----------exec()方法接收一个参数，即匹配的字符串，然后返回包含第一个匹配项的数组;或者没有匹配项的情况下返回null。----------");
        var text = "cat,bat,sat,fat";
        var pattern = /.at/g;
        var matches = pattern.exec(text);
        console.log("----------index属性表示匹配项在字符串中的位置----------");
        console.log(matches.index);
        console.log(matches[0]);
        console.log(pattern.lastIndex);

        matches = pattern.exec(text);
        console.log(matches.index);
        console.log(matches[0]);
        console.log(pattern.lastIndex);
        console.log("----------在全局模式下每次调用exec()都会返回字符串中下一个匹配项，直至搜索到字符串末尾为止。----------");
        console.log("----------此外模式中的lastIndex属性，在全局模式下每次调用exec()后会增加，而在非全局模式下始终保持不变----------");

        console.log("-----------不使用全局标志(g)-----------");
        pattern = /.at/;
        matches = pattern.exec(text);
        console.log(matches.index);
        console.log(matches[0]);
        console.log(pattern.lastIndex)

        matches = pattern.exec(text);
        console.log(matches.index);
        console.log(matches[0]);
        console.log(pattern.lastIndex);

        console.log("----------test()方法接收一个字符串参数，匹配字符串返回true，否则返回false----------");
        var text = "000-00-0000";
        pattern = /\d{3}-\d{2}-\d{4}/;
        if (pattern.test(text)) {
            console.log("The pattern is matched.");
        }
        console.log(pattern.toString());
        console.log("\\");
    </script>
</body>
</html>
