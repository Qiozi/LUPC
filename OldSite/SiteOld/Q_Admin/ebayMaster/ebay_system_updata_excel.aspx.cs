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
using System.Data.OleDb;

public partial class Q_Admin_ebayMaster_ebay_system_updata_excel : PageBase
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

    protected void Button_updata_Click(object sender, EventArgs e)
    {
        string filename = "";
        if (this.FileUpload1.HasFile)
        {
            filename = DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls";
            filename = Server.MapPath(Config.update_product_data_excel_path + filename);
            FileUpload1.SaveAs(filename);

            using (OleDbConnection conn = new OleDbConnection(Config.ExcelConnstring(filename)))
            {
                try
                {
                    conn.Open();
                    OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM [table$]", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    conn.Close();
                    da.Dispose();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        int issue;
                        int.TryParse(dr["issue"].ToString(), out issue);
                        string new_sku = dr["new_sku"].ToString().Trim();
                        string old_sku = dr["old_sku"].ToString().Trim();

                        if (new_sku.Length == 6)
                        {
                            string exec_sql = string.Format(@"insert into tb_ebay_system 
( id, category_id, ebay_system_name, ebay_system_price, ebay_system_current_number, 
showit, 
view_count, 
logo_filenames,
keywords, 
system_title1, 
system_title2, 
system_title3, 
large_pic_name, 
is_issue ,
regdate

)
select '{1}', category_id, ebay_system_name, ebay_system_price, ebay_system_current_number, 
showit, 
view_count, 
logo_filenames, 
keywords, 
system_title1, 
system_title2, 
system_title3, 
large_pic_name, 
'1', now() from tb_ebay_system where id='{0}';
", old_sku, new_sku);
                             Config.ExecuteNonQuery(exec_sql);

                            Config.ExecuteNonQuery(string.Format(@"insert into tb_ebay_system_parts 
( system_sku, luc_sku, comment_id, price, cost, part_quantity, 
max_quantity, 
compatibility_parts,
regdate
)
select '{0}' , luc_sku, comment_id, price, cost, part_quantity, 
max_quantity, 
compatibility_parts
,now()
from tb_ebay_system_parts where system_sku='{1}'", new_sku, old_sku));
	            
                        }
                        else
                        {
                            Config.ExecuteNonQuery(string.Format("Update tb_ebay_system set is_issue='{0}' where id='{1}'", issue, old_sku));
                        }
                        
                    }
                    CH.Alert("Updata is end.", this.Literal1);
                }
                catch (Exception ex) { CH.Alert(ex.Message, this.Literal1); }
            }
            System.IO.File.Delete(filename);

        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        System.Text.StringBuilder noExistSB = new System.Text.StringBuilder();
        string filename = "";
        if (this.FileUpload2.HasFile)
        {
            filename = DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls";
            filename = Server.MapPath(Config.update_product_data_excel_path + filename);
            FileUpload2.SaveAs(filename);

            using (OleDbConnection conn = new OleDbConnection(Config.ExcelConnstring(filename)))
            {
                try
                {
                    conn.Open();
                    OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM [table$]", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    conn.Close();
                    da.Dispose();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        int showit;
                        int.TryParse(dr["showit"].ToString(), out showit);

                        string ebay_comment = dr["ebay_comment"].ToString().Trim();
                        string ebay_name = dr["ebay_name"].ToString().Trim();
                        string mfp = dr["mfp"].ToString().Trim();

                        int sku = ProductModel.FindSkuByManufacture(mfp);

                        if (sku >0)
                        {
                            string exec_sql = string.Format(@"
Delete from tb_ebay_part_comment where part_sku='{4}' ;
insert into tb_ebay_part_comment (ebay_comment, part_sku, showit,regdate) values 
('{0}', '{1}', '{2}' ,now()) ;
Update tb_product set product_ebay_name='{3}',is_modify=1 where product_serial_no='{4}'
", ebay_comment, sku, showit, ebay_name, sku);
                            Config.ExecuteNonQuery(exec_sql); 
                        }
                        else
                        {
                            noExistSB.Append("<br/>" + mfp);
                        }

                    }
                    this.Literal1.Text = "No exist Product:" + noExistSB.ToString();
                }
                catch (Exception ex) { CH.Alert(ex.Message, this.Literal1); }
            }
            System.IO.File.Delete(filename);

        }
    }
}
