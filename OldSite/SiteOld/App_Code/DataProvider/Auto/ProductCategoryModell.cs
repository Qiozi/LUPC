// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	4/27/2007 10:41:39 AM
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Collections;
using System.Data;

[ActiveRecord("tb_product_category")]
[Serializable]
public class ProductCategoryModel : ActiveRecordBase<ProductCategoryModel>
{
    int _menu_child_serial_no;
    string _menu_child_name;
    string _menu_child_href;
    byte _menu_is_exist_sub;
    int _menu_parent_serial_no;
    int _menu_pre_serial_no;
    byte _tag;
    int _menu_child_order;
    int _old_db_id;
    string _menu_child_name_f;
    int _page_category;
    byte _is_noebook;
    bool _export;
    bool _is_display_stock;
    bool _is_virtual = false;
    string _keywords_cates;
    bool _is_view_menu;
    string _eBayCategoryID;
    string _ebayStoreCategoryID_1;
    string _eBayStoreCategoryID_2;

    public ProductCategoryModel()
    {

    }


    /// <summary>
    /// 
    /// </summary>
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int menu_child_serial_no
    {
        get { return _menu_child_serial_no; }
        set { _menu_child_serial_no = value; }
    }
    public static ProductCategoryModel GetProductCategoryModel(int _menu_child_serial_no)
    {
        ProductCategoryModel[] models = ProductCategoryModel.FindAllByProperty("menu_child_serial_no", _menu_child_serial_no);
        if (models.Length == 1)
            return models[0];
        else
            return new ProductCategoryModel();
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string menu_child_name
    {
        get { return _menu_child_name; }
        set { _menu_child_name = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string menu_child_href
    {
        get { return _menu_child_href; }
        set { _menu_child_href = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public byte menu_is_exist_sub
    {
        get { return _menu_is_exist_sub; }
        set { _menu_is_exist_sub = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int menu_parent_serial_no
    {
        get { return _menu_parent_serial_no; }
        set { _menu_parent_serial_no = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int menu_pre_serial_no
    {
        get { return _menu_pre_serial_no; }
        set { _menu_pre_serial_no = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public byte tag
    {
        get { return _tag; }
        set { _tag = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int menu_child_order
    {
        get { return _menu_child_order; }
        set { _menu_child_order = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int old_db_id
    {
        get { return _old_db_id; }
        set { _old_db_id = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string menu_child_name_f
    {
        get { return _menu_child_name_f; }
        set { _menu_child_name_f = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int page_category
    {
        get { return _page_category; }
        set { _page_category = value; }
    }

    [Property]
    public byte is_noebook
    {
        get { return _is_noebook; }
        set { _is_noebook = value; }
    }
    [Property]
    public bool export
    {
        get { return _export; }
        set { _export = value; }
    }
    [Property]
    public bool is_display_stock
    {
        get { return _is_display_stock; }
        set { _is_display_stock = value; }
    }
    [Property]
    public bool is_virtual
    {
        get { return _is_virtual; }
        set { _is_virtual = value; }
    }
    
    [Property]
    public string keywords_cates
    {
        get { return _keywords_cates; }
        set { _keywords_cates = value; }
    }
     [Property]
     public bool is_view_menu
     {
         get { return _is_view_menu; }
         set { _is_view_menu = value; }
     }

     [Property]
     public string eBayCategoryID
     {
         get { return _eBayCategoryID; }
         set { _eBayCategoryID = value; }
     }


     [Property]
     public string ebayStoreCategoryID_1
     {
         get { return _ebayStoreCategoryID_1; }
         set { _ebayStoreCategoryID_1 = value; }
     }


     [Property]
     public string eBayStoreCategoryID_2
     {
         get { return _eBayStoreCategoryID_2; }
         set { _eBayStoreCategoryID_2 = value; }
     }

    /// <summary>
    /// 根据父类显示子类产品类别
    /// </summary>
    /// <param name="menu_pre_serial_no">父类ID</param>
    /// <returns></returns>
    public static ProductCategoryModel[] ProductCategoryModelsByMenuPreSerialNo(int menu_pre_serial_no)
    {
        NHibernate.Expression.EqExpression eq1 = new NHibernate.Expression.EqExpression("menu_parent_serial_no", 1);
        NHibernate.Expression.EqExpression eq2 = new NHibernate.Expression.EqExpression("menu_pre_serial_no", menu_pre_serial_no);
        NHibernate.Expression.AndExpression a = new NHibernate.Expression.AndExpression(eq1, eq2);
        return ProductCategoryModel.FindAll(CurrentOrder(true),a);
    }

    /// <summary>
    /// 根据父类显示子类产品类别
    /// </summary>
    /// <param name="menu_pre_serial_no">父类ID</param>
    /// <returns></returns>
    public static ProductCategoryModel[] ProductCategoryModelsByMenuPreSerialNo(int menu_pre_serial_no, bool tag, int page_category)
    {
        NHibernate.Expression.EqExpression eq1 = new NHibernate.Expression.EqExpression("menu_parent_serial_no", 1);
        NHibernate.Expression.EqExpression eq2 = new NHibernate.Expression.EqExpression("menu_pre_serial_no", menu_pre_serial_no);
        NHibernate.Expression.EqExpression eq3 = new NHibernate.Expression.EqExpression("tag", tag == true ? byte.Parse("1") : byte.Parse("0"));
        NHibernate.Expression.AndExpression a = new NHibernate.Expression.AndExpression(eq1, eq2);
        NHibernate.Expression.AndExpression a2 = new NHibernate.Expression.AndExpression(a, eq3);
        NHibernate.Expression.EqExpression eq4 = new NHibernate.Expression.EqExpression("page_category", page_category);
        NHibernate.Expression.AndExpression a3 = new NHibernate.Expression.AndExpression(a2, eq4);
        if (page_category != -1)
        {
            return ProductCategoryModel.FindAll(CurrentOrder(true), a3);
        }
        return ProductCategoryModel.FindAll(CurrentOrder(true), a2);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="menu_pre_serial_no"></param>
    /// <param name="tag"></param>
    /// <returns></returns>
    public static ProductCategoryModel[] ProductCategoryModelsByMenuPreSerialNo(int menu_pre_serial_no, bool tag)
    {
        return ProductCategoryModelsByMenuPreSerialNo(menu_pre_serial_no, tag, -1);
    }

    /// <summary>
    /// 显示父类
    /// </summary>
    /// <param name="menu_pre_serial_no"></param>
    /// <returns></returns>
    public static ProductCategoryModel[] ProductCategoryModelsParent()
    {
        return ProductCategoryModelsByMenuPreSerialNo(0, true);
    }
    /// <summary>
    /// search first level
    /// </summary>
    /// <param name="is_part"></param>
    /// <param name="show_it"></param>
    /// <returns></returns>
    public static ProductCategoryModel[] FindModelsByIsSystem(bool is_part, bool show_it, int menu_pre_serial_no)
    {
        return ProductCategoryModelsByMenuPreSerialNo(menu_pre_serial_no, show_it, (is_part == true ? 1 : 0));
       // return Config.ExecuteDataTable("Select * from tb_product_category where page_category=" + (is_part == true ? "1" : "0") + " and tag=" + (show_it == true ? "1" : "0") + " and menu_pre_serial_no=" + menu_pre_serial_no);
    }

    /// <summary>
    /// 显示父类
    /// </summary>
    /// <param name="menu_pre_serial_no"></param>
    /// <returns></returns>
    public static ProductCategoryModel[] ProductCategoryModelsParentAll()
    {
        return ProductCategoryModelsByMenuPreSerialNo(0);
    }

    public static NHibernate.Expression.Order[] CurrentOrder(bool order)
    {
        NHibernate.Expression.Order o1 = new NHibernate.Expression.Order("menu_child_order", order);
        NHibernate.Expression.Order o = new NHibernate.Expression.Order("menu_child_serial_no", order);
        NHibernate.Expression.Order[] oo = new NHibernate.Expression.Order[] { o1, o };
        return oo;
    }


    public static ProductCategoryModel[]  ProductCategoryModelsByParts(bool order)
    {
        //return ProductCategoryModelsByMenuPreSerialNo(0);
        NHibernate.Expression.EqExpression eq2 = new NHibernate.Expression.EqExpression("menu_pre_serial_no", 0);
        NHibernate.Expression.EqExpression e = new NHibernate.Expression.EqExpression("page_category", 1);
        NHibernate.Expression.EqExpression eq1 = new NHibernate.Expression.EqExpression("menu_parent_serial_no", 1);
        NHibernate.Expression.AndExpression a = new NHibernate.Expression.AndExpression(e, eq2);
        NHibernate.Expression.AndExpression a2 = new NHibernate.Expression.AndExpression(a, eq1);
        return ProductCategoryModel.FindAll(CurrentOrder(order), a2);
    }


    /// <summary>
    /// 所有产品类别
    /// </summary>
    /// <returns></returns>
    public static ProductCategoryModel[] ProductCategoryModels()
    {
        return ProductCategoryModel.FindAllByProperty("menu_parent_serial_no", 1);
    }
    /// <summary>
    /// 显示是否可以展示的显产品类别
    /// </summary>
    /// <param name="showit"></param>
    /// <returns></returns>
    public static ProductCategoryModel[] ProductCategoryModelsByShowIt(bool showit)
    {
        return ProductCategoryModel.FindAll();
    }


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
                DataTable child = Config.ExecuteDataTable(sql + " and menu_pre_serial_no=" + dr["menu_child_serial_no"] +order);
                for (int j = 0; j < child.Rows.Count; j++)
                {
                    DataRow chDR = child.Rows[j];
                    DataRowToDataTable(chDR, category, 4);
                    if (chDR["menu_is_exist_sub"].ToString() == "1")
                    {
                        DataTable subDT = Config.ExecuteDataTable(sql + " and menu_pre_serial_no=" + dr["menu_child_serial_no"] +order);
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

    public static ArrayList GetAllPartCategory()
    {
        //string sql = "select menu_child_name, menu_child_serial_no from tb_product_category where tag=1 and menu_is_exist_sub=0 and page_category=1 and menu_parent_serial_no=1  order by menu_child_order asc";
        ProductCategoryModel[] ms = ProductCategoryModelsByParts(true);

        ArrayList al = new ArrayList();
        for (int i = 0; i < ms.Length; i++)
        {
            ProductCategoryModel[] pcms = ProductCategoryModel.ProductCategoryModelsByMenuPreSerialNo(ms[i].menu_child_serial_no);
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

    public static DataTable FindCategoryNoParent()
    {
        DataTable dt = Config.ExecuteDataTable("select menu_child_name, menu_child_serial_no,menu_pre_serial_no from tb_product_category where tag=1 and menu_is_exist_sub=0 and menu_pre_serial_no > 0 order by menu_child_order asc");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            int _menu_child = int.Parse(dt.Rows[i]["menu_pre_serial_no"].ToString());
            ProductCategoryModel m = ProductCategoryModel.GetProductCategoryModel(_menu_child);
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

    public static DataTable FindCategoryFilterName()
    {
        DataTable dt = FindCategoryLowLevel();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string name = dt.Rows[i]["name"].ToString();
            GetParentName(int.Parse(dt.Rows[i]["menu_child_serial_no"].ToString()), ref name);
            dt.Rows[i]["name"] = name;
        }
        return dt;
    }

    public static void GetParentName(int categoryID, ref string name)
    {
        ProductCategoryModel pcm = ProductCategoryModel.GetProductCategoryModel(categoryID);
        name = pcm.menu_child_name + "-->" + name;
        if (pcm.menu_pre_serial_no > 0)
        {
            GetParentName(pcm.menu_pre_serial_no, ref name);
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
                        dr["name"] = dt1.Rows[i]["menu_child_name"].ToString() + "-->" + dt2.Rows[j]["menu_child_name"].ToString() ;
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
    public static int GetCategoryID(string eBayCategoryID)
    {
        NHibernate.Expression.EqExpression eq1 = new NHibernate.Expression.EqExpression("eBayCategoryID", eBayCategoryID);
        ProductCategoryModel[] list = ProductCategoryModel.FindAll(eq1);
        if (list != null)
            if (list.Length > 0)
                return list[0].menu_child_serial_no;

        return 0;
    }

    /// <summary>
    /// 是否是笔记本类
    /// </summary>
    /// <param name="CategoryID"></param>
    /// <returns></returns>
    public static bool IsNotebook(int CategoryID)
    {
        ProductCategoryModel pcm = GetProductCategoryModel(CategoryID);
        if (pcm != null)
            return pcm.is_noebook == 1;
        else
            return false;
    }


}
