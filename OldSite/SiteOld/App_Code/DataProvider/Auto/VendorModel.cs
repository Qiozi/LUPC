// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-5-26 13:46:05
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;

[ActiveRecord("tb_vendor")]
[Serializable]
public class VendorModel : ActiveRecordBase<VendorModel>
{
    string _company_name;
    string _customer;
    int _account_number;
    string _sales_person;
    string _telephone1;
    string _telephone2;
    string _fax;
    string _address1;
    string _address2;
    string _cite_state_zip;
    string _email;
    string _website;
    string _rma_telphone;
    string _rma_person;
    string _note;
    string _vendor_category;
    int _vendor_serial_no;
    byte _tag;
    string _user_name;
    string _pass_word;
    string _msn;
    string _icq;
    int _system_category_serial_no;

    public VendorModel()
    {

    }


    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string company_name
    {
        get { return _company_name; }
        set { _company_name = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string customer
    {
        get { return _customer; }
        set { _customer = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int account_number
    {
        get { return _account_number; }
        set { _account_number = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string sales_person
    {
        get { return _sales_person; }
        set { _sales_person = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string telephone1
    {
        get { return _telephone1; }
        set { _telephone1 = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string telephone2
    {
        get { return _telephone2; }
        set { _telephone2 = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string fax
    {
        get { return _fax; }
        set { _fax = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string address1
    {
        get { return _address1; }
        set { _address1 = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string address2
    {
        get { return _address2; }
        set { _address2 = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string cite_state_zip
    {
        get { return _cite_state_zip; }
        set { _cite_state_zip = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string email
    {
        get { return _email; }
        set { _email = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string website
    {
        get { return _website; }
        set { _website = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string rma_telphone
    {
        get { return _rma_telphone; }
        set { _rma_telphone = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string rma_person
    {
        get { return _rma_person; }
        set { _rma_person = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string note
    {
        get { return _note; }
        set { _note = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string vendor_category
    {
        get { return _vendor_category; }
        set { _vendor_category = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int vendor_serial_no
    {
        get { return _vendor_serial_no; }
        set { _vendor_serial_no = value; }
    }
    public static VendorModel GetVendorModel(int _vendor_serial_no)
    {
        VendorModel[] models = VendorModel.FindAllByProperty("vendor_serial_no", _vendor_serial_no);
        if (models.Length == 1)
            return models[0];
        else
            return new VendorModel();
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
    public string user_name
    {
        get { return _user_name; }
        set { _user_name = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string pass_word
    {
        get { return _pass_word; }
        set { _pass_word = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string msn
    {
        get { return _msn; }
        set { _msn = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string icq
    {
        get { return _icq; }
        set { _icq = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int system_category_serial_no
    {
        get { return _system_category_serial_no; }
        set { _system_category_serial_no = value; }
    }
}
