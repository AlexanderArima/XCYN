using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading;

/// <summary>
/// 线程中安全访问控件，避免重复的delegate,Invoke
/// </summary>
public static class CrossThreadCalls
{
    public delegate void TaskDelegate();

    private delegate void InvokeMethodDelegate(Control control, TaskDelegate handler);

    /// <summary>
    /// .net2.0中线程安全访问控件扩展方法，可以获取返回值，可能还有其它问题
    /// </summary>
    /// CrossThreadCalls.SafeInvoke(this.statusStrip1, new CrossThreadCalls.TaskDelegate(delegate()
    /// {
    ///    tssStatus.Text = "开始任务...";
    /// }));
    /// CrossThreadCalls.SafeInvoke(this.rtxtChat, new CrossThreadCalls.TaskDelegate(delegate()
    /// {
    ///     rtxtChat.AppendText("测试中");
    /// }));
    /// 参考：http://wenku.baidu.com/view/f0b3ac4733687e21af45a9f9.html
    /// <summary>
    public static void SafeInvoke(Control control, TaskDelegate handler)
    {
        if (control.InvokeRequired)
        {
            while (!control.IsHandleCreated)
            {
                if (control.Disposing || control.IsDisposed)
                    return;
            }
            IAsyncResult result = control.BeginInvoke(new InvokeMethodDelegate(SafeInvoke), new object[] { control, handler });
            control.EndInvoke(result);//获取委托执行结果的返回值
            return;
        }
        IAsyncResult result2 = control.BeginInvoke(handler);
        control.EndInvoke(result2);
    }

    /// <summary>
    /// 线程安全访问控件,扩展方法.net3.5用Lambda简化跨线程访问窗体控件,避免重复的delegate,Invoke
    /// this.statusStrip1.SafeInvoke(() =>
    /// {
    ///     tsStatus.Text = "开始任务....";
    /// });
    /// this.rtxtChat.SafeInvoke(() =>
    /// {
    ///     rtxtChat.AppendText("测试中");
    /// });
    /// </summary>
    //public static void SafeInvoke(this Control control, TaskDelegate handler)
    //{
    //    if (control.InvokeRequired)
    //    {
    //        while (!control.IsHandleCreated)
    //        {
    //            if (control.Disposing || control.IsDisposed)
    //                return;
    //        }
    //        IAsyncResult result = control.BeginInvoke(new InvokeMethodDelegate(SafeInvoke), new object[] { control, handler });
    //        control.EndInvoke(result);//获取委托执行结果的返回值
    //        return;
    //    }
    //    IAsyncResult result2 = control.BeginInvoke(handler);
    //    control.EndInvoke(result2);
    //}

}