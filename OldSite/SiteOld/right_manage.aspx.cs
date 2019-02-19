using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class right_manage : PageBase
{
    string all_default_page = "all default page";
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            InitialDatabase();
        }
    }
    public override void InitialDatabase()
    {
        base.InitialDatabase();
        BindRightPageRadio();
       // SetRadioSelected(this.cmd_page);
       // SetPageContent(this.cmd_page);

        try
        {
            if ("-1" == part_product_category)
                this.lbl_part_product_category.Text = "&nbsp;";
            else
                this.lbl_part_product_category.Text = ProductCategoryModel.GetProductCategoryModel(int.Parse(this.part_product_category)).menu_child_name;
        }
        catch { }
    }

    public DataTable GetPageDT()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("id");
        dt.Columns.Add("text");

       
        DataRow dr = dt.NewRow();
        dr[0] = 1;
        dr[1] = all_default_page;
        dt.Rows.Add(dr);
        // Response.Write(this.cmd_page);
        if (this.part_product_category == "-1")
        {
            DataRow dr1 = dt.NewRow();
            dr1[0] = "-1";
            dr1[1] = cmd_page;
            dt.Rows.Add(dr1);
        }

        else
        {
            ProductCategoryModel pc = ProductCategoryModel.GetProductCategoryModel(int.Parse(this.part_product_category));
            if (pc != null)
            {
                DataRow dr1 = dt.NewRow();
                dr1[0] = pc.menu_pre_serial_no;
                dr1[1] = cmd_page + "    ||    <span style='color:red; '>" + ProductCategoryModel.GetProductCategoryModel(pc.menu_pre_serial_no).menu_child_name + "</span>";
                dt.Rows.Add(dr1);

                DataRow dr2 = dt.NewRow();
                dr2[0] = pc.menu_child_serial_no;
                dr2[1] = cmd_page + "    ||    <span style='color:blue'>" + pc.menu_child_name + "</span>";
                dt.Rows.Add(dr2);
            }
        }
        return dt;
    }
    private void BindRightPageRadio()
    {
        this.RadioButtonList1.DataSource = this.GetPageDT();
        this.RadioButtonList1.DataTextField = "text";
        this.RadioButtonList1.DataValueField = "id";
        this.RadioButtonList1.DataBind();
        this.RadioButtonList1.UpdateAfterCallBack = true;
    }

    public void SetRadioSelected(string text)
    {
        for (int i = 0; i < this.RadioButtonList1.Items.Count; i++)
        {
            this.RadioButtonList1.Items[i].Selected = false;
            if (this.RadioButtonList1.Items[i].Text == text)
                this.RadioButtonList1.Items[i].Selected = true;
        }
    }

    public void SetPageContent(string page_name_value)
    {
        string page_name = "default";
        if (page_name_value == "-1")
            page_name_value = "-1";
        if (page_name_value != "1")
            page_name = this.cmd_page;

        // AnthemHelper.Alert(page_name + page_name_value);
        // AnthemHelper.Alert(page_name_value);
        object o = RightModel.FindModelByRightPage(page_name, page_name_value);
        if (o != null)
        {
            RightModel rm = (RightModel)o;
            this.txt_content.Text = rm.right_content;
            this.txt_content.UpdateAfterCallBack = true;

            this.txt_left_content.Text = rm.left_content;
            this.txt_left_content.UpdateAfterCallBack = true;

            this.txt_main_content.Text = rm.main_content;
            this.txt_main_content.UpdateAfterCallBack = true;

            this.cb_top.Checked = rm.exist_top;
            this.cb_top.UpdateAfterCallBack = true;
        }
        else
        {
            this.txt_content.Text = "";
            this.txt_content.UpdateAfterCallBack = true;

            this.txt_left_content.Text = "";
            this.txt_left_content.UpdateAfterCallBack = true;

            this.txt_main_content.Text = "";
            this.txt_main_content.UpdateAfterCallBack = true;
        }
    }

    private void CreatePageContent(string page_name, string page_content, string main_content, string left_content)
    {
        try
        {
            RightModel rm = new RightModel();
            
            string page_type = this.part_product_category;

            page_type = this.RadioButtonList1.SelectedValue.ToString();

            RightModel.DeleteNull(page_name, page_type);
               
            rm.right_content = page_content;
            rm.right_page = page_name;
            rm.create_datetime = DateTime.Now;
            rm.main_content = main_content;
            rm.left_content = left_content;
            rm.part_product_category = page_type;
            rm.exist_top = this.cb_top.Checked;
            rm.Create();

            // generate file
            FileHelper fh = new FileHelper();
            GenerateFile(fh, rm);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string cmd_page
    {
        get { return Util.GetStringSafeFromQueryString(Page, "page").ToLower(); }
    }

    public string part_product_category
    {
        get {
            if (cmd_page == "product_list.asp" || cmd_page == "product_parts_detail.asp"
                || cmd_page == "product_detail.asp")
                return Util.GetStringSafeFromQueryString(Page, "id");
            else
                return "-1";
        }
    }

    public string CurrentPageValue
    {
        get
        {
            object o = ViewState["CurrentPageValue"];
            if (o != null)
                return o.ToString();
            return "-1";
        }
        set { ViewState["CurrentPageValue"] = value; }
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            CurrentPageValue = this.RadioButtonList1.SelectedItem.Value;
           
            this.SetPageContent(CurrentPageValue);
            
        }
        catch (Exception ex)
        {
            AnthemHelper.Alert(ex.Message);
        }
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.RadioButtonList1.SelectedValue == null)
            {
                AnthemHelper.Alert("Please Checked!");
                return;
                
            }
            if (this.RadioButtonList1.SelectedValue.ToString() == "1")
                CurrentPageValue = "default";
            else
                CurrentPageValue = this.RadioButtonList1.SelectedItem.Text;
            //AnthemHelper.Alert(CurrentPageValue);
            if (CurrentPageValue.IndexOf("||") != -1)
                CurrentPageValue = CurrentPageValue.Substring(0, CurrentPageValue.IndexOf("||")).Trim();
            // AnthemHelper.Alert(CurrentPageValue);
            CreatePageContent(CurrentPageValue, this.txt_content.Text, this.txt_main_content.Text, this.txt_left_content.Text);
            
            AnthemHelper.Alert(KeyFields.SaveIsOK);
        }
        catch (Exception ex)
        {
            AnthemHelper.Alert(ex.Message);
        }
    }

    private void GenerateFile(FileHelper fh, RightModel rm)
    {

        FileHelper.GenerateFile(Server.MapPath("~/adv_comment/" + rm.right_id.ToString() + "_left_comment.html"), rm.left_content);
        FileHelper.GenerateFile(Server.MapPath("~/adv_comment/" + rm.right_id.ToString() + "_main_comment.html"), rm.main_content);
        FileHelper.GenerateFile(Server.MapPath("~/adv_comment/" + rm.right_id.ToString() + "_right_comment.html"), rm.right_content);
       
    }

    protected void btn_generate_all_file_Click(object sender, EventArgs e)
    {
        FileHelper fh = new FileHelper();
        RightModel[] rm =  RightModel.FindAll();
        for(int i=0; i<rm.Length ;i++)
        {
            GenerateFile(fh, rm[i]);
        }
    }
}
