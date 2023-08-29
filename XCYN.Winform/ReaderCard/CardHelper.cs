using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XCYN.Winform.ReaderCard
{
    public class CardHelper
    {
        public static bool Fun01(int ComPort)
        {
            int intOpenPortRtn = 0;    // 打开端口的返回值，144 - 0x90 - 打开端口成功
            int EdziPortID = 0;    // 端口号
            bool bUsbPort = false;    // 是否使用USB接口，false - 串口
            int iPort = 0;    // 端口号
            int rtnTemp = 0;    // 找卡方法的返回值，159 - 0x9f - 找卡成功
            byte[] pucIIN = new byte[8];    // 芯片号
            int EdziIfOpen = 0;    // 读卡模式，0 - 需要打开端口或关闭端口，1 - 无需打开端口或关闭端口
            byte[] pucSN = new byte[8];   // 证/卡芯片序列号
            int puiCHMsgLen = 0;
            int puiPHMsgLen = 0;

            // 找卡
            // 检查是否是USB口连接
            for (int i = 1001; i <= 1016; i++)
            {
                intOpenPortRtn = CardSDK.SDT_OpenPort(i);
                if (intOpenPortRtn == 144)
                {
                    EdziPortID = i;
                    bUsbPort = true;
                    break;
                }
            }

            // 检测是否是串口连接
            if (!bUsbPort)
            {
                intOpenPortRtn = CardSDK.SDT_OpenPort(ComPort);
                if (intOpenPortRtn == 144)
                {
                    EdziPortID = 8;
                    bUsbPort = false;
                }
            }

            iPort = EdziPortID;
            if (intOpenPortRtn != 144)
            {
                // 连接失败
                return false;
            }

            // 找卡
            rtnTemp = CardSDK.SDT_StartFindIDCard(EdziPortID, pucIIN, EdziIfOpen);
            if (rtnTemp != 159)
            {
                rtnTemp = CardSDK.SDT_StartFindIDCard(EdziPortID, pucIIN, EdziIfOpen);  // 再找卡
                if (rtnTemp != 159)
                {
                    // 找卡失败
                    rtnTemp = CardSDK.SDT_ClosePort(EdziPortID);
                    return false;
                }
            }

            // 选卡
            rtnTemp = CardSDK.SDT_SelectIDCard(EdziPortID, pucSN, EdziIfOpen);
            if (rtnTemp != 144)
            {
                rtnTemp = CardSDK.SDT_SelectIDCard(EdziPortID, pucSN, EdziIfOpen);  // 再选卡
                if (rtnTemp != 144)
                {
                    rtnTemp = CardSDK.SDT_ClosePort(EdziPortID);
                    return false;
                }
            }

            // 注意，在这里，用户必须有应用程序当前目录的读写权限
            byte[] baseMsg = new byte[256];
            byte[] fpmsg = new byte[1024];
            byte[] pic = new byte[1024];
            rtnTemp = CardSDK.SDT_ReadBaseMsg(iPort, baseMsg, ref puiCHMsgLen, pic, ref puiPHMsgLen, EdziIfOpen);
            if (rtnTemp != 144)
            {
                rtnTemp = CardSDK.SDT_ClosePort(EdziPortID);
                return false;
            }

            rtnTemp = CardSDK.SDT_ClosePort(EdziPortID);
            byte[] bt = baseMsg;
            string str = UnicodeEncoding.Unicode.GetString(bt);
            var name = UnicodeEncoding.Unicode.GetString(bt, 0, 30).Trim();
            var sex  = UnicodeEncoding.Unicode.GetString(bt, 30, 2).Trim();
            string strBird = UnicodeEncoding.Unicode.GetString(bt, 36, 16).Trim();
            var birthDay = Convert.ToDateTime(strBird.Substring(0, 4) + "年" + strBird.Substring(4, 2) + "月" + strBird.Substring(6) + "日").ToString();
            var address = UnicodeEncoding.Unicode.GetString(bt, 52, 70).Trim();
            var iDCard = UnicodeEncoding.Unicode.GetString(bt, 122, 36).Trim();
            var folk = UnicodeEncoding.Unicode.GetString(bt, 32, 4).Trim();
            if (string.IsNullOrEmpty(folk))
            {
                folk = "99";
            }

            var TXZH = string.Empty;
            try
            {
                string txzhm = UnicodeEncoding.Unicode.GetString(bt, 188, 68).Trim();
                Regex reg = new Regex(@"[a-zA-Z][0-9]+");
                Match m = reg.Match(txzhm);
                GroupCollection gs = m.Groups;
                TXZH = gs[0].ToString();
            }
            catch
            {
                TXZH = string.Empty;
            }

            byte[] src = new byte[38556];
            byte[] bmp = new byte[38862];
            string uuid = Guid.NewGuid().ToString();
            var directory_image = string.Empty;
            directory_image = "Images";
            string path = string.Format("{0}\\{1}", Application.StartupPath, directory_image);
            string savepath = string.Format("{0}\\{1}.wlt", path, uuid);
            FileStream fs;
            using (fs = new FileStream(savepath, FileMode.Create, FileAccess.ReadWrite);)
            {
                fs.Write(pic, 0, pic.Length);
            }
          
            int b = CardSDK.GetBmp(savepath, 2);
            var imgPath = path + string.Format("\\{0}.bmp", uuid);
            return true;
        }
    }
}
