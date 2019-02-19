// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	4/27/2007 10:41:39 AM
//
// // // // // // // // // // // // // // // //
using LU.Data;
using System;
using System.Collections;
using System.Data;
using System.Linq;

[Serializable]
public class ProductCategoryModel
{

    public ProductCategoryModel()
    {

    }

    public static tb_product_category GetProductCategoryModel(nicklu2Entities context, int _menu_child_serial_no)
    {
        var models = context.tb_product_category.SingleOrDefault(me => me.menu_child_serial_no.Equals(_menu_child_serial_no));// "menu_child_serial_no", _menu_child_serial_no);
        return models;
    }


    /// <summary>
    /// 根据父类显示子类产品类别
    /// </summary>
    /// <param name="menu_pre_serial_no">父类ID</param>
    /// <returns></returns>
    public static tb_product_category[] ProductCategoryModelsByMenuPreSerialNo(LU.Data.nicklu2Entities context, int menu_pre_serial_no)
    {
        //NHibernate.Expression.EqExpression eq1 = new NHibernate.Expression.EqExpression("menu_parent_serial_no", 1);
        //NHibernate.Expression.EqExpression eq2 = new NHibernate.Expression.EqExpression("menu_pre_serial_no", menu_pre_serial_no);
        //NHibernate.Expression.AndExpression a = new NHibernate.Expression.AndExpression(eq1, eq2);
        //return ProductCategoryModel.FindAll(CurrentOrder(true),a);

        var query = context.tb_product_category.Where(me => me.menu_parent_serial_no.Value.Equals(1) && me.menu_pre_serial_no.Value.Equals(menu_pre_serial_no)).ToList();
        return query.ToArray();
    }

