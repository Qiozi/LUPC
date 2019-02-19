using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

/// <summary>
/// Summary description for GetAllValidCategory
/// </summary>
public class GetAllValidCategory
{
	public GetAllValidCategory()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void GenerateAllValidCategory()
    {
        DataTable dt = Config.ExecuteDataTable(@"
update tb_product_category set valid =0;
update tb_product_category set valid=1 where menu_child_serial_no in (


select menu_child_serial_no from (
select menu_child_serial_no, menu_child_name ,menu_child_order,menu_pre_serial_no, page_category from tb_product_category where tag=1 and  menu_pre_serial_no in (
select menu_child_serial_no  from tb_product_category where  tag=1 and  menu_pre_serial_no in (
select menu_child_serial_no  from tb_product_category where menu_parent_serial_no=1 and menu_pre_serial_no =0 and tag=1) and menu_is_exist_sub=1)

union all 
select menu_child_serial_no, menu_child_name ,menu_child_order,menu_pre_serial_no, page_category  from tb_product_category where menu_parent_serial_no=1  and tag=1 and  menu_pre_serial_no in (
select menu_child_serial_no  from tb_product_category where menu_parent_serial_no=1 and menu_pre_serial_no =0 and tag=1) and menu_is_exist_sub=0) tmp order by menu_pre_serial_no asc 
);

select * from (
select menu_child_serial_no, menu_child_name ,menu_child_order,menu_pre_serial_no, page_category from tb_product_category where tag=1 and  menu_pre_serial_no in (
select menu_child_serial_no  from tb_product_category where  tag=1 and  menu_pre_serial_no in (
select menu_child_serial_no  from tb_product_category where menu_parent_serial_no=1 and menu_pre_serial_no =0 and tag=1) and menu_is_exist_sub=1)

union all 
select menu_child_serial_no, menu_child_name ,menu_child_order,menu_pre_serial_no, page_category  from tb_product_category where menu_parent_serial_no=1  and tag=1 and  menu_pre_serial_no in (
select menu_child_serial_no  from tb_product_category where menu_parent_serial_no=1 and menu_pre_serial_no =0 and tag=1) and menu_is_exist_sub=0) tmp order by menu_pre_serial_no asc 

");
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        System.Text.StringBuilder validSB = new System.Text.StringBuilder();

        sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
        sb.AppendLine("<data>");

        validSB.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
        validSB.AppendLine("<data>");
        XmlHelper xh = new XmlHelper();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            sb.AppendLine(string.Format("<row><menu_child_serial_no>{0}</menu_child_serial_no></row>", dt.Rows[i][0].ToString()));

            // 只需要零件与笔记本
            if (dt.Rows[i][4].ToString() == "1")
            {
                validSB.AppendLine(string.Format("<row><menu_child_serial_no>{0}</menu_child_serial_no><menu_child_name>{1}</menu_child_name><menu_pre_serial_no>{2}</menu_pre_serial_no></row>"
                    , dt.Rows[i][0].ToString()
                    , XmlHelper.ChangeString(dt.Rows[i][1].ToString())
                    , dt.Rows[i][3].ToString()));
            }
        }
        sb.AppendLine("</data>");
        validSB.AppendLine("</data>");
   
        System.IO.StreamWriter sw = new System.IO.StreamWriter(System.Web.HttpContext.Current.Server.MapPath("~/q_admin/XmlStore/PartValidCategory.xml"));
        sw.Write(sb.ToString());
        sw.Close();

        sw = new System.IO.StreamWriter(System.Web.HttpContext.Current.Server.MapPath("~/q_admin/XmlStore/PartValidCategoryName.xml"));
        sw.Write(validSB.ToString());
        sw.Close();
        sw.Dispose();

        GenerateCategorySelectedFile(product_category.noebooks);
        GenerateCategorySelectedFile(product_category.system_product);
        GenerateCategorySelectedFile(product_category.system_virtual);
        GenerateCategorySelectedFile(product_category.part_product_virtual);
        GenerateCategorySelectedFile(product_category.parent);
        GenerateCategorySelectedFile(product_category.entityAll);
        GenerateCategorySelectedFile(product_category.AllAll);
    }

