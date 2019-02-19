using System;
using System.Data;
using System.Xml;
using System.Drawing;
using System.IO;
using LU.Data;

public partial class Q_Admin_ebayMaster_ebay_cmd : PageBaseNoInit
{
  

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (ReqCmd2 != "qiozi@msn.com_wu.th@qq.com")
            {
                InitialDatabase();
            }

            Response.Clear();
            DataTable dt = new DataTable();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            switch (ReqCmd)
            {
                case "getTopOneItemid":
                    dt = Config.ExecuteDataTable("select ebay_code from tb_ebay_code_and_luc_sku where sku='" + ReqSku + "' order by id desc limit 1");
                    if (dt.Rows.Count == 1)
                        Response.Write(dt.Rows[0][0].ToString());
                    else
                        Response.Write(".");
                    break;

                case "getStockQty":
                    dt = Config.ExecuteDataTable("select ltd_stock from tb_product where product_serial_no='" + ReqSku + "'");
                    if (dt.Rows.Count == 1)
                        Response.Write(dt.Rows[0][0].ToString());
                    else
                        Response.Write(".");
                    break;

                case "getStockQtyText":
                    #region getStockQtyText
                    dt = Config.ExecuteDataTable(string.Format(@"select luc_sku, other_inc_id, other_inc_price, other_inc_store_sum, date_format(o.last_regdate, '%y-%m-%d') last_regdate, inc.other_inc_name from tb_other_inc_part_info  o inner join tb_other_inc inc 
on inc.id=o.other_inc_id where luc_sku='{0}' and inc.id>0 and o.id>0 and inc.other_inc_type=1 order by other_inc_name desc ", ReqSku));
                    sb = new System.Text.StringBuilder();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append(dt.Rows[i]["other_inc_name"].ToString() + "(" + dt.Rows[i]["last_regdate"].ToString() + ")($" + dt.Rows[i]["other_inc_price"].ToString() + ")(" + dt.Rows[i]["other_inc_store_sum"].ToString() + ")");
                    }
                    Response.Write(sb.ToString());
                    #endregion
                    break;

                case "getEbayItemInfo":
                    #region getEbayItemInfo
                    if (ReqItemid.Length == 12 && ReqSku > 0)
                    {
                        string result = EbayItemGenerate.GetItem(DBContext, ReqItemid, "Storefront,PrimaryCategory", ReqSku, false);
                        try
                        {
                            XmlDocument xmlDoc = new XmlDocument();
                            xmlDoc.LoadXml(result);
                            XmlNode root = xmlDoc["GetItemResponse"];

                            string ebayCateValue1 = root["Item"]["PrimaryCategory"]["CategoryID"].InnerText;
                            string ebayCateText1 = root["Item"]["PrimaryCategory"]["CategoryName"].InnerText;

                            string ebayMyCateValue1 = root["Item"]["Storefront"]["StoreCategoryID"].InnerText;



                            string title = root["Item"]["Title"].InnerText;

                            int productCategoryID = 0;

                            dt = Config.ExecuteDataTable("Select product_ebay_name, menu_child_serial_no from tb_product where product_serial_no='" + ReqSku + "'");
                            if (dt.Rows.Count == 1)
                            {
                                if (string.IsNullOrEmpty(dt.Rows[0][0].ToString()))
                                    Config.ExecuteNonQuery("Update tb_product set product_ebay_name = '" + title + "' where product_serial_no='" + ReqSku + "'");
                                int.TryParse(dt.Rows[0]["menu_child_serial_no"].ToString(), out productCategoryID);
                            }

                            Config.ExecuteNonQuery("delete from tb_ebay_category_and_product where sku='" + ReqSku + "'");
                            Config.ExecuteNonQuery(string.Format(@"insert into tb_ebay_category_and_product(Sku, ProdType, eBayCateID_1,eBayCateText_1,eBayMyCateID_1,regdate)
                                values ('{0}','{1}','{2}','{3}','{4}',now())"
                                , ReqSku
                                , ReqSku.ToString().Length == 6 ? "S" : (ProductCategoryModel.IsNotebook(DBContext, productCategoryID) ? "N" : "P")
                                , ebayCateValue1
                                , ebayCateText1
                                , ebayMyCateValue1));
                            Response.Write(ebayCateValue1 + "|" + ebayMyCateValue1);
                        }
                        catch (Exception ex) { Response.Write(ex.Message); }
                    }
                    #endregion
                    break;

                case "getImgInfo":
                    #region getImgInfo
                    GetImageSize();
                    #endregion
                    break;
                case "MImage500":// 把小图改为500*500
                    MImage();
                    break;

                case "validateItemTitle":
                    validateItemTitle();
                    break;

                case "nameAndForSysName": // 保存生成系统需要的小短名称
                    #region 保存生成系统需要的小短名称
                    string pname = Util.GetStringSafeFromQueryString(Page, "pname");
                    string sname = Util.GetStringSafeFromQueryString(Page, "sname");
                    string shortName = Util.GetStringSafeFromQueryString(Page, "prodShortName");
                    var pm = ProductModel.GetProductModel(DBContext, ReqSku);
                    if (pm != null)
                    {
                        pm.product_ebay_name = pname;
                        pm.short_name_for_sys = sname;
                        pm.product_short_name = shortName;
                        DBContext.SaveChanges();

                    }
                    Response.Write("OK");
                    #endregion
                    break;

                case "getSysAutoTitle":
                    #region 取得系统 自动生成的标题
                    string cpuShortName = "";
                    Response.Write(eBaySysTitle.GetSystemAutoTitle(ReqSku, ref cpuShortName));

                    #endregion
                    break;

                case "getCPUSList":

                    #region 取得所有CPU给系统标题用的短名称
                    int commentID = Util.GetInt32SafeFromQueryString(Page, "commentid", 0);
                    Response.Write("<input type='hidden' name='commentid' value='" + commentID + "'>");
                    DataTable cdt = Config.ExecuteDataTable("Select product_serial_no,short_name_for_sys from tb_product  where menu_child_serial_no=22 and length(short_name_for_sys)>4 order by short_name_for_sys desc");
                    DataTable sdt = Config.ExecuteDataTable("select luc_sku from tb_part_cpu_and_comment where commentID='" + commentID + "'");


                    foreach (DataRow dr in cdt.Rows)
                    {
                        bool isexist = false;
                        foreach (DataRow sdr in sdt.Rows)
                        {
                            if (sdr["luc_sku"].ToString() == dr["product_serial_no"].ToString())
                            {
                                isexist = true;
                                break;
                            }
                        }
                        Response.Write(string.Format(@"<div><input type='checkbox' {0} value='" + dr["product_serial_no"].ToString() + "'>" + dr["short_name_for_sys"].ToString() + "</div>"
                            , isexist ? " checked='true' " : ""

                            ));
                    }
                    #endregion

                    break;
                case "saveCPUList":
                    string skus = Util.GetStringSafeFromQueryString(Page, "skus");
                    commentID = Util.GetInt32SafeFromQueryString(Page, "commentid", 0);
                    Config.ExecuteNonQuery("delete from tb_part_cpu_and_comment where commentID='" + commentID + "'");
                    string[] sku = skus.Split(new char[] { ',' });
                    foreach (var s in sku)
                    {
                        int id;
                        int.TryParse(s, out id);
                        if (id > 0)
                        {
                            Config.ExecuteNonQuery("insert into tb_part_cpu_and_comment(luc_sku, commentID, regdate) values ('" + id + "', '" + commentID + "', now());");
                        }
                    }
                    Response.Write("OK");
                    break;

                case "ClearPartForSys":
                    Config.ExecuteNonQuery("update tb_product set for_sys=0 where is_fixed=0");
                    break;

                case "ChangeForSys":
                    #region changeForSys
                    if (ReqQty > 0)
                    {
                        Config.ExecuteNonQuery("Update tb_product set for_sys=1 where product_serial_no='" + ReqSku + "'");
                        Config.ExecuteNonQuery("Update tb_product set for_sys=1 where price_sku='" + ReqSku + "'");
                    }
                    else
                    {
                        if (Config.ExecuteScalarInt32("select count(id) from tb_other_inc_part_info where luc_sku='" + ReqSku + "' and other_inc_store_sum>0 and tag=1 and prodType='new' and date_format(now(), '%y%j')-date_format(last_regdate, '%y%j') <15") > 0)
                        {
                            Config.ExecuteNonQuery("Update tb_product set for_sys=1 where product_serial_no='" + ReqSku + "'");
                            Config.ExecuteNonQuery("Update tb_product set for_sys=1 where price_sku='" + ReqSku + "'");
                        }
                    }
                    Response.Write("OK");
                    #endregion
                    break;

                case "changePartInGroup":
                    // var sku = ReqSku;
                    var gid = Util.GetInt32SafeFromQueryString(Page, "gid", 0);
                    var isdel = Util.GetInt32SafeFromQueryString(Page, "isdel", 0) == 1;
                    if (isdel)
                    {
                        Config.ExecuteNonQuery("delete from tb_part_group_detail where part_group_id='" + gid + "' and product_serial_no='" + ReqSku + "' ");
                    }
                    else
                    {
                        Config.ExecuteNonQuery(string.Format(@"insert into tb_part_group_detail 
(part_group_id, product_serial_no, showit
	)
	values
	('{0}', '{1}', 1)", gid, ReqSku));
                    }
                    Response.Write("OK");
                    break;
            }

            Response.End();
        }
    }


    void validateItemTitle()
    {
        try
        {
            var title = Util.GetStringSafeFromQueryString(Page, "title");
            if (!string.IsNullOrEmpty(title))
            {
                if (EbayHelper.ValidateItemTitle(ReqSku, false, title))
                {
                    Response.Write("<span style='color:green;'>产品名称可以使用</span>");
                }
                else
                {
                    Response.Write("<span style='color:red;'>产品名称重复，不可以使用</span>");
                }
            }
            else
            {
                Response.Write("<span style='color:red;'>产品名称不能为空</span>");
            }
        }
        catch
        {
            Response.Write("<span style='color:red;'>程序出错</span>");
        }
    }
    /// <summary>
    /// 
    /// </summary>
    private void GetImageSize()
    {
        string filename = Server.MapPath("~/pro_img/COMPONENTS/" + ReqSku + "_g_1.jpg");
        if (File.Exists(filename))
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(filename);
            System.Drawing.Size size = new Size(image.Width, image.Height);

            Response.Write(string.Format("{0}*{1}"
                , size.Width.ToString()
                , size.Height.ToString()));
            if (size.Width < 500)
                MImage(ReqSku);
        }
        else
        {
            Response.Write("0*0");
        }
    }


