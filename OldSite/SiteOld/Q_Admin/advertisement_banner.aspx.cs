using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Q_Admin_advertisement_banner : PageBase
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

        //
        // load banner
        DataTable dt = Config.ExecuteDataTable(@"select 	advertise_serial_no, image_file_name, advertise_link_url, advertise_type 
	from 
	tb_advertise where advertise_type=1");
        if (dt.Rows.Count == 6)
        {
            this.Image_banner_1.ImageUrl = dt.Rows[0][1].ToString();
            this.txt_banner_image1.Text = dt.Rows[0][1].ToString();
            this.txt_banner_url1.Text = dt.Rows[0][2].ToString();

            this.Image_banner_2.ImageUrl = dt.Rows[1][1].ToString();
            this.txt_banner_image2.Text = dt.Rows[1][1].ToString();
            this.txt_banner_url2.Text = dt.Rows[1][2].ToString();

            this.Image_banner_3.ImageUrl = dt.Rows[2][1].ToString();
            this.txt_banner_image3.Text = dt.Rows[2][1].ToString();
            this.txt_banner_url3.Text = dt.Rows[2][2].ToString();

            this.Image_banner_4.ImageUrl = dt.Rows[3][1].ToString();
            this.txt_banner_image4.Text = dt.Rows[3][1].ToString();
            this.txt_banner_url4.Text = dt.Rows[3][2].ToString();

            this.Image_banner_5.ImageUrl = dt.Rows[4][1].ToString();
            this.txt_banner_image5.Text = dt.Rows[4][1].ToString();
            this.txt_banner_url5.Text = dt.Rows[4][2].ToString();

            this.Image_banner_6.ImageUrl = dt.Rows[5][1].ToString();
            this.txt_banner_image6.Text = dt.Rows[5][1].ToString();
            this.txt_banner_url6.Text = dt.Rows[5][2].ToString();
        }

        //
        // load banner
        dt = Config.ExecuteDataTable(@"select 	advertise_serial_no, image_file_name, advertise_link_url, advertise_type 
	from 
	tb_advertise where advertise_type=2");
        if (dt.Rows.Count == 1)
        {
            this.Image_banner_7.ImageUrl = dt.Rows[0][1].ToString();
            this.txt_banner_image7.Text = dt.Rows[0][1].ToString();
            this.txt_banner_url7.Text = dt.Rows[0][2].ToString();
        }

        BindBannerImageList();
    }

    private void BindBannerImageList()
    {
        DirectoryInfo dir = new DirectoryInfo(Server.MapPath("/soft_img/advertise/"));
        FileInfo[] fis = dir.GetFiles();

        DataTable dt = new DataTable();
        dt.Columns.Add("image_filename");

        for (int i = 0; i < fis.Length; i++)
        {
            if (fis[i].Extension.ToLower() == ".jpg")
            {
                DataRow dr = dt.NewRow();
                dr["image_filename"] = "/soft_img/advertise/" + fis[i].Name;
                dt.Rows.Add(dr);
            }
        }

        this.lv_banner_image_list.DataSource = dt;
        this.lv_banner_image_list.DataBind();
    }
  
    protected void lv_banner_image_list_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "SetIn":
                SaveImageFilenameTOADV(e.CommandArgument.ToString());
                break;
        }
    }

    private void SaveImageFilenameTOADV(string filename)
    {
        int id = 0;
        string url = string.Empty;
        if (RadioButton_banner_flash_image1.Checked)
        {
            url = this.txt_banner_url1.Text.Trim();
            id = 1;
        }
        if (RadioButton_banner_flash_image2.Checked)
        {
            url = this.txt_banner_url2.Text.Trim();
            id = 2;
        }

        if (RadioButton_banner_flash_image3.Checked)
        {
            url = this.txt_banner_url3.Text.Trim();
            id = 3;
        }
        if (RadioButton_banner_flash_image4.Checked)
        {
            url = this.txt_banner_url4.Text.Trim();
            id = 4;
        }
        if (RadioButton_banner_flash_image5.Checked)
        {
            url = this.txt_banner_url5.Text.Trim();
            id = 5;
        }
        if (RadioButton_banner_flash_image6.Checked)
        {
            url = this.txt_banner_url6.Text.Trim();
            id = 6;
        }
        if (RadioButton_banner_flash_image7.Checked)
        {
            url = this.txt_banner_url7.Text.Trim();
            id = 7;
        }

        try
        {
            Config.ExecuteNonQuery(string.Format("Update tb_advertise set image_file_name='{0}', advertise_link_url='{1}' where advertise_serial_no='{2}'", filename, url, id));
            CH.Alert(KeyFields.SaveIsOK, this.Literal1);
            InitialDatabase();
        }
        catch (Exception ex)
        {
            CH.Alert(ex.Message, this.Literal1);
        }
    }
    protected void btn_save_banner_type_Click(object sender, EventArgs e)
    {
        try
        {
            StreamWriter sw = new StreamWriter(Server.MapPath("/site/inc/inc_banner.asp"));
            if (!RadioButton2.Checked)
            {
                string pics = string.Format("{0}|{1}|{2}|{3}|{4}|{5}",
                    this.txt_banner_image1.Text.Trim()
                    , this.txt_banner_image2.Text.Trim()
                    , this.txt_banner_image3.Text.Trim()
                    , this.txt_banner_image4.Text.Trim()
                    , this.txt_banner_image5.Text.Trim()
                    , this.txt_banner_image6.Text.Trim());
              
                pics = "0";
                if (this.txt_banner_image1.Text.Trim() != "")
                {
                    pics += "|" + this.txt_banner_image1.Text.Trim();
                }
                if (this.txt_banner_image2.Text.Trim() != "")
                {
                    pics += "|" + this.txt_banner_image2.Text.Trim();
                }
                if (this.txt_banner_image3.Text.Trim() != "")
                {
                    pics += "|" + this.txt_banner_image3.Text.Trim();
                }
                if (this.txt_banner_image4.Text.Trim() != "")
                {
                    pics += "|" + this.txt_banner_image4.Text.Trim();
                }
                if (this.txt_banner_image5.Text.Trim() != "")
                {
                    pics += "|" + this.txt_banner_image5.Text.Trim();
                }
                if (this.txt_banner_image6.Text.Trim() != "")
                {
                    pics += "|" + this.txt_banner_image6.Text.Trim();
                }
                if (pics != "0")
                    pics = pics.Substring(2);
                CreatePAVFile(this.txt_banner_url1.Text.Trim(), this.txt_banner_url2.Text.Trim(), this.txt_banner_url3.Text.Trim(), this.txt_banner_url4.Text.Trim(), this.txt_banner_url5.Text.Trim(), this.txt_banner_url6.Text.Trim());
                // var pics = '/soft_img/advertise/amd-platform.jpg|/soft_img/advertise/asus3870x2.jpg|/soft_img/advertise/gtx280top.jpg|/soft_img/advertise/asus360service.jpg'


                string links = "/site/p_a_v.asp?fid=1|/site/p_a_v.asp?fid=2|/site/p_a_v.asp?fid=3|/site/p_a_v.asp?fid=4|/site/p_a_v.asp?fid=5|/site/p_a_v.asp?fid=6";

                sw.Write(string.Format(@"
<table width=""100%""  border=""0"" cellspacing=""0"" cellpadding=""0"" style=""padding-top:1px; padding-bottom:4px; "">
  <tr>   
    <td>
        <div id=""focusViwer"" align=""center"">
            <a href=""/site/p_a_v.asp?fid=1"" target=""_blank"">
                <img src=""/soft_img/app/02.jpg"" width=""600"" height=""110"" alt=""fffffff""/>
            </a>
        </div><span id='is_notebook_adv' style='display:none;'>0</span>
        </td>
  </tr>
</table>
<script>
		<!--
    var focus_width = 600
    var focus_height = 249
    var text_height = 0
    var swf_height = focus_height + text_height
    var pics = '{0}'
   
    var links = '{1}'
    var texts = 'LU AD|LU AD|LU AD|LU AD|LU AD|LU AD'

    var FocusFlash = new sinaFlash(""/flash/flash.swf"", ""focusflash"", focus_width, swf_height, ""7"", ""#FFFFFF"", false, ""High"");
    FocusFlash.addParam(""allowScriptAccess"", ""sameDomain"");
    FocusFlash.addParam(""menu"", ""false"");
    FocusFlash.addParam(""wmode"", ""opaque"");
    FocusFlash.addVariable(""pics"", pics);
    FocusFlash.addVariable(""links"", links);
    FocusFlash.addVariable(""texts"", texts);
    FocusFlash.addVariable(""borderwidth"", focus_width);
    FocusFlash.addVariable(""borderheight"", focus_height);
    FocusFlash.addVariable(""textheight"", text_height);
    //FocusFlash.addVariable(""curhref"", curhref);
    FocusFlash.write(""focusViwer"");
		//-->
</script>
", pics, links));
//                sw.Write(string.Format(@"
//<table width=""100%""  border=""0"" cellspacing=""0"" cellpadding=""0"" style=""padding-top:1px; padding-bottom:4px; "">
//  <tr>   
//    <td>
//        <div id=""focusViwer"" align=""center"">
//            <a href=""/site/p_a_v.asp?fid=1"" target=""_blank"">
//                <img src=""/soft_img/app/02.jpg"" width=""600"" height=""110"" alt=""fffffff""/>
//            </a>
//        </div><span id='is_notebook_adv' style='display:none;'>0</span>
//        </td>
//  </tr>
//</table>
//<script>
//		<!--
//    var focus_width = 600
//    var focus_height = 133
//    var text_height = 0
//    var swf_height = focus_height + text_height
//    var pics = '{0}'
//   
//    var links = '{1}'
//    var texts = 'ffff|ttttt|hhhhh|yyyyy|vvvvv|dddddd'
//
//    var FocusFlash = new sinaFlash(""/flash/flash.swf"", ""focusflash"", focus_width, swf_height, ""7"", ""#FFFFFF"", false, ""High"");
//    FocusFlash.addParam(""allowScriptAccess"", ""sameDomain"");
//    FocusFlash.addParam(""menu"", ""false"");
//    FocusFlash.addParam(""wmode"", ""opaque"");
//    FocusFlash.addVariable(""pics"", pics);
//    FocusFlash.addVariable(""links"", links);
//    FocusFlash.addVariable(""texts"", texts);
//    FocusFlash.addVariable(""borderwidth"", focus_width);
//    FocusFlash.addVariable(""borderheight"", focus_height);
//    FocusFlash.addVariable(""textheight"", text_height);
//    //FocusFlash.addVariable(""curhref"", curhref);
//    FocusFlash.write(""focusViwer"");
//		//-->
//</script>
//", pics, links));
                AdvertiseModel am = AdvertiseModel.GetAdvertiseModel(1);
                am.advertise_link_url = this.txt_banner_url1.Text;
                am.image_file_name = this.txt_banner_image1.Text;
                am.Update();
                am = AdvertiseModel.GetAdvertiseModel(2);
                am.advertise_link_url = this.txt_banner_url2.Text;
                am.image_file_name = this.txt_banner_image2.Text;
                am.Update();
                am = AdvertiseModel.GetAdvertiseModel(3);
                am.advertise_link_url = this.txt_banner_url3.Text;
                am.image_file_name = this.txt_banner_image3.Text;
                am.Update();
                am = AdvertiseModel.GetAdvertiseModel(4);
                am.advertise_link_url = this.txt_banner_url4.Text;
                am.image_file_name = this.txt_banner_image4.Text;
                am.Update();
                am = AdvertiseModel.GetAdvertiseModel(5);
                am.advertise_link_url = this.txt_banner_url5.Text;
                am.image_file_name = this.txt_banner_image5.Text;
                am.Update();
                am = AdvertiseModel.GetAdvertiseModel(6);
                am.advertise_link_url = this.txt_banner_url6.Text;
                am.image_file_name = this.txt_banner_image6.Text;
                am.Update();
               

            }

            else
            {
                sw.Write(string.Format(@"
<table width=""100%""  border=""0"" cellspacing=""0"" cellpadding=""0"" style=""padding-top:1px; padding-bottom:4px; "">
 <tr>
    <td><a href=""{1}""><img src=""{0}"" border=""0"" /></a></td>
  </tr>
</table>
", this.txt_banner_image7.Text.Trim(), this.txt_banner_url7.Text.Trim()));

                AdvertiseModel am = AdvertiseModel.GetAdvertiseModel(7);
                am.advertise_link_url = this.txt_banner_url7.Text;
                am.image_file_name = this.txt_banner_image7.Text;
                am.Update();
            }
            sw.Close();
            sw.Dispose();

            CH.Alert(KeyFields.SaveIsOK, this.Literal1);
        }
        catch (Exception ex)
        {
            CH.Alert(ex.Message, this.Literal1);
        }
    }
    protected void Button_upload_Click(object sender, EventArgs e)
    {
        if (this.FileUpload1.PostedFile != null)
        {
            try
            {

                string newFilename = Server.MapPath(string.Format("{0}{1}",
                 "/soft_img/advertise/",
                  string.Format("adv_{0}.jpg", DateTime.Now.ToString("yyyyMMddhhmmss"))));
                this.FileUpload1.PostedFile.SaveAs(newFilename);

                BindBannerImageList();
            }
            catch (Exception ex)
            {
                CH.Alert(ex.Message, this.Literal1);
            }
        }
    }

    private void CreatePAVFile(string url1, string url2, string url3, string url4, string url5, string url6)
    {
        StreamWriter sw = new StreamWriter(Server.MapPath("/site/p_a_v.asp"));
        string str =string.Format(@"<%@LANGUAGE=""VBSCRIPT"" CODEPAGE=""65001""%>
<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
<html xmlns=""http://www.w3.org/1999/xhtml"">
<head>
<meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />
<title>LU Computers</title>
</head>

<body>
<%
dim fid 
fid = request(""fid"")

select case fid
	case ""1"":
		response.Redirect(""{0}"")
	case ""2"":
		response.Redirect(""{1}"")
	case ""3"":
		response.Redirect(""{2}"")
	case ""4"":
		response.Redirect(""{3}"")
    case ""5"":
		response.Redirect(""{4}"")
    case ""6"":
		response.Redirect(""{5}"")
end select
%>
</body>
</html>
", url1, url2, url3, url4, url5, url6);
        sw.Write(str);
        sw.Close();
    }
    protected void RadioButton_banner_flash_image7_CheckedChanged(object sender, EventArgs e)
    {

    }
}
