// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	4/27/2007 10:41:39 AM
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[ActiveRecord("tb_menu")]
[Serializable]
public class MenuModel : ActiveRecordBase<MenuModel>
{
    int _menu_child_serial_no;
    string _menu_child_name;
    string _menu_child_href;
    byte _menu_is_exist_sub;
    int _menu_parent_serial_no;
    int _menu_pre_serial_no;
    bool _tag;
    int _menu_child_order;
    int _old_db_id;
    string _target;

    public MenuModel()
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
    public static MenuModel GetMenuModel(int _menu_child_serial_no)
    {
        MenuModel[] models = MenuModel.FindAllByProperty("menu_child_serial_no", _menu_child_serial_no);
        if (models.Length == 1)
            return models[0];
        else
            return new MenuModel();
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
    public bool tag
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
    public string target
    {
        get { return _target; }
        set { _target = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="menu_pre_serial_no"></param>
    /// <returns></returns>
    public static MenuModel[] MenuModelsByMenuParentSerialNo(int menu_parent_serial_no, bool tag)
    {
        NHibernate.Expression.EqExpression eq1 = new NHibernate.Expression.EqExpression("menu_parent_serial_no", menu_parent_serial_no);
        NHibernate.Expression.EqExpression eq2 = new NHibernate.Expression.EqExpression("tag", tag);
        NHibernate.Expression.AndExpression aa = new NHibernate.Expression.AndExpression(eq1, eq2);

        NHibernate.Expression.Order o = new NHibernate.Expression.Order("menu_child_order", true);
        NHibernate.Expression.Order[] oo = new NHibernate.Expression.Order[] { o };
        return MenuModel.FindAll(oo, aa);
    }

    public static DataTable FindMenuTopLevel()
    {
        return Config.ExecuteDataTable(@"select menu_child_serial_no,menu_child_name, menu_child_href,menu_is_exist_sub 
from tb_menu where menu_parent_serial_no=0 and tag=1  order by menu_child_order asc ");

    }
}
