using System;
using System.Collections;
using System.Data;
/// <summary>
/// Summary description for eBaySysDescription
/// </summary>
public class eBaySysDescription
{
    DataTable KeywordDT = null;
    const string LU_WEB_EBAY_SYSTEM_MAIN_COMMENT = "[lu_web_ebay_system_main_comment]";

	public eBaySysDescription()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    ///// <summary>
    ///// Get eBay system description.
    ///// </summary>
    ///// <param name="SystemSKU"></param>
    ///// <param name="IsNew"></param>
    ///// <returns></returns>
    //public string GetDescription(int SystemSKU, bool IsNew, System.Web.HttpServerUtility page)
    //{
    //    EbayHelper EH = new EbayHelper();
    //    KeywordDT = Config.ExecuteDataTable("Select keyword from tb_ebay_system_part_name_keyword");

    //    int categoryID = EH.GetCategoryID(SystemSKU);
    //    int templete_id = EH.GetTempleteID(categoryID);

    //    DataTable dt = Config.ExecuteDataTable("select templete_content, templete_content2, templete_info,templete_top from tb_ebay_templete where  id='" + templete_id.ToString() + "'");
    //    if (dt.Rows.Count > 0)
    //    {
    //        return FilterTemplete(dt.Rows[0]["templete_content"].ToString() + dt.Rows[0]["templete_content2"].ToString()
    //            , dt.Rows[0]["templete_info"].ToString()
    //            , dt.Rows[0]["templete_top"].ToString()
    //            , SystemSKU
    //            , categoryID
    //            , page);

