using System;
using System.Collections;
using System.Data;
using System.Drawing.Imaging;
using System.Drawing;
using System.Web.UI.WebControls;
using System.IO;

public partial class Q_Admin_product_helper_upload_part_image : PageBase
{
    int current_sku = 0;
    string source_pathfile = Config.ProImgBasePath + "pro_img\\source_components\\";
    string part_pathfile = Config.ProImgBasePath + "pro_img\\COMPONENTS\\";
    string part_pathfile_no_shuiying = Config.ProImgBasePath + "pro_img\\not_shui_yin\\";
    string image_url = Config.http_domain + "pro_img/COMPONENTS/";//"http://www.lucomputers.com/pro_img/COMPONENTS/";
    string ebay_path = Config.ProImgBasePath + "pro_img\\ebay_gallery\\";
    ArrayList AL = new ArrayList();

    public int CurrentSKU
    {
        get { return (int)ViewState["CurrentSKU"]; }
        set { ViewState["CurrentSKU"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindUploadDV();
        }
    }
    
    public void BindUploadDV()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("id");

        for (int i = 1; i <= 10; i++)
        {
            DataRow dr = dt.NewRow();
            dr[0] = i.ToString();
            dt.Rows.Add(dr);
        }

        this.GridView_Upload.DataSource = dt;
        this.GridView_Upload.DataBind();
    }

