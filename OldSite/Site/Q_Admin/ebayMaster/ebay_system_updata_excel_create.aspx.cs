using LU.Data;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;

public partial class Q_Admin_ebayMaster_ebay_system_updata_excel_create : PageBase
{

    DataTable _ErrorList = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
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
                    OleDbDataAdapter da = new OleDbDataAdapter("SELECT * FROM [SKUs$]", conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    conn.Close();
                    da.Dispose();

                    //this.GridView2.DataSource = dt;
                    //this.GridView2.DataBind();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];

                        int luc_sys_sku;
                        int.TryParse(dr["lu_sku"].ToString(), out luc_sys_sku);
                        string cutom_label = dr["cutom_label"].ToString();

                        try
                        {

                            //int luc_sys_sku;
                            //int.TryParse(dr["lu_sku"].ToString(), out luc_sys_sku);


                            decimal CAD_price;
                            decimal.TryParse(dr["CAD_PRICE"].ToString(), out CAD_price);

                            int cpu;
                            int.TryParse(dr["cpu"].ToString(), out cpu);

                            int OVERCLOCK;
                            int.TryParse(dr["OVERCLOCK"].ToString(), out OVERCLOCK);

                            int RAM;
                            int.TryParse(dr["RAM"].ToString(), out RAM);

                            int MOTHERBOARD;
                            int.TryParse(dr["MOTHERBOARD"].ToString(), out MOTHERBOARD);

                            int DVDRW;
                            int.TryParse(dr["DVDRW"].ToString(), out DVDRW);

                            int DVDRW2;
                            int.TryParse(dr["DVDRW2"].ToString(), out DVDRW2);///////////////

                            int HD_Quantity;
                            int.TryParse(dr["HD_Quantity"].ToString(), out HD_Quantity);

                            int HARD_DRIVE;
                            int.TryParse(dr["HARD_DRIVE"].ToString(), out HARD_DRIVE);

                            int is_ATI;
                            int.TryParse(dr["is_ATI"].ToString(), out is_ATI);

                            int VC_Quantity;
                            int.TryParse(dr["VC_Quantity"].ToString(), out VC_Quantity);

                            int VIDEO_CARD;
                            int.TryParse(dr["VIDEO_CARD"].ToString(), out VIDEO_CARD);

                            int CASE;
                            int.TryParse(dr["CASE"].ToString(), out CASE);

                            int POWER_SUPPLY;
                            int.TryParse(dr["POWER_SUPPLY"].ToString(), out POWER_SUPPLY);

                            int CPU_COOLER;
                            int.TryParse(dr["CPU_COOLER"].ToString(), out CPU_COOLER);

                            int NEON_LIGHT;
                            int.TryParse(dr["NEON_LIGHT"].ToString(), out NEON_LIGHT);

                            int WINDOWS;
                            int.TryParse(dr["WINDOWS"].ToString(), out WINDOWS);

                            int KEYBOARD_MOUSE;
                            int.TryParse(dr["KEYBOARD_MOUSE"].ToString(), out KEYBOARD_MOUSE);

                            int Card_reader;
                            int.TryParse(dr["Card_reader"].ToString(), out Card_reader);

                            int LCD_MONITOR;
                            int.TryParse(dr["LCD_MONITOR"].ToString(), out LCD_MONITOR);

                            int Network;
                            int.TryParse(dr["Network"].ToString(), out Network);

                            int Ports;
                            int.TryParse(dr["Ports"].ToString(), out Ports);
                            //int Video ;
                            //int.TryParse(dr["Video"].ToString(),out Video);

                            int Audio;
                            int.TryParse(dr["Audio"].ToString(), out Audio);

                            //int optical_drive;
                            //int.TryParse(dr["optical_drive"].ToString(), out optical_drive);

                            int Comment1;
                            int.TryParse(dr["Comment1"].ToString(), out Comment1);

                            int Comment2;
                            int.TryParse(dr["Comment2"].ToString(), out Comment2);

                            int Comment3;
                            int.TryParse(dr["Comment3"].ToString(), out Comment3);

                            int Comment4;
                            int.TryParse(dr["Comment4"].ToString(), out Comment4);

                            string TITLE = dr["TITLE"].ToString();
                            //string SUBTITLE = dr["SUBTITLE"].ToString();
                            string LARGE_PICTURE = dr["LARGE_PICTURE"].ToString();

                            int LU_Category;
                            int.TryParse(dr["LU_Category"].ToString(), out LU_Category);

                            #region Create,Updata System 
                            if (luc_sys_sku > 0)
                            {
                                var es = EbaySystemModel.GetEbaySystemModel(DBContext, luc_sys_sku);
                                es.category_id = LU_Category;
                                es.ebay_system_name = TITLE;
                                es.ebay_system_price = CAD_price;
                                es.is_from_ebay = false;
                                es.is_issue = true;
                                es.large_pic_name = LARGE_PICTURE;
                                es.main_comment_ids = string.Format("{0}|{1}|{2}|{3}", Comment1
                                    , Comment2
                                    , Comment3
                                    , Comment4);
                                es.showit = true;
                                es.cutom_label = cutom_label;
                                DBContext.SaveChanges();

                                //
                                // Delete Sys Part
                                Config.ExecuteNonQuery("Delete from tb_ebay_system_parts  where system_sku='" + luc_sys_sku.ToString() + "'");
                            }
                            else
                            {
                                var es = new tb_ebay_system();
                                es.category_id = LU_Category;
                                es.ebay_system_name = TITLE;
                                es.ebay_system_price = CAD_price;
                                es.is_from_ebay = false;
                                es.is_issue = true;
                                es.large_pic_name = LARGE_PICTURE;
                                es.main_comment_ids = string.Format("{0}|{1}|{2}|{3}", Comment1
                                    , Comment2
                                    , Comment3
                                    , Comment4);
                                es.showit = true;
                                es.cutom_label = cutom_label;
                                es.id = luc_sys_sku;
                                DBContext.tb_ebay_system.Add(es);
                                DBContext.SaveChanges();
                                luc_sys_sku = es.id;
                            }
                            #endregion

                            #region Create Sys Part.
                            if (cpu > 0)
                            {
                                SaveEbayPart(luc_sys_sku, cpu, "cpu");
                            }

                            if (OVERCLOCK > 0)
                            {
                                SaveEbayPart(luc_sys_sku, OVERCLOCK, "OVERCLOCK");
                            }

                            if (RAM > 0)
                            {
                                SaveEbayPart(luc_sys_sku, RAM, "RAM");
                            }

                            if (MOTHERBOARD > 0)
                            {
                                SaveEbayPart(luc_sys_sku, MOTHERBOARD, "MOTHERBOARD");
                            }

                            if (DVDRW > 0)
                            {
                                SaveEbayPart(luc_sys_sku, DVDRW, "DVDRW");
                            }
                            if (DVDRW2 > 0)
                            {
                                SaveEbayPart(luc_sys_sku, DVDRW2, "DVDRW2");
                            }

                            if (CASE > 0)
                            {
                                SaveEbayPart(luc_sys_sku, CASE, "CASE");
                            }

                            if (POWER_SUPPLY > 0)
                            {
                                SaveEbayPart(luc_sys_sku, POWER_SUPPLY, "POWER_SUPPLY");
                            }

                            if (CPU_COOLER > 0)
                            {
                                SaveEbayPart(luc_sys_sku, CPU_COOLER, "CPU_COOLER");
                            }
                            if (NEON_LIGHT > 0)
                            {
                                SaveEbayPart(luc_sys_sku, NEON_LIGHT, "NEON_LIGHT");
                            }
                            if (WINDOWS > 0)
                            {
                                SaveEbayPart(luc_sys_sku, WINDOWS, "WINDOWS");
                            }

                            if (KEYBOARD_MOUSE > 0)
                            {
                                SaveEbayPart(luc_sys_sku, KEYBOARD_MOUSE, "KEYBOARD_MOUSE");
                            }
                            if (Card_reader > 0)
                            {
                                SaveEbayPart(luc_sys_sku, Card_reader, "Card_reader");
                            }
                            if (LCD_MONITOR > 0)
                            {
                                SaveEbayPart(luc_sys_sku, LCD_MONITOR, "LCD_MONITOR");
                            }
                            if (Network > 0)
                            {
                                SaveEbayPart(luc_sys_sku, Network, "Network");
                            }
                            if (Ports > 0)
                            {
                                SaveEbayPart(luc_sys_sku, Ports, "Ports");
                            }

                            if (Audio > 0)
                            {
                                SaveEbayPart(luc_sys_sku, Audio, "Audio");
                            }

                            //if (optical_drive > 0)
                            //{
                            //    SaveEbayPart(luc_sys_sku, optical_drive, "optical_drive");
                            //}

                            if (DVDRW2 > 0)
                            {
                                SaveEbayPart(luc_sys_sku, DVDRW2, "DVDRW2");
                            }

                            #region Hard drive
                            if (HD_Quantity > 1)
                            {
                                // 14085:2
                                // 14087:4
                                // 14088:8
                                if (HD_Quantity == 2)
                                {
                                    SaveEbayPart(luc_sys_sku, 14085, "Raid");

                                    SaveEbayPart(luc_sys_sku, HARD_DRIVE, "HARD_DRIVE1");
                                    SaveEbayPart(luc_sys_sku, HARD_DRIVE, "HARD_DRIVE2");
                                }

                                if (HD_Quantity == 4)
                                {
                                    SaveEbayPart(luc_sys_sku, 14087, "Raid");

                                    SaveEbayPart(luc_sys_sku, HARD_DRIVE, "HARD_DRIVE1");
                                    SaveEbayPart(luc_sys_sku, HARD_DRIVE, "HARD_DRIVE2");
                                    SaveEbayPart(luc_sys_sku, HARD_DRIVE, "HARD_DRIVE3");
                                    SaveEbayPart(luc_sys_sku, HARD_DRIVE, "HARD_DRIVE4");
                                }

                                if (HD_Quantity == 8)
                                {
                                    SaveEbayPart(luc_sys_sku, 14088, "Raid");

                                    SaveEbayPart(luc_sys_sku, HARD_DRIVE, "HARD_DRIVE1");
                                    SaveEbayPart(luc_sys_sku, HARD_DRIVE, "HARD_DRIVE2");
                                    SaveEbayPart(luc_sys_sku, HARD_DRIVE, "HARD_DRIVE3");
                                    SaveEbayPart(luc_sys_sku, HARD_DRIVE, "HARD_DRIVE4");
                                    SaveEbayPart(luc_sys_sku, HARD_DRIVE, "HARD_DRIVE5");
                                    SaveEbayPart(luc_sys_sku, HARD_DRIVE, "HARD_DRIVE6");
                                    SaveEbayPart(luc_sys_sku, HARD_DRIVE, "HARD_DRIVE7");
                                    SaveEbayPart(luc_sys_sku, HARD_DRIVE, "HARD_DRIVE8");
                                }
                            }
                            else if (HARD_DRIVE > 0)
                            {
                                SaveEbayPart(luc_sys_sku, HARD_DRIVE, "HARD_DRIVE");
                            }
                            #endregion

                            #region Video Card
                            if (VC_Quantity == 2)
                            {
                                if (is_ATI == 1)
                                {
                                    SaveEbayPart(luc_sys_sku, 14082, "Multi_GPU_Support");

                                    SaveEbayPart(luc_sys_sku, VIDEO_CARD, "CrossFire_Video1");
                                    SaveEbayPart(luc_sys_sku, VIDEO_CARD, "CrossFire_Video2");
                                }
                                else
                                {
                                    SaveEbayPart(luc_sys_sku, 14039, "Multi_GPU_Support");

                                    SaveEbayPart(luc_sys_sku, VIDEO_CARD, "SLI_Video_1");
                                    SaveEbayPart(luc_sys_sku, VIDEO_CARD, "SLI_Video_2");
                                }
                            }
                            else
                            {
                                SaveEbayPart(luc_sys_sku, VIDEO_CARD, "VIDEO_CARD");
                            }
                            #endregion

                            #endregion

                            SetErrorListInfo(luc_sys_sku, "Success", cutom_label);
                        }
                        catch (Exception ex)
                        {
                            SetErrorListInfo(luc_sys_sku, ex.Message, cutom_label);
                        }

                    }
                    CH.Alert("Updata is end.", this.Literal1);
                }
                catch (Exception ex) { CH.Alert(ex.Message, this.Literal1); }

                this.GridView1.DataSource = _ErrorList;
                this.GridView1.DataBind();
            }
            System.IO.File.Delete(filename);

        }
    }

    public int SaveEbayPart(int system_sku, int luc_sku, string e_field_name)
    {
        string comment = "";
        int commentID = 0;
        GetCommentNameAndID(e_field_name, ref comment, ref commentID);

        if (comment != "")
        {

            var pm = ProductModel.GetProductModel(DBContext,luc_sku);
            var espm = new tb_ebay_system_parts();
            espm.system_sku = system_sku;
            espm.part_quantity = 1;
            espm.price = pm.part_ebay_price;
            espm.max_quantity = espm.part_quantity;
            espm.luc_sku = luc_sku;
            espm.cost = pm.product_current_cost;
            espm.comment_id = commentID;
            espm.comment_name = comment;
            DBContext.tb_ebay_system_parts.Add(espm);
            DBContext.SaveChanges();
            return espm.id;
            //espm.
        }
        else
        {
            throw new Exception("没有找到对应的Part Comment, " + e_field_name + " 无法添加");
        }
    }

    public void GetCommentNameAndID(string e_field_name, ref string comment, ref int id)
    {
        DataTable dt = Config.ExecuteDataTable("Select id, comment from  tb_ebay_system_part_comment where e_field_name='" + e_field_name + "'");
        if (dt.Rows.Count == 1)
        {
            comment = dt.Rows[0]["comment"].ToString();
            int.TryParse(dt.Rows[0]["id"].ToString(), out id);
        }
    }

    public void SetErrorListInfo(int system_sku, string Comment, string Custom_Label)
    {
        if (_ErrorList == null)
        {
            _ErrorList = new DataTable("_ErrorList");
            _ErrorList.Columns.Add("System_SKU");
            _ErrorList.Columns.Add("Custom_Label");
            _ErrorList.Columns.Add("Comment");

            SetErrorListInfo(system_sku, Comment, Custom_Label);
        }
        else
        {
            DataRow dr = _ErrorList.NewRow();
            dr["System_SKU"] = system_sku.ToString();
            dr["Custom_Label"] = Custom_Label;
            dr["Comment"] = Comment;
            _ErrorList.Rows.Add(dr);
        }
    }
}
