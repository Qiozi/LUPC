// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-6-10 19:50:14
//
// // // // // // // // // // // // // // // //
using LU.Data;
using System;
using System.Linq;
using System.Data;

[Serializable]
public class ChatMsgModel
{
    //int _msg_id;
    //string _msg_content_text;
    //int _msg_type;
    //int _staff_id;
    //string _msg_author;
    //DateTime _regdate;
    //string _msg_order_code;

    //public ChatMsgModel()
    //{

    //}


    ///// <summary>
    ///// 
    ///// </summary>
    //[PrimaryKey(PrimaryKeyType.Identity)]
    //public int msg_id
    //{
    //    get { return _msg_id; }
    //    set { _msg_id = value; }
    //}
    //public static ChatMsgModel GetAccountModel(int _msg_id)
    //{
    //    ChatMsgModel[] models = ChatMsgModel.FindAllByProperty("msg_id", _msg_id);
    //    if (models.Length == 1)
    //        return models[0];
    //    else
    //        return new ChatMsgModel();
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    //[Property]
    //public string msg_content_text
    //{
    //    get { return _msg_content_text; }
    //    set { _msg_content_text = value; }
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    //[Property]
    //public int msg_type
    //{
    //    get { return _msg_type; }
    //    set { _msg_type = value; }
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    //[Property]
    //public int staff_id
    //{
    //    get { return _staff_id; }
    //    set { _staff_id = value; }
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    //[Property]
    //public string msg_author
    //{
    //    get { return _msg_author; }
    //    set { _msg_author = value; }
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    //[Property]
    //public DateTime regdate
    //{
    //    get { return _regdate; }
    //    set { _regdate = value; }
    //}
    //[Property]
    //public string msg_order_code
    //{
    //    get { return _msg_order_code; }
    //    set { _msg_order_code = value; }
    //}
    public static tb_chat_msg[] FindModelsByOrderCode(nicklu2Entities context, string order_code)
    {
        return context.tb_chat_msg.Where(me => me.msg_order_code.Equals(order_code)).ToList().ToArray();
    }

    public static DataTable FindModelsByCount(int count)
    {
        return Config.ExecuteDataTable(@"select msg_order_code, msg_content_text, date_format(regdate,'%W %M-%d-%Y %r') regdate, 
case 
when (date_format(regdate,'%W %M-%d-%Y')=date_format(now(),'%W %M-%d-%Y'))=1 then 'color:red;' 
else '' end today_color
 from tb_chat_msg order by msg_id desc limit 0," + count.ToString());
    }
}
