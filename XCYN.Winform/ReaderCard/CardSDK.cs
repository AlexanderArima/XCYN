using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Winform.ReaderCard
{
    public class CardSDK
    {
        /// <summary>
        /// 参数待修改.
        /// </summary>
        /// <param name="iComID"></param>
        /// <returns></returns>
        [DllImport("sdtapi.dll")]
        public static extern int SDT_GetCOMBaud(int iComID, ref int puiAppMsgLen);

        /// <summary>
        /// 设置串口上 SAM_V 的波特率.
        /// </summary>
        /// <param name="iComID">端口号。此处端口号必须为 1-16，表示串口.</param>
        /// <param name="uiCurrBaud">当前的波特率.</param>
        /// <param name="uiSetBaud">设置的波特率.</param>
        /// <returns>返回是否操作成功.</returns>
        [DllImport("sdtapi.dll")]
        public static extern int SDT_SetCOMBaud(int iComID, int uiCurrBaud, int uiSetBaud);

        /// <summary>
        /// 打开串口/USB.
        /// </summary>
        /// <param name="iPortID">如果是串口传1-16，如果是USB接口传1001-1016，默认是1001.</param>
        /// <returns></returns>
        [DllImport("sdtapi.dll")]
        public static extern int SDT_OpenPort(int iPortID);

        /// <summary>
        /// 关闭串口/USB.
        /// </summary>
        /// <param name="iPortID">如果是串口传1-16，如果是USB接口传1001-1016，默认是1001.</param>
        /// <returns></returns>
        [DllImport("sdtapi.dll")]
        public static extern int SDT_ClosePort(int iPortID);

        /// <summary>
        /// 开始找卡.
        /// </summary>
        /// <param name="iPortID">端口号.</param>
        /// <param name="pucIIN">证/卡芯片管理号，4 个字节.</param>
        /// <param name="iIfOpen"></param>
        /// <returns></returns>
        [DllImport("sdtapi.dll")]
        public static extern int SDT_StartFindIDCard(int iPortID, byte[] pucIIN, int iIfOpen);

        /// <summary>
        /// 选卡（参数pucSN做了修改）.
        /// </summary>
        /// <param name="iPortID"></param>
        /// <param name="pucSN">证/卡芯片序列号.</param>
        /// <param name="iIfOpen"></param>
        /// <returns></returns>
        [DllImport("sdtapi.dll")]
        public static extern int SDT_SelectIDCard(int iPortID, byte[] pucSN, int iIfOpen);

        /// <summary>
        /// 读取卡体管理号.
        /// </summary>
        /// <param name="iPortID"></param>
        /// <param name="pucManageMsg">卡体管理号。</param>
        /// <param name="iIfOpen"></param>
        /// <returns></returns>
        [DllImport("sdtapi.dll")]
        public static extern int SDT_ReadMngInfo(int iPortID, byte[] pucManageMsg, int iIfOpen);

        /// <summary>
        /// 读取证/卡固定信息.
        /// </summary>
        /// <param name="iPortID">端口号.</param>
        /// <param name="pucCHMsg">读到的文字信息.</param>
        /// <param name="puiCHMsgLen">读到的文字信息长度.</param>
        /// <param name="pucPHMsg">读到的照片信息.</param>
        /// <param name="puiPHMsgLen">读到的照片信息长度.</param>
        /// <param name="iIfOpen"></param>
        /// <returns></returns>
        [DllImport("sdtapi.dll")]
        public static extern int SDT_ReadBaseMsg(int iPortID, byte[] pucCHMsg, ref int puiCHMsgLen, byte[] pucPHMsg, ref int puiPHMsgLen, int iIfOpen);

        /// <summary>
        /// 读取追加信息.
        /// </summary>
        /// <param name="iPortID"></param>
        /// <param name="pucAppMsg">读到的追加信息.</param>
        /// <param name="puiAppMsgLen">读到的追加信息长度.</param>
        /// <param name="iIfOpen"></param>
        /// <returns></returns>
        [DllImport("sdtapi.dll")]
        public static extern int SDT_ReadNewAppMsg(int iPortID, ref byte pucAppMsg, ref int puiAppMsgLen, int iIfOpen);

        [DllImport("WltRS.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        public static extern int GetBmp(string filename, int nType);
    }
}