    /// <summary>
    /// 合并图片
    /// </summary>
    void MImage()
    {
        DataTable dt = Config.ExecuteDataTable(@"select distinct case when p.other_product_sku >0 then p.other_product_sku else p.product_serial_no end as imgSKU 
from tb_product p where tag=1");
        foreach (DataRow dr in dt.Rows)
        {
            MImage(int.Parse(dr["imgSKU"].ToString()));
        }
    }

    /// <summary>
    /// 合并图片
    /// </summary>
    /// <param name="luc_sku"></param>
    private void MImage(int luc_sku)
    {
        string tmpDir = Server.MapPath("~/pro_img/tmpPart500");
        if (!Directory.Exists(tmpDir))
            Directory.CreateDirectory(tmpDir);

        string filename = Server.MapPath("~/pro_img/COMPONENTS/" + luc_sku + "_g_1.jpg");
        System.Drawing.Image imageWhite = System.Drawing.Image.FromFile(Server.MapPath("~/soft_img/white.jpg"));
        if (File.Exists(filename))
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(filename);
            System.Drawing.Size size = new Size(image.Width, image.Height);

            if (size.Width < 500)
            {
                Bitmap newBmp = new Bitmap(500, 500);
                // System.Drawing.Image imageWhite = System.Drawing.Image.f(newBmp);
                Graphics g = Graphics.FromImage(newBmp);
                g.DrawImage(imageWhite, 0, 0, imageWhite.Width, imageWhite.Height);
                g.DrawImage(image, (500 - size.Width) / 2, (500 - size.Height) / 2, size.Width, size.Height);
                newBmp.Save(Server.MapPath("~/pro_img/tmpPart500/" + luc_sku + "_g_1.jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            image.Dispose();

        }
        imageWhite.Dispose();
    }

    string ReqCmd
    {
        get { return Util.GetStringSafeFromQueryString(Page, "cmd"); }
    }
    string ReqCmd2
    {
        get { return Util.GetStringSafeFromQueryString(Page, "cmd2"); }
    }
    int ReqSku
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "sku", 0); }
    }

    string ReqItemid
    {
        get { return Util.GetStringSafeFromQueryString(Page, "itemid"); }
    }

    int ReqQty
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "qty", 0); }
    }

}