    public override string ToString()
    {
        XmlHelper xh = new XmlHelper();
        DataTable dt = xh.GetForXmlFileStore("PartValidCategory.xml");
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            sb.Append("," + dt.Rows[i][0].ToString());
        }
        if (sb.ToString().Length > 2)
        {
            return sb.ToString().Substring(1);
        }
        else
            return "999999";
    }


    public void GenerateCategorySelectedFile(product_category pc)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string filename = "";
        string sql = "";
        switch (pc)
        {

            case product_category.system_product:
                sql = "select menu_child_serial_no, menu_child_name,menu_child_order from tb_product_category where tag=1 and menu_parent_serial_no=1 and menu_pre_serial_no =0 and page_category=0 order by menu_child_order asc";
                filename = "category_selected_sys.asp" ;
                break;
            case product_category.system_virtual:
                sql = "select menu_child_serial_no, menu_child_name,menu_child_order from tb_product_category where tag=1 and menu_parent_serial_no=1 and menu_pre_serial_no =0 and page_category=0 order by menu_child_order asc";
                filename =  "category_selected_sys_virtual.asp";
                break;
            case product_category.part_product_virtual:
                sql = @"select menu_child_serial_no, menu_child_name,menu_child_order from tb_product_category where tag=1 and menu_parent_serial_no=1 and menu_pre_serial_no =0 and page_category=1 order by menu_child_order asc";
                filename = "category_selected_not_sys_virtual.asp" ;
                break;

            case product_category.parent:
                sql = @"select menu_child_serial_no, menu_child_name,menu_child_order from tb_product_category where tag=1 and menu_parent_serial_no=1 and menu_pre_serial_no =0 and page_category=1 order by menu_child_order asc";
                filename = "category_selected_parent.asp";
                break;
            case product_category.entityAll:
                sql = @"select menu_child_serial_no, menu_child_name,menu_child_order from tb_product_category where tag=1 and menu_parent_serial_no=1 and menu_pre_serial_no =0 order by menu_child_order asc";
                filename = "category_selected_all.asp";
                break;
            default:
                sql = @"select menu_child_serial_no, menu_child_name,menu_child_order from tb_product_category where tag=1 and menu_parent_serial_no=1 and menu_pre_serial_no =0 order by menu_child_order asc";

                filename = "category_selected_not_sys.asp";
                break;
            case product_category.AllAll:
                sql = @"select menu_child_serial_no, menu_child_name,menu_child_order from tb_product_category where tag=1 and menu_parent_serial_no=1 and menu_pre_serial_no =0 order by menu_child_order asc";
                filename = "category_selected_allall.asp";
                break;

        }
        DataTable part_dt = Config.ExecuteDataTable(sql);
        sb.Append(string.Format(@"<ul>"));
        for (int i = 0; i < part_dt.Rows.Count; i++)
        {
            DataRow dr = part_dt.Rows[i];
            sb.Append(string.Format(@"<li>{0}", dr["menu_child_name"].ToString()));
            DataTable childDT = Config.ExecuteDataTable(string.Format(@"select menu_child_serial_no, menu_child_name, menu_is_exist_sub, is_virtual from tb_product_category 
where tag=1 and menu_parent_serial_no=1 and menu_pre_serial_no ={0} {1} order by menu_child_order asc"
                , dr["menu_child_serial_no"].ToString()
                , product_category.AllAll == pc ? "" : " and page_category=1"
                ));
            sb.Append("<ul>");
            for (int j = 0; j < childDT.Rows.Count; j++)
            {
                DataRow childDR = childDT.Rows[j];


                //sb.Append(string.Format(@"<li {1}>{0}", childDR["menu_child_name"].ToString(), childDR["menu_is_exist_sub"].ToString() == "1" ? "" : "class='validTitle'"));


                if (childDR["menu_is_exist_sub"].ToString() == "1")
                {
                    if (pc == product_category.parent)
                    {
                        sb.Append(string.Format(@"<li class='validTitle'>" + GetCategoryTitleHref(childDR["menu_child_name"].ToString(), childDR["menu_child_serial_no"].ToString()) + "</li>"));
                    }
                    else
                    {
                        sb.Append(string.Format(@"<li {1}>{0}", childDR["menu_child_name"].ToString(), childDR["menu_is_exist_sub"].ToString() == "1" ? "" : "class='validTitle'"));

                        DataTable subDT = Config.ExecuteDataTable(string.Format(@"select menu_child_serial_no, menu_child_name, menu_is_exist_sub, is_virtual from tb_product_category 
where tag=1 and menu_parent_serial_no=1 and menu_pre_serial_no ={0}  order by menu_child_order asc ", childDR["menu_child_serial_no"].ToString()));
                        sb.Append("<ul>");
                        for (int x = 0; x < subDT.Rows.Count; x++)
                        {
                            DataRow subDR = subDT.Rows[x];
                            if (pc == product_category.part_product_virtual || pc == product_category.system_virtual)
                            {
                                if (subDR["is_virtual"].ToString() == "1")
                                    sb.Append(string.Format(@"<li class='validTitle'>" + GetCategoryTitleHref(subDR["menu_child_name"].ToString(), subDR["menu_child_serial_no"].ToString()) + "</li>"));

                            }
                            else
                            {
                                if (subDR["is_virtual"].ToString() == "0")
                                    sb.Append(string.Format(@"<li class='validTitle'>" + GetCategoryTitleHref(subDR["menu_child_name"].ToString(), subDR["menu_child_serial_no"].ToString()) + "</li>"));
                            }
                        }
                        sb.Append("</ul>");
                    }
                }
                else
                {
                    // 虚类的操作
                    if (pc == product_category.part_product_virtual || pc == product_category.system_virtual)
                    {
                        if (childDR["is_virtual"].ToString() == "1")
                            sb.Append(string.Format(@"<li class='validTitle'>" + GetCategoryTitleHref(childDR["menu_child_name"].ToString(), childDR["menu_child_serial_no"].ToString()) + "</li>"));
                    }
                    else if (pc == product_category.parent)
                    {                       
                          sb.Append(string.Format(@"<li class='validTitle'>" + GetCategoryTitleHref(childDR["menu_child_name"].ToString(), childDR["menu_child_serial_no"].ToString()) + "</li>"));
                    }
                    else
                    {
                        if (childDR["is_virtual"].ToString() == "0" || pc == product_category.AllAll)
                            sb.Append(string.Format(@"<li class='validTitle'>" + GetCategoryTitleHref(childDR["menu_child_name"].ToString(), childDR["menu_child_serial_no"].ToString()) + "</li>"));
                    }
                }
                //sb.Append("</li>");
            }
            sb.Append("</ul>");
            sb.Append("</li>");
        }
        sb.Append(string.Format(@"</ul>"));

        //
        // save to file.
        // 
        System.IO.StreamWriter sw = new System.IO.StreamWriter(System.Web.HttpContext.Current.Server.MapPath(string.Format("~/q_admin/asp/{0}", filename)));
        sw.Write(string.Format(@"<html>
<head>
    <title>Category Select</title>
</head>
<style>
.validTitle{{color: #8B8BD1;}}
body {{ font-size: 8.5pt;}}
ul, li {{ margin-top: 0px; margin-bottom: 0px;}}
a {{ display: block; padding: 2px;}}
a:hover {{ display:block;padding: 2px; background: blue; color: white;}}
</style>
<body>
    {0}
</body>
</html>
", sb.ToString()));
        sw.Close();

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="title"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public string GetCategoryTitleHref(string title, string id)
    {

        return string.Format(@"<a href='' onclick=""parent.document.getElementById('{0}').value = '{2}';parent.document.getElementById('{1}').value = '{3}';parent.document.getElementById('{4}').style.display='None';return false;"">{3}</a>
"
            , "<%= Request.QueryString(\"id\") %>"
            , "<%= Request.QueryString(\"textid\") %>"
            , id
            , title
            , "<%= Request.QueryString(\"div_id\") %>"
            );
    }
}
