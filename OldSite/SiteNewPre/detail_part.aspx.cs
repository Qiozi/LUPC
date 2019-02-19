using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;

public partial class detail_part : PageBase
{
    public int ImgSku = 0;
    public string Manufacturer = string.Empty;
    public string ManufacturerCode = string.Empty;
    public int CateID = 0;
    public string AllQtyCateView = string.Empty;
    public string PartTitle = string.Empty;
    public string LogoGallery = string.Empty;
    public string LogoGallerySumHtml = string.Empty;
    public string SuggestString = string.Empty; // 推荐
    public string PartSpecificationString = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var prod = db.tb_product.FirstOrDefault(p => p.product_serial_no.Equals(ReqSKU));
            if (prod != null)
            {
                metaKeyword.Text = string.Format(@" 
    <meta name=""description"" content=""{0}"" />
    <meta name=""keywords"" content=""LU Computer & Part Specifications & Computer Specifications & {0}"" />
    <meta name=""rebots"" content=""all"">
    <title>{0} - LU Computer</title>"
                    , string.IsNullOrEmpty(prod.product_ebay_name) ? prod.product_name : prod.product_ebay_name);

                CateID = prod.menu_child_serial_no.Value;

                #region categories

                var cateModel = db.tb_product_category.FirstOrDefault(p => p.menu_child_serial_no.Equals(CateID));
                if (cateModel != null)
                {
                    var preId = cateModel.menu_pre_serial_no.Value;
                    var subCateModel = db.tb_product_category.FirstOrDefault(p => p.menu_child_serial_no.Equals(preId));
                    if (subCateModel != null)
                    {
                        ltCateNameParent.Text = subCateModel.menu_child_name + "";// +cateModel.menu_child_name;
                        ltCateName.Text = cateModel.menu_child_name;

                        #region cate list
                        var parentCateList = db.tb_product_category.Where(p => p.menu_pre_serial_no.HasValue
                            && p.menu_pre_serial_no.Value.Equals(0)
                            && p.tag.HasValue
                            && p.tag.Value.Equals(1)
                            ).OrderBy(p => p.menu_child_order).ToList();
                        for (int i = 0; i < parentCateList.Count; i++)
                        {
                            //ltCatesParent.Text += "<li>" + parentCateList[i].menu_child_name + "</li>";
                        }

                        var cateList = db.tb_product_category.Where(p => p.menu_pre_serial_no.HasValue
                            && p.menu_pre_serial_no.Value.Equals(preId)
                            && p.tag.HasValue
                            && p.tag.Value.Equals(1)
                            ).OrderBy(p => p.menu_child_order).ToList();
                        for (int i = 0; i < cateList.Count; i++)
                        {
                            if (cateList[i].menu_child_serial_no == 378)
                            {
                                continue;
                            }
                            if (cateList[i].page_category.Value == 0)
                            {
                                ltCates.Text += "<li role='pressntation'><a href='/list_sys.aspx?cid=" + cateList[i].menu_child_serial_no + "'>" + cateList[i].menu_child_name + "</a></li>";
                            }
                            else
                            {
                                ltCates.Text += "<li role='pressntation'><a href='/list_part.aspx?cid=" + cateList[i].menu_child_serial_no + "'>" + cateList[i].menu_child_name + "</a></li>";
                            }
                        }
                        #endregion
                    }
                }
                #endregion

                PartTitle = string.IsNullOrEmpty(prod.product_ebay_name) ? prod.product_name : prod.product_ebay_name;
                this.Title = string.IsNullOrEmpty(prod.manufacturer_part_number) ? PartTitle : string.Concat(prod.manufacturer_part_number, "-", PartTitle);
                ImgSku = !prod.other_product_sku.HasValue ? prod.product_serial_no : (prod.other_product_sku.Value > 0 ? prod.other_product_sku.Value : prod.product_serial_no);
                Manufacturer = prod.producter_serial_no;
                ManufacturerCode = prod.manufacturer_part_number;
                LogoGallery = GetLogoGallery(ImgSku, prod.product_img_sum.Value, Manufacturer, PartTitle);
                LogoGallerySumHtml = prod.product_img_sum > 1 ? "<div class='text-center'><a onclick=\"$('#someBigImage').ekkoLightbox();\" style='cursor:pointer;'>Gallery</a></div>" : "";
                SuggestString = GetSuggests(prod.menu_child_serial_no.Value);


                if (ReqShowSpecificationHtml)
                {
                    string filename = "C:\\Workspaces\\Web\\Part_Comment\\" + ReqSKU + "_comment.html";
                    if (File.Exists(filename))
                    {
                        string cont = File.ReadAllText(filename);
                        if (cont.IndexOf("width=\"565\"") > 1)
                            cont = cont.Replace("width=\"565\"", "width='100%'");
                        if (cont.IndexOf("width=\"575\"") > 1)
                            cont = cont.Replace("width=\"575\"", "width='100%'");
                        PartSpecificationString = cont;
                    }
                }
                else
                {
                    PartSpecificationString = @"<iframe name=""iframePartSpecification"" id=""iframePartSpecification"" src=""/detail_part_specification.aspx?sku=" + ReqSKU + @"""
                        width=""100%"" style=""height: 0px; border: 0px;"" frameborder=""0""></iframe>";
                }
            }
        }
    }

    bool ReqShowSpecificationHtml
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "specifType", 0) == 1; }
    }

    string GetSuggests(int cid)
    {
        var filename = Server.MapPath(string.Format("/Computer/suggest-{0}.txt", cid));
        if (File.Exists(filename))
        {
            var content = File.ReadAllText(filename);
            return content;
        }
        return string.Empty;
        //        return string .Format(@"    <div>
        //                <div>
        //                    <h4>May We Also Suggest</h4>
        //                    <div class=""suggestList"">
        //                        <table width=""230"">
        //                            <tr>
        //                                <td>
        //                                    <img src=""https://o9ozc36tl.qnssl.com/30606.jpg?imageView/3/w/150/h/150"" alt=""Lenovo IdeaPad Y700 Notebook i7-6700HQ 8GB RAM DDR4 256GB SSD Win10 80NV0029US"" /></td>
        //                            </tr>
        //                            <tr>
        //                                <td>
        //                                    <label>
        //                                        Lenovo IdeaPad Y700 Notebook i7-6700HQ 8GB RAM DDR4 256GB SSD Win10 80NV0029US
        //                                    </label>
        //                                </td>
        //                            </tr>
        //                            <tr>
        //                                <td>
        //                                    <label class=""price"">Price: $10008</label>
        //                                </td>
        //                            </tr>
        //                        </table>
        //                    </div>
        //                </div>
        //
        //            </div>");
    }

    string GetLogoGallery(int imgsku, int qty, string mfp, string title)
    {
        var result = string.Empty;
        for (int i = 0; i < qty; i++)
        {
            var imgUrl = WebClientHelper.Get(imgsku, 420, 420, 0);
            var immUrl2 = WebClientHelper.Get(imgsku, 600, 600, i);
            result += string.Format(@" <a href=""" + immUrl2 + @"""
                            data-toggle=""lightbox""
                            data-lightbox=""{1}""
                            data-title=""{2}""
                            data-gallery=""{1}""
                            data-footer=""{4}/{5}""
                            {3}
                            onclick=""$(this).ekkoLightbox();return false;"">
                            <img src=""" + imgUrl + @""" width=""420""
                                border='1' data-img=""{0}""/>
                        </a>"
                , imgsku
                , mfp
                , title
                , i == 0 ? "id=\"someBigImage\"" : " class=\"hide\""
                , i + 1
                , qty);

        }
        return result;
    }

}