    void SaveUpload(int sku)
    {
        string webfilename = source_pathfile + sku.ToString() + ".jpg";
        if (File.Exists(webfilename))
        {
            SaveUpload(null, 1, false, webfilename);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="FileUpload1"></param>
    /// <param name="index"></param>
    private void SaveUpload(FileUpload FileUpload1, int index, bool IsUpload, string webfilename)
    {
        string error = string.Empty;
        string webFilePath = "";
        if (!IsUpload)
        {
            if (webfilename == null)
                return;
            if (!File.Exists(webfilename))
                return;
            webFilePath = webfilename;
        }
        else
        {
            if (FileUpload1.HasFile)
            {
                string fileContentType = FileUpload1.PostedFile.ContentType;
                //if (fileContentType == "image/bmp" || fileContentType == "image/gif" || fileContentType == "image/jpeg")

                string fileName = FileUpload1.FileName;
                webFilePath = source_pathfile + fileName;        // 服务器端文件路径

                if (fileContentType == "image/jpeg")
                {
                    FileUpload1.SaveAs(webFilePath);                                // 使用 SaveAs 方法保存文件
                }
                else
                    return;
            }
            else
                return;
        }

        //string name = FileUpload1.PostedFile.FileName;                  // 客户端文件路径

        FileInfo file = new FileInfo(webFilePath);
        // 文件名称
        string fileName_s = "s_" + file.Name;                           // 缩略图文件名称
        string fileName_sy = "sy_" + file.Name;                         // 水印图文件名称（文字）
        string fileName_syp = "syp_" + file.Name;                       // 水印图文件名称（图片）

        string webFilePath_s = string.Empty;// Server.MapPath("~/txts/" + fileName_s);　　// 服务器端缩略图路径
        string webFilePath_sy = string.Empty;//Server.MapPath("file/" + fileName_sy); // 服务器端带水印图路径(文字)
        string webFilePath_syp = string.Empty;// Server.MapPath("file/" + fileName_syp);　// 服务器端带水印图路径(图片)
        //string webFilePath_sypf = Server.MapPath("file/shuiyin.jpg");　// 服务器端水印图路径(图片)

        // get sku
        if (index == 1)
        {
            //CH.Alert(fileName.Trim().Substring(0, fileName.Trim().IndexOf(".")),this.FileUpload1);
            int.TryParse(file.Name.Trim().Substring(0, file.Name.Trim().IndexOf(".")), out current_sku);

        }
        else
        {
            if (current_sku == 0)
                int.TryParse(this.txt_sku.Text, out current_sku);
            if (current_sku == 0)
                throw new Exception("SKU is Null.");

        }
        CurrentSKU = current_sku;
        //if (!File.Exists(webFilePath))
        {
            try
            {
                string min_image = string.Format("{0}_t.jpg", CurrentSKU);
                string list_image = string.Format("{0}_list_{1}.jpg", CurrentSKU, index);
                string max_image = string.Format("{0}_g_{1}.jpg", CurrentSKU, index);
                string ebay_min_img = string.Format("{0}_ebay_list_t_{1}.jpg", CurrentSKU, index);
                string ebay_max_img = string.Format("{0}_ebay_list_g_{1}.jpg", CurrentSKU, index);
                //
                // use show image to page
                if (index == 1)
                    AL.Add(min_image);
                AL.Add(list_image);
                AL.Add(max_image);

                //min_image = Server.MapPath(string.Format("{1}{0}", min_image, part_pathfile));
                //list_image = Server.MapPath(string.Format("{1}{0}", list_image, part_pathfile));
                //max_image = Server.MapPath(string.Format("{1}{0}", max_image, part_pathfile));

                string min_image_no_shuiyin = string.Format("{1}{0}", min_image, GenerateNoShuiYinPath(CurrentSKU));
                string list_image_no_shuiyin = string.Format("{1}{0}", list_image, GenerateNoShuiYinPath(CurrentSKU));
                string max_image_no_shuiyin = string.Format("{1}{0}", max_image, GenerateNoShuiYinPath(CurrentSKU));

                string min_image_have_shuiyin = string.Format("{1}{0}", min_image, part_pathfile);
                string list_image_have_shuiyin = string.Format("{1}{0}", list_image, part_pathfile);
                string max_image_have_shuiyin = string.Format("{1}{0}", max_image, part_pathfile);

                //throw new Exception(min_image_no_shuiyin);
                ebay_min_img = GenerateFolderEbayGallery(ebay_min_img, ebay_path);
                ebay_max_img = GenerateFolderEbayGallery(ebay_max_img, ebay_path);


                //AddShuiYinWord(webFilePath, webFilePath_sy);
                //AddShuiYinPic(webFilePath, webFilePath_syp, webFilePath_sypf);
                if (index == 1)
                {
                    MakeThumbnail(webFilePath, min_image_no_shuiyin, 50, 50, "Cut");     // 生成缩略图方法   
                    File.Copy(min_image_no_shuiyin, min_image_have_shuiyin, true);
                }
                Config.ExecuteNonQuery(string.Format("update tb_product set other_product_sku='0',is_modify=1 where product_serial_no='{0}'", CurrentSKU));
                MakeThumbnail(webFilePath, ebay_min_img, 135, 135, "Cut");
                MakeThumbnail(webFilePath, ebay_max_img, 350, 350, "Cut");
                //AddShuiYinWord(webFilePath, Server.MapPath("~/txts/test.jpg"));
                //AddShuiYinPic(webFilePath, Server.MapPath("~/txts/test2.jpg"), Server.MapPath("~/images/logo_flash_bg.png"));
                MakeThumbnail(webFilePath, list_image_no_shuiyin, 300, 300, "Cut");     // 生成缩略图方法
                FileInfo _fi = new FileInfo(webFilePath);
                _fi.CopyTo(max_image_no_shuiyin, true);

                if (RadioButton_image1.Checked)
                    webFilePath_syp = Server.MapPath("~/soft_img/shuyin/1.gif");
                if (RadioButton_image2.Checked)
                    webFilePath_syp = Server.MapPath("~/soft_img/shuyin/2.png");
                if (RadioButton_image3.Checked)
                    webFilePath_syp = Server.MapPath("~/soft_img/shuyin/3.gif");
                //AddShuiYinWord(list_image_shuiyin, Server.MapPath("~/txts/test.jpg"));
                int x_300;
                int y_300;
                int x_418;
                int y_418;
                int.TryParse(this.txt_300_x.Text, out x_300);
                int.TryParse(this.txt_300_y.Text, out y_300);
                int.TryParse(this.txt_418_x.Text, out x_418);
                int.TryParse(this.txt_418_y.Text, out y_418);
                int transparency_v;
                int.TryParse(this.RadioButtonList_transparency_v.SelectedValue.ToString(), out transparency_v);

                AddShuiYinPic(list_image_no_shuiyin, list_image_have_shuiyin, webFilePath_syp, x_300, y_300, transparency_v);
                AddShuiYinPic(max_image_no_shuiyin, max_image_have_shuiyin, webFilePath_syp, x_418, y_418, transparency_v);
                //AddShuiYinPic(webFilePath, list_image_no_shuiyin, Server.MapPath("~/images/logo_flash_bg.png"));
                //AddShuiYinPic(max_image_shuiyin, max_image_no_shuiyin, webFilePath_syp);

                // MakeThumbnail(webFilePath, max_image, 500, 500, "Cut");     // 生成缩略图方法
                //error= "提示：文件“" + fileName + "”成功上传，并生成“" + fileName_s + "”缩略图，文件类型为：" + FileUpload1.PostedFile.ContentType + "，文件大小为：" + FileUpload1.PostedFile.ContentLength + "B";

            }
            catch (Exception ex)
            {
                error = "提示：文件上传失败，失败原因：" + ex.Message;
            }
        }
        //else
        //{
        //    error = "提示：文件已经存在，请重命名后上传";
        //}


        if (error != string.Empty)
            CH.Alert(error, this.GridView_Upload);
        else
            CH.Alert("Upload success!", this.GridView_Upload);
    }

    private string GenerateNoShuiYinPath(int sku)
    {
        DataTable dt = Config.ExecuteDataTable("select menu_child_serial_no from tb_product where product_serial_no='" + sku.ToString() + "'");
        if (dt.Rows.Count > 0)
        {
            string path =string.Format("{0}{1}/", part_pathfile_no_shuiying, dt.Rows[0][0].ToString());
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                return path;
            }
            else
                return path;
        }
        else
        {
            throw new Exception("SKU isn't exist...");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="filename"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    private string GenerateFolderEbayGallery(string filename, string path)
    {
        string _path = string.Format("{0}{1}", path, filename.Trim().Substring(0, 1));
        if (!Directory.Exists(_path))
        {
            Directory.CreateDirectory(_path);
        }
        return string.Format("{0}\\{1}", _path, filename);

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="al"></param>
    private void ShowImageToPage(ArrayList al)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        for (int i = 0; i < al.Count; i++)
        {
            sb.Append(string.Format("<div style='float:left; margin: 10px'><img src='{0}{1}' alt=''></div>", image_url, al[i].ToString()));
        }
        this.Literal1.Text = sb.ToString();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sku"></param>
    public void MakeChangePartImgSum(int sku)
    {
        DirectoryInfo dir = new DirectoryInfo(part_pathfile);
        FileInfo[] fis = dir.GetFiles();
        int count = 0;
        for (int i = 0; i < fis.Length; i++)
        {
            if (fis[i].Name.IndexOf(string.Format("{0}_list_", sku)) != -1)
                count += 1;
        }
        if (count > 0)
        {
            // throw new Exception(sku.ToString());
            var pm = ProductModel.GetProductModel(DBContext, sku);

            pm.product_img_sum = sbyte.Parse(count.ToString());
            DBContext.SaveChanges();
        }
        // CH.Alert(string.Format("images: {0}", count), this.txt_sku);

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            CurrentSKU = 0;
            AL.Clear();
            this.lbl_sku.Text = "";
            for (int i = 0; i < this.GridView_Upload.Rows.Count; i++)
            {
                FileUpload fu = (FileUpload)this.GridView_Upload.Rows[i].Cells[1].FindControl("_FileUpload1");

                SaveUpload(fu, i + 1, true, null);
            }
            this.lbl_sku.Text = current_sku.ToString();
            ShowImageToPage(AL);
            MakeChangePartImgSum(CurrentSKU);
            CurrentSKU = 0;
            this.txt_sku.Text = "";
        }
        catch (Exception ex)
        {
            CH.Alert(ex.Message, this.txt_sku);
        }
    }
    /**/
    /// <summary>
    /// 生成缩略图
    /// </summary>
    /// <param name="originalImagePath">源图路径（物理路径）</param>
    /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
    /// <param name="width">缩略图宽度</param>
    /// <param name="height">缩略图高度</param>
    /// <param name="mode">生成缩略图的方式</param>   
    public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, string mode)
    {
        System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);

        int towidth = width;
        int toheight = height;

        int x = 0;
        int y = 0;
        int ow = originalImage.Width;
        int oh = originalImage.Height;

        switch (mode)
        {
            case "HW"://指定高宽缩放（可能变形）               
                break;
            case "W"://指定宽，高按比例                   
                toheight = originalImage.Height * width / originalImage.Width;
                break;
            case "H"://指定高，宽按比例
                towidth = originalImage.Width * height / originalImage.Height;
                break;
            case "Cut"://指定高宽裁减（不变形）               
                if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                {
                    oh = originalImage.Height;
                    ow = originalImage.Height * towidth / toheight;
                    y = 0;
                    x = (originalImage.Width - ow) / 2;
                }
                else
                {
                    ow = originalImage.Width;
                    oh = originalImage.Width * height / towidth;
                    x = 0;
                    y = (originalImage.Height - oh) / 2;
                }
                break;
            default:
                break;
        }

        //新建一个bmp图片
        System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

        //新建一个画板
        System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

        //设置高质量插值法
        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

        //设置高质量,低速度呈现平滑程度
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

        //清空画布并以透明背景色填充
        g.Clear(System.Drawing.Color.Transparent);

        //在指定位置并且按指定大小绘制原图片的指定部分
        g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
            new System.Drawing.Rectangle(x, y, ow, oh),
            System.Drawing.GraphicsUnit.Pixel);

        try
        {
            //以jpg格式保存缩略图
            bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
        catch (System.Exception e)
        {
            throw e;
        }
        finally
        {
            originalImage.Dispose();
            bitmap.Dispose();
            g.Dispose();
        }
    }

    /**/
    /// <summary>
    /// 在图片上增加文字水印
    /// </summary>
    /// <param name="Path">原服务器图片路径</param>
    /// <param name="Path_sy">生成的带文字水印的图片路径</param>
    protected void AddShuiYinWord(string Path, string Path_sy)
    {
        string addText = "LU";
        System.Drawing.Image image = System.Drawing.Image.FromFile(Path);
        System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);
        g.DrawImage(image, 0, 0, image.Width, image.Height);
        System.Drawing.Font f = new System.Drawing.Font("Verdana", 30);
        System.Drawing.Brush b = new System.Drawing.SolidBrush(System.Drawing.Color.Red);

        g.DrawString(addText, f, b, 50, image.Height - 80);
        g.Dispose();

        image.Save(Path_sy);
        image.Dispose();
    }

