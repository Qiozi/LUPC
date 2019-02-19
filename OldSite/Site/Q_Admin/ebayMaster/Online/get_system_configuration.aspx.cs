using LU.Data;
using System;
using System.Data;
using System.IO;

public partial class Q_Admin_ebayMaster_Online_get_system_configuration : System.Web.UI.Page
{
    string xmlPath = "~/ebay_page_store/system/";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Command == "delall")
            {
                DirectoryInfo dir = new DirectoryInfo(Server.MapPath(xmlPath));
                FileInfo[] fis = dir.GetFiles();
                foreach (var f in fis)
                {
                    File.Delete(f.FullName);
                }
            }
            else
            {
                //System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath("~/ttt.txt"));

                if (Version == 1)
                {
                    string str = WritePartGroupInfoNew(SystemSKU);// WritePartGroupInfo(SystemSKU);
                    Response.Write(str);
                }
                else if (Version == 3)
                {
                    string filename = XmlConfigurationFilename(SystemSKU.ToString(), xmlPath);// Server.MapPath(xmlPath + SystemSKU.ToString() + ".xml");
                    string str = "";

                    if (Command == "GenerateXmlFile")
                    {
                        Response.Clear();
                        str = WritePartGroupInfoAndLabelControlOnFlash(SystemSKU);
                        Response.Write("OK");
                        if (IsClose)
                            Response.Write("<script>this.close();</script>");
                        Response.End();
                        return;
                    }
                    if (File.Exists(filename))
                    {
                        StreamReader sr = new StreamReader(filename);
                        str = sr.ReadToEnd().Trim();
                        sr.Close();

                        if (str == "")
                            str = WritePartGroupInfoAndLabelControlOnFlash(SystemSKU);
                    }
                    else
                    {
                        str = WritePartGroupInfoAndLabelControlOnFlash(SystemSKU);// WritePartGroupInfo(SystemSKU);

                    }
                    Response.Write(str);
                }
            }
        }
        Response.End();
    }

    string ConcatName(string nameForSys, string prodName)
    {
        if (string.IsNullOrEmpty(nameForSys))
        {
            return prodName;
        }
        return string.Concat(nameForSys, " - ", prodName);
    }
    /// <summary>
    /// The Label control on flash.
    /// 
    /// </summary>
    /// <param name="system_sku"></param>
    /// <returns></returns>
    string WritePartGroupInfoAndLabelControlOnFlash(int system_sku)
    {
        var context = new LU.Data.nicklu2Entities();

        int motherboard_group_id = 0;
        //int cpu_group_id = 0;
        int index_cpu = -1;
        int index_motherboard = -1;
        int index_video = -1;
        int index_audio = -1;
        int index_network = -1;
        int index_cpu_fan = -1;

        DataTable dt = Config.ExecuteDataTable(string.Format(@"select ep.id detail_id, p.product_serial_no, p.product_current_price-p.product_current_discount price, 
case when length(p.product_ebay_name)>5 then p.product_ebay_name
      else p.product_name end as product_name
, ep.part_group_id, ep.comment_id , c.comment
, case when p.other_product_sku > 1 then p.other_product_sku
    else p.product_serial_no end as img_sku
    ,ep.is_label_of_flash
, c.is_mb, c.is_cpu
, c.is_video
, c.is_audio
, c.is_network
, c.is_cpu_fan
, p.short_name_for_sys
 from tb_ebay_system_parts ep inner join tb_product p on p.product_serial_no=ep.luc_sku  and ep.ebayShowit=1
 inner join tb_ebay_system_part_comment c on c.id=ep.comment_id
where system_sku='{0}' and ep.part_group_id > 0 order by c.priority asc ", system_sku));

        DataTable subdt = Config.ExecuteDataTable(string.Format(@"select 
pp.detail_id,
pd.product_serial_no
, pd.part_group_id
, p.product_current_price-p.product_current_discount price 
,case when length(p.product_ebay_name)>5 then p.product_ebay_name
      else p.product_name end as product_name
,pp.comment_id
, 'null' comment
, case when p.other_product_sku > 1 then p.other_product_sku
    else p.product_serial_no end as img_sku
, p.short_name_for_sys
 from tb_part_group_detail pd inner join 
(select id detail_id, part_group_id, comment_id from tb_ebay_system_parts where system_sku='{0}' and is_label_of_flash=0 and ebayShowit=1) pp on pp.part_group_id=pd.part_group_id 
inner join tb_product p on p.product_serial_no=pd.product_serial_no where p.tag=1 and p.for_sys=1 and p.split_line=0 and pd.showit=1 order by p.short_name_for_sys asc, p.product_ebay_name asc  ", system_sku));

        //decimal profits = 0M;
        //decimal ebay_fee = 0M;
        //decimal web_price = 0M;
        //decimal cost = 0M;
        //GetEbayPrice.GetEbaySysCost(system_sku, false, ref web_price);

        var ebaySys = new tb_ebay_system();// EbaySystemModel();
        decimal adjustment = GetEbayPrice.GetEbaySysAdjustment(context, system_sku, ref ebaySys);

        //decimal sys_price = GetEbayPrice.GetEbaySysPrice(cost, adjustment,ref profits, ref ebay_fee);
        decimal all_sell = 0M;
        decimal belong_sell = 0M;
        decimal no_belong_sell = 0M;

        decimal sys_price = GetEbayPrice.GetSysPrice(context, system_sku
            , ref belong_sell
            , ref no_belong_sell
            , ref all_sell);

        //decimal cost_label_selected = 0M;
        //decimal belong_price = 0M;

        //GetEbayPrice.GetEbaySysCost(system_sku,
        //    ref cost_label_selected
        //    , ref belong_price
        //    , ref cost
        //    , ref web_price);
        //decimal sys_price_label_selected = GetEbayPrice.GetEbaySysPrice(cost_label_selected, 0M, ref profits, ref ebay_fee);

        // type
        int flash_type = 1;
        if (ebaySys.is_barebone.Value)
        {
            flash_type = 3;
        }
        else if (ebaySys.is_shrink.Value)
            flash_type = 2;
        else
        {
            flash_type = 1;
        }


        // throw new Exception(flash_type.ToString());
        if (flash_type == 3)
        {
            belong_sell = 0M;
        }
        else
        {
            if (belong_sell > 0M)
                belong_sell = ConvertPrice.RoundPrice2(belong_sell) - 0.01M;
        }
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<result flash_type='" + flash_type + "' total='" + (ConvertPrice.RoundPrice2(all_sell) - 0.01M).ToString() + "' cputotal='" + (belong_sell).ToString() + "'>");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            //
            // 数据顺序不可变化
            //
            DataRow dr = dt.Rows[i];

            if (dr["is_cpu"].ToString() == "1")
                index_cpu = i;
            if (dr["is_mb"].ToString() == "1")
                index_motherboard = i;
            if (dr["is_video"].ToString() == "1")
                index_video = i;
            if (dr["is_audio"].ToString() == "1")
                index_audio = i;
            if (dr["is_network"].ToString() == "1")
                index_network = i;
            if (dr["is_cpu_fan"].ToString() == "1")
                index_cpu_fan = i;



            sb.Append(string.Format(@"<item>"));
            sb.Append(string.Format(@"<sku>{0}</sku>", dr["product_serial_no"].ToString()));
            sb.Append(string.Format(@"<cost>{0}</cost>", dr["price"].ToString()));
            sb.Append(string.Format(@"<comment>{0}</comment>", dr["comment"].ToString()));
            sb.Append(string.Format(@"<name>{0}</name>", ConcatName(dr["short_name_for_sys"].ToString(), dr["product_name"].ToString())));
            sb.Append(string.Format(@"<partGroupID>{0}</partGroupID>", dr["part_group_id"].ToString()));
            sb.Append(string.Format(@"<commentID>{0}</commentID>", dr["comment_id"].ToString()));
            sb.Append(string.Format(@"<price>+{0}</price>", 0));
            sb.Append(string.Format(@"<addShippingValue>{0}</addShippingValue>", 0));
            sb.Append(string.Format(@"<detailID>{0}</detailID>", dr["detail_id"].ToString()));
            sb.Append(string.Format(@"<selected>1</selected>"));
            sb.Append(string.Format(@"<imgSKU>{0}</imgSKU>", dr["img_sku"].ToString()));
            sb.Append(string.Format(@"<isLabel>{0}</isLabel>", dr["is_label_of_flash"].ToString()));
            sb.Append(string.Format("</item>"));

            var x = 0;
            decimal curr_price;
            decimal.TryParse(dr["price"].ToString(), out curr_price);

            for (int j = 0; j < subdt.Rows.Count; j++)
            {
                if (x < 200)
                {
                    DataRow sdr = subdt.Rows[j];
                    if (sdr["product_serial_no"].ToString() != dr["product_serial_no"].ToString()
                        && dr["part_group_id"].ToString() == sdr["part_group_id"].ToString()
                        && sdr["comment_id"].ToString() == dr["comment_id"].ToString())
                    {
                        decimal price;
                        decimal.TryParse(sdr["price"].ToString(), out price);

                        decimal diff_price;
                        decimal.TryParse((price - curr_price).ToString("###"), out diff_price);



                        sb.Append(string.Format(@"<item>"));
                        sb.Append(string.Format(@"<sku>{0}</sku>", sdr["product_serial_no"].ToString()));
                        sb.Append(string.Format(@"<cost>{0}</cost>", price - curr_price));          // diff price.
                        sb.Append(string.Format(@"<comment>{0}</comment>", sdr["comment"].ToString()));
                        sb.Append(string.Format(@"<name>{0}</name>", ConcatName(sdr["short_name_for_sys"].ToString(), sdr["product_name"].ToString())));
                        sb.Append(string.Format(@"<partGroupID>{0}</partGroupID>", sdr["part_group_id"].ToString()));
                        sb.Append(string.Format(@"<commentID>{0}</commentID>", sdr["comment_id"].ToString()));
                        sb.Append(string.Format(@"<price>{0}</price>", (diff_price > 0 ? "+" + diff_price.ToString() : diff_price.ToString())));
                        sb.Append(string.Format(@"<addShippingValue>{0}</addShippingValue>", 0));
                        sb.Append(string.Format(@"<detailID>{0}</detailID>", dr["detail_id"].ToString()));
                        sb.Append(string.Format(@"<selected>0</selected>"));
                        sb.Append(string.Format(@"<imgSKU>{0}</imgSKU>", sdr["img_sku"].ToString()));
                        sb.Append(string.Format(@"<isLabel>{0}</isLabel>", "0"));
                        sb.Append(string.Format("</item>"));
                        x += 1;
                    }
                }
            }
        }

        // motherboard 

        foreach (DataRow dr in dt.Rows)
        {
            if (dr["is_mb"].ToString() == "1")
                motherboard_group_id = int.Parse(dr["part_group_id"].ToString());
        }

        if (motherboard_group_id > 0)
        {
            DataTable mbDT = Config.ExecuteDataTable(string.Format(@"
select distinct pr.mb_sku, video_sku, audio_sku, network_sku from tb_part_group_detail pgd inner join 
	tb_part_relation_motherboard_video_audio_port pr on pr.mb_sku=pgd.product_serial_no
where part_group_id='{0}' ", motherboard_group_id));

            DataTable rDT = Config.ExecuteDataTable(string.Format(@"select p.product_serial_no, p.product_ebay_name, p.product_current_cost cost from tb_product p 
inner join tb_part_group_detail pgd on pgd.part_group_id in ({0}) and pgd.product_serial_no = p.product_serial_no"
                , string.Format("{0},{1},{2}"
                    , EbaySettings.relation_motherboard_audio_group_id
                    , EbaySettings.relation_motherboard_network_group_id
                    , EbaySettings.relation_motherboard_video_group_id)));

            sb.Append("<Motherboard>");
            string VideoPartName = "";
            string audioPartName = "";
            string networkPartName = "";
            string videoPartCost = "";
            string audioPartCost = "";
            string networkPartCost = "";

            foreach (DataRow dr in mbDT.Rows)
            {
                foreach (DataRow rdr in rDT.Rows)
                {
                    if (rdr["product_serial_no"].ToString() == dr["video_sku"].ToString())
                    {
                        VideoPartName = rdr["product_ebay_name"].ToString();
                    }
                    if (rdr["product_serial_no"].ToString() == dr["audio_sku"].ToString())
                    {
                        audioPartName = rdr["product_ebay_name"].ToString();
                    }
                    if (rdr["product_serial_no"].ToString() == dr["network_sku"].ToString())
                    {
                        networkPartName = rdr["product_ebay_name"].ToString();
                    }
                }
                sb.Append("<Row sku='" + dr["mb_sku"].ToString() + "'>");
                sb.Append(string.Format(@"<video sku='{0}' price='{1}'>{2}</video>", dr["video_sku"].ToString(), videoPartCost, VideoPartName));
                sb.Append(string.Format(@"<audio sku='{0}' price='{1}'>{2}</audio>", dr["audio_sku"].ToString(), audioPartCost, audioPartName));
                sb.Append(string.Format(@"<network sku='{0}' price='{1}'>{2}</network>", dr["network_sku"].ToString(), networkPartCost, networkPartName));
                sb.Append("</Row>");
            }
            sb.Append("</Motherboard>");
        }

        //CPU


        // cpu, motherboard, video, audio, network Index...
        sb.Append(string.Format("<PartIndex cpu='{0}' motherboard='{1}' video='{2}' audio='{3}' network='{4}' />"
            , index_cpu
            , index_motherboard
            , index_video
            , index_audio
            , index_network));


        if (dt.Rows.Count == 0)
        {
            sb.Append(string.Format(@"<error>No Match Data.</error>"));
        }
        sb.Append("</result>");

        FileHelper.GenerateFile(XmlConfigurationFilename(system_sku.ToString(), xmlPath)
            , sb.ToString());

        return sb.ToString();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="system_sku"></param>
    /// <returns></returns>
    string WritePartGroupInfoNew(int system_sku)
    {
        var context =new LU.Data.nicklu2Entities();
        DataTable dt = Config.ExecuteDataTable(string.Format(@"select ep.id detail_id, p.product_serial_no, p.product_current_price-p.product_current_discount price, 
case when length(p.product_ebay_name)>5 then p.product_ebay_name
      else p.product_name end as product_name
, ep.part_group_id, ep.comment_id , c.comment
, case when p.other_product_sku > 1 then p.other_product_sku
    else p.product_serial_no end as img_sku
 from tb_ebay_system_parts ep inner join tb_product p on p.product_serial_no=ep.luc_sku and ep.ebayShowit=1
 inner join tb_ebay_system_part_comment c on c.id=ep.comment_id
where system_sku='{0}' and ep.part_group_id > 0 order by c.priority asc ", system_sku));

        DataTable subdt = Config.ExecuteDataTable(string.Format(@"select 
pp.detail_id,
pd.product_serial_no, pd.part_group_id, p.product_current_price-p.product_current_discount price , 
case when length(p.product_ebay_name)>5 then p.product_ebay_name
      else p.product_name end as product_name
,pp.comment_id, 'null' comment
, case when p.other_product_sku > 1 then p.other_product_sku
    else p.product_serial_no end as img_sku
 from tb_part_group_detail pd inner join 
(select id detail_id, part_group_id, comment_id from tb_ebay_system_parts where system_sku='{0}' and ebayShowit=1) pp on pp.part_group_id=pd.part_group_id 
inner join tb_product p on p.product_serial_no=pd.product_serial_no where p.tag=1 and p.for_sys=1 and p.split_line=0 and pd.showit=1 order by p.product_ebay_name asc ", system_sku));

        decimal selected_ebay_sell = 0M;
        decimal no_selected_ebay_sell = 0M;
        decimal all_ebay_sell = 0M;
        decimal sys_price = GetEbayPrice.GetSysPrice(context, system_sku
            , ref selected_ebay_sell
            , ref no_selected_ebay_sell
            , ref all_ebay_sell);
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<result flash_type='1' total='" + sys_price.ToString("###.00") + "'>");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            //
            // 数据顺序不可变化
            //
            DataRow dr = dt.Rows[i];
            sb.Append(string.Format(@"<item>"));
            sb.Append(string.Format(@"<sku>{0}</sku>", dr["product_serial_no"].ToString()));
            sb.Append(string.Format(@"<cost>{0}</cost>", dr["price"].ToString()));
            sb.Append(string.Format(@"<comment>{0}</comment>", dr["comment"].ToString()));
            sb.Append(string.Format(@"<name>{0}</name>", dr["product_name"].ToString()));
            sb.Append(string.Format(@"<partGroupID>{0}</partGroupID>", dr["part_group_id"].ToString()));
            sb.Append(string.Format(@"<commentID>{0}</commentID>", dr["comment_id"].ToString()));
            sb.Append(string.Format(@"<price>+{0}</price>", 0));
            sb.Append(string.Format(@"<addShippingValue>{0}</addShippingValue>", 0));
            sb.Append(string.Format(@"<detailID>{0}</detailID>", dr["detail_id"].ToString()));
            sb.Append(string.Format(@"<selected>1</selected>"));
            sb.Append(string.Format(@"<imgSKU>{0}</imgSKU>", dr["img_sku"].ToString()));
            sb.Append(string.Format("</item>"));

            var x = 0;
            decimal curr_price;
            decimal.TryParse(dr["price"].ToString(), out curr_price);

            for (int j = 0; j < subdt.Rows.Count; j++)
            {
                if (x < 200)
                {
                    DataRow sdr = subdt.Rows[j];
                    if (sdr["product_serial_no"].ToString() != dr["product_serial_no"].ToString()
                        && dr["part_group_id"].ToString() == sdr["part_group_id"].ToString()
                        && sdr["comment_id"].ToString() == dr["comment_id"].ToString())
                    {
                        decimal price;
                        decimal.TryParse(sdr["price"].ToString(), out price);

                        decimal diff_price;
                        decimal.TryParse((price - curr_price).ToString("###.0"), out diff_price);

                        sb.Append(string.Format(@"<item>"));
                        sb.Append(string.Format(@"<sku>{0}</sku>", sdr["product_serial_no"].ToString()));
                        sb.Append(string.Format(@"<cost>{0}</cost>", price - curr_price));          // diff price.
                        sb.Append(string.Format(@"<comment>{0}</comment>", sdr["comment"].ToString()));
                        sb.Append(string.Format(@"<name>{0}</name>", sdr["product_name"].ToString()));
                        sb.Append(string.Format(@"<partGroupID>{0}</partGroupID>", sdr["part_group_id"].ToString()));
                        sb.Append(string.Format(@"<commentID>{0}</commentID>", sdr["comment_id"].ToString()));
                        sb.Append(string.Format(@"<price>{0}</price>", (diff_price > 0 ? "+" + diff_price.ToString() : diff_price.ToString())));
                        sb.Append(string.Format(@"<addShippingValue>{0}</addShippingValue>", 0));
                        sb.Append(string.Format(@"<detailID>{0}</detailID>", dr["detail_id"].ToString()));
                        sb.Append(string.Format(@"<selected>0</selected>"));
                        sb.Append(string.Format(@"<imgSKU>{0}</imgSKU>", sdr["img_sku"].ToString()));
                        sb.Append(string.Format("</item>"));
                        x += 1;
                    }
                }
            }

        }
        if (dt.Rows.Count == 0)
        {
            sb.Append(string.Format(@"<error>No Match Data.</error>"));
        }
        sb.Append("</result>");

        // FileHelper.GenerateFile(Server.MapPath("~/ebay_page_store/system/" + system_sku.ToString() + ".xml"), sb.ToString());

        return sb.ToString();

    }


    string XmlConfigurationFilename(string system_sku, string path)
    {
        return Server.MapPath(path + system_sku + ".xml");
    }

    public int SystemSKU
    {
        get { return Util.GetInt32SafeFromQueryString(this, "system_sku", -1); }
    }

    public string Command
    {

        get { return Util.GetStringSafeFromQueryString(this, "cmd"); }
    }

    public bool IsClose
    {
        get { return Util.GetStringSafeFromQueryString(this, "IsClose") == "true"; }
    }

    /// <summary>
    /// 1 old
    /// 3 new, Have Label Control on flash.
    /// </summary>
    int Version
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "Version", 1); }
    }
}
