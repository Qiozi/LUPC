using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Q_Admin_sale_product_export_file : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            if(Util.GetInt32SafeFromQueryString(Page, "cmd", -1)== 9867054)
            {
                GenerateFile();
                Response.Write("OK _" + DateTime.Now.ToString());
                Response.End();
            }
            else
                InitialDatabase();
        }
    }

    #region methods

    public void GenerateFile()
    {
        string filename1 = "lucomp.csv";
        string filename2 = "lucomp.xml";
        string filename3 = "lucomputers.csv";
        string filename4 = "lucomputers.xml";

        DataTable dt = ProductModel.FindExportProduct(string.Empty);
        ExportXml(dt, Server.MapPath("~/export/" + filename2));
        ExportXml(dt, Server.MapPath("~/export/" + filename4));

        ExportCsv(dt, Server.MapPath("~/export/" + filename1),"\t");
        ExportCsv(dt, Server.MapPath("~/export/" + filename3),"\t");
    }

    public override void InitialDatabase()
    {
        base.InitialDatabase();

        BindListDG(false);
        BindFolderRPT(false);
    }

    private void BindListDG(bool autoUpdate)
    {
        this.dg_product_category.DataSource = ProductCategoryModel.FindCategoryLastLevel();
        this.dg_product_category.DataBind();
        this.dg_product_category.UpdateAfterCallBack = autoUpdate;
    }

    private void BindFolderRPT(bool autoUpdate)
    {
        FileHelper fh = new FileHelper();
        this.rpt_all_filename.DataSource = fh.FindAllFilenameByPath(FilePath);
        this.rpt_all_filename.DataBind();
        this.rpt_all_filename.UpdateAfterCallBack = autoUpdate;
    }


    #region export xml
    private void ExportXml(DataTable dt)
    {
        ExportXml(dt, FilePath + "\\" + Filename);
    }

    private void ExportXml(DataTable dt, string filename)
    {
      
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>");
        sb.Append("<Feed>");
        sb.Append("<Version>1.2</Version>");
        sb.Append("<Products>");

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];
            decimal price;
            decimal.TryParse(XmlHelper.ChangeString(dr["ProductPrice"].ToString()), out price);
            if (price > 0M)
            {
                sb.Append("<Product>");
                sb.Append("<VendorSku><![CDATA[" + XmlHelper.ChangeString(dr["vendorsku"].ToString()) + "]]></VendorSku>");
                sb.Append("<VendorUrl><![CDATA[" + XmlHelper.ChangeString(dr["vendorurl"].ToString()) + "]]></VendorUrl>");
                sb.Append("<ProductName><![CDATA[" + XmlHelper.ChangeString(dr["ProductName"].ToString()) + "]]></ProductName>");
                sb.Append("<ProductShortDescription><![CDATA[" + XmlHelper.ChangeString(dr["ProductShortDescription"].ToString()) + "]]></ProductShortDescription>");
                sb.Append("<ProductImageUrl><![CDATA[" + XmlHelper.ChangeString(dr["ProductImageUrl"].ToString()) + "]]></ProductImageUrl>");
                sb.Append("<ProductPrice><![CDATA[" + XmlHelper.ChangeString(dr["ProductPrice"].ToString()) + "]]></ProductPrice>");
                sb.Append("<MfrName><![CDATA[" + XmlHelper.ChangeString(dr["MfrName"].ToString()) + "]]></MfrName>");
                sb.Append("<MfrSku><![CDATA[" + XmlHelper.ChangeString(dr["MfrSku"].ToString()) + "]]></MfrSku>");
                sb.Append("<ProductCondition><![CDATA[" + XmlHelper.ChangeString(dr["ProductCondition"].ToString()) + "]]></ProductCondition>");
                sb.Append("</Product>");
            }
        }
        sb.Append("</Products>");
        sb.Append("</Feed>");
        System.IO.StreamWriter sw = new System.IO.StreamWriter(filename);
        sw.Write(sb.ToString());
        sw.Close();
    }
    #endregion

    #region export csv
    private void ExportCsv(DataTable dt)
    {
        string cht = "\t";
        if (this.ddl_csv_type.SelectedValue.ToString() == "2")
            cht = ",";
        if (this.ddl_csv_type.SelectedValue.ToString() == "3")
            cht = "<tab>";

        ExportCsv(dt, FilePath + "\\" + Filename, cht);
    }

    private void ExportCsv(DataTable dt, string filename, string split_cht)
    {

        string columns_head = "vendor_sku" + split_cht + "vendor_url" + split_cht + "product_name" + split_cht + "product_short_description" + split_cht + "product_image_url" + split_cht + "product_price" + split_cht + "mfr_name" + split_cht + "mfr_sku" + split_cht + "product_condition";
        XmlHelper xh = new XmlHelper();


        System.IO.StreamWriter sw = new System.IO.StreamWriter(filename);
        sw.WriteLine(columns_head);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            DataRow dr = dt.Rows[i];
            decimal price;
            decimal.TryParse(dr["ProductPrice"].ToString(), out price);
            if (price > 0M)
            {
                sb.Append(ChangeCSVstring(dr["vendorsku"].ToString()) + split_cht);
                sb.Append(ChangeCSVstring(dr["vendorurl"].ToString()) + split_cht);
                sb.Append(ChangeCSVstring(dr["ProductName"].ToString()) + split_cht);
                sb.Append(ChangeCSVstring(dr["ProductShortDescription"].ToString()) + split_cht);
                sb.Append(ChangeCSVstring(dr["ProductImageUrl"].ToString()) + split_cht);
                sb.Append(ChangeCSVstring(dr["ProductPrice"].ToString()) + split_cht);
                sb.Append(ChangeCSVstring(dr["MfrName"].ToString()) + split_cht);
                sb.Append(ChangeCSVstring(dr["MfrSku"].ToString()) + split_cht);
                sb.Append(ChangeCSVstring(dr["ProductCondition"].ToString()));
                sw.WriteLine(sb.ToString());
            }
        }

        sw.Close();
    }
    #endregion

    public string ChangeCSVstring(string s)
    {
        if (s.Length > 0)
        {
            s = s.Replace("\"", "").Replace(",", " ");
          //s = s.Replace("&", "&amp").Replace("'", 
        }
        return s;
    }

    #endregion
    #region porperties
    public string Filename
    {
        get { return this.txt_file_name.Text.Trim() + "." + this.txt_file_ext.Text.Trim(); }
    }
    public string FilePath
    {
        get { return Server.MapPath("~/" + this.txt_path.Text.Trim()); }
    }
    #endregion
    #region events
    protected void radio_file_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (this.radio_file_type.SelectedValue)
        {
            case "1":
                this.txt_file_ext.Text = "excel";
                break;
            case "2":
                this.txt_file_ext.Text = "csv";
                break;
            case "3":
                this.txt_file_ext.Text = "xml";
                break;
        }
        this.txt_file_ext.UpdateAfterCallBack = true;
    }
    protected void btn_save_setting_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < this.dg_product_category.Items.Count; i++)
            {
                int cid = AnthemHelper.GetAnthemDataGridCellText(this.dg_product_category.Items[i], 0);
                bool export = AnthemHelper.GetAnthemDataGridCellCheckBoxChecked(this.dg_product_category.Items[i], 1, "cb_checked");

                ProductCategoryModel pcm = ProductCategoryModel.GetProductCategoryModel(cid);
                pcm.export = export;
                pcm.Update();

            }
            this.BindListDG(true);
            AnthemHelper.Alert(KeyFields.SaveIsOK);
        }
        catch (Exception ex)
        {
            AnthemHelper.Alert(ex.Message);
        }
    }
    protected void btn_create_file_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.txt_file_name.Text.Trim().Length < 1)
            {
                AnthemHelper.Alert("please input filename");
                return;
            }
            if (this.txt_file_ext.Text.Trim().Length < 1)
            {
                AnthemHelper.Alert("please input filename");
                return;
            }
            //
            // get ids
            //
            System.Text.StringBuilder ids = new System.Text.StringBuilder();
            ids.Append("0");
            for (int i = 0; i < this.dg_product_category.Items.Count; i++)
            {
                int cid = AnthemHelper.GetAnthemDataGridCellText(this.dg_product_category.Items[i], 0);
                if (AnthemHelper.GetAnthemDataGridCellCheckBoxChecked(this.dg_product_category.Items[i], 1, "cb_checked"))
                {
                    ids.Append("," + cid.ToString());
                }
            }
            //
            //  
            //
            DataTable dt = ProductModel.FindExportProduct(ids.ToString());

            switch (this.radio_file_type.SelectedValue)
            {
                case "1"://excel

                    break;
                case "2"://csv
                    ExportCsv(dt);
                    break;
                case "3"://xml
                    ExportXml(dt);
                    break;
            }
            this.BindFolderRPT(true);
            AnthemHelper.Alert(KeyFields.SaveIsOK);
        }
        catch (Exception ex)
        {
            AnthemHelper.Alert(ex.Message);
        }
    }
    #endregion
    protected void dg_product_category_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Manage":
                int categoryid = AnthemHelper.GetAnthemDataGridCellText(e.Item, 0);
                AnthemHelper.OpenWin("../part_showit_manage.aspx?categoryID= " + categoryid.ToString(), 800, 600, 100, 100);

                break;
        }
    }
    protected void cb_export_all_CheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < this.dg_product_category.Items.Count; i++)
        {
            AnthemHelper.SetAnthemDataGridCellCheckBoxChecked(this.dg_product_category.Items[i], 1, "cb_checked", this.cb_export_all.Checked);
        }
        this.dg_product_category.UpdateAfterCallBack = true;
    }
    protected void btn_account_Click(object sender, EventArgs e)
    {
        try
        {
            string xml_name = this.txt_xml_name.Text.Trim();
            if (xml_name != "")
            {
                int count = 0;
                System.Xml.XmlTextReader xr = new System.Xml.XmlTextReader(Server.MapPath("~/export/" + xml_name));
                this.lbl_sum.Text = Server.MapPath("~/export/" + xml_name);
                while (xr.Read())
                {
                    if (xr.NodeType == System.Xml.XmlNodeType.EndElement && xr.Name == "Feed")
                    {
                        break;
                    }
                    if (xr.IsStartElement("Product"))
                        count++;
                }
                this.lbl_sum.Text = count.ToString();
            }
            else
            {
                this.lbl_sum.Text = "请输入文件名";
            }
        }
        catch (Exception ex)
        {
            this.lbl_sum.Text = ex.Message;
        }
    }
}