    //    }
    //    else
    //    {
    //        return "No Match Data.";
    //    }
    //}
    /// <summary>
    /// 
    /// </summary>
    /// <param name="templete"></param>
    /// <param name="templete_top"></param>
    /// <param name="templete_info"></param>
    /// <param name="system_part_list"></param>
    /// <returns></returns>
    private string FilterTemplete(string templete
        , string templete_top
        , string templete_info
        , int system_sku
        , int categoryID
        , System.Web.HttpServerUtility page)
    {
        try
        {

            bool is_exist_memory = false;
            bool is_exist_HD = false;
            bool is_exist_OD = false;
            bool is_exist_OS = false;
            bool is_exist_LCD = false;

            GenerateFlashViewFile gfvf = new GenerateFlashViewFile(system_sku, flashViewFile.system);

            string part_logo_img_name = "";

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string str = "";
            string top_str = "";
            string part_list_upgrades = "";
            string lu_web_top_row_2 = "[lu_web_top_row2]";
            System.Text.StringBuilder sb_sku_list = new System.Text.StringBuilder();
            ArrayList al = new ArrayList();

            DataTable system_part_list = Config.ExecuteDataTable(string.Format(@"Select p.product_serial_no
,case when length(p.product_ebay_name)>5 then p.product_ebay_name
      else p.product_name end as product_name
,cs.comment part_group_name
,case when other_product_sku >0 then other_product_sku
    else p.product_serial_no end as img_sku
,p.product_current_price
from tb_product p
    inner join tb_ebay_system_parts ep          on p.product_serial_no=ep.luc_sku
    inner join tb_ebay_system_part_comment cs   on ep.comment_id=cs.id
where ep.system_sku='{0}' and cs.section=1 order by cs.priority asc", system_sku));

            DataTable system_part_list2 = Config.ExecuteDataTable(string.Format(@"Select concat(p.product_serial_no,'') product_serial_no
,case when length(p.product_ebay_name)>5 then p.product_ebay_name
      else p.product_name end as product_name
,cs.comment part_group_name
,case when other_product_sku >0 then other_product_sku
    else p.product_serial_no end as img_sku
,p.product_current_price
from tb_product p
    inner join tb_ebay_system_parts ep          on p.product_serial_no=ep.luc_sku
    inner join tb_ebay_system_part_comment cs   on ep.comment_id=cs.id
where ep.system_sku='{0}' and p.product_serial_no = 1000000 and cs.section=2 order by cs.priority asc", system_sku));


            // part lsit 1
            for (int i = 0; i < system_part_list.Rows.Count; i++)
            {
                DataRow dr = system_part_list.Rows[i];
                int lu_sku;
                int.TryParse(dr["product_serial_no"].ToString(), out lu_sku);
                string desc = "";

                string comment = dr["part_group_name"].ToString().ToLower();

                if (!is_exist_memory)
                {
                    if (comment.IndexOf("memory") != -1)
                    {
                        is_exist_memory = true;
                    }
                }

                if (!is_exist_HD)
                {
                    if (comment.IndexOf("hard") != -1)
                        is_exist_HD = true;
                }

                if (!is_exist_OD)
                {
                    if (comment.IndexOf("optical") != -1)
                        is_exist_OD = true;
                }

                if (!is_exist_OS)
                {
                    if (comment.ToLower().IndexOf("windows os") != -1)
                        is_exist_OS = true;
                }
                if (!is_exist_LCD)
                {
                    if (comment.ToLower().IndexOf("lcd") != -1)
                        is_exist_LCD = true;
                }

                string part_path_file = string.Format("{0}{1}_comment.html", Config.Part_Comment_Path, lu_sku);// page.MapPath(string.Format("~/Part_Comment/{0}_comment.html", lu_sku)).ToLower();
           
                if (System.IO.File.Exists(part_path_file))
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(part_path_file);
                    desc = sr.ReadToEnd().Replace("<a ", "<luComputer ").Replace("</a>", "</luComputer>").Replace("<A ", "<luComputer ").Replace("</A>", "</luComputer>").Replace(" onclick", " lucomputer").Replace("window.open", "lucomputer");
                }
                int desc_length = desc.Trim().Length;
                string s = "";

                decimal product_current_price;
                decimal.TryParse(dr["product_current_price"].ToString(), out product_current_price);
                if (desc_length > 5)
                {
                    s = string.Format(@"<table width=""100%"" border=""0"" cellpadding=""2"" cellspacing=""0"">
<tbody>
<tr>
<td valign=""top"" align=""middle"" colspan=""2"">
<table width=""100%"" bgcolor=""#d0dae1"" border=""0"" cellpadding=""0"" cellspacing=""1"">
<tbody>
<tr>
<td bgcolor=""#CDE3F2"" height=""7"" style=""padding-left: 8px; padding-top: 3px; padding-bottom: 3px"">
<b><font face=""Verdana"" size=""2"" color=""#006699"">{0}</font></b><br></td><td width='20' bgcolor=""#CDE3F2""><a href='#sList'><img src='http://www.lucomputers.com/ebay/images/backUp2.jpg' border=0></a></td></tr></tbody></table></td>
</tr>
<tr>
<td valign=""top"" width=""19%"" align=""middle"">
<a onclick=""javascript:popImage('{3}','Lu Computers','middle_center',true,true);return false;"" href=""#""></a>
<img alt="""" src=""{1}""  border=""0"" ></td>
<td valign=""top"">
<table width=""100%"" border=""0"" cellpadding=""2"" cellspacing=""0"">
<tbody>
<tr>
<td style=""font-size: 11px; color: rgb(102, 102, 102); line-height: 14px; font-family: Verdana; letter-spacing: 0px;"" class=""part_detail_desc"">
{2}
</td></tr>
</tbody></table></td></tr></tbody></table>"
                        , string.Format("{0}<a name=\"{1}\"></a>", dr["product_name"].ToString(), dr["product_serial_no"].ToString())
                        , Config.http_domain + gfvf.GetPartMinImgForEbayT(int.Parse(dr["img_sku"].ToString()))
                        , desc
, gfvf.GetPartMinImgForEbayG(int.Parse(dr["img_sku"].ToString())));
                }

                //
                //
                part_logo_img_name += GetPartLogoIMG(gfvf.GetPartMinImgForEbayT(int.Parse(dr["img_sku"].ToString())));

                if (i % 2 == (system_part_list.Rows.Count % 2))
                {
                    top_str += string.Format(@"<tr>
<td style=""width:120px;padding-left: 35px; font-weight: 700; font-size: 13px; color: rgb(0, 102, 153); line-height: 16px; font-family: Verdana;letter-spacing: 0px;"" 
valign=""top"" bgcolor=""#ffffff"">
{0}</td>

<td style=""font-size: 12px; color: rgb(85, 85, 85); line-height: 16px; font-family: Verdana; letter-spacing: 0px;"" bgcolor=""#ffffff"">
{1}</td><td  bgcolor=""#ffffff"">
<a href=""#{2}"">
{3}
</a></td></tr>
", dr["part_group_name"].ToString(), ReplacePartNameKeyword(dr["product_name"].ToString()), dr["product_serial_no"].ToString(), desc_length > 5 ? @"<img src=""http://www.lucomputers.com/pro_img/COMPONENTS/ar.jpg"" width=""11"" border=""0"" height=""11"">" : "");
                }
                else
                {
                    top_str += string.Format(@"<tr>
<td style=""width:120px;padding-left: 35px; font-weight: 700; font-size: 13px; color: rgb(0, 102, 153); line-height: 16px; font-family: Verdana; letter-spacing: 0px;"" 
valign=""top"" bgcolor=""#edf3f2"">
{0}</td>

<td style=""font-size: 12px; color: rgb(85, 85, 85); line-height: 16px; font-family: Verdana; letter-spacing: 0px;"" bgcolor=""#edf3f2"">
{1}</td><td bgcolor=""#edf3f2"">
<a href=""#{2}"">
{3}
</a></td></tr>
", dr["part_group_name"].ToString(), ReplacePartNameKeyword(dr["product_name"].ToString()), dr["product_serial_no"].ToString(), desc_length > 5 ? @"<img src=""http://www.lucomputers.com/pro_img/COMPONENTS/ar.jpg"" width=""11"" border=""0"" height=""11"">" : "");
                }

                if (sb.ToString().IndexOf(string.Format("[{0}]", lu_sku)) == -1)
                    str += s;


                //if (System.IO.File.Exists(Server.MapPath(string.Format("~/pro_img/COMPONENTS/{0}_t.jpg", dr["img_sku"].ToString()))) && dr["img_sku"].ToString() != "999999")
                {

                    al.Add(string.Format("{0}|{1}", dr["img_sku"].ToString(), dr["part_group_name"].ToString()));
                    //  sb_sku_list.Append("|" + dr["img_sku"].ToString() + "," + dr["part_group_name"].ToString());
                }

                sb.Append(string.Format("[{0}]", lu_sku));

            }

            DataRow dr3 = system_part_list2.NewRow();
            dr3["product_serial_no"] = -1;
            dr3["part_group_name"] = "Customize";
            dr3["product_name"] = string.Format(@"You may change or upgrade any of the components to your specifics.&nbsp;
All upgrades are available! Please call sales help line <b>toll free 1866.999.7828</b> 
or <a href=""mailto: sale@lucomputers.com?subject= %28Please input item number%29"">
<font color=""#006699"">send us email</font></a></td><td valign=""top"">
<a href=""#-1"">
</a></td>
				</tr>
				<tr>
                          <td style=""padding-left: 35px; font-weight: 700; font-size: 13px; color: rgb(0, 102, 153); line-height: 16px; font-family: Verdana; letter-spacing: 0px;"" height=""11"" valign=""top""></td>
                          <td style=""font-size: 12px; color: rgb(85, 85, 85); line-height: 16px; font-family: Verdana; letter-spacing: 0px;"" height=""11""> 
							<a style=""font-size: 12px; color: rgb(0, 102, 153);"" href=""http://cgi3.ebay.ca/ws/eBayISAPI.dll?ViewUserPage&amp;userid=dpowerseller"" target=""_blank"">
							Click here for information on upgrades and further customization </a>");

            dr3["img_sku"] = -1;
            dr3["product_current_price"] = 0;
            system_part_list2.Rows.InsertAt(dr3, 0);

            DataRow dr2 = system_part_list2.NewRow();
            dr2["product_serial_no"] = "Terms";
            dr2["part_group_name"] = "Shipping Out";
            dr2["product_name"] = string.Format(@"We ship out your computer within 3 days and we will email the tracking number to you.");

            dr2["img_sku"] = -1;
            dr2["product_current_price"] = 0;
            system_part_list2.Rows.InsertAt(dr2, 2);



            DataRow dr1 = system_part_list2.NewRow();
            dr1["product_serial_no"] = -1;
            dr1["part_group_name"] = "Assembly";
            dr1["product_name"] = string.Format(@"Fully Assembled and Tested. Every unique computer takes 1-2 days to be preassembled and tested before installed into the computer chassis. System includes meticulous hand assembly and precision cable routing. We tune system performance to its best. All manufacturer documentations and disks are included.");

            dr1["img_sku"] = -1;
            dr1["product_current_price"] = 0;
            system_part_list2.Rows.InsertAt(dr1, 1);

            // if part isn't exist Memory, Hard Drive, Optical Drive, LCD, OSand add Part Comment.

            if (!is_exist_OS)
            {
                DataRow dr = system_part_list2.NewRow();
                dr["product_serial_no"] = "win";
                dr["part_group_name"] = "Windows OS";
                dr["product_name"] = "Not included (Please click and read details)";
                dr["img_sku"] = -1;
                dr["product_current_price"] = 0;
                system_part_list2.Rows.InsertAt(dr, 3);
            }
            if (!is_exist_LCD)
            {
                DataRow dr = system_part_list2.NewRow();
                dr["product_serial_no"] = "promo";
                dr["part_group_name"] = "LCD Monitor";
                dr["product_name"] = "Not included (LCD shown on the picture for decoration only, see details)";
                dr["img_sku"] = -1;
                dr["product_current_price"] = 0;
                system_part_list2.Rows.InsertAt(dr, 3);
            }


            if (!is_exist_OD)
            {
                DataRow dr = system_part_list2.NewRow();
                dr["product_serial_no"] = -1;
                dr["part_group_name"] = "Optical Drives";
                dr["product_name"] = "Not included";
                dr["img_sku"] = -1;
                dr["product_current_price"] = 0;
                system_part_list2.Rows.InsertAt(dr, 3);
            }
            if (!is_exist_HD)
            {
                DataRow dr = system_part_list2.NewRow();
                dr["product_serial_no"] = -1;
                dr["part_group_name"] = "Hard Drives";
                dr["product_name"] = "Not included";
                dr["img_sku"] = -1;
                dr["product_current_price"] = 0;
                system_part_list2.Rows.InsertAt(dr, 3);
            }

            if (!is_exist_memory)
            {
                DataRow dr = system_part_list2.NewRow();
                dr["product_serial_no"] = -1;
                dr["part_group_name"] = "Memory";
                dr["product_name"] = "Not included ( Please check motherboard memory capablity bleow)";
                dr["img_sku"] = -1;
                dr["product_current_price"] = 0;
                system_part_list2.Rows.InsertAt(dr, 3);
            }




            // part list 2
            for (int i = 0; i < system_part_list2.Rows.Count; i++)
            {
                DataRow dr = system_part_list2.Rows[i];
                if (i % 2 == (system_part_list2.Rows.Count % 2))
                {
                    part_list_upgrades += string.Format(@"<tr>
<td style=""width:120px;padding-left: 35px; font-weight: 700; font-size: 13px; color: rgb(0, 102, 153); line-height: 16px; font-family: Verdana;letter-spacing: 0px;"" 
valign=""top"" bgcolor=""#ffffff"">
{0}</td>

<td style=""font-size: 12px; color: rgb(85, 85, 85); line-height: 16px; font-family: Verdana; letter-spacing: 0px;"" bgcolor=""#ffffff"">
{1}</td><td  bgcolor=""#ffffff"">
<a href=""#{2}"">
{3}
</a></td></tr>
", dr["part_group_name"].ToString(), ReplacePartNameKeyword(dr["product_name"].ToString()), dr["product_serial_no"].ToString(), dr["product_serial_no"].ToString() != "-1" ? @"<img src=""http://www.lucomputers.com/pro_img/COMPONENTS/ar.jpg"" width=""11"" border=""0"" height=""11"">" : "");
                }
                else
                {
                    part_list_upgrades += string.Format(@"<tr>
<td style=""width:120px;padding-left: 35px; font-weight: 700; font-size: 13px; color: rgb(0, 102, 153); line-height: 16px; font-family: Verdana; letter-spacing: 0px;"" 
valign=""top"" bgcolor=""#ffffff"">
{0}</td>

<td style=""font-size: 12px; color: rgb(85, 85, 85); line-height: 16px; font-family: Verdana; letter-spacing: 0px;"" bgcolor=""#ffffff"">
{1}</td><td bgcolor=""#ffffff"">
<a href=""#{2}"">
{3}
</a></td></tr>
", dr["part_group_name"].ToString(), ReplacePartNameKeyword(dr["product_name"].ToString()), dr["product_serial_no"].ToString(), dr["product_serial_no"].ToString() != "-1" ? @"<img src=""http://www.lucomputers.com/pro_img/COMPONENTS/ar.jpg"" width=""11"" border=""0"" height=""11"">" : "");
                }
            }

            //string sku_list = "";
            //if (sb_sku_list.ToString().Length > 2)
            //    sku_list = sb_sku_list.ToString().Substring(1);

            //
            //  lucomputers SKU Number: {0}
            // 
            top_str = string.Format(@"<tr>
<td style=""padding-left: 35px; font-weight: 700; font-size: 13px; color: rgb(0, 102, 153); line-height: 16px; font-family: Verdana;letter-spacing: 0px;"" 
valign=""top"" bgcolor=""#ffffff"">
</td><td style=""font-size: 12px; color: rgb(85, 85, 85); line-height: 16px; font-family: Verdana; letter-spacing: 0px;text-align:right;"" bgcolor=""#ffffff"">
<b>SKU Number: {0}</b>
</td><td  bgcolor=""#ffffff""></td></tr>
<tr>
<td style=""padding-left: 35px; font-weight: 700; font-size: 13px; color: rgb(0, 102, 153); line-height: 16px; font-family: Verdana;letter-spacing: 0px;"" 
valign=""top"" bgcolor=""#ffffff"" colspan='2'><font color=""#a53411"" size=""2"">Item includes the following components: </font>

</td><td  bgcolor=""#ffffff""></td></tr>
<tr>
<td style=""padding-left: 35px; font-weight: 700; font-size: 13px; color: rgb(0, 102, 153); line-height: 16px; font-family: Verdana;letter-spacing: 0px;"" 
valign=""top"" bgcolor=""#ffffff"">
</td><td style=""font-size: 13px; color: rgb(0, 102, 153); line-height: 16px; font-family: Verdana; letter-spacing: 0px;text-align:right;"" bgcolor=""#ffffff"" colspan='2'>
<b>Info</b>
</td></tr>
{1}
<!--tr bgcolor=""#ffffff"">
                          <td style=""padding-left: 35px; font-weight: 700; font-size: 13px; color: rgb(0, 102, 153); line-height: 16px; font-family: Verdana; letter-spacing: 0px;"" valign=""top"" >Note</td>
                          <td style=""font-size: 12px; color: rgb(255, 102, 0); line-height: 16px; font-family: Verdana; letter-spacing: 0px;"" >Please take above as the ONLY correct specification if any conflicts are found on rest of the page.</td>
                          <td >&nbsp;</td></tr-->
", system_sku, top_str);

            //
            // generate flash view file.
            // 
            gfvf.al_sku = al;
            if (gfvf.Export())
            { }

            if (!is_exist_OS)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(page.MapPath("/soft_img/params/no_win_OS.txt"));
                templete = templete.Replace("[lu_web_no_window_OS_info]", sr.ReadToEnd());
                sr.Close();
                sr.Dispose();
            }
            else
                templete = templete.Replace("[lu_web_no_window_OS_info]", "");

            templete = ReplaceSystemTitle(templete.Replace("[lu_web_info_row]", str).Replace("[lu_web_top_row]", top_str).Replace("[lu_web_content_part_logo]", part_logo_img_name), system_sku);


            if (templete.IndexOf(lu_web_top_row_2) != -1 && part_list_upgrades.Length > 100)
            {
                templete = templete.Replace(lu_web_top_row_2, string.Format(@"<table id=""__"" width=""770"" align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"" style='margin-left:3px;'>
          <tbody><tr>
            <td><img alt="""" src=""http://www.lucomputers.com/ebay/images/in_box7_01.gif"" width=""6"" height=""6""></td>
            <td background=""http://www.lucomputers.com/ebay/images/in_box7_02.gif"">
			<img alt="""" src=""http://www.lucomputers.com/ebay/images/in_box7_02.gif"" width=""1"" height=""6""></td>
            <td><img alt="""" src=""http://www.lucomputers.com/ebay/images/in_box7_03.gif"" width=""6"" height=""6""></td>
          </tr>
          <tr>
            <td width=""6"" background=""http://www.lucomputers.com/ebay/images/in_box7_04.gif"">
			<img alt="""" src=""http://www.lucomputers.com/ebay/images/in_box7_04.gif"" width=""6"" height=""1""></td>
            <td style=""padding: 15px 5px;"" valign=""top"" bgcolor=""#ffffff""><table width=""100%"" border=""0"" cellpadding=""2"" cellspacing=""0"">
              <tbody>
<tr>
<td style=""width: 120px; padding-left: 35px; font-weight: 700; font-size: 13px; color: rgb(0, 102, 153); line-height: 16px; font-family: Verdana; letter-spacing: 0px;"" valign=""top"" bgcolor=""#ffffff"">
<font color=""#a53411"">Notes</font></td>
<td style=""font-size: 12px; color: rgb(85, 85, 85); line-height: 16px; font-family: Verdana; letter-spacing: 0px;"" bgcolor=""#ffffff"">
</td><td bgcolor=""#ffffff"">
<a href=""#13964"">
</a></td></tr>
{0}
            </tbody></table>

<p align=""center"">
  <font color=""#a53411"" face=""Verdana"" size=""2"">
  <b><a href=""#sList"" style=""text-decoration: none;""><font color=""#006699"">
	Specifics&nbsp;&nbsp;
	</font></a></b><a href=""#sList"" style=""text-decoration: none;"">
	<font color=""#006699"">|</font></a></font><b><font color=""#006699"" face=""Verdana"" size=""2"">&nbsp;&nbsp; </font><font color=""#a53411"" face=""Verdana"" size=""2"">

	<a href=""#promo"" style=""text-decoration: none;""><font color=""#006699"">Bundled 
	Items&nbsp;&nbsp;
	</font></a></font></b><font color=""#a53411"" face=""Verdana"" size=""2"">
	<a href=""#promo"" style=""text-decoration: none;""><font color=""#006699"">|</font></a><b><font color=""#006699"" face=""Verdana"" size=""2"">&nbsp;&nbsp;
	<a style=""text-decoration: none;"" target=""_blank"" href=""http://stores.shop.ebay.ca/LU-Computers-Inc"">
	<font color=""#006699"">Store </font></a></font><font color=""#006699"">&nbsp;</font><a style=""text-decoration: none;"" href=""http://stores.shop.ebay.ca/LU-Computers-Inc""><font color=""#006699"">
	</font></a></b>
	<a style=""text-decoration: none;"" href=""http://stores.shop.ebay.ca/LU-Computers-Inc"">

	<font color=""#006699"">|</font></a><b><font color=""#006699"" face=""Verdana"" size=""2"">&nbsp;&nbsp; </font>
	<a href=""#Terms"" style=""text-decoration: none;""><font color=""#006699"">
	Shipping&nbsp;&nbsp;
	</font></a></b><font color=""#006699"">
	<a href=""#Terms"" style=""text-decoration: none;"">|<b> <font color=""#006699"">&nbsp; 
	Payment&nbsp;&nbsp;

	</font></b><font color=""#006699"">|</font><b><font color=""#006699"">&nbsp;&nbsp; 
	Tax&nbsp;
	</font></b></a></font>
  <b><font color=""#006699"" face=""Verdana"" size=""2""> <br>
	</font><a href=""#Terms"" style=""text-decoration: none;""><font color=""#006699"">
	Return Policy&nbsp;&nbsp;
	</font></a></b><a href=""#Terms"" style=""text-decoration: none;"">

	<font color=""#006699"">|<b>&nbsp;&nbsp; Warranty&nbsp;&nbsp; </b></font></a>
	<a href=""#me"" style=""text-decoration: none;""><font color=""#006699"">|<b>&nbsp;&nbsp; 
	About Us&nbsp;&nbsp; </b>|</font></a><b><font color=""#006699"" face=""Verdana"" size=""2"">&nbsp;&nbsp; </font>
	<a href=""#contact"" style=""text-decoration: none;""><font color=""#006699"">
	Contact Us&nbsp;

	</font></a></b><a href=""#contact"" style=""text-decoration: none;"">
	<font color=""#006699"">&nbsp;|</font></a><b><font color=""#006699"" face=""Verdana"" size=""2"">&nbsp;&nbsp; </font>
	<a style=""text-decoration: none;"" target=""_blank"" href=""http://feedback.ebay.ca/ws/eBayISAPI.dll?ViewFeedback2&amp;userid=dpowerseller&amp;&amp;sspagename=VIP:feedback&amp;ftab=FeedbackAsSeller"">
	<font color=""#006699"">Feedback</font></a></b></font></p>


</td>
            <td width=""6"" background=""http://www.lucomputers.com/ebay/images/in_box7_06.gif"">
			<img alt="""" src=""http://www.lucomputers.com/ebay/images/in_box7_06.gif"" width=""6"" height=""1""></td>
          </tr>
          <tr>
            <td><img alt="""" src=""http://www.lucomputers.com/ebay/images/in_box7_07.gif"" width=""6"" height=""6""></td>
            <td background=""http://www.lucomputers.com/ebay/images/in_box7_08.gif"">
			<img alt="""" src=""http://www.lucomputers.com/ebay/images/in_box7_08.gif"" width=""1"" height=""6""></td>
            <td><img alt="""" src=""http://www.lucomputers.com/ebay/images/in_box7_09.gif"" width=""6"" height=""6""></td>
          </tr>
        </tbody></table>", part_list_upgrades));
            }
            templete = ReplaceSystemBIGImg(templete, system_sku);
            templete = ReplaceSystemMainComment(templete, system_sku);
            return ReplaceSystemSumm(templete, categoryID);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// replace Part title keyword. bold
    /// </summary>
    /// <param name="part_name"></param>
    /// <returns></returns>
    public string ReplacePartNameKeyword(string part_name)
    {
        DataTable dt = KeywordDT;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string keyword = dt.Rows[i]["keyword"].ToString();
            part_name = part_name.Replace(keyword, string.Format("<span style='font-weight:bold; color:#333333;'>{0}</span>", keyword));
        }
        return part_name;
    }
    /// <summary>
    /// Replace system big image.
    /// </summary>
    /// <param name="templete"></param>
    /// <param name="system_sku"></param>
    /// <returns></returns>
    public string ReplaceSystemBIGImg(string templete
        , int system_sku)
    {
        string large_picture_name = "";
        string string_big_img = "[lu_web_ebay_system_big_img]";
        if (templete.IndexOf(string_big_img) != -1)
        {
            DataTable dt = Config.ExecuteDataTable("Select large_pic_name from tb_ebay_system where id='" + system_sku.ToString() + "'");
            if (dt.Rows.Count == 1)
                large_picture_name = dt.Rows[0][0].ToString() ?? "";
            if (large_picture_name != "")
                return templete.Replace(string_big_img, "<img border='0' src='http://www.lucomputers.com/ebay/" + large_picture_name + ".jpg' style='border: 0px solid #666666' width='758' height='563'/>");
            else
                return templete.Replace(string_big_img, "<img border='0' src='http://www.lucomputers.com/ebay/" + system_sku.ToString() + ".jpg' style='border: 0px solid #666666' width='758' height='563'/>");
        }
        return templete;
    }
    /// <summary>
    /// replace system Title.
    /// </summary>
    /// <param name="templete"></param>
    /// <param name="system_sku"></param>
    /// <returns></returns>
    public string ReplaceSystemTitle(string templete
        , int system_sku)
    {
        string system_title1 = "[lu_web_system_titel1]";
        string system_title2 = "[lu_web_system_titel2]";
        string system_title3 = "[lu_web_system_titel3]";

        DataTable dt = Config.ExecuteDataTable("Select System_title1, system_title2, system_title3 from tb_ebay_system where id='" + system_sku.ToString() + "'");
        if (dt.Rows.Count == 1)
        {
            if (templete.IndexOf(system_title1) != -1)
            {
                templete = templete.Replace(system_title1, dt.Rows[0]["system_title1"].ToString());
            }
            if (templete.IndexOf(system_title2) != -1)
                templete = templete.Replace(system_title2, dt.Rows[0]["system_title2"].ToString());

            if (templete.IndexOf(system_title3) != -1)
                templete = templete.Replace(system_title3, dt.Rows[0]["system_title3"].ToString());
        }
        return templete;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="templete"></param>
    /// <param name="categoryID"></param>
    /// <returns></returns>
    public string ReplaceSystemSumm(string templete
        , int categoryID)
    {
        string system_summ_1 = "[lu_web_summ_1]";
        string system_summ_2 = "[lu_web_summ_2]";

        DataTable dt = Config.ExecuteDataTable("Select templete_summ_1, templete_summ_2 from tb_ebay_templete where id='" + categoryID.ToString() + "'");
        if (dt.Rows.Count == 1)
        {
            if (dt.Rows[0][0].ToString().Length > 10)
                templete = templete.Replace(system_summ_1, dt.Rows[0][0].ToString());

            if (dt.Rows[0][1].ToString().Length > 10)
                templete = templete.Replace(system_summ_2, dt.Rows[0][0].ToString());

        }
        return templete;
    }

    /// <summary>
    /// replace system Main Comment.
    /// </summary>
    /// <param name="templete"></param>
    /// <param name="system_sku"></param>
    /// <returns></returns>
    public string ReplaceSystemMainComment(string templete
        , int system_sku)
    {
        string sub_temp = @"<tr>
<td style=""padding-right: 0px; padding-left: 3px; padding-top: 0px;"" valign=""top"">
<table id=""__29"" width=""100%"" align=""center"" border=""0"" cellpadding=""0"" cellspacing=""0"">
<tbody><tr>
<td>
<img alt="""" src=""http://www.lucomputers.com/ebay/newebaytemplate/in_box7_01.gif"" width=""6"" height=""6""></td>
<td background=""http://www.lucomputers.com/ebay/newebaytemplate/in_box7_02.gif"">
<img alt="""" src=""http://www.lucomputers.com/ebay/newebaytemplate/in_box7_02.gif"" width=""1"" height=""6""></td>
<td>
<img alt="""" src=""http://www.lucomputers.com/ebay/newebaytemplate/in_box7_03.gif"" width=""6"" height=""6""></td></tr>
<tr>
<td width=""6"" background=""http://www.lucomputers.com/ebay/newebaytemplate/in_box7_04.gif"">
<img alt="""" src=""http://www.lucomputers.com/ebay/newebaytemplate/in_box7_04.gif"" width=""6"" height=""1""></td>
<td style=""padding: 14px 33px 33px;"" bgcolor=""#ffffff"">
<p><font color=""#006699"" face=""Verdana"" size=""2"">
[lu_web_ebay_system_main_comment]
<b>99% positive customer 
	satisfaction<br>
	</b>We have already sold thousands of computers just like this one with 98% 
	customer satisfaction. Take a look at our past customer reviews
<a href=""http://feedback.ebay.ca/ws/eBayISAPI.dll?ViewFeedback2&amp;userid=dpowerseller&amp;&amp;ftab=FeedbackAsSeller&amp;sspagename=VIP:feedback:2:ca&amp;iid=260179212784"">
<font color=""#006699"">here</font></a>. </font>

<a name=""FAQ6""></a>
<a name=""Contact6""></a>
	<font color=""#006699"" face=""Verdana"" size=""2"">
  . <br>
	<br>
	</font><b>
	<font color=""#A53411"" face=""Verdana"" size=""2"">
  3 Years Labor and Parts warranty &amp; Life Time Toll Free Support<br>

	Pay by Paypal or credit card directly </font></b></p>
</td>
<td width=""6"" background=""http://www.lucomputers.com/ebay/newebaytemplate/in_box7_06.gif"">
<img alt="""" src=""http://www.lucomputers.com/ebay/newebaytemplate/in_box7_06.gif"" width=""6"" height=""1""></td></tr>
<tr>
<td>
<img alt="""" src=""http://www.lucomputers.com/ebay/newebaytemplate/in_box7_07.gif"" width=""6"" height=""6""></td>
<td background=""http://www.lucomputers.com/ebay/newebaytemplate/in_box7_08.gif"">
<img alt="""" src=""http://www.lucomputers.com/ebay/newebaytemplate/in_box7_08.gif"" width=""1"" height=""6""></td>
<td>
<img alt="""" src=""http://www.lucomputers.com/ebay/newebaytemplate/in_box7_09.gif"" width=""6"" height=""6""></td></tr></tbody></table></td></tr>";

        if (templete.IndexOf(LU_WEB_EBAY_SYSTEM_MAIN_COMMENT) != -1)
        {
            string comm_ids = GetMainCommentIds(system_sku);

            string comm = "";
            if (comm_ids.Length > 0)
            {
                if (comm_ids.IndexOf('|') != -1)
                {
                    DataTable dt = Config.ExecuteDataTable("select comment,id from tb_ebay_system_main_comment where id in (" + comm_ids.Replace('|', ',').ToString() + ")");
                    string[] cids = comm_ids.Split(new char[] { '|' });
                    for (int j = 0; j < cids.Length; j++)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (cids[j].ToString() == dt.Rows[i]["id"].ToString())
                                comm += dt.Rows[i][0].ToString() + "<br/><br/>";
                        }
                    }
                }
                else
                {
                    int id;
                    int.TryParse(comm_ids, out id);
                    DataTable dt = Config.ExecuteDataTable("select comment from tb_ebay_system_main_comment where id='" + id.ToString() + "'");
                    if (dt.Rows.Count == 1)
                    {
                        comm = dt.Rows[0][0].ToString() + "<br/><br/>";
                    }
                }

                sub_temp = sub_temp.Replace(LU_WEB_EBAY_SYSTEM_MAIN_COMMENT, comm);
                templete = templete.Replace(LU_WEB_EBAY_SYSTEM_MAIN_COMMENT, sub_temp);
            }
            else
                templete = templete.Replace(LU_WEB_EBAY_SYSTEM_MAIN_COMMENT, "");
        }

        return templete;
    }
    /// <summary>
    /// get logo for part comment list .
    /// </summary>
    /// <param name="img_Sku_name"></param>
    /// <returns></returns>
    public string GetPartLogoIMG(string img_Sku_name)
    {
        return string.Format(@"<img alt="""" src=""{0}"" width=""80"">", img_Sku_name);
    }
    /// <summary>
    /// get main comment ids.
    /// </summary>
    /// <param name="system_sku"></param>
    /// <returns></returns>
    private string GetMainCommentIds(int system_sku)
    {
        string ids = string.Empty;
        DataTable dt = Config.ExecuteDataTable("Select main_comment_ids from tb_ebay_system where id='" + system_sku.ToString() + "'");
        if (dt.Rows.Count == 1)
            ids = dt.Rows[0][0].ToString();
        return ids;
    }
}
