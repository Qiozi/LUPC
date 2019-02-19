using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.OleDb;

public partial class Q_Admin_product_helper_import_store_price_2 : PageBase
{
    const string UPLOAD_FILE_PATH = "~/ltd_upload_file_store/";
    const string UPLOAD_FILE_PATH_BACKUP = "~/Q_Admin/product_update_excel_file_store/";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Util.GetInt32SafeFromQueryString(Page, "cmd", -1) == 9867054)
            {
                SwitchFile();
                Response.Write("end_" + DateTime.Now.ToString());
                Response.End();
            }
            else
            {
                InitialDatabase();
                CH.CloseParentWatting(this.btn_download_info);
            }
        }
    }

    private void SwitchFile()
    {
        DirectoryInfo dir = new DirectoryInfo(Server.MapPath(UPLOAD_FILE_PATH));
        FileInfo[] fis = dir.GetFiles();
        for (int i = 0; i < fis.Length; i++)
        {
            try
            {
                if (fis[i].FullName.IndexOf("xls") != -1)
                {
                    UpdateDB(fis[i].FullName);
                    Response.Write(fis[i].FullName+"<br> save is ok "+ DateTime.Now.ToString() +"<br>" );
                    fis[i].MoveTo(Server.MapPath(UPLOAD_FILE_PATH_BACKUP + "ltd_upload_file_store_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls"));
                }
            }
            catch (Exception ex) { Response.Write(ex.Message); }

        }
    }

    public override void InitialDatabase()
    {
        base.InitialDatabase();
        BindLtd();
        BindLtdGridView();
    }

    private void BindLtdGridView()
    {
        //LtdHelper lh = new LtdHelper();
        //this.gv_ltd_store_sum.DataSource = lh.LtdHelperToDataTable();
        //this.gv_ltd_store_sum.DataBind();
    }

    private void UpdateDB(string filename)
    {
        using (OleDbConnection conn = new OleDbConnection(Config.ExcelConnstring(filename)))
        {
            conn.Open();
            // [Ltd_code],[Ltd_sku],[Ltd_cost],[Ltd_stock],[Ltd_manufacture_code],[Ltd_part_name]
            OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM [table$]", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            //this.GridView1.DataSource = dt;
            //this.GridView1.DataBind();

            int error_sum = 0; int success_sum = 0;
            //CH.Alert(dt.Rows.Count.ToString(), this.btn_download_info);
            SaveCostStore(dt, ref error_sum, ref success_sum);
            CH.Alert(success_sum + " Upload Success " + (error_sum != 0 ? " <br> " + error_sum + " Create error" : ""), this.file_upload_store_price);
        }
    }

    protected void btn_upload_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.file_upload_store_price.PostedFile != null)
            {
                string newFilename = Server.MapPath(string.Format("{0}{1}",
                  Config.update_product_data_excel_path,
                  string.Format("cost_store_{0}.xls", DateTime.Now.ToString("yyyyMMddhhmmss"))));
                this.file_upload_store_price.PostedFile.SaveAs(newFilename);

                UpdateDB(newFilename);
                System.IO.FileInfo fi = new System.IO.FileInfo(newFilename);
                fi.Delete();
                CH.CloseParentWatting(this.file_upload_store_price);
            }

        }
        catch (Exception ex)
        {
            CH.Alert(ex.Message, this.file_upload_store_price);
            CH.CloseParentWatting(this.file_upload_store_price);
        }
    }

    private void SaveCostStore(DataTable dt, ref int error_sum, ref int success_sum)
    {
        LtdHelper ltd = new LtdHelper();
        
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            
            DataRow dr = dt.Rows[i];
            int lit_code;
            int.TryParse(dr["Ltd_code"].ToString(), out lit_code);

            if (i == 0)
                //Config.ExecuteNonQuery("update tb_product_store_sum set product_store_sum=0 , tag=0 where product_store_category='" + lit_code.ToString() + "'");
                Config.ExecuteNonQuery("delete from  tb_other_inc_part_info  where other_inc_id='" + lit_code.ToString() + "'");
            string ltd_sku = dr["Ltd_sku"].ToString();

            decimal ltd_cost ;
            decimal .TryParse(dr["Ltd_cost"].ToString(),out ltd_cost);

            int ltd_stock ;
            int.TryParse(dr["Ltd_stock"].ToString(), out ltd_stock);

            string part_name = dr["Ltd_part_name"].ToString();
            string manufture_code = dr["Ltd_manufacture_code"].ToString().Trim();

            if (lit_code == ltd.LtdHelperValue(Ltd.lu))
            {
                ProductModel pm = ProductModel.GetProductModel(int.Parse(ltd_sku));
                pm.product_store_sum = ltd_stock;
                pm.last_regdate = DateTime.Now;
                
                pm.Update();
                success_sum += 1;
            }
            else
            {
                //ProductStoreSumModel[] pssm = ProductStoreSumModel.FindBySKUAndLtd(ltd_sku, lit_code);
                //OtherIncPartInfoModel[] pssm = OtherIncPartInfoModel.FindBySKUAndLtd(ltd_sku, lit_code);
                    //int exist_count = Config.ExecuteScalarInt32(string.Format(@"select count(*) from tb_other_inc_part_info where other_inc_id='{0}' and other_inc_sku='{1}'", lit_code,ltd_sku));
                
                //for(int j=0; j<pssm.Length ; j++){
                //    if (j == 0)
                //{
                //    pssm[0].other_inc_price = ltd_cost;
                //    pssm[0].other_inc_store_sum = ltd_stock;
                //    pssm[0].last_regdate = DateTime.Now;
                //    if (manufture_code.Trim().Length > 0)
                //        pssm[0].manufacture_part_number = manufture_code;                 
                   
                        
                //    pssm[0].tag = true;
                //    pssm[0].Update();
                //    success_sum += 1;
                //}
                //else
                //        pssm[j].Delete();
                //}
                //if (pssm.Length == 0)                
                {
                    try
                    {
                        OtherIncPartInfoModel psm = new OtherIncPartInfoModel();
                        psm.manufacture_part_number = manufture_code;
                        psm.other_inc_price = ltd_cost;
                        psm.other_inc_sku = ltd_sku;
                        psm.other_inc_id = lit_code;
                        psm.other_inc_store_sum = ltd_stock;
                        psm.last_regdate = DateTime.Now;
                        psm.tag = true;                       
                        
                        success_sum += 1;
                        //throw new Exception(string.Format("{0}|{1}", ltd_sku, lit_code));
                        int _sku;
                        bool exist_match_lu_sku = false;
                        DataTable skuDT = Config.ExecuteDataTable(string.Format(@"select lu_sku 
from tb_other_inc_match_lu_sku where other_inc_type='{0}' and other_inc_sku='{1}'", lit_code, ltd_sku));
                        if (skuDT.Rows.Count == 0)
                        {
                            _sku = ProductModel.FindSkuByManufacture(manufture_code);
                        }
                        else
                        {
                            int.TryParse(skuDT.Rows[0][0].ToString(), out _sku);
                            exist_match_lu_sku = true;
                        }
                        psm.luc_sku = _sku;
                        psm.Create();
                        if (_sku > 0 && !exist_match_lu_sku)
                        {
                            //Config.ExecuteNonQuery(string.Format("delete from  tb_other_inc_match_lu_sku where other_inc_type='{0}' and other_inc_sku='{1}' and lu_sku='{2}'", lit_code,ltd_sku, _sku));
                            OtherIncMatchLuSkuModel oimsm = new OtherIncMatchLuSkuModel();
                            oimsm.lu_sku = _sku;
                            oimsm.other_inc_sku = ltd_sku;
                            oimsm.other_inc_type = lit_code;
                            oimsm.Create();
                        }
                    }
                    catch (Exception ex) {
                        error_sum += 1;
                        Response.Write("<br>lid_id = "+ lit_code +" ;;;; ltd_sku=" +ltd_sku+"__"+ ex.Message + "<br>");
                    }
                }
            }
            
        }
       
    }

    private void BindLtd()
    {
        LtdHelper lh = new LtdHelper();
        this.ddr_ltd_category.DataSource = lh.LtdHelperToDataTable();
        this.ddr_ltd_category.DataTextField = "text";
        this.ddr_ltd_category.DataValueField = "id";
        this.ddr_ltd_category.DataBind();
    }

    protected void btn_download_info_Click(object sender, EventArgs e)
    {
        try
        {
            LtdHelper lh = new LtdHelper();
            DataTable dt = new DataTable();
            int ltd_id;
            int.TryParse(this.ddr_ltd_category.SelectedValue.ToString(), out ltd_id);
            if (ltd_id == lh.LtdHelperValue(Ltd.lu))
            {
                ProductModel pm = new ProductModel();
                dt = pm.FindModelToStoreAndCostAndMenufacture();
            }
            else
            {
                ProductStoreSumModel pssm = new ProductStoreSumModel();
                dt = pssm.FindModelsByLtdID(ltd_id);
            }
            CH.CloseParentWatting(this.btn_download_info);
            ExcelHelper eh = new ExcelHelper(dt);
            eh.MaxRecords = 20000;
            eh.FileName = "table.xls";
            eh.Export();
            
        }
        catch (Exception ex)
        {
            CH.Alert(ex.Message, this.btn_download_info);
        }
    }
//    protected void gv_ltd_store_sum_RowDataBound(object sender, GridViewRowEventArgs e)
//    {
       
//        if (e.Row.RowType != DataControlRowType.Footer
//            && e.Row.RowType != DataControlRowType.Header
//            && e.Row.RowType != DataControlRowType.Pager)
//        {
//            int ltd_id;
//            int.TryParse(e.Row.Cells[0].Text, out ltd_id);

//            if (ltd_id == 1)
//            {
//                ProductModel pm = new ProductModel();
//                e.Row.Cells[2].Text = pm.FindModelSum().ToString();
//            }
//            else
//            {
//                //OtherIncPartInfoModel pssm = new OtherIncPartInfoModel();
//                e.Row.Cells[2].Text =Config.ExecuteScalarInt32("select count(*) c from tb_other_inc_part_info where other_inc_id='"+ ltd_id+"'").ToString()  ;

//                e.Row.Cells[3].Text = Config.ExecuteScalarInt32("select count(*) c from tb_other_inc_part_info where tag=1 and other_inc_id='" + ltd_id + "'").ToString();

//                e.Row.Cells[4].Text = Config.ExecuteScalarInt32(@"select count(*) c from tb_other_inc_match_lu_sku ol inner join tb_other_inc_part_info oi
// on oi.other_inc_sku=ol.other_inc_sku and other_inc_id='" + ltd_id + "'").ToString();

                
//            }
//        }
//    }
    protected void btn_run_Click(object sender, EventArgs e)
    {
        try
        {


            string error = "";
            //Config.ExecuteNonQuery(@"delete from tb_other_inc_part_info  where other_inc_id=0");
            Config.ExecuteNonQuery(@"delete from tb_other_inc_match_lu_sku where other_inc_type not in (select id from tb_other_inc where id <> 50);
delete from tb_other_inc_part_info where other_inc_id not in (select id from tb_other_inc where id <> 50);");
//            DataTable dt = Config.ExecuteDataTable(@"select product_serial_no from tb_product p 
//inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no where p.tag=1 and split_line=0 and is_non=0 and pc.tag=1");

            DataTable dt = Config.ExecuteDataTable(@"select product_serial_no from tb_product p where (p.tag=1 or p.issue=0) and split_line=0 and is_non=0 and menu_child_serial_no in ("+new GetAllValidCategory().ToString() +")");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    int lu_sku;
                    int.TryParse(dt.Rows[i][0].ToString(), out lu_sku);
                    decimal cost = 0M;
                    int max_stock = 0;
                    int min_stock = 0;
                    int sum_stock = 0;

                    decimal _cost;

                    DataTable _storeDT = Config.ExecuteDataTable(@"select other_inc_price product_cost, other_inc_store_sum product_store_sum from tb_other_inc_match_lu_sku ol inner join tb_other_inc_part_info oi
on oi.other_inc_sku=ol.other_inc_sku and  tag=1 and lu_sku='" + lu_sku.ToString() + "'");
                    for (int j = 0; j < _storeDT.Rows.Count; j++)
                    {
                        DataRow dr = _storeDT.Rows[j];
                        int _stock;
                        int.TryParse(dr["product_store_sum"].ToString(), out _stock);
                        if (_stock > 0)
                            sum_stock += _stock;
                        if (_stock > max_stock)
                            max_stock = _stock;
                        if (_stock < min_stock)
                            min_stock = _stock;


                        decimal.TryParse(dr["product_cost"].ToString(), out _cost);
                        if (max_stock > 0)
                        {
                            if (_stock > 0)
                            {
                                if (_cost < cost)
                                {
                                    cost = _cost;
                                }
                            }
                        }
                        else
                        {
                            if (_stock > 0)
                                cost = _cost;
                            else
                            {
                                if (_cost < cost)
                                    cost = _cost;
                            }
                        }
                    }

                    if (sum_stock == 0)
                    {
                        sum_stock = min_stock;
                    }
                    ProductModel pm = ProductModel.GetProductModel(lu_sku);
                    pm.product_current_cost_2 = cost;
                    pm.ltd_stock = sum_stock;
                    pm.last_regdate = DateTime.Now;
                    pm.Update();
                    //if (lu_sku == 3783)
                    //{
                    //    CH.Alert(string.Format("{0}|{1}|{2}|{3}|{4}", lu_sku, sum_stock, max_stock, min_stock, cost), this.btn_run);
                    //    return;
                    //}
                }
                catch (Exception ex)
                {
                    error += ex.Message + "<br>";

                }
            }
            CH.CloseParentWatting(this.btn_run);
            if (error != "")
            {
                CH.Alert(error, this.btn_run);
            }else

            CH.Alert(KeyFields.SaveIsOK, this.btn_run);
        }
        catch (Exception ex)
        {
            CH.CloseParentWatting(this.btn_run);
            CH.Alert(ex.Message, this.btn_run);
        }
    }
}
