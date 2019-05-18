using System;
using System.Runtime.InteropServices;

namespace XCYN.Common.ActiveX
{
    /// <summary>
    /// IE会查询ActiveX组件是否实现了IObjectSafety接口，如果实现了该接口IE会认为脚本是安全的
    /// </summary>
    [Guid("CB5BDC81-93C1-11CF-8F20-00805F2CD064"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComImport]
    public interface IObjectSafety
    {

        /// <summary>
        /// 由COM客户端取得此组件的安全选项。
        /// </summary>
        /// <param name="riid">接口代码</param>
        /// <param name="pdwSupportedOptions">支持的安全性选项</param>
        /// <param name="pdwEnabledOptions">启用选项</param>
        /// <returns></returns>
        [PreserveSig]
        int GetInterfaceSafetyOptions(ref Guid riid, [MarshalAs(UnmanagedType.U4)] ref int pdwSupportedOptions, [MarshalAs(UnmanagedType.U4)] ref int pdwEnabledOptions);

        /// <summary>
        /// 由COM客户端设定此组件的安全选项。
        /// </summary>
        /// <param name="riid"></param>
        /// <param name="dwOptionSetMask"></param>
        /// <param name="dwEnabledOptions"></param>
        /// <returns></returns>
        [PreserveSig]
        int SetInterfaceSafetyOptions(ref Guid riid, [MarshalAs(UnmanagedType.U4)] int dwOptionSetMask, [MarshalAs(UnmanagedType.U4)] int dwEnabledOptions);
    }
}
