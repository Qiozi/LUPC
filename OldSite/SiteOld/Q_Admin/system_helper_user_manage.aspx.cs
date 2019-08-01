﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Q_Admin_system_helper_user_manage : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.ValidateLoginRule(Role.default_index_page);
            InitialDatabase();
        }
    }

    public override void InitialDatabase()
    {
        base.InitialDatabase();
        BindUserList(false);

        BindModelsCheckBoxList(false);
    }

    #region Methods
    public void BindUserList(bool autoUpdate)
    {
        this.dg_user_list.DataSource = StaffModel.FindAll();
        this.dg_user_list.DataBind();
        this.dg_user_list.UpdateAfterCallBack = autoUpdate;
         
    }

    public void BindModelsCheckBoxList(bool autoUpdate)
    {
        this.cbl_models.DataSource = RoleHelper.RoleToDataTable();
        this.cbl_models.DataTextField = KeyText.description;
        this.cbl_models.DataValueField = KeyText.value;
        this.cbl_models.DataBind();
        this.cbl_models.UpdateAfterCallBack = autoUpdate;
    }

    public void SetRultModelsValue()
    {
        RuleModel[] rms = RuleModel.FindModelsByStaffAndModel(CurrentUserID);
        BindModelsCheckBoxList(true);
        for (int i = 0; i < rms.Length; i++)
        {
            for (int j = 0; j < this.cbl_models.Items.Count; j++)
            {
               
                if (rms[i].model_id == int.Parse(this.cbl_models.Items[j].Value))
                {
                    this.cbl_models.Items[j].Selected = true;
                }
            }
        }
        this.cbl_models.UpdateAfterCallBack = true;
    }
    #endregion

    #region Events

    protected void dg_user_list_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        AnthemHelper.Alert("没开通");
    }
    protected void dg_user_list_EditCommand(object source, DataGridCommandEventArgs e)
    {
        this.dg_user_list.EditItemIndex = e.Item.ItemIndex;
        this.BindUserList(true);
    }
    protected void dg_user_list_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            string staff_id = ((TextBox)e.Item.Cells[0].Controls[0]).Text.Trim();
            string login_name = ((TextBox)e.Item.Cells[1].Controls[0]).Text.Trim();
            string real_name = ((TextBox)e.Item.Cells[2].Controls[0]).Text.Trim();

            StaffModel sm = StaffModel.GetStaffModel(int.Parse(staff_id));
            sm.staff_login_name = login_name;
            sm.staff_realname = real_name;
            sm.Update();

            AnthemHelper.Alert(KeyFields.SaveIsOK);
            this.dg_user_list.EditItemIndex = -1;
            this.BindUserList(true);
        }
        catch (Exception ex)
        {
            AnthemHelper.Alert(ex.Message);
        }
    }
    protected void dg_user_list_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        this.dg_user_list.EditItemIndex = -1;
        this.BindUserList(true);
    }
    protected void dg_user_list_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        CurrentUserID = int.Parse(this.dg_user_list.SelectedItem.Cells[0].Text);

        this.lbl_current_staff.Text = "Current Staff: &nbsp;" + StaffModel.GetStaffModel(CurrentUserID).staff_realname;
        this.lbl_current_staff.UpdateAfterCallBack = true;

        SetRultModelsValue();
    } 
    
    protected void btn_save_Click(object sender, EventArgs e)
    {
        RuleModel.DeleteModelsByStaff(this.CurrentUserID);
        for (int i = 0; i < this.cbl_models.Items.Count; i++)
        {
            if (this.cbl_models.Items[i].Selected)
            {
                RuleModel rm = new RuleModel();
                rm.model_id = int.Parse(this.cbl_models.Items[i].Value.ToString());
                rm.staff_serial_no = this.CurrentUserID;
                rm.Create();
            }
        }
        AnthemHelper.Alert(KeyFields.SaveIsOK);
    }

    #endregion


    #region fields
    public int CurrentUserID
    {
        get
        {
            object o = ViewState["CurrentUserID"];
            if (o != null)
                return int.Parse(o.ToString());
            return -1;
        }
        set { ViewState["CurrentUserID"] = value; }
    }
    #endregion
}