    /// <summary>
    /// 根据父类显示子类产品类别
    /// </summary>
    /// <param name="menu_pre_serial_no">父类ID</param>
    /// <returns></returns>
    public static tb_product_category[] ProductCategoryModelsByMenuPreSerialNo(nicklu2Entities context, int menu_pre_serial_no, bool tag, int page_category)
    {
        //NHibernate.Expression.EqExpression eq1 = new NHibernate.Expression.EqExpression("menu_parent_serial_no", 1);
        //NHibernate.Expression.EqExpression eq2 = new NHibernate.Expression.EqExpression("menu_pre_serial_no", menu_pre_serial_no);
        //NHibernate.Expression.EqExpression eq3 = new NHibernate.Expression.EqExpression("tag", tag == true ? byte.Parse("1") : byte.Parse("0"));
        //NHibernate.Expression.AndExpression a = new NHibernate.Expression.AndExpression(eq1, eq2);
        //NHibernate.Expression.AndExpression a2 = new NHibernate.Expression.AndExpression(a, eq3);
        //NHibernate.Expression.EqExpression eq4 = new NHibernate.Expression.EqExpression("page_category", page_category);
        //NHibernate.Expression.AndExpression a3 = new NHibernate.Expression.AndExpression(a2, eq4);
        //if (page_category != -1)
        //{
        //    return ProductCategoryModel.FindAll(CurrentOrder(true), a3);
        //}
        //return ProductCategoryModel.FindAll(CurrentOrder(true), a2);
        var tagValue = tag ? sbyte.Parse("1") : sbyte.Parse("0");
        var query = context.tb_product_category.Where(me => me.menu_parent_serial_no.Value.Equals(1) &&
                                                            me.menu_pre_serial_no.Value.Equals(menu_pre_serial_no) &&
                                                            me.tag.Value.Equals(tagValue) &&
                                                            me.page_category.Value.Equals(page_category)).ToList();
        return query.ToArray();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="menu_pre_serial_no"></param>
    /// <param name="tag"></param>
    /// <returns></returns>
    public static tb_product_category[] ProductCategoryModelsByMenuPreSerialNo(nicklu2Entities context, int menu_pre_serial_no, bool tag)
    {
        return ProductCategoryModelsByMenuPreSerialNo(context, menu_pre_serial_no, tag, -1);
    }

    /// <summary>
    /// 显示父类
    /// </summary>
    /// <param name="menu_pre_serial_no"></param>
    /// <returns></returns>
    public static tb_product_category[] ProductCategoryModelsParent(nicklu2Entities context)
    {
        return ProductCategoryModelsByMenuPreSerialNo(context, 0, true);
    }
    /// <summary>
    /// search first level
    /// </summary>
    /// <param name="is_part"></param>
    /// <param name="show_it"></param>
    /// <returns></returns>
    public static tb_product_category[] FindModelsByIsSystem(nicklu2Entities context, bool is_part, bool show_it, int menu_pre_serial_no)
    {
        return ProductCategoryModelsByMenuPreSerialNo(context, menu_pre_serial_no, show_it, (is_part == true ? 1 : 0));
        // return Config.ExecuteDataTable("Select * from tb_product_category where page_category=" + (is_part == true ? "1" : "0") + " and tag=" + (show_it == true ? "1" : "0") + " and menu_pre_serial_no=" + menu_pre_serial_no);
    }

    /// <summary>
    /// 显示父类
    /// </summary>
    /// <param name="menu_pre_serial_no"></param>
    /// <returns></returns>
    public static tb_product_category[] ProductCategoryModelsParentAll(nicklu2Entities context)
    {
        return ProductCategoryModelsByMenuPreSerialNo(context, 0);
    }

    //public static NHibernate.Expression.Order[] CurrentOrder(bool order)
    //{
    //    NHibernate.Expression.Order o1 = new NHibernate.Expression.Order("menu_child_order", order);
    //    NHibernate.Expression.Order o = new NHibernate.Expression.Order("menu_child_serial_no", order);
    //    NHibernate.Expression.Order[] oo = new NHibernate.Expression.Order[] { o1, o };
    //    return oo;
    //}


    public static tb_product_category[] ProductCategoryModelsByParts(nicklu2Entities context, bool order)
    {
        //return ProductCategoryModelsByMenuPreSerialNo(0);
        //NHibernate.Expression.EqExpression eq2 = new NHibernate.Expression.EqExpression("menu_pre_serial_no", 0);
        //NHibernate.Expression.EqExpression e = new NHibernate.Expression.EqExpression("page_category", 1);
        //NHibernate.Expression.EqExpression eq1 = new NHibernate.Expression.EqExpression("menu_parent_serial_no", 1);
        //NHibernate.Expression.AndExpression a = new NHibernate.Expression.AndExpression(e, eq2);
        //NHibernate.Expression.AndExpression a2 = new NHibernate.Expression.AndExpression(a, eq1);
        //return ProductCategoryModel.FindAll(CurrentOrder(order), a2);

        var query = context.tb_product_category.Where(me => me.menu_pre_serial_no.Value.Equals(0) &&
                                                            me.page_category.Value.Equals(1) &&
                                                            me.menu_parent_serial_no.Value.Equals(1))
                                               .OrderBy(me => me.menu_child_serial_no)
                                               .OrderBy(me => me.menu_child_order)
                                               .ToList()
                                               .ToArray();
        return query;
    }


    /// <summary>
    /// 所有产品类别
    /// </summary>
    /// <returns></returns>
    public static tb_product_category[] ProductCategoryModels(nicklu2Entities context)
    {
        return context.tb_product_category.Where(me => me.menu_parent_serial_no.Value.Equals(1)).ToList().ToArray();
    }
    /// <summary>
    /// 显示是否可以展示的显产品类别
    /// </summary>
    /// <param name="showit"></param>
    /// <returns></returns>
    //public static ProductCategoryModel[] ProductCategoryModelsByShowIt(bool showit)
    //{
    //    return ProductCategoryModel.FindAll();
    //}


    public static DataTable ProductCategoryTreeByOrder()
    {
        string sql = "Select menu_child_serial_no, menu_child_name ,menu_is_exist_sub from tb_product_category where tag=1 and menu_parent_serial_no=1 and page_category=1";
        string order = " order by menu_child_order asc";
        DataTable category = new DataTable();
        category.Columns.Add("menu_child_serial_no");
        category.Columns.Add("menu_child_name");
        DataTable parent = Config.ExecuteDataTable(sql + order);
        for (int i = 0; i < parent.Rows.Count; i++)
        {
            DataRow dr = parent.Rows[i];
            DataRowToDataTable(dr, category, 0);
            if (dr["menu_is_exist_sub"].ToString() == "1")
            {
                DataTable child = Config.ExecuteDataTable(sql + " and menu_pre_serial_no=" + dr["menu_child_serial_no"] + order);
                for (int j = 0; j < child.Rows.Count; j++)
                {
                    DataRow chDR = child.Rows[j];
                    DataRowToDataTable(chDR, category, 4);
                    if (chDR["menu_is_exist_sub"].ToString() == "1")
                    {
                        DataTable subDT = Config.ExecuteDataTable(sql + " and menu_pre_serial_no=" + dr["menu_child_serial_no"] + order);
                        for (int n = 0; n < subDT.Rows.Count; n++)
                        {
                            DataRow subDR = subDT.Rows[j];
                            DataRowToDataTable(subDR, category, 8);
                        }
                    }
                }
            }
        }
        return category;
    }

    public static void DataRowToDataTable(DataRow dr, DataTable dt, int bankcount)
    {
        DataRow d = dt.NewRow();
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            if (dt.Columns[i].ColumnName == "menu_child_name")
                d[i] = getbank(bankcount) + dr[i].ToString();
            else
                d[i] = dr[i].ToString();

        }
        dt.Rows.Add(d);
    }

