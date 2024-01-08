using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.XmlAndJson
{
    public class JsonToString
    {
        public static JObject Fun01()
        {
            // 将字符串转换为对象
            var str = JObject.Parse(@"{'code':'0','msg':'成功','data':{'accessToken':'KCD96','expiresIn':null,'scope':'read write','userInfo':{'id':234,'isDelete':'0','status':'启用','createDate':'2019','updateDate':'2023','username':'2365634345','userId':'234','password':'tertewewrfewsfsdfdsfsdf','gxdwbm':null,'level':null,'phone':'4324324234','email':'3423432424@qq.com','avatar':'','age':null,'type':'','address':null,'lastLoginTime':'2023','nickname':'测试名称'},'jti':'2345234234234234','urlPrefix':'https://www.baidu.com/','flag':'cs'}}");
            return str;
        }

        public static JObject Fun02()
        {
            var str = JObject.Parse(@"
            {
            'code':'0',
            'msg':'成功',
            'data':
            [
                {'configId':1,'configCode':'cf','configValue':'吃饭'},
                {'configId':2,'configCode':'hs','configValue':'喝水'}
            ]}");
            return str;
        }
    }
}
