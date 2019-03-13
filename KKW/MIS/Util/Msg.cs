using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Util
{
    public class Msg
    {
        public const int User = 0x0400;
        public const int WM_Test = User + 101;
        public const int WM_Msg = User + 102;


        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(
            IntPtr hWnd,
            uint Msg,
            IntPtr wParam,
            ref CLS lParam);


        public static void RefreshOrderList(IntPtr intPtr)
        {
            Util.CLS cls = new Util.CLS() { Title = "刷新订单列表", Comment = "", Value = 0, Maximum = 0, clsType = Util.ClsType.RefreshOrderList };
            Util.Msg.SendMessage(intPtr
                , Util.Msg.WM_Msg
                , IntPtr.Zero
                , ref cls
                );
        }
    }

    public struct CLS
    {
        public string Title { set; get; }
        public string Comment { set; get; }
        public int Maximum { set; get; }
        public int Value { set; get; }
        public bool IsEnd { set; get; }
        public ClsType clsType { set; get; }
    }

    public enum ClsType{
        Msg,
        Pross,
        RefreshOrderList
    }

}
