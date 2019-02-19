// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-11-30 13:46:05
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;

/// <summary>
/// StaffModel 的摘要说明
/// </summary>
[ActiveRecord("tb_staff")]
[Serializable]
public class StaffModel : ActiveRecordBase<StaffModel>
{
    int _staff_serial_no;
    string _staff_login_name;
    string _staff_email;
    string _staff_phone;
    bool _tag;
    string _staff_password;
    string _staff_realname;
    DateTime _staff_last_login_date;
    string _staff_system_category;
    int _staff_type;


	public StaffModel()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
    }    
    /// <summary>
    /// 
    /// </summary>
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int staff_serial_no
    {
        get { return _staff_serial_no; }
        set { _staff_serial_no = value; }
    }
    public static StaffModel GetStaffModel(int _staff_serial_no)
    {
        StaffModel[] models = StaffModel.FindAllByProperty("staff_serial_no", _staff_serial_no);
        if (models.Length == 1)
            return models[0];
        else
            return new StaffModel();
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string staff_login_name
    {
        get { return _staff_login_name; }
        set { _staff_login_name = value; }
    }
    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string staff_email
    {
        get { return _staff_email; }
        set { _staff_email = value; }
    }
    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string staff_phone
    {
        get { return _staff_phone; }
        set { _staff_phone = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public bool tag
    {
        get { return _tag; }
        set { _tag = value; }
    }    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string staff_password
    {
        get { return _staff_password; }
        set { _staff_password = value; }
    } /// <summary>
    /// 
    /// </summary>
    [Property]
    public string staff_realname
    {
        get { return _staff_realname; }
        set { _staff_realname = value; }
    }
/// <summary>
    /// 
    /// </summary>
    [Property]
    public DateTime staff_last_login_date
    {
        get { return _staff_last_login_date; }
        set { _staff_last_login_date = value; }
    }
    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string staff_system_category
    {
        get { return _staff_system_category; }
        set { _staff_system_category = value; }
    }
    
    [Property]
    public int staff_type
    {
        get { return _staff_type; }
        set { _staff_type = value; }
    }
    public static object FindModelsByLoginName(string login_name)
    {
        StaffModel[] sms = StaffModel.FindAllByProperty("staff_login_name", login_name);
        if (sms.Length == 1)
        {
            return sms[0];
        }
        return null;
    }
}
