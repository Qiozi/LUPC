using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for ControlHelper
/// </summary>
public class ControlHelper
{
	public ControlHelper()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Scropt for client side eval
   
    public void Alert(string str, System.Web.UI.Control c)
    {
       ScriptManager.RegisterClientScriptBlock(c, c.GetType(), "alert", "sAlert(\"" + str+ "\");", true);                    
    }

    public void RunJavaScript(string script, System.Web.UI.Control c)
    {
        ScriptManager.RegisterClientScriptBlock(c, c.GetType(), "script", script, true);
    }

    public void CloseWaitting(System.Web.UI.Control c)
    {
        ScriptManager.RegisterClientScriptBlock(c, c.GetType(), "script_RemoveLoadWait", "RemoveLoadWait();", true);
    }

    public void CloseParentWatting(System.Web.UI.Control c)
    {
        ScriptManager.RegisterClientScriptBlock(c, c.GetType(), "script", "ParentRemoveLoadWait();", true);
    }

    public void Redirect(string path, System.Web.UI.Control c)
    {
        ScriptManager.RegisterClientScriptBlock(c, c.GetType(), "Redirect", "window.location.href='" + path + "';", true);   
    }



    public static void Close( System.Web.UI.Control c)
    {
        ScriptManager.RegisterClientScriptBlock(c, c.GetType(), "Close", "window.close();", true);
    }
    #endregion

    #region TextBox
    public string GetTextBox(System.Web.UI.WebControls.TextBox tb)
    {
        return tb.Text.Trim();
    }

    public int GetTextBoxAndInt(System.Web.UI.WebControls.TextBox tb, int defaultReturn)
    {
        try
        {
            return int.Parse(GetTextBox(tb));
        }
        catch { }
        return defaultReturn;
    }

    public double GetTextBoxAndDouble(System.Web.UI.WebControls.TextBox tb, double defaultReturn)
    {
        try
        {
            return double.Parse(GetTextBox(tb));
        }
        catch { }
        return defaultReturn;
    }

    public decimal GetTextBoxAndDecimal(System.Web.UI.WebControls.TextBox tb, decimal defaultReturn)
    {
        try
        {
            return decimal.Parse(GetTextBox(tb));
        }
        catch { }
        return defaultReturn;
    }

    public void SetTextBox(System.Web.UI.WebControls.TextBox tb, string text)
    {
        tb.Text = text;
    }
    public void SetTextBoxNULL(System.Web.UI.WebControls.TextBox tb)
    {
        SetTextBox(tb, "");
    }

    public void SetAnthenTextBox(System.Web.UI.WebControls.TextBox tb, string text)
    {
        tb.Text = text;
    }

    public void SetAnthenTextBoxNULL(System.Web.UI.WebControls.TextBox tb)
    {
        SetAnthenTextBox(tb, "");
    }
    #endregion

    #region HiddlenField
    public string GetAnthenHiddenField(System.Web.UI.WebControls.HiddenField tb)
    {
        return tb.Value.Trim();
    }
    public void SetAnthenHiddenField(System.Web.UI.WebControls.HiddenField tb, string text)
    {
        tb.Value = text;
    }
    #endregion

    #region Bottons
    public void SetButton(System.Web.UI.WebControls.Button btn, string txt)
    {
        btn.Text = txt;
    }

    public void SetButtonEnabled(System.Web.UI.WebControls.Button btn, bool b)
    {
        btn.Enabled = b;
    }
    #endregion

    #region dropdownlist
    public int GetDropDownList(System.Web.UI.WebControls.DropDownList ddl)
    {
        return int.Parse(ddl.SelectedValue);
    }

    public void SetDropDownListValue(System.Web.UI.WebControls.DropDownList ddl, string value)
    {
        for (int i = 0; i < ddl.Items.Count; i++)
        {
            ddl.Items[i].Selected = false;
            if (value == ddl.Items[i].Value.ToString())
                ddl.Items[i].Selected = true;

        }
    }

    public void SetDropDownListText(System.Web.UI.WebControls.DropDownList ddl, string text)
    {
        for (int i = 0; i < ddl.Items.Count; i++)
        {
            ddl.Items[i].Selected = false;
            if (text == ddl.Items[i].Text.ToString())
                ddl.Items[i].Selected = true;

        }
    }

    public void BindDropDownList(System.Web.UI.WebControls.DropDownList ddl, DataTable dt, string value_fields, string text_fields, bool autoUpdate)
    {
        ddl.DataSource = dt;
        BindDropDownList(ddl, value_fields, text_fields, autoUpdate);
    }

    public void BindDropDownList(System.Web.UI.WebControls.DropDownList ddl, string value_fields, string text_fields, bool autoUpdate)
    {
        ddl.DataTextField = text_fields;
        ddl.DataValueField = value_fields;
        ddl.DataBind();
    }

