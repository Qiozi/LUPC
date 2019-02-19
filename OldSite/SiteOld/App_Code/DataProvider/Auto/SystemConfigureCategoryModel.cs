// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-5-22 15:28:18
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;

[ActiveRecord("tb_system_configure_category")]
[Serializable]
public class SystemConfigureCategoryModel : ActiveRecordBase<SystemConfigureCategoryModel>
{
    int _system_configure_category_serial_no;
    string _menu_child_list;
    string _system_configure_category_name;

    public SystemConfigureCategoryModel()
    {

    }


    /// <summary>
    /// 
    /// </summary>
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int system_configure_category_serial_no
    {
        get { return _system_configure_category_serial_no; }
        set { _system_configure_category_serial_no = value; }
    }
    public static SystemConfigureCategoryModel GetSystemConfigureCategoryModel(int _system_configure_category_serial_no)
    {
        SystemConfigureCategoryModel[] models = SystemConfigureCategoryModel.FindAllByProperty("system_configure_category_serial_no", _system_configure_category_serial_no);
        if (models.Length == 1)
            return models[0];
        else
            return new SystemConfigureCategoryModel();
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string menu_child_list
    {
        get { return _menu_child_list; }
        set { _menu_child_list = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string system_configure_category_name
    {
        get { return _system_configure_category_name; }
        set { _system_configure_category_name = value; }
    }
}
