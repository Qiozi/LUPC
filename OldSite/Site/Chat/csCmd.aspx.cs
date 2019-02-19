using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public partial class Chat_csCmd : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            Response.Clear();
            DataTable dt = new DataTable("dt");
            string json = "";
            switch (ReqCmd)
            {
                case "getCustomerList":
                    dt = Config.ExecuteDataTable("select distinct UserName, concat(firstname,' ', lastname) realName, date_format(max(regdate), '%Y-%m-%d') regdate, sum(isreadbyserver) c from tb_chat_msg_online where id>0 order by isreadbyserver desc, id desc limit 30");
                    List<ChatCustomerListModel> cList = new List<ChatCustomerListModel>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        ChatCustomerListModel model = new ChatCustomerListModel();
                        model.Code = dr["UserName"].ToString();
                        model.Name = dr["realName"].ToString();
                        model.Date = dr["regdate"].ToString();
                        model.Msg = dr["c"].ToString();

                        cList.Add(model);
                    }
                    json = JsonConvert.SerializeObject(cList);
                    Response.Write(json);
                    break;

                case "SaveMsg":

                    break;
                case "getMsg":
                    dt = Config.ExecuteDataTable(string.Format(@"select concat(firstname,' ', lastname) realName, ChatText, date_format(regdate, '%Y-%m-%d') regdate,ServerName from tb_chat_msg_online where UserName='{0}'"
                        , ReqCustomerID));

                    List<ChatCustomerMsg> cMsgList = new List<ChatCustomerMsg>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        ChatCustomerMsg model = new ChatCustomerMsg();
                        model.Date = dr["regdate"].ToString();
                        model.Name = dr["realName"].ToString();
                        model.Msg = dr["ChatText"].ToString();
                        model.IsServerMsg = !string.IsNullOrEmpty(dr["ServerName"].ToString());
                        model.ServerName = dr["ServerName"].ToString();
                        cMsgList.Add(model);
                    }
                    json = JsonConvert.SerializeObject(cMsgList);
                    Response.Write(json);
                    break;
            }
            Response.End();
        }
    }

    string ReqCmd
    {
        get { return  Util.GetStringSafeFromQueryString(Page, "cmd"); }
    }

    string ReqCustomerID
    {
        get { return Util.GetStringSafeFromQueryString(Page, "cid"); }
    }
}

public class ChatCustomerListModel
{
    public ChatCustomerListModel()
    {
        
    }

    public string Code { set; get; }

    public string Name { set; get; }

    public string Date { set; get; }

    public string Msg { get; set; }
}
[Serializable]
public class ChatCustomerMsg
{
    public ChatCustomerMsg() { }

    public string Name { get; set; }

    public string Date { set; get; }

    public string Msg { set; get; }

    public bool IsServerMsg { set; get; }

    public int MaxId { set; get; }

    public string ServerName { set; get; }
}