using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Collections;

/// <summary>
/// Summary description for GenerateFlashViewFile
/// </summary>
public class GenerateFlashViewFile
{
    const string source_path = "~/pro_img/components/";
    const string ebay_img_path = "~/pro_img/ebay_gallery/";
    const string ebay_img_path_url =  "/pro_img/ebay_gallery/";
    const string test_img_path = "~/pro_img/components/";//"E:\\Projects\\LUcomputer\\document\\COMPONENTS_photo\\COMPONENTS\\";
    
    flashViewFile _flashViewFile = flashViewFile.system;
    int _id;
    ArrayList _al_sku = new ArrayList();



	public GenerateFlashViewFile(int id, flashViewFile fvf)
	{
		//
		// TODO: Add constructor logic here
		//
        _flashViewFile = fvf;
        _id = id;
    }

    public GenerateFlashViewFile(ArrayList al_skus, int id, flashViewFile fvf)
    {
        _al_sku = al_skus;
        _flashViewFile = fvf;
        _id = id;
    }

    #region Properties

    public flashViewFile flashViewFile
    {
        get { return _flashViewFile; }
        set { _flashViewFile = value; }
    }


    public int id
    {
        get { return _id; }
        set { _id = value; }
    }

    public ArrayList al_sku
    {
        get { return _al_sku; }
        set { _al_sku = value; }
    }
    #endregion

    public bool Export()
    {
        try
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
          //  throw new Exception(string.Format("{0}",  al_sku.Count.ToString()));


            for (int i = 0; i < al_sku.Count; i++)
            {
                string al = al_sku[i].ToString();
                string[] als = al.Split(new char[] { '|' });

                string sku =  als[0];
                string comment =  als[1];
                //throw new Exception(string.Format("{0}|{1}<br>{2}", sku, comment, al));

                DirectoryInfo dir = new DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath(source_path));
                //
                // test
                //
                dir = new DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath(test_img_path));
                FileInfo[] fis = dir.GetFiles();
                for (int j = 0; j < fis.Length; j++)
                {

                    if (fis[j].Name.IndexOf(string.Format("{0}{1}", sku, "_list_")) != -1)
                    {
                        string fullname1 = GenerateFolderEbayGallery(fis[j].Name.Replace("_list", "_ebay_list_t"), ebay_img_path);
                        if (!File.Exists(fullname1))
                        {
                            string fullname2 = GenerateFolderEbayGallery(fis[j].Name.Replace("_list", "_ebay_list_g"), ebay_img_path);
                            MakeThumbnail.Run(fis[j].FullName, fullname1, 75, 75, "W");
                            MakeThumbnail.Run(fis[j].FullName, fullname1.Replace("_ebay_list_t", "_ebay_list_m"), 135, 135, "W");
                            MakeThumbnail.Run(fis[j].FullName, fullname2, 350, 350, "W");
                            
                        }
                        sb.Append("|" + GetPartMinImgForEbay(int.Parse(sku), fis[j].Name) + "," + comment);                    
                    }
                }
            }
            string sku_lists = string.Empty;
            if (sb.ToString().Length > 2)
                sku_lists = sb.ToString().Substring(1);

            //System.IO.StreamWriter sw = new StreamWriter(FlashFilePath(string.Format("{0}{1}", id, "_flash.asp")));

            //sw.Write(PageTemplete.Replace("[[0]]", sku_lists));
            //sw.Close();
            return true;
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }



    public string FlashFilePath(string filename)
    {
        
            if (flashViewFile == flashViewFile.system)
                return System.Web.HttpContext.Current.Server.MapPath(string.Format("~/ebay_page_store/system/{0}", filename));
            else
                return System.Web.HttpContext.Current.Server.MapPath(string.Format("~/ebay_page_store/part/", filename));
       
    }
    /// <summary>
    /// document.getElementById(""TTQiozi"").GetHttpHeard(0);
    /// </summary>
    public string PageTemplete
    {
        get
        {
            return @"<%@LANGUAGE=""VBSCRIPT"" CODEPAGE=""65001""%>
<html>
<head>
<meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" />
<title>LU Computers</title>
<script type=""text/javascript"" src=""/js/flashobject.js""></script>
</head>
<style>
body { margin:0px ;}
</style>
<body style=""margin:0px ; padding:0px"">
<div id=""flash_video"" style=""text-align:center;""></div>
<script >
function javascript_to_flash(texttoflash) {
	document.getElementById(""TTQiozi"").asFunc(texttoflash);
}
var fo = new FlashObject(""/flash/LU_View_Gallery.swf"", ""TTQiozi"", ""750"", ""400"", ""7"", ""#cccccc""); 
fo.write(""flash_video""); 
	
setTimeout(function() { javascript_to_flash('[[0]]');}, 2000);
</script>

</body>
</html>";
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filename"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    public string GenerateFolderEbayGallery(string filename, string path)
    {
        if (path == string.Empty)
            path = ebay_img_path;
        string _path = System.Web.HttpContext.Current.Server.MapPath(string.Format("{0}{1}", path, filename.Trim().Substring(0, 1)));
        if (!Directory.Exists(_path))
        {
            Directory.CreateDirectory(_path);
        }
        return string.Format("{0}\\{1}", _path, filename);
    }

    public string GetPartMinImgForEbayM(int sku)
    {
        return string.Format("{0}{2}/{1}_ebay_list_m_1.jpg", ebay_img_path_url, sku, sku.ToString().Substring(0, 1));


    }
    public string GetPartMinImgForEbayT(int sku)
    {
        return string.Format("{0}{2}/{1}_ebay_list_t_1.jpg", ebay_img_path_url, sku, sku.ToString().Substring(0, 1));
    }
    public string GetPartMinImgForEbayG(int img_sku)
    {
        return string.Format("http://www.lucomputers.com/pro_img/COMPONENTS/{0}_g_1.jpg", img_sku);
    }
    public string GetPartMinImgForEbay(int sku, string source_filename)
    {
        return string.Format("{0}{2}/{1}", ebay_img_path_url, source_filename.Replace("_list", "_ebay_list_t"), sku.ToString().Substring(0, 1));
    }
}

public enum flashViewFile
{
    part,
    system
}