    public static ArrayList GetAllPartCategory(nicklu2Entities context)
    {
        //string sql = "select menu_child_name, menu_child_serial_no from tb_product_category where tag=1 and menu_is_exist_sub=0 and page_category=1 and menu_parent_serial_no=1  order by menu_child_order asc";
        tb_product_category[] ms = ProductCategoryModelsByParts(context, true);

        ArrayList al = new ArrayList();
        for (int i = 0; i < ms.Length; i++)
        {
            tb_product_category[] pcms = ProductCategoryModel.ProductCategoryModelsByMenuPreSerialNo(context, ms[i].menu_child_serial_no);
            for (int j = 0; j < pcms.Length; j++)
            {
                if (pcms[j].tag == 1)
                    al.Add(pcms[j]);
            }
        }

        return al;
    }

    public static string getbank(int count)
    {
        string s = "";
        for (int i = 0; i < count; i++)
        {
            s += "..";
        }
        return s;
    }

    public static DataTable FindCategoryNoParent(nicklu2Entities context)
    {
        DataTable dt = Config.ExecuteDataTable("select menu_child_name, menu_child_serial_no,menu_pre_serial_no from tb_product_category where tag=1 and menu_is_exist_sub=0 and menu_pre_serial_no > 0 order by menu_child_order asc");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            int _menu_child = int.Parse(dt.Rows[i]["menu_pre_serial_no"].ToString());
            var m = GetProductCategoryModel(context, _menu_child);
            dt.Rows[i]["menu_child_name"] = ChangeName(dt.Rows[i]["menu_child_serial_no"].ToString(), 4) + ChangeName(m.menu_child_name, 25) + dt.Rows[i]["menu_child_name"].ToString();
        }
        DataRow dr = dt.NewRow();
        dr["menu_child_serial_no"] = -1;
        dr["menu_child_name"] = "select";
        dt.Rows.InsertAt(dr, 0);
        return dt;
    }

    public static string ChangeName(string name, int length)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        int count = length - name.Length;
        sb.Append(name);
        if (count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                sb.Append("=");
            }
        }
        else
            sb.Append("=");
        return sb.ToString();
    }
    /// <summary>
    /// 查找带最后一级产品类别
    /// </summary>
    /// <returns></returns>
    public static DataTable FindCategoryLowLevel()
    {
        DataTable dt = Config.ExecuteDataTable("select menu_child_serial_no, menu_child_name name, (select count(p.product_serial_no) from tb_product p where p.menu_child_serial_no=pc.menu_child_serial_no) ready from tb_product_category pc where page_category=1 and tag=1 and menu_is_exist_sub=0 and menu_parent_serial_no=1 order by menu_child_order asc ");
        return dt;
    }

    public static DataTable FindCategoryFilterName(nicklu2Entities context)
    {
        DataTable dt = FindCategoryLowLevel();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string name = dt.Rows[i]["name"].ToString();
            GetParentName(context, int.Parse(dt.Rows[i]["menu_child_serial_no"].ToString()), ref name);
            dt.Rows[i]["name"] = name;
        }
        return dt;
    }

    public static void GetParentName(nicklu2Entities context, int categoryID, ref string name)
    {
        var pcm = ProductCategoryModel.GetProductCategoryModel(context, categoryID);
        name = pcm.menu_child_name + "-->" + name;
        if (pcm.menu_pre_serial_no > 0)
        {
            GetParentName(context, pcm.menu_pre_serial_no.Value, ref name);
        }
    }



    public static DataTable FindCategoryLastLevel()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("id");
        dt.Columns.Add("short_name");
        dt.Columns.Add("name");
        dt.Columns.Add("ready");
        dt.Columns.Add("export");

        // 1 level
        DataTable dt1 = Config.ExecuteDataTable("select menu_child_serial_no, menu_child_name,menu_is_exist_sub, menu_pre_serial_no from tb_product_category where tag=1 and menu_parent_serial_no=1 and menu_pre_serial_no=0 and page_category=1 order by menu_child_order asc ");
        for (int i = 0; i < dt1.Rows.Count; i++)
        {
            if (dt1.Rows[i]["menu_is_exist_sub"].ToString() == "1")
            {
                int menu_pre_id = int.Parse(dt1.Rows[i]["menu_child_serial_no"].ToString());
                DataTable dt2 = Config.ExecuteDataTable("select menu_child_serial_no,export, menu_child_name,menu_is_exist_sub, menu_pre_serial_no from tb_product_category where tag=1 and menu_parent_serial_no=1 and menu_pre_serial_no='" + menu_pre_id + "' and page_category=1 order by menu_child_order asc ");
                for (int j = 0; j < dt2.Rows.Count; j++)
                {
                    if (dt2.Rows[j]["menu_is_exist_sub"].ToString() == "1")
                    {
                        int sub_menu_pre_id = int.Parse(dt2.Rows[j]["menu_child_serial_no"].ToString());
                        DataTable dt3 = Config.ExecuteDataTable("select menu_child_serial_no, export,menu_child_name,menu_is_exist_sub, menu_pre_serial_no from tb_product_category where tag=1 and menu_parent_serial_no=1 and menu_pre_serial_no='" + sub_menu_pre_id + "' and page_category=1 order by menu_child_order asc ");
                        for (int x = 0; x < dt3.Rows.Count; x++)
                        {
                            DataRow dr = dt.NewRow();
                            dr["id"] = dt3.Rows[x]["menu_child_serial_no"].ToString();
                            dr["name"] = dt1.Rows[i]["menu_child_name"].ToString() + "-->" + dt2.Rows[j]["menu_child_name"].ToString() + "-->" + dt3.Rows[x]["menu_child_name"].ToString();
                            dr["ready"] = ProductModel.FindPartSumByCategory(int.Parse(dt3.Rows[x]["menu_child_serial_no"].ToString()));
                            dr["short_name"] = dt3.Rows[x]["menu_child_name"].ToString();
                            dr["export"] = dt3.Rows[x]["export"].ToString();

                            dt.Rows.Add(dr);
                        }
                    }
                    else
                    {
                        DataRow dr = dt.NewRow();
                        dr["id"] = dt2.Rows[j]["menu_child_serial_no"].ToString();
                        dr["name"] = dt1.Rows[i]["menu_child_name"].ToString() + "-->" + dt2.Rows[j]["menu_child_name"].ToString();
                        dr["ready"] = ProductModel.FindPartSumByCategory(int.Parse(dt2.Rows[j]["menu_child_serial_no"].ToString()));
                        dr["short_name"] = dt2.Rows[j]["menu_child_name"].ToString();
                        dr["export"] = dt2.Rows[j]["export"].ToString();
                        dt.Rows.Add(dr);
                    }
                }
            }
        }
        return dt;
    }

    /// <summary>
    /// 虚拟目录
    /// </summary>
    /// <param name="category_id"></param>
    /// <returns></returns>
    public DataTable FindVirtualCategoryByCategoryID(int category_id)
    {
        return Config.ExecuteDataTable(string.Format(@"select menu_child_serial_no, menu_child_name from tb_product_category where is_virtual=1 and menu_pre_serial_no in 
(select menu_pre_serial_no from  tb_product_category pc where pc.menu_child_serial_no={0})", category_id));
    }

    /// <summary>
    /// 按ebay category id, 取得相对应的web category id.
    /// </summary>
    /// <param name="eBayCategoryID"></param>
    /// <returns></returns>
    public static int GetCategoryID(nicklu2Entities context, string eBayCategoryID)
    {
        //NHibernate.Expression.EqExpression eq1 = new NHibernate.Expression.EqExpression("eBayCategoryID", eBayCategoryID);
        //ProductCategoryModel[] list = ProductCategoryModel.FindAll(eq1);
        //if (list != null)
        //    if (list.Length > 0)
        //        return list[0].menu_child_serial_no;

        //return 0;
        var query = context.tb_product_category.FirstOrDefault(me => me.eBayCategoryID.Equals(eBayCategoryID));

        return query == null
                 ? 0
                 : query.menu_child_serial_no;

    }

    /// <summary>
    /// 是否是笔记本类
    /// </summary>
    /// <param name="CategoryID"></param>
    /// <returns></returns>
    public static bool IsNotebook(nicklu2Entities context, int CategoryID)
    {
        var pcm = GetProductCategoryModel(context, CategoryID);
        if (pcm != null)
        {
            if (pcm.is_noebook.HasValue)
                return pcm.is_noebook.Value == 1;
        }

        return false;
    }


}
