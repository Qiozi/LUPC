using System;

public partial class product_part_desc : PageBase
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
        SetDescTxt(ProductID);
        BindPartDetail(ProductID);
    }

    private void BindPartDetail(int lu_sku)
    {
        ProductModel pm = ProductModel.GetProductModel(lu_sku);
        this.lbl_sku.Text = lu_sku.ToString();
        //this.txt_cost.Text = pm.product_current_cost.ToString();
        this.txt_img_sum.Text = pm.product_img_sum.ToString();
        this.txt_keywords.Text = pm.keywords;
        this.txt_long_name.Text = pm.product_name_long_en;
        this.txt_manufacturer.Text = pm.producter_serial_no;
        this.txt_manufacturer_url.Text = pm.producter_url;
        this.txt_middle_name.Text = pm.product_name;
        this.txt_other_product_sku.Text = pm.other_product_sku.ToString();
        //this.txt_price.Text = pm.product_current_price.ToString();
        this.txt_priority.Text = pm.product_order.ToString();
        this.txt_short_name.Text = pm.product_short_name;
        this.txt_supplier_sku.Text = pm.supplier_sku;

        this.CheckBox__export.Checked = pm.export;
        this.CheckBox_hot.Checked = pm.hot == 1;
        this.CheckBox_is_non.Checked = pm.is_non == 1;
        this.CheckBox_new.Checked = pm.new_product == 1;
        this.CheckBox_showit.Checked = pm.tag == 1;
        this.CheckBox_split_line.Checked = pm.split_line == 1;

        //OnSaleModel osm = new OnSaleModel();
        //OnSaleModel[] osms = osm.FindModelByProductSerialNo(lu_sku);
        //if (osms.Length == 1)
        //{
        //    this.txt_onsale_begin.Text = osms[0].begin_datetime.ToString("yyyy-MM-dd");
        //    this.txt_onsale_end.Text = osms[0].end_datetime.ToString("yyyy-MM-dd");
        //    this.txt_onsale_sold.Text = osms[0].sale_price.ToString();
        //    this.lbl_discount.Text = string.Format("discount:{0}", pm.product_current_discount.ToString());
        //    this.lbl_discount.ForeColor = System.Drawing.Color.Red;
        //}
    }

    protected void btn_save_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    ProductModel pm = ProductModel.GetProductModel(ProductID);

        //    decimal cost;
        //    //decimal.TryParse(this.txt_cost.Text, out cost);
        //    //pm.product_current_cost = cost;
        //    byte img_sum;
        //    byte.TryParse(this.txt_img_sum.Text, out img_sum);
        //    pm.product_img_sum = img_sum;

        //    pm.keywords = this.txt_keywords.Text;
        //    pm.product_name_long_en = this.txt_long_name.Text;
        //    pm.producter_serial_no = this.txt_manufacturer.Text;
        //    pm.producter_url = this.txt_manufacturer_url.Text;
        //    pm.product_name = this.txt_middle_name.Text;
        //    int other_product_sku;
        //    int.TryParse(this.txt_other_product_sku.Text, out other_product_sku);
        //    pm.other_product_sku = other_product_sku;
        //    decimal price;
        //    //decimal.TryParse(this.txt_price.Text, out price);
        //    //pm.product_current_price = price;
        //    int priority;
        //    int.TryParse(this.txt_priority.Text, out priority);
        //    pm.product_order = priority;
        //    pm.product_short_name = this.txt_short_name.Text ;
        //    pm.supplier_sku = this.txt_supplier_sku.Text;

        //    pm.export = this.CheckBox__export.Checked;
        //    pm.hot = byte.Parse(this.CheckBox_hot.Checked == true ? "1" : "0");
        //    pm.is_non = byte.Parse(this.CheckBox_is_non.Checked == true ? "1" : "0");
        //    pm.new_product = byte.Parse(this.CheckBox_new.Checked == true ? "1" : "0");
        //    pm.tag = byte.Parse(this.CheckBox_showit.Checked == true ? "1" : "0");
        //    pm.split_line = byte.Parse(this.CheckBox_split_line.Checked == true ? "1" : "0");
        //    pm.last_regdate = DateTime.Now;   
           
            
        //    //if (this.txt_onsale_end.Text.Trim().Length > 8 && this.txt_onsale_begin.Text.Trim().Length > 8 && this.txt_onsale_sold.Text .Trim().Length>0)
        //    {
        //        //OnSaleModel osm = new OnSaleModel();
        //        //OnSaleModel[] osms = osm.FindModelByProductSerialNo(ProductID);

        //        //DateTime begin;
        //        //DateTime.TryParse(this.txt_onsale_begin.Text, out begin);
        //        //DateTime end;
        //        //DateTime.TryParse(this.txt_onsale_end.Text, out end);
        //        //decimal sold;
        //        //decimal.TryParse(this.txt_onsale_sold.Text, out sold);

        //        //if (osms.Length < 1)
        //        //{
        //        //    osm.product_serial_no = ProductID;
        //        //    osm.modify_datetime = DateTime.Now;
        //        //    osm.begin_datetime = begin;
        //        //    osm.end_datetime = end;
        //        //    osm.sale_price = sold;
        //        //    osm.cost = pm.product_current_cost;
        //        //    osm.price = pm.product_current_price;
        //        //    osm.save_price = pm.product_current_price - osm.sale_price;
        //        //    pm.product_current_discount = osm.save_price;
        //        //    osm.Create();
        //        //    InsertTraceInfo(string.Format("Create onsale part:{0}", ProductID));
        //        //}
        //        //for (int i = 0; i < osms.Length; i++)
        //        //{
                    
        //        //    osms[i].begin_datetime = begin;
        //        //    osms[i].end_datetime = end;
        //        //    osms[i].sale_price = sold;
        //        //    osms[i].cost = pm.product_current_cost;
        //        //    osms[i].price = pm.product_current_price;
        //        //    osms[i].modify_datetime = DateTime.Now;
        //        //    osms[i].save_price = pm.product_current_price - osms[i].sale_price;
        //        //    pm.product_current_discount = osms[i].save_price;

        //        //    osms[i].Update();
        //        //    InsertTraceInfo(string.Format("Update onsale part:{0}", ProductID));
        //        //}
        //    }
        //    pm.Update();

        //    //string comment = this.TextBox1.Text;
        //    //string comment_short = this.
        //    //ProductDescModel pdm = new ProductDescModel();
        //    //pdm.SavePartComment(ProductID, comment，);
        //    //GenerateFile(ProductID, comment);
        //    //InsertTraceInfo(string.Format("modify part detail {0}", ProductID));

        //    //AnthemHelper.Alert(KeyFields.SaveIsOK);
        //}
        //catch (Exception ex)
        //{
        //    AnthemHelper.Alert(ex.Message);
        //}
    }

    private void GenerateFile(int sku, string desc)
    {
        FileHelper.GenerateFile(string.Format("{0}{1}_comment.html", Server.MapPath(Config.Part_Comment_Path), sku), desc);
        InsertTraceInfo(string.Format("Create file {0}", string.Format("{0}{1}_comment.html", Server.MapPath(Config.Part_Comment_Path), sku)));
    }

    public void SetDescTxt(int product_id)
    {
        ProductDescModel[] pm = ProductDescModel.FindAllByProperty("part_sku", ProductID);
        if (pm.Length > 0)
            this.TextBox1.Text = pm[0].part_comment;

        ProductModel p = ProductModel.GetProductModel(ProductID);
        this.lbl_part_name.Text = p.product_name;

    }

    public int ProductID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "product_id", -1); }
    }
    protected void btn_generate_all_file_Click(object sender, EventArgs e)
    {
        ProductDescModel[] pdms = ProductDescModel.FindAll();
        for (int i = 0; i < pdms.Length; i++)
        {
            GenerateFile(pdms[i].part_sku, pdms[i].part_comment);
        }
        AnthemHelper.Alert(KeyFields.SaveIsOK);
    }
}
