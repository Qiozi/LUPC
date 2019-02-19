// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-8-11 15:32:22
//
// // // // // // // // // // // // // // // //
using System;
using Castle.ActiveRecord;
using System.Data;

[ActiveRecord("tb_ask_question")]
public class AskQuestionModel : ActiveRecordBase<AskQuestionModel>
{
    int _aq_serial_no;
    string _aq_email;
    string _aq_title;
    string _aq_body;
    string _aq_product_title;
    int _menu_child_serial_no;
    int _product_serial_no;
    string _aq_reply_body;
    byte _aq_send;
    DateTime _create_datetime;
    string _ip;
    byte _product_category;
    DateTime _send_regdate;

    public AskQuestionModel()
    {

    }


    /// <summary>
    /// 
    /// </summary>
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int aq_serial_no
    {
        get { return _aq_serial_no; }
        set { _aq_serial_no = value; }
    }
    public static AskQuestionModel GetAskQuestionModel(int _aq_serial_no)
    {
        AskQuestionModel[] models = AskQuestionModel.FindAllByProperty("aq_serial_no", _aq_serial_no);
        if (models.Length == 1)
            return models[0];
        else
            return new AskQuestionModel();
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string aq_email
    {
        get { return _aq_email; }
        set { _aq_email = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string aq_title
    {
        get { return _aq_title; }
        set { _aq_title = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string aq_body
    {
        get { return _aq_body; }
        set { _aq_body = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string aq_product_title
    {
        get { return _aq_product_title; }
        set { _aq_product_title = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int menu_child_serial_no
    {
        get { return _menu_child_serial_no; }
        set { _menu_child_serial_no = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int product_serial_no
    {
        get { return _product_serial_no; }
        set { _product_serial_no = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string aq_reply_body
    {
        get { return _aq_reply_body; }
        set { _aq_reply_body = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public byte aq_send
    {
        get { return _aq_send; }
        set { _aq_send = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public DateTime create_datetime
    {
        get { return _create_datetime; }
        set { _create_datetime = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string ip
    {
        get { return _ip; }
        set { _ip = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public byte product_category
    {
        get { return _product_category; }
        set { _product_category = value; }
    }

    [Property]
    public DateTime send_regdate
    {
        get { return _send_regdate; }
        set { _send_regdate = value; }
    }
    public static DataTable GetModelsByAll()
    {
        return
            Config.ExecuteDataTable(
                @"select 	aq_serial_no, aq_email, aq_title, aq_product_title, 
	menu_child_serial_no, product_serial_no, 
	(case WHEN aq_send>0 THEN 'Y' ELSE 'N' END) aq_send
	, date_format( create_datetime, ""%b-%d-%Y"") create_datetime, ip, product_category 
	from 
	tb_ask_question
order by aq_serial_no desc");
    }

   

    public static int FindNoSentSum()
    {
        return int.Parse(Config.ExecuteScalar("select count(aq_serial_no) from tb_ask_question where aq_send=0").ToString());
    }

}
