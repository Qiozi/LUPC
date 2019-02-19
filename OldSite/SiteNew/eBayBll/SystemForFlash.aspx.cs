using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class eBayBll_SystemForFlash : System.Web.UI.Page
{
    LU.Data.nicklu2Entities DBContext = new LU.Data.nicklu2Entities();
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

                string filename = XmlConfigurationFilename(ReqSku.ToString(), xmlPath);// Server.MapPath(xmlPath + SystemSKU.ToString() + ".xml");
                string str = WritePartGroupInfoAndLabelControlOnFlash(ReqSku); ;

                //if (Command == "GenerateXmlFile")
                //{
                //    Response.Clear();
                //   // str = WritePartGroupInfoAndLabelControlOnFlash(SystemSKU);
                //    Response.Write("OK");
                //    if (IsClose)
                //        Response.Write("<script>this.close();</script>");
                //    Response.End();
                //    return;
                //}
                if (File.Exists(filename)) { }
                //{
                //    StreamReader sr = new StreamReader(filename);
                //    str = sr.ReadToEnd().Trim();
                //    sr.Close();

                //    if (str == "")
                //        str = WritePartGroupInfoAndLabelControlOnFlash(SystemSKU);
                //}
                else
                {
                    //  str = WritePartGroupInfoAndLabelControlOnFlash(SystemSKU);// WritePartGroupInfo(SystemSKU);

                }
                Response.Write(str);

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
        int motherboard_group_id = 0;
        //int cpu_group_id = 0;
        int index_cpu = -1;
        int index_motherboard = -1;
        int index_video = -1;
        int index_audio = -1;
        int index_network = -1;
        int index_cpu_fan = -1;

        //decimal profits = 0M;
        //decimal ebay_fee = 0M;
        //decimal web_price = 0M;
        //decimal cost = 0M;
        //GetEbayPrice.GetEbaySysCost(system_sku, false, ref web_price);

        //EbaySystemModel ebaySys = new tb_ebay_system();
        decimal adjustment = 0M;
        var models = LU.BLL.SystemFlashProvider.GetSystemForFlash(DBContext, ReqSku, out adjustment);
        //decimal sys_price = GetEbayPrice.GetEbaySysPrice(cost, adjustment,ref profits, ref ebay_fee);
        decimal all_sell = 0M;
        decimal belong_sell = 0M;
        decimal no_belong_sell = 0M;

        var system = DBContext.tb_ebay_system.Single(p => p.id.Equals(ReqSku));

        decimal sys_price = system.is_shrink.HasValue && system.is_shrink.Value ?
            system.selected_ebay_sell.Value : system.ebay_system_price.Value;
        all_sell = system.ebay_system_price.Value;
        belong_sell = system.selected_ebay_sell.Value;
        no_belong_sell = system.no_selected_ebay_sell.Value;

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
        if (system.is_barebone.Value)
        {
            flash_type = 3;
        }
        else if (system.is_shrink.Value)
        {
            flash_type = 2;
        }
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
        }

        var sb = new System.Text.StringBuilder();
        sb.Append("<result flash_type='" + flash_type + "' total='" + (all_sell).ToString() + "' cputotal='" + (belong_sell).ToString() + "'>");
        // for (int i = 0; i < dt.Rows.Count; i++)
        foreach (var sys in models)
        {
            //
            // 数据顺序不可变化
            //
            //DataRow dr = dt.Rows[i];

            //if (dr["is_cpu"].ToString() == "1")
            //    index_cpu = i;
            //if (dr["is_mb"].ToString() == "1")
            //    index_motherboard = i;
            //if (dr["is_video"].ToString() == "1")
            //    index_video = i;
            //if (dr["is_audio"].ToString() == "1")
            //    index_audio = i;
            //if (dr["is_network"].ToString() == "1")
            //    index_network = i;
            //if (dr["is_cpu_fan"].ToString() == "1")
            //    index_cpu_fan = i;

            sb.Append(string.Format(@"<item>"));
            sb.Append(string.Format(@"<sku>{0}</sku>", sys.PartSku));
            sb.Append(string.Format(@"<cost>{0}</cost>", sys.PartPrice));
            sb.Append(string.Format(@"<comment>{0}</comment>", sys.CommentText));
            sb.Append(string.Format(@"<name>{0}</name>", ConcatName(sys.ShortNameForSystem, sys.PartName)));
            sb.Append(string.Format(@"<partGroupID>{0}</partGroupID>", sys.PartGroupId));
            sb.Append(string.Format(@"<commentID>{0}</commentID>", sys.CommentId));
            sb.Append(string.Format(@"<price>+{0}</price>", 0));
            sb.Append(string.Format(@"<addShippingValue>{0}</addShippingValue>", 0));
            sb.Append(string.Format(@"<detailID>{0}</detailID>", sys.DetailId));
            sb.Append(string.Format(@"<selected>1</selected>"));
            sb.Append(string.Format(@"<imgSKU>{0}</imgSKU>", sys.ImgSku));
            sb.Append(string.Format(@"<isLabel>{0}</isLabel>", sys.IsLabelOfFlash ? 1 : 0));
            sb.Append(string.Format(@"<eBayCode>{0}</eBayCode>", sys.eBayCode));
            sb.Append(string.Format("</item>"));

            var x = 0;
            decimal curr_price = sys.PartPrice;

            //for (int j = 0; j < subdt.Rows.Count; j++)
            foreach (var subItem in sys.SubParts)
            {
                // if (x < 200)
                {
                    //DataRow sdr = subdt.Rows[j];
                    if (subItem.PartSku != sys.PartSku// dr["product_serial_no"].ToString()
                        && subItem.PartGroupId == sys.PartGroupId// sdr["part_group_id"].ToString()
                        )
                    //&& subItem.c sdr["comment_id"].ToString() == dr["comment_id"].ToString())
                    {
                        decimal price = subItem.PartPrice;
                        decimal diff_price;
                        decimal.TryParse((price - curr_price).ToString("###"), out diff_price);

                        sb.Append(string.Format(@"<item>"));
                        sb.Append(string.Format(@"<sku>{0}</sku>", subItem.PartSku));
                        sb.Append(string.Format(@"<cost>{0}</cost>", price - curr_price));          // diff price.
                        sb.Append(string.Format(@"<comment>{0}</comment>", ""));
                        sb.Append(string.Format(@"<name>{0}</name>", ConcatName(subItem.ShortNameForSystem, subItem.PartName)));
                        sb.Append(string.Format(@"<partGroupID>{0}</partGroupID>", subItem.PartGroupId));
                        sb.Append(string.Format(@"<commentID>{0}</commentID>", sys.CommentId));
                        sb.Append(string.Format(@"<price>{0}</price>", (diff_price > 0 ? "+" + diff_price.ToString() : diff_price.ToString())));
                        sb.Append(string.Format(@"<addShippingValue>{0}</addShippingValue>", 0));
                        sb.Append(string.Format(@"<detailID>{0}</detailID>", sys.DetailId));
                        sb.Append(string.Format(@"<selected>0</selected>"));
                        sb.Append(string.Format(@"<imgSKU>{0}</imgSKU>", subItem.ImgSku));
                        sb.Append(string.Format(@"<isLabel>{0}</isLabel>", "0"));
                        sb.Append(string.Format(@"<eBayCode>{0}</eBayCode>", subItem.eBayCode));
                        sb.Append(string.Format("</item>"));
                        x += 1;
                    }
                }
            }
            if (sys.IsMotherboard)
                motherboard_group_id = sys.PartGroupId;
        }

        // motherboard 

        if (motherboard_group_id > 0)
        {
            //            DataTable mbDT = Config.ExecuteDataTable(string.Format(@"
            //select distinct pr.mb_sku, video_sku, audio_sku, network_sku from tb_part_group_detail pgd inner join 
            //	tb_part_relation_motherboard_video_audio_port pr on pr.mb_sku=pgd.product_serial_no
            //where part_group_id='{0}' ", motherboard_group_id));

            var mbQuery = (from c in DBContext.tb_part_group_detail
                           join p in DBContext.tb_part_relation_motherboard_video_audio_port on c.product_serial_no.Value equals p.mb_sku.Value
                           where c.part_group_id.HasValue && c.part_group_id.Value.Equals(motherboard_group_id)
                           select new
                           {
                               MbSku = p.mb_sku.Value,
                               Video = p.video_sku.Value,
                               Audio = p.audio_sku.Value,
                               Network = p.network_sku.Value
                           }).Distinct().ToList();

            int[] relationGroupId = { LU.BLL.Config.relation_motherboard_audio_group_id
                                        , LU.BLL.Config.relation_motherboard_network_group_id
                                        , LU.BLL.Config.relation_motherboard_video_group_id };

            var relationQuery = (from p in DBContext.tb_product
                                 join g in DBContext.tb_part_group_detail on p.product_serial_no equals g.product_serial_no.Value
                                 where relationGroupId.Contains(g.part_group_id.Value)
                                 select new
                                 {
                                     PartSku = p.product_serial_no,
                                     Name = p.product_ebay_name,
                                     Cost = p.product_current_cost.Value
                                 }).ToList();

            //            DataTable rDT = Config.ExecuteDataTable(string.Format(@"select p.product_serial_no, p.product_ebay_name, p.product_current_cost cost from tb_product p 
            //inner join tb_part_group_detail pgd on pgd.part_group_id in ({0}) and pgd.product_serial_no = p.product_serial_no"
            //                , string.Format("{0},{1},{2}"
            //                    , EbaySettings.relation_motherboard_audio_group_id
            //                    , EbaySettings.relation_motherboard_network_group_id
            //                    , EbaySettings.relation_motherboard_video_group_id)));

            sb.Append("<Motherboard>");
            string VideoPartName = "";
            string audioPartName = "";
            string networkPartName = "";
            string videoPartCost = "";
            string audioPartCost = "";
            string networkPartCost = "";

            foreach (var dr in mbQuery)
            {
                foreach (var rdr in relationQuery)
                {
                    if (rdr.PartSku == dr.Video)
                    {
                        VideoPartName = rdr.Name;
                    }
                    if (rdr.PartSku == dr.Audio)
                    {
                        audioPartName = rdr.Name;
                    }
                    if (rdr.PartSku == dr.Network)
                    {
                        networkPartName = rdr.Name;
                    }
                }
                sb.Append("<Row sku='" + dr.MbSku + "'>");
                sb.Append(string.Format(@"<video sku='{0}' price='{1}'>{2}</video>", dr.Video, videoPartCost, VideoPartName));
                sb.Append(string.Format(@"<audio sku='{0}' price='{1}'>{2}</audio>", dr.Audio, audioPartCost, audioPartName));
                sb.Append(string.Format(@"<network sku='{0}' price='{1}'>{2}</network>", dr.Network, networkPartCost, networkPartName));
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


        if (models.Count == 0)
        {
            sb.Append(string.Format(@"<error>No Match Data.</error>"));
        }
        sb.Append("</result>");

        //FileHelper.GenerateFile(XmlConfigurationFilename(system_sku.ToString(), xmlPath)
        //    , sb.ToString());

        return sb.ToString();
    }

    string XmlConfigurationFilename(string system_sku, string path)
    {
        return Server.MapPath(path + system_sku + ".xml");
    }

    public int ReqSku
    {
        get { return Util.GetInt32SafeFromQueryString(this, "sku", -1); }
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