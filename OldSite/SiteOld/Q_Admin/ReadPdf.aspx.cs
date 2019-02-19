using System;
using iTextSharp.text.pdf;
using System.IO;
using System.Data;

public partial class Q_Admin_ReadPdf : PageBase
{
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
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

       

    }

    public void parsePdf(String src, String dest)
    {
        PdfReader reader = new PdfReader(src);
        StreamWriter output = new StreamWriter(new FileStream(dest, FileMode.Create));
        int pageCount = reader.NumberOfPages;
        Response.Write(pageCount.ToString());
        for (int pg = 1; pg <= pageCount; pg++)
        {
            // we can inspect the syntax of the imported page
            byte[] streamBytes = reader.GetPageContent(pg);
            PRTokeniser tokenizer = new PRTokeniser(streamBytes);
            while (tokenizer.NextToken())
            {
                if (tokenizer.TokenType == PRTokeniser.TK_STRING)
                {
                    output.WriteLine(tokenizer.StringValue);
                }
            }
        }
        if (pageCount < 1)
            output.WriteLine("No data");
        output.Flush();
        output.Close();


    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        Literal1.Text = "";

        if (FileUpload1.HasFile)
        {
            string name = FileUpload1.PostedFile.FileName;                  // 客户端文件路径

            FileInfo file = new FileInfo(name);
            string filename = Server.MapPath("~/pro_img/rebate_pdf/" + file.Name);
            if (!File.Exists(filename))
                FileUpload1.SaveAs(filename);
            Response.Write(filename);
            this.TextBox1.Text = file.Name;

        }
    }
    protected void ButtonRead_Click(object sender, EventArgs e)
    {
        Literal1.Text = "";

        MFPDT = null;
        string tmpPdf = Server.MapPath("../pdf.txt");
        parsePdf(Server.MapPath("~/pro_img/rebate_pdf/" + TextBox1.Text), tmpPdf);

        StreamReader sr = new StreamReader(tmpPdf);
        string content = sr.ReadToEnd();
        sr.Close();

        StreamWriter sw = new StreamWriter(Server.MapPath("~/pdf.txt"));
        sw.Write(content);
        sw.Close();
        
        DataTable dt = new DataTable("item");
        dt.Columns.Add("MFP");
        dt.Columns.Add("UPS");
        dt.Columns.Add("Money");
        dt.Columns.Add("Begin");
        dt.Columns.Add("End");
        dt.Columns.Add("SKU");
        dt.Columns.Add("PartName");

        if (RadioButtonList1.SelectedItem == null)
            return;

        if (RadioButtonList1.SelectedItem.Text.ToLower() == "asus")
        {
            #region asus
            string[] split = new string[] { "AMOUNT:", "PROMOTION", "From", "to", "How" };
            
            string[] contArray = content.Split(split, 6, StringSplitOptions.None);

            //for (int i = 0; i < contArray.Length; i++)
              // Response.Write(contArray[i] + "<br>");


            if (contArray.Length < 4)
            {
                Response.Write("数据不对.");
                return;
            }
            int preMoneyIndex = 0;
            string[] mfps = contArray[1].IndexOf('$') == -1 ? contArray[2].Trim().Split(new char[] { '\n' }) : contArray[1].Trim().Split(new char[] { '\n' });
            for (int i = 0; i < mfps.Length; i++)
            {
                //Response.Write(mfps[i].Trim() + "<br>");
                if (mfps[i].IndexOf("$") > -1)
                {
                    string mfp = "";
                    for (int j = (preMoneyIndex > 0 ? preMoneyIndex + 1 : 0); j < i - 1; j++)
                    {
                        mfp += mfps[j].Trim();

                        if (j != i - 2)
                            mfp += " ";
                    }
                    
                    string partName = "";
                    int sku = ProductModel.FindSkuByManufacture(mfp);
                    if (sku != 0)
                        partName = ProductModel.GetProductModel(sku).product_name;

                    DataRow dr = dt.NewRow();
                    dr["MFP"] = mfp;
                    dr["UPS"] = mfps[i - 1].Trim(); 
                    dr["Money"] = mfps[i].Substring(1).Trim();
                    dr["Begin"] = beginDate.Text != "" ?beginDate.Text: contArray[4].Trim();
                    dr["end"] = endDate.Text != ""?  endDate.Text: contArray[5].Trim().Split(new char[]{'\n'})[0];
                    dr["SKU"] = sku;
                    dr["PartName"] = partName;
                    dt.Rows.Add(dr);

                    preMoneyIndex = i;
                }
            }
            MFPDT = dt;
            GridView1.DataSource = dt;
            GridView1.DataBind();
               // Response.Write(contArray[i] + "<br>");

            //sponse.Write(content.ToString());
            #endregion
        }
        else if (RadioButtonList1.SelectedItem.Text.ToLower() == "gigabyte")
        {
            #region gigabyte
            string[] split = new string[] { "From:", "to", "Only", "AMOUNT", "How" };
            string[] contArray = content.Split(split, 6, StringSplitOptions.None);
            if (contArray.Length < 4)
            {
                Response.Write("数据不对.");
                return;
            }
            for (int i = 0; i < contArray.Length; i++)
            {
               // Response.Write(contArray[i] + "<br>");
            }

            int preMoneyIndex = 0;
            string[] mfps = contArray[4].Trim().Split(new char[] {'\r' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < mfps.Length; i++)
            {

                if (mfps[i].IndexOf("$") > -1)
                {
                    //string mfp = i - 3 > 0 ? (mfps[i - 3].IndexOf("$") == -1 ? (mfps[i - 3].Trim() + " " + mfps[i - 2].Trim()) : mfps[i - 2].Trim()) : mfps[i - 2].Trim();
                    string mfp = "";
                    for (int j = (preMoneyIndex > 0 ? preMoneyIndex + 1 : 0); j < i - 1; j++)
                    {
                        mfp += mfps[j].Trim();

                        if (j != i - 2)
                            mfp += " ";
                    }
                    
                    string upc = mfps[i - 1];
                    string money = mfps[i].Replace("$", "");
                    string partName = "";
                    int sku = ProductModel.FindSkuByManufacture(mfp.Trim());
                    if (sku != 0)
                        partName = ProductModel.GetProductModel(sku).product_name;

                    DataRow dr = dt.NewRow();
                    dr["MFP"] = mfp.Trim();
                    dr["UPS"] = upc.Trim();
                    dr["Money"] = money;
                    dr["Begin"] = beginDate.Text != "" ? beginDate.Text : contArray[1].Trim();
                    dr["end"] = endDate.Text != "" ? endDate.Text : contArray[2].Trim();
                    dr["SKU"] = sku;
                    dr["PartName"] = partName;
                    dt.Rows.Add(dr);
                    preMoneyIndex = i;
                }
            }
            MFPDT = dt;
            GridView1.DataSource = dt;
            GridView1.DataBind();

            #endregion
        }
        else if (RadioButtonList1.SelectedItem.Text.ToLower() == "msi")
        {
            #region MSI
            string[] split = new string[] { "Only", "PRODUCT:", "AMOUNT:", "How" };
            string[] contArray = content.Split(split, 10, StringSplitOptions.None);
            if (contArray.Length < 4)
            {
                Response.Write("数据不对.");
                return;
            }
            for (int i = 0; i < contArray.Length; i++)
            {
                 // Response.Write(contArray[i] + "<br>");
            }


            string[] mfps = contArray[3].Trim().Split(new char[] { '\r' }, StringSplitOptions.RemoveEmptyEntries);
            int preMoneyIndex = 0;
            for (int i = 0; i < mfps.Length; i++)
            {
                //Response.Write(mfps[i] + "<br>");
                //continue;
                if (mfps[i].IndexOf("$") > -1)
                {
                   
                    string mfp = "";
                    for (int j = (preMoneyIndex > 0 ? preMoneyIndex + 1 : 0); j < i - 1; j++)
                    {
                        mfp += mfps[j].Trim();

                        if (j != i - 2)
                            mfp += " ";
                    }
                    mfp = mfp.Trim();
                    Response.Write(string.Format("{0} {1} {2}", mfp, mfps[i - 1], mfps[i]) + "<br>");

                    string upc = mfps[i - 1];
                    string money = mfps[i].Replace("$", "");
                    string partName = "";
                    // Response.Write("--" + mfp + "--");
                    int sku = ProductModel.FindSkuByManufacture(mfp);
                    if (sku != 0)
                        partName = ProductModel.GetProductModel(sku).product_name;

                    DataRow dr = dt.NewRow();
                    dr["MFP"] = mfp.Trim();
                    dr["UPS"] = upc.Trim();
                    dr["Money"] = money;
                    dr["Begin"] = beginDate.Text != "" ? beginDate.Text : contArray[1].Trim().Split(new char[] { '(', '-', ')' })[1];
                    dr["end"] = endDate.Text != "" ? endDate.Text : contArray[1].Trim().Split(new char[] { '(', '-', ')' })[2];
                    dr["SKU"] = sku;
                    dr["PartName"] = partName;
                    dt.Rows.Add(dr);

                    preMoneyIndex = i;
                }
            }
            MFPDT = dt;
            GridView1.DataSource = dt;
            GridView1.DataBind();

            #endregion
        }
        else if (RadioButtonList1.SelectedItem.Text.ToLower() == "Cooler Master".ToLower())
        {
            #region CoolerMaster
            string[] split = new string[] { "Model", "UPC", "$", "between", "and", "2." };
            string[] contArray = content.Split(split, 10, StringSplitOptions.None);
            if (contArray.Length < 4)
            {
                Response.Write("数据不对.");
                return;
            }
            for (int i = 0; i < contArray.Length; i++)
            {
                //Response.Write(contArray[i] + "<br>");
            }

            string mfp = contArray[1].Replace("#", "").Trim();
            string upc = contArray[2].Trim();

            string money = contArray[3].Split(new char[] { '\r' })[0].Trim();
            string partName = "";
            // Response.Write("--" + mfp + "--");
            int sku = ProductModel.FindSkuByManufacture(mfp);
            if (sku != 0)
                partName = ProductModel.GetProductModel(sku).product_name;

            DataRow dr = dt.NewRow();
            dr["MFP"] = mfp.Trim();
            dr["UPS"] = upc.Trim();
            dr["Money"] = money;
            dr["Begin"] = beginDate.Text != "" ? beginDate.Text : contArray[5].Trim();
            dr["end"] = endDate.Text != "" ? endDate.Text : contArray[6].Trim();
            dr["SKU"] = sku;
            dr["PartName"] = partName;
            dt.Rows.Add(dr);



            MFPDT = dt;
            GridView1.DataSource = dt;
            GridView1.DataBind();

            #endregion
        }
    }

    DataTable MFPDT
    {
        get
        {
            object obj = ViewState["MFPDT"];
            if (obj != null)
                return (DataTable)obj;
            return null;
        }
        set { ViewState["MFPDT"] = value; }
    }
    protected void ButtonSave_Click(object sender, EventArgs e)
    {
        Literal1.Text = "";

        DataTable dt = MFPDT;
        if (dt != null)
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["Money"].ToString() != "")
                {
                    DateTime begin;
                    DateTime.TryParse(dr["Begin"].ToString(), out begin);

                    DateTime enddate;
                    DateTime.TryParse(dr["end"].ToString(), out enddate);

                    SalePromotionModel spm = new SalePromotionModel();
                    spm.begin_datetime = begin;
                    spm.end_datetime = enddate;
                    spm.create_datetime = DateTime.Now;
                    spm.cost = 0M;
                    spm.pdf_filename = TextBox1.Text;
                    spm.price = 0M;
                    int SKU;
                    int.TryParse(dr["SKU"].ToString(), out SKU);
                    if (SKU < 1)
                        continue;
                    spm.product_serial_no = SKU;
                    spm.promotion_or_rebate = 2;
                    decimal save_price;
                    decimal.TryParse(dr["Money"].ToString(), out save_price);

                    spm.save_cost = save_price;
                    spm.show_it = true;
                    spm.comment = "(Canada Only)";
                    spm.Create();

                    InsertTraceInfo("add rebate sku:" + SKU.ToString());
                   
                }
            }
            Literal1.Text = "OK";
        }
    }
}
