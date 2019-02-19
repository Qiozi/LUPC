// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	4/27/2007 10:41:39 AM
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;

[ActiveRecord("tb_menu_child")]
[Serializable]
public class MenuChildModel : ActiveRecordBase<MenuChildModel>
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

    public MenuChildModel()
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
    public static MenuChildModel GetMenuChildModel(int _menu_child_serial_no)
    {
        MenuChildModel[] models = MenuChildModel.FindAllByProperty("menu_child_serial_no", _menu_child_serial_no);
        if (models.Length == 1)
            return models[0];
        else
            return new MenuChildModel();
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
    /// 根据父类显示子类产品类别
    /// </summary>
    /// <param name="menu_pre_serial_no">父类ID</param>
    /// <returns></returns>
    public static MenuChildModel[] MenuChildModelsByMenuPreSerialNo(int menu_pre_serial_no)
    {
        NHibernate.Expression.EqExpression eq1 = new NHibernate.Expression.EqExpression("menu_parent_serial_no", 1);
        NHibernate.Expression.EqExpression eq2 = new NHibernate.Expression.EqExpression("menu_pre_serial_no", menu_pre_serial_no);
        NHibernate.Expression.AndExpression a = new NHibernate.Expression.AndExpression(eq1, eq2);
        return MenuChildModel.FindAll(a);
    }

    /// <summary>
    /// 所有产品类别
    /// </summary>
    /// <returns></returns>
    public static MenuChildModel[] MenuChildModels()
    {
        return MenuChildModel.FindAllByProperty("menu_parent_serial_no", 1);
    }
    /// <summary>
    /// 显示是否可以展示的显产品类别
    /// </summary>
    /// <param name="showit"></param>
    /// <returns></returns>
    public static MenuChildModel[] MenuChildModelsByShowIt(bool showit)
    {
        NHibernate.Expression.EqExpression eq1 = new NHibernate.Expression.EqExpression("menu_parent_serial_no", 1);
        NHibernate.Expression.EqExpression eq2 = new NHibernate.Expression.EqExpression("tag", showit);
        NHibernate.Expression.AndExpression a = new NHibernate.Expression.AndExpression(eq1, eq2);
        return MenuChildModel.FindAll();
    }
}
