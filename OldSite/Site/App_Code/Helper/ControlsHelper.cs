using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// ControlsHelper 的摘要说明
/// </summary>
public class ControlsHelper
{
	public ControlsHelper()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public static string GetTextBox(System.Web.UI.WebControls.TextBox tb)
    {
        return tb.Text.Trim();
    }
    public static void SetTextBox(System.Web.UI.WebControls.TextBox tb, string text)
    {
        tb.Text = text;
    }
    public static void SetTextBoxNULL(System.Web.UI.WebControls.TextBox tb)
    {
        SetTextBox(tb, "");
    }

    public static void SetAnthenTextBox(Anthem.TextBox tb, string text)
    {
        tb.Text = text;
        tb.AutoUpdateAfterCallBack = true;
    }
    public static string GetAnthenHiddenField(Anthem.HiddenField tb)
    {
        return tb.Value.Trim();
    }
    public static void SetAnthenHiddenField(Anthem.HiddenField tb, string text)
    {
        tb.Value = text;
        tb.AutoUpdateAfterCallBack = true;
    }
    public static string GetAnthenTextBox(Anthem.TextBox tb)
    {
        return tb.Text.Trim();
    }
    public static void SetAnthenTextBoxNULL(Anthem.TextBox tb)
    {
        SetAnthenTextBox(tb, "");
    }

}
