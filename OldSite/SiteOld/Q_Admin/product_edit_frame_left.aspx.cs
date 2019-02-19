using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;

public partial class Q_Admin_product_edit_frame_left : PageBase
{
    ProductModel PM = new ProductModel();
    List<int[]> storeSum = new List<int[]>();
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
        BindTreeView();
    }

    #region methods
    private void BindTreeView()
    {
        ProductCategoryModel[] mms = ProductCategoryModel.ProductCategoryModelsByMenuPreSerialNo(0, true, 1);
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<ul>");
        for (int i = 0; i < mms.Length; i++)
        {
            sb.Append(string.Format("<li><h2>{0}</h2>", mms[i].menu_child_name));

            ProductCategoryModel[] childMMS = ProductCategoryModel.ProductCategoryModelsByMenuPreSerialNo(mms[i].menu_child_serial_no, true);
            if (mms[i].menu_is_exist_sub == 1)
            {
                sb.Append(string.Format("<ul>"));
                for (int j = 0; j < childMMS.Length; j++)
                {

                    if (childMMS[j].menu_is_exist_sub == 1)
                    {
                        string subid = string.Format("sub_{0}", childMMS[j].menu_child_serial_no);

                        sb.Append(string.Format("<li><img src='/images/arrow.gif' alt='', title='' align='absmiddle' /><a href=\"\" onclick='ViewElement(\"" + subid + "\"); return false;'>{0}</a>", childMMS[j].menu_child_name));
                        //sb.Append(AddMenuFunction(childMMS[j].menu_child_serial_no, false));
                        sb.Append("<ul style='display:none' id='" + subid + "'>");
                        ProductCategoryModel[] subMMS = ProductCategoryModel.ProductCategoryModelsByMenuPreSerialNo(childMMS[j].menu_child_serial_no, true);
                        for (int x = 0; x < subMMS.Length; x++)
                        {
                            subid = string.Format("sub_{0}", subMMS[x].menu_child_serial_no);
                            int mSum = PM.FindExaminePriceSum(subMMS[x].menu_child_serial_no);
                            int[] member = new int[] { subMMS[x].menu_child_serial_no, mSum };
                            storeSum.Add(member);
                            sb.Append("<li>");
                            sb.Append(string.Format("<a href='' class='fun2' onclick='ViewElement(\"" + subid + "\"); return false;'>{0}<b>({1})</b></a>", subMMS[x].menu_child_name, mSum));
                            sb.Append(AddMenuFunction(subMMS[x], false));
                            sb.Append("</li>");
                        }
                        sb.Append("</ul>");
                        sb.Append("</li>");
                    }
                    else
                    {
                        string subid = string.Format("sub_{0}", childMMS[j].menu_child_serial_no);
                        int mSum = PM.FindExaminePriceSum(childMMS[j].menu_child_serial_no);
                        int[] member = new int[] { childMMS[j].menu_child_serial_no, mSum };
                        storeSum.Add(member);
                        sb.Append(string.Format("<li><img src='/images/arrow.gif' alt='', title='' align='absmiddle' /><a href=\"\" onclick='ViewElement(\"" + subid + "\"); return false;'>{0}<b>({1})</b></a>",
                            childMMS[j].menu_child_name, mSum));
                        sb.Append(AddMenuFunction(childMMS[j], false));
                        sb.Append("</li>");
                    }

                }
                sb.Append(string.Format("</ul>"));
            }
            sb.Append(string.Format("</li>"));
        }
        sb.Append("</ul>");
        this.literal_menu.Text = sb.ToString();
    }

    private string AddMenuFunction(ProductCategoryModel pcm, bool view)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        if (!view)
        {
            sb.Append("<ul id='sub_" + pcm.menu_child_serial_no.ToString() + "' " + (view ? "" : " style='display:none' ") + ">");
        }
        else
        {
            sb.Append("<ul >");
        }
        int result = 0;
        for (int i = 0; i < storeSum.Count; i++)
        {
            int[] member = storeSum[i];
            if (member[0] == pcm.menu_child_serial_no)
            {
                result = member[1];
            }
        }

        if (pcm.page_category == 0)
        {
            if (!pcm.is_virtual)
            {
                sb.Append(string.Format("<li><img src='images/node.gif' alt='', title='' align='absmiddle' style='width: 6px'/><a href='product_helper_system_view_info.aspx?categoryID={0}' class='fun1' target='iframe1' onclick='sAlert(\"功能未完成\"); return false;'>view info</a></li>", pcm.menu_child_serial_no));
            }
            else
                sb.Append(string.Format("<li><img src='images/node.gif' alt='', title='' align='absmiddle' style='width: 6px'/><a href='product_helper_system_view_info.aspx?categoryID={0}' class='fun1' onclick=\"JSAParent(this);return false;\">Update info</a></li>", pcm.menu_child_serial_no));
        }
        else
        {
            if (pcm.is_virtual)
            {
                sb.Append(string.Format("<li><img src='images/node.gif' alt='', title='' align='absmiddle' style='width: 6px'/><a href='product_helper_virtual_update_info.aspx?categoryID={0}' class='fun1'  onclick=\"JSAParent(this);return false;\">Update Info</a></li>", pcm.menu_child_serial_no));
            }
            else
            {
                sb.Append(string.Format("<li><img src='images/node.gif' alt='', title='' align='absmiddle' style='width: 6px'/><a href='product_helper_part_view_info.aspx?categoryID={0}' class='fun1'  onclick=\"JSAParent(this);return false;\">view info</a></li>", pcm.menu_child_serial_no));
                sb.Append(string.Format("<li><img src='images/node.gif' alt='', title='' align='absmiddle' style='width: 6px'/><a href='/part_showit_manage.aspx?categoryID={0}' class='fun1'  onclick=\"JSAParent(this);return false;\">modify info</a></li>", pcm.menu_child_serial_no));
                // sb.Append(string.Format("<li><img src='images/node.gif' alt='', title='' align='absmiddle' style='width: 6px'/><a href='product_helper_category_coefficient.aspx?categoryID={0}' target='iframe1' class='fun1'>modify coefficient</a></li>", menu_child_serial_no));
                sb.Append(string.Format("<li><img src='images/node.gif' alt='', title='' align='absmiddle' style='width: 6px'/><a href='product_helper_part_import_export.aspx?categoryID={0}'  onclick=\"JSAParent(this);return false;\" class='fun1'>import/export</a></li>", pcm.menu_child_serial_no));
                sb.Append(string.Format("<li><img src='images/node.gif' alt='', title='' align='absmiddle' style='width: 6px'/><a href='product_helper_view_sort.aspx?categoryID={0}'  onclick=\"JSAParent(this);return false;\" class='fun1'>Sort<b>({1})</b></a></li>", pcm.menu_child_serial_no, result));
                sb.Append(string.Format("<li><img src='images/node.gif' alt='', title='' align='absmiddle' style='width: 6px'/><a href='product_helper_match_sku_2.aspx?categoryID={0}'  onclick=\"JSAParent(this);return false;\" class='fun1'>Match SKU</a></li>", pcm.menu_child_serial_no));
                sb.Append(string.Format("<li><img src='images/node.gif' alt='', title='' align='absmiddle' style='width: 6px'/><a href='product_helper_price_compare.aspx?categoryID={0}'  onclick=\"JSAParent(this);return false;\" class='fun1' >price compare</a></li>", pcm.menu_child_serial_no));
                //sb.Append("<li><img src='images/node.gif' alt='', title='' align='absmiddle' style='width: 6px'/><a  class='fun1' onclick='sAlert(\"功能未完成\"); return false;'>modify group</a></li>"); 
            }
        }
        sb.Append("</ul>");
        return sb.ToString();
    }

    #endregion
}