    /**/
    /// <summary>
    /// 在图片上生成图片水印
    /// </summary>
    /// <param name="Path">原服务器图片路径</param>
    /// <param name="Path_syp">生成的带图片水印的图片路径</param>
    /// <param name="Path_sypf">水印图片路径</param>
    protected void AddShuiYinPic(string Path, string Path_syp, string Path_sypf)
    {
        System.Drawing.Image image = System.Drawing.Image.FromFile(Path);
        System.Drawing.Image copyImage = System.Drawing.Image.FromFile(Path_sypf);
        System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);
        g.DrawImage(copyImage, new System.Drawing.Rectangle(image.Width - copyImage.Width, image.Height - copyImage.Height, copyImage.Width, copyImage.Height), 0, 0, copyImage.Width, copyImage.Height, System.Drawing.GraphicsUnit.Pixel);
        g.Dispose();

        image.Save(Path_syp);
        image.Dispose();
    }
    /**/
    /// <summary>
    /// 在图片上生成图片水印
    /// </summary>
    /// <param name="Path">原服务器图片路径</param>
    /// <param name="Path_syp">生成的带图片水印的图片路径</param>
    /// <param name="Path_sypf">水印图片路径</param>
    protected void AddShuiYinPic(string Path, string Path_syp, string Path_sypf, int x, int y, int transparency_v)
    {
        System.Drawing.Image image = System.Drawing.Image.FromFile(Path);
        System.Drawing.Image copyImage = System.Drawing.Image.FromFile(Path_sypf);
        System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);
        ImageAttributes imageAttributes = new ImageAttributes();
        ColorMap colorMap = new ColorMap();

        colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
        colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
        ColorMap[] remapTable = { colorMap };

        imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

        float transparency = 0.5F;
        //if (watermarkTransparency >= 1 && watermarkTransparency <= 10)
        //{
        transparency = (transparency_v / 10.0F);
        //}

        float[][] colorMatrixElements = {
                                                new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},
                                                new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},
                                                new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},
                                                new float[] {0.0f,  0.0f,  0.0f,  transparency, 0.0f},
                                                new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}
                                            };

        ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);

        imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

        g.DrawImage(copyImage, new System.Drawing.Rectangle(x, y, copyImage.Width, copyImage.Height), 0, 0,
            copyImage.Width, copyImage.Height, System.Drawing.GraphicsUnit.Pixel, imageAttributes);
        g.Dispose();

        image.Save(Path_syp);
        image.Dispose();
        imageAttributes.Dispose();
    }

    protected void btn_generate_photo_for_ebay_Click(object sender, EventArgs e)
    {
        DirectoryInfo dir = new DirectoryInfo(part_pathfile);
        FileInfo[] fis = dir.GetFiles();
        for (int i = 0; i < fis.Length; i++)
        {
            if (fis[i].Name.IndexOf("_list_1.jpg") != -1)
            {
                string sku = fis[i].Name.Substring(0, fis[i].Name.Trim().IndexOf("_"));

                MakeThumbnail(fis[i].FullName, part_pathfile + "_t_ebay_flash_1.jpg", 210, 210, "Cut");
                MakeThumbnail(fis[i].FullName, part_pathfile + "_t_ebay_info_1.jpg", 75, 75, "Cut");
                MakeThumbnail(fis[i].FullName, part_pathfile + "_g_ebay_info_1.jpg", 350, 350, "Cut");

            }
        }
        CH.CloseParentWatting(this.btn_generate_photo_for_ebay);
        CH.Alert(KeyFields.SaveIsOK, this.btn_generate_photo_for_ebay);
    }

    /// <summary>
    /// 生成图片给flash 显示使用
    /// </summary>
    /// <param name="sku"></param>
    /// <param name="width"></param>
    void GeneratePhotoForEBayFlashView(string sku, int width)
    {
        string imgSkuString = string.Format("{0}\\{1}_list_1.jpg", part_pathfile, sku);

        if (File.Exists(imgSkuString))
        {
            MakeThumbnail(imgSkuString, part_pathfile + "_t_ebay_flash_1.jpg", 210, 210, "Cut");
        }

    }

    protected void RadioButton_image1_CheckedChanged(object sender, EventArgs e)
    {
        this.RadioButton_image2.Checked = false;
        this.RadioButton_image3.Checked = false;
    }
    protected void RadioButton_image2_CheckedChanged(object sender, EventArgs e)
    {
        this.RadioButton_image1.Checked = false;
        this.RadioButton_image3.Checked = false;

    }
    protected void RadioButton_image3_CheckedChanged(object sender, EventArgs e)
    {
        this.RadioButton_image1.Checked = false;
        this.RadioButton_image2.Checked = false;
    }
    protected void btn_tmp_Click(object sender, EventArgs e)
    {
        if (this.CategoryDropDownLoad1.categoryId > 0)
        {
            DataTable dt = Config.ExecuteDataTable("select product_serial_no,product_img_sum, menu_child_serial_no from tb_product where menu_child_serial_no='" + this.CategoryDropDownLoad1.categoryId.ToString() + "'");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                string path = string.Format("{0}{1}/", part_pathfile_no_shuiying, dr["menu_child_serial_no"].ToString());

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string simall_file = string.Format("{0}{1}_t.jpg", path, dr["product_serial_no"].ToString());
                string source_file = string.Format("{0}{1}_t.jpg", part_pathfile, dr["product_serial_no"].ToString());
                if (!File.Exists(simall_file))
                {
                    if (File.Exists(source_file))
                        File.Copy(source_file, simall_file);
                    int sum;
                    int.TryParse(dr["product_img_sum"].ToString(), out sum);
                    for (int j = 0; j < sum; j++)
                    {
                        string list_file = string.Format("{0}{1}_list_{2}.jpg", path, dr["product_serial_no"].ToString(), j + 1);
                        string big_file = string.Format("{0}{1}_g_{2}.jpg", path, dr["product_serial_no"].ToString(), j + 1);

                        string source_list_file = string.Format("{0}{1}_list_{2}.jpg", part_pathfile, dr["product_serial_no"].ToString(), j + 1);
                        string source_big_file = string.Format("{0}{1}_g_{2}.jpg", part_pathfile, dr["product_serial_no"].ToString(), j + 1);

                        if (!File.Exists(list_file) && File.Exists(source_list_file))
                            File.Move(source_list_file, list_file);
                        if (!File.Exists(big_file) && File.Exists(source_big_file))
                            File.Move(source_big_file, big_file);

                        int x_300;
                        int y_300;
                        int x_418;
                        int y_418;
                        int.TryParse(this.txt_300_x.Text, out x_300);
                        int.TryParse(this.txt_300_y.Text, out y_300);
                        int.TryParse(this.txt_418_x.Text, out x_418);
                        int.TryParse(this.txt_418_y.Text, out y_418);
                        int transparency_v;
                        int.TryParse(this.RadioButtonList_transparency_v.SelectedValue.ToString(), out transparency_v);
                        if (File.Exists(list_file))
                            AddShuiYinPic(list_file, source_list_file, Server.MapPath("~/soft_img/shuyin/2.gif"), x_300, y_300, transparency_v);
                        if (File.Exists(big_file))
                            AddShuiYinPic(big_file, source_big_file, Server.MapPath("~/soft_img/shuyin/2.gif"), x_418, y_418, transparency_v);
                    }
                }
            }
            this.Button1.Text = DateTime.Now.ToString();
        }
    }
    protected void btn_tmp_single_Click(object sender, EventArgs e)
    {
        if (this.txt_sku.Text.Trim().Length < 1)
        {
            CH.Alert("Please input SKU", this.Literal1);
            this.txt_sku.Focus();
            return;
        }
        DataTable dt = Config.ExecuteDataTable("select product_serial_no,product_img_sum, menu_child_serial_no from tb_product where product_serial_no='" + this.txt_sku.Text + "'");
        if (dt.Rows.Count == 1)
        {
            DataRow dr = dt.Rows[0];
            string path = string.Format("{0}{1}/", part_pathfile_no_shuiying, dr["menu_child_serial_no"].ToString());

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string simall_file = string.Format("{0}{1}_t.jpg", path, dr["product_serial_no"].ToString());
            string source_file = string.Format("{0}{1}_t.jpg", part_pathfile, dr["product_serial_no"].ToString());
            if (!File.Exists(simall_file))
            {
                if (File.Exists(source_file))
                    File.Copy(source_file, simall_file);
                int sum;
                int.TryParse(dr["product_img_sum"].ToString(), out sum);
                for (int j = 0; j < sum; j++)
                {
                    string list_file =string.Format("{0}{1}_list_{2}.jpg", path, dr["product_serial_no"].ToString(), j + 1);
                    string big_file = string.Format("{0}{1}_g_{2}.jpg", path, dr["product_serial_no"].ToString(), j + 1);

                    string source_list_file =string.Format("{0}{1}_list_{2}.jpg", part_pathfile, dr["product_serial_no"].ToString(), j + 1);
                    string source_big_file = string.Format("{0}{1}_g_{2}.jpg", part_pathfile, dr["product_serial_no"].ToString(), j + 1);

                    if (!File.Exists(list_file) && File.Exists(source_list_file))
                        File.Move(source_list_file, list_file);
                    if (!File.Exists(big_file) && File.Exists(source_big_file))
                        File.Move(source_big_file, big_file);

                    int x_300;
                    int y_300;
                    int x_418;
                    int y_418;
                    int.TryParse(this.txt_300_x.Text, out x_300);
                    int.TryParse(this.txt_300_y.Text, out y_300);
                    int.TryParse(this.txt_418_x.Text, out x_418);
                    int.TryParse(this.txt_418_y.Text, out y_418);
                    int transparency_v;
                    int.TryParse(this.RadioButtonList_transparency_v.SelectedValue.ToString(), out transparency_v);
                    if (File.Exists(list_file))
                        AddShuiYinPic(list_file, source_list_file, Server.MapPath("~/soft_img/shuyin/2.gif"), x_300, y_300, transparency_v);
                    if (File.Exists(big_file))
                        AddShuiYinPic(big_file, source_big_file, Server.MapPath("~/soft_img/shuyin/2.gif"), x_418, y_418, transparency_v);
                }
            }
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        DataTable dt = Config.ExecuteDataTable("Select sku from tb_tmp_sku");
        foreach (DataRow dr in dt.Rows)
        {
            SaveUpload(int.Parse(dr["sku"].ToString()));
            Config.ExecuteDataTable("delete from tb_tmp_sku where sku='" + dr["sku"].ToString() + "'");
        }
    }
}
