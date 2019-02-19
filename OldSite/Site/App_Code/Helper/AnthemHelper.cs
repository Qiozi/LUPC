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
/// AnthemHelper 的摘要说明
/// </summary>
public class AnthemHelper
{
	public AnthemHelper()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
    }
    #region Scropt for client side eval
    public static void Alert(string str)
    {
        Anthem.Manager.AddScriptForClientSideEval("alert('" + str + "');");
    }

    public static void Alert(int str)
    {
        Anthem.Manager.AddScriptForClientSideEval("alert('" + str.ToString() + "');");
    }

    public static void Redirect(string path)
    {
        Anthem.Manager.AddScriptForClientSideEval("window.location.href='" + path + "';");
    }

    /// <summary>
    /// showModalDialog
    /// </summary>
    /// <param name="filename"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    public static void OpenWin(string filename, int width, int height, int left, int top)
    {
        Anthem.Manager.AddScriptForClientSideEval("window.open('" + filename + "', '','scrollbars=yes,top=" + top + ", left=" + left + ",width=" + width + ", height=" + height + "');");
    }
    public static void OpenWinModelDialog(string filename, int width, int height, int left, int top)
    {
        // Anthem.Manager.AddScriptForClientSideEval("window.showModalDialog('" + filename + "', '', 'status:0;help:0;dialogleft:" + left + "px;dialogtop:" + top + "px;dialogheight:" + height + "px;dialogwidth:" + width + "px');");
        Anthem.Manager.AddScriptForClientSideEval("window.open('" + filename + "', '', 'scrollbars=yes,, left=" + left + ",top=" + top + ", height=" + height + ",width=" + width + "');");
    }

    public static void Close()
    {
        Anthem.Manager.AddScriptForClientSideEval("window.close();");
    }
    #endregion

    #region TextBox
    public static string GetTextBox(System.Web.UI.WebControls.TextBox tb)
    {
        return tb.Text.Trim();
    }

    public static int GetTextBoxAndInt(System.Web.UI.WebControls.TextBox tb, int defaultReturn)
    {
        try
        {
            return int.Parse(GetTextBox(tb));
        }
        catch { }
        return defaultReturn;
    }

    public static double GetTextBoxAndDouble(System.Web.UI.WebControls.TextBox tb, double defaultReturn)
    {
        try
        {
            return double.Parse(GetTextBox(tb));
        }
        catch { }
        return defaultReturn;
    }

    public static decimal GetTextBoxAndDecimal(System.Web.UI.WebControls.TextBox tb, decimal defaultReturn)
    {
        try
        {
            return decimal.Parse(GetTextBox(tb));
        }
        catch { }
        return defaultReturn;
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

    public static string GetAnthemTextBox(Anthem.TextBox tb)
    {
        return tb.Text.Trim();
    }
    public static void SetAnthenTextBoxNULL(Anthem.TextBox tb)
    {
        SetAnthenTextBox(tb, "");
    }
    #endregion

    #region HiddlenField
    public static string GetAnthenHiddenField(Anthem.HiddenField tb)
    {
        return tb.Value.Trim();
    }
    public static void SetAnthenHiddenField(Anthem.HiddenField tb, string text)
    {
        tb.Value = text;
        tb.AutoUpdateAfterCallBack = true;
    }
    #endregion

    #region Bottons
    public static void SetAnthemButton(Anthem.Button btn, string txt)
    {
        btn.Text = txt;
        btn.AutoUpdateAfterCallBack = true;
    }

    public static void SetAnthemButtonEnabled(Anthem.Button btn, bool b)
    {
        btn.Enabled = b;
        btn.AutoUpdateAfterCallBack = true;
    }
    #endregion

    #region dropdownlist
    public static int GetDropDownList(Anthem.DropDownList ddl)
    {
        return int.Parse(ddl.SelectedValue);
    }

    public static void SetDropDownListValue(Anthem.DropDownList ddl, string value)
    {
        for (int i = 0; i < ddl.Items.Count; i++)
        {
            ddl.Items[i].Selected = false;
            if (value == ddl.Items[i].Value.ToString())
                ddl.Items[i].Selected = true;

        }
        ddl.AutoUpdateAfterCallBack = true;
    }

    public static void SetDropDownListText(Anthem.DropDownList ddl, string text)
    {
        for (int i = 0; i < ddl.Items.Count; i++)
        {
            ddl.Items[i].Selected = false;
            if (text == ddl.Items[i].Text.ToString())
                ddl.Items[i].Selected = true;

        }
        ddl.AutoUpdateAfterCallBack = true;
    }

    public static void BindDropDownList(Anthem.DropDownList ddl, DataTable dt, string value_fields, string text_fields, bool autoUpdate)
    {
        ddl.DataSource = dt;
        BindDropDownList(ddl, value_fields, text_fields, autoUpdate);
    }

    public static void BindDropDownList(Anthem.DropDownList ddl, string value_fields, string text_fields, bool autoUpdate)
    {
        ddl.DataTextField = text_fields;
        ddl.DataValueField = value_fields;
        ddl.DataBind();
        ddl.AutoUpdateAfterCallBack = autoUpdate;
    }

    public static void ClearDropDownList(Anthem.DropDownList ddl)
    {
        ddl.Items.Clear();
        ddl.AutoUpdateAfterCallBack = true;
    }

    public static void SetDropDownListCheckItem(Anthem.DropDownList ddl)
    {
        ListItem li = new ListItem("Select", "-1");
        ddl.Items.Insert(0, li);
        ddl.AutoUpdateAfterCallBack = true;

    }

    #endregion

    #region Label
    public static string GetLabel(Anthem.Label lbl)
    {
        return lbl.Text.Trim();
    }

    public static void SetLabel(Anthem.Label lbl, string text)
    {
        lbl.Text = text;
        lbl.AutoUpdateAfterCallBack = true;
    }
    #endregion

    #region DataGrid
    public static void BindAnthemDataGrid(Anthem.DataGrid dg, bool autoUpdate)
    {
        dg.DataBind();
        dg.AutoUpdateAfterCallBack = autoUpdate;
    }

    public static int GetAnthemDataGridCellText(DataGridItem item, int cellindex)
    {
        return int.Parse(item.Cells[cellindex].Text.Trim());
    }


    public static decimal GetAnthemDataGridCellTextDecimal(DataGridItem item, int cellindex)
    {
        return decimal.Parse(item.Cells[cellindex].Text.Trim());
    }
    public static string GetAnthemDataGridCellTextBoxText(DataGridItem item, int cellindex, string keyStr)
    {
        return AnthemHelper.GetAnthemTextBox((Anthem.TextBox)item.Cells[cellindex].FindControl(keyStr));
    }

    public static void SetAnthemDataGridCellTextBoxText(DataGridItem item, int cellindex, string keyStr, string text)
    {
        Anthem.TextBox txt = (Anthem.TextBox)item.Cells[cellindex].FindControl(keyStr);
        txt.Text = text;
        txt.AutoUpdateAfterCallBack = true;

    }
    public static int GetAnthemDataGridCellDropDownList(DataGridItem item, int cellindex, string keyStr)
    {

        try
        {
            Anthem.DropDownList ddl = (Anthem.DropDownList)item.Cells[cellindex].FindControl(keyStr);
            return int.Parse(ddl.SelectedValue);
        }
        catch
        { }
        return -1;
    }


    public static bool GetAnthemDataGridCellCheckBoxChecked(DataGridItem item, int cellindex, string keyStr)
    {
        Anthem.CheckBox cb = (Anthem.CheckBox)item.Cells[cellindex].FindControl(keyStr);
        return cb.Checked;
    }

    public static void SetAnthemDataGridCellCheckBoxChecked(DataGridItem item, int cellindex, string keyStr, bool b)
    {
        Anthem.CheckBox cb = (Anthem.CheckBox)item.Cells[cellindex].FindControl(keyStr);
        cb.Checked = b;
        cb.AutoUpdateAfterCallBack = true;
    }

    public static string GetAnthemDataGridCellCheckBoxText(DataGridItem item, int cellindex, string keyStr)
    {
        Anthem.CheckBox cb = (Anthem.CheckBox)item.Cells[cellindex].FindControl(keyStr);
        return cb.Text;
    }

    public static int GetAnthemDataGridCellTextBoxTextInt(DataGridItem item, int cellindex, string keyStr)
    {
        string v = GetAnthemDataGridCellTextBoxText(item, cellindex, keyStr);
        try
        {
            int value = int.Parse(v);
            return value;
        }
        catch
        {
            AnthemHelper.Alert("请填写数字类型");
        }
        return -1;
    }

    public static decimal GetAnthemDataGridCellTextBoxTextDecimal(DataGridItem item, int cellindex, string keyStr)
    {
        string v = GetAnthemDataGridCellTextBoxText(item, cellindex, keyStr);
        try
        {
            decimal value = decimal.Parse(v);
            return value;
        }
        catch
        {
            AnthemHelper.Alert("请填写数字类型");
        }
        return -1;
    }
    public static double GetAnthemDataGridCellTextBoxTextDouble(DataGridItem item, int cellindex, string keyStr)
    {
        string v = GetAnthemDataGridCellTextBoxText(item, cellindex, keyStr);
        try
        {
            double value = double.Parse(v);
            return value;
        }
        catch
        {
            AnthemHelper.Alert("请填写数字类型");
        }
        return -1;
    }

    public static float GetAnthemDataGridCellTextBoxTextFloat(DataGridItem item, int cellindex, string keyStr)
    {
        string v = GetAnthemDataGridCellTextBoxText(item, cellindex, keyStr);
        try
        {
            float value = float.Parse(v);
            return value;
        }
        catch
        {
            AnthemHelper.Alert("请填写数字类型");
        }
        return -1;
    }
    #endregion

    #region Repeater
    public static void BindAnthemRepeater(Anthem.Repeater rpt, bool autoUpdate)
    {
        rpt.DataBind();
        rpt.AutoUpdateAfterCallBack = autoUpdate;
    }
    #endregion

    #region CheckBox
    public static void SetCheckBox(Anthem.CheckBox cb, bool value)
    {
        cb.Checked = value;
        cb.AutoUpdateAfterCallBack = true;
    }

    public static void SetCheckBox(Anthem.CheckBox cb, int value)
    {
        cb.Checked = value == 1 ? true : false;
        cb.AutoUpdateAfterCallBack = true;
    }

    #endregion

    #region d
    public static string GetAnthemDataGridByLinkButton(DataGridItem item, int index, int controls_index)
    {
        LinkButton lb = (LinkButton)item.Cells[index].Controls[controls_index];
        return lb.Text;
    }
    #endregion

    #region Panel
    public static void SetAnthemPanelVisible(Anthem.Panel p, bool b)
    {
        p.Visible = b;
        p.AutoUpdateAfterCallBack = true;
    }
    #endregion
}
