using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace LUComputers
{
    public partial class ConvertGallery : Form
    {

        string source_path = "C:\\Wu.TH.China\\lucomputers.web\\pro_img\\ebay_gallery\\";
       
        public ConvertGallery()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        DirectoryInfo dir = new DirectoryInfo(source_path);
        FileInfo[] fis = dir.GetFiles();

        //for (int i = 0; i < fis.Length; i++)
        //{
        //    //if (fis[i].Name.IndexOf("ebay_list") == -1)
        //    {
        //        fis[i].Delete();
        //    }
        //}

        //dir = new DirectoryInfo(Server.MapPath(sorce_path));
        //fis = dir.GetFiles();



        for (int i = 0; i < fis.Length; i++)
        {
            string fullname1 = "";
            string fullname2 = "";
            if (fis[i].Name.IndexOf("_list") != -1)
            {
                //fis[i].CopyTo(Server.MapPath(string.Format("{0}{1}", to_path, fis[i].Name)), true);
                string sku = fis[i].Name.Substring(0, fis[i].Name.Trim().IndexOf("_"));

                fullname1 = GenerateFolderEbayGallery(fis[i].Name.Replace("_list", "_ebay_list_t"), source_path);
                fullname2 = GenerateFolderEbayGallery(fis[i].Name.Replace("_list", "_ebay_list_g"), source_path);
                MakeThumbnail(fis[i].FullName, fullname1, 135, 135, "Cut");
                MakeThumbnail(fis[i].FullName, fullname2, 350, 350, "Cut");

                this.richTextBox1.Text += fis[i].Name + "\r\n";
                this.richTextBox1.ScrollToCaret();
                this.richTextBox1.Update();
            }

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

                bitmap.Dispose();
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                //originalImage.Dispose();
                //bitmap.Dispose();
                //g.Dispose();
            }
        }

    }
}
