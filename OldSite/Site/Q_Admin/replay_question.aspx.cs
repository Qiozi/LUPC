using System;

public partial class Q_Admin_replay_question : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.ValidateLoginRule(Role.Question);

            BindQuestionListDG(false);
        }
    }

    #region Porperties
    public int SerialNO
    {
        get { return (int)ViewState["SerialNO"]; }
        set { ViewState["SerialNO"] = value; }
    }
#endregion

    #region methods

    private void BindQuestionListDG(bool autoUpdate)
    {
        this.dg_question_list.DataSource = AskQuestionModel.GetModelsByAll();
        this.dg_question_list.DataBind();
        this.dg_question_list.UpdateAfterCallBack = autoUpdate;
    }

    //private void BindQuestionRPT(bool autoUpdate)
    //{
    //    this.rpt_replay_question.DataSource = AskQuestionModel.GetModelsByAll();
    //    this.rpt_replay_question.DataBind();
    //    this.rpt_replay_question.UpdateAfterCallBack = true;
    //}
    #endregion

    #region Events
    protected void dg_question_list_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Replay":
                SerialNO = AnthemHelper.GetAnthemDataGridCellText(e.Item, 0);
                this.txt_subject.Text = "RE: " + e.Item.Cells[4].Text;
                this.txt_subject.UpdateAfterCallBack = true;
                this.lbl_to_email.Text = e.Item.Cells[2].Text;
                this.lbl_to_email.UpdateAfterCallBack = true;

                var aqm = AskQuestionModel.GetAskQuestionModel(DBContext, SerialNO);
                this.lbl_email_body.Text = aqm.aq_body;
                this.lbl_email_body.UpdateAfterCallBack = true;

                break;
            case "ViewProduct":
                System.Web.UI.WebControls.LinkButton li = (System.Web.UI.WebControls.LinkButton)e.CommandSource;
                int product_id = int.Parse(li.Text);
                AnthemHelper.OpenWin("system_product_detail.aspx?product_id=" + product_id.ToString(), 600, 400, 200, 200);
                //AnthemHelper.Alert(li.Text.ToString());
                break;
        }
    }
    protected void btn_reset_Click(object sender, EventArgs e)
    {
        this.lbl_to_email.Text = "";
        this.lbl_to_email.UpdateAfterCallBack = true;

        this.txt_subject.Text = "";
        this.txt_subject.UpdateAfterCallBack = true;

        this.txt_replay_body.Text = "";
        this.txt_replay_body.UpdateAfterCallBack = true;
    }
    protected void btn_replay_Click(object sender, EventArgs e)
    {
        //string email = this.lbl_to_email.Text;
        //if (email != "")
        //{
        //    MessageClass mc = new MessageClass();
        //    //mc.ContentType = "text/html";
        //    mc.Body = this.txt_replay_body.Text ;
        //    mc.Logging = true;
        //    mc.Silent = true;
        //    mc.MailServerUserName = Config.mailUserName;
        //    mc.MailServerPassWord = Config.mailPassword;
        //    mc.From = Config.mailUserName;
        //    mc.FromName = "LUComputer";
        //    mc.Subject = this.txt_subject.Text.Trim(); ;

        //    mc.AddRecipient(email, email, null);
        //    if (mc.Send(Config.mailServer, false))
        //    {
        //        var am = AskQuestionModel.GetAskQuestionModel(DBContext, SerialNO);
        //        am.aq_send = sbyte.Parse("1");
        //        am.send_regdate = DateTime.Now;
        //        am.aq_reply_body = this.txt_replay_body.Text;
        //        DBContext.SaveChanges();
        //        AnthemHelper.Alert("OK");
        //        BindQuestionListDG(true);
        //    }
        //    else
        //    {
        //        AnthemHelper.Alert("falid");
        //    }
        //}
    }
    #endregion
    protected void dg_question_list_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        this.dg_question_list.CurrentPageIndex = e.NewPageIndex;
        this.BindQuestionListDG(true);

    }
   
}