    public void ClearDropDownList(System.Web.UI.WebControls.DropDownList ddl)
    {
        ddl.Items.Clear();
    }

    public void SetDropDownListCheckItem(System.Web.UI.WebControls.DropDownList ddl)
    {
        ListItem li = new ListItem("Select", "-1");
        ddl.Items.Insert(0, li);
    }

    #endregion

    #region Label
    public string GetLabel(System.Web.UI.WebControls.Label lbl)
    {
        return lbl.Text.Trim();
    }

    public void SetLabel(System.Web.UI.WebControls.Label lbl, string text)
    {
        lbl.Text = text;
    }
    #endregion

    #region DataGrid
    public void BindDataGrid(System.Web.UI.WebControls.DataGrid dg, bool autoUpdate)
    {
        dg.DataBind();
    }

    public int GetDataGridCellText(DataGridItem item, int cellindex)
    {
        return int.Parse(item.Cells[cellindex].Text.Trim());
    }


    public decimal GetDataGridCellTextDecimal(DataGridItem item, int cellindex)
    {
        return decimal.Parse(item.Cells[cellindex].Text.Trim());
    }
    public string GetDataGridCellTextBoxText(DataGridItem item, int cellindex, string keyStr)
    {
        return GetTextBox((System.Web.UI.WebControls.TextBox)item.Cells[cellindex].FindControl(keyStr));
    }

    public void SetDataGridCellTextBoxText(DataGridItem item, int cellindex, string keyStr, string text)
    {
        System.Web.UI.WebControls.TextBox txt = (System.Web.UI.WebControls.TextBox)item.Cells[cellindex].FindControl(keyStr);
        txt.Text = text;
    }
    public int GetDataGridCellDropDownList(DataGridItem item, int cellindex, string keyStr)
    {

        try
        {
            System.Web.UI.WebControls.DropDownList ddl = (System.Web.UI.WebControls.DropDownList)item.Cells[cellindex].FindControl(keyStr);
            return int.Parse(ddl.SelectedValue);
        }
        catch
        { }
        return -1;
    }


    public bool GetDataGridCellCheckBoxChecked(DataGridItem item, int cellindex, string keyStr)
    {
        System.Web.UI.WebControls.CheckBox cb = (System.Web.UI.WebControls.CheckBox)item.Cells[cellindex].FindControl(keyStr);
        return cb.Checked;
    }

    public void SetDataGridCellCheckBoxChecked(DataGridItem item, int cellindex, string keyStr, bool b)
    {
        CheckBox cb = (CheckBox)item.Cells[cellindex].FindControl(keyStr);
        cb.Checked = b;
    }

    public string GetDataGridCellCheckBoxText(DataGridItem item, int cellindex, string keyStr)
    {
        CheckBox cb = (CheckBox)item.Cells[cellindex].FindControl(keyStr);
        return cb.Text;
    }

    public int GetDataGridCellTextBoxTextInt(DataGridItem item, int cellindex, string keyStr)
    {
        string v = GetDataGridCellTextBoxText(item, cellindex, keyStr);
        try
        {
            int value = int.Parse(v);
            return value;
        }
        catch
        {
          this.Alert ("请填写数字类型", item);
        }
        return -1;
    }

    public decimal GetDataGridCellTextBoxTextDecimal(DataGridItem item, int cellindex, string keyStr)
    {
        string v = GetDataGridCellTextBoxText(item, cellindex, keyStr);
        try
        {
            decimal value = decimal.Parse(v);
            return value;
        }
        catch
        {
            Alert("请填写数字类型", item);
        }
        return -1;
    }
    public double GetDataGridCellTextBoxTextDouble(DataGridItem item, int cellindex, string keyStr)
    {
        string v = GetDataGridCellTextBoxText(item, cellindex, keyStr);
        try
        {
            double value = double.Parse(v);
            return value;
        }
        catch
        {
            Alert("请填写数字类型", item);
        }
        return -1;
    }
    #endregion

    #region Repeater
    public void BindRepeater(Repeater rpt, bool autoUpdate)
    {
        rpt.DataBind();
    }
    #endregion

    #region CheckBox
    public void SetCheckBox(CheckBox cb, bool value)
    {
        cb.Checked = value;
    }

    public void SetCheckBox(CheckBox cb, int value)
    {
        cb.Checked = value == 1 ? true : false;
    }

    #endregion

    #region d
    public string GetDataGridByLinkButton(DataGridItem item, int index, int controls_index)
    {
        LinkButton lb = (LinkButton)item.Cells[index].Controls[controls_index];
        return lb.Text;
    }
    #endregion

    #region Panel
    public void SetPanelVisible(Panel p, bool b)
    {
        p.Visible = b;
    }
    #endregion
}
