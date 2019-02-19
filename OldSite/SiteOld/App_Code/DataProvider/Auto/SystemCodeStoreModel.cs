using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Castle.ActiveRecord;
/// <summary>
/// Summary description for SystemCodeStoreModel
/// </summary>
[ActiveRecord("tb_system_code_store")]
[Serializable]
public class SystemCodeStoreModel : ActiveRecordBase<SystemCodeStoreModel>
{
	public SystemCodeStoreModel()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    int _id;
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int id
    {
        get { return _id; }
        set { _id = value; }
    }
    int _system_templete_serial_no;
    [Property]
    public int system_templete_serial_no
    {
        get { return _system_templete_serial_no; }
        set { _system_templete_serial_no = value; }
    }
    int _system_code;
    [Property]
    public int system_code
    {
        get { return _system_code; }
        set { _system_code = value; }
    }
    DateTime _create_datetime;
    [Property]
    public DateTime create_datetime
    {
        get { return _create_datetime; }
        set { _create_datetime = value; }
    }
    string _ip;
    [Property]
    public string ip
    {
        get { return _ip; }
        set { _ip = value; }
    }
    int _old_system_code;
    [Property]
    public int old_system_code
    {
        get { return _old_system_code; }
        set { _old_system_code = value; }
    }
    bool _is_buy;
    [Property]
    public bool is_buy
    {
        get { return _is_buy; }
        set { _is_buy = value; }
    }

 
}
