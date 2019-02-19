using System;
using System.Data;

public partial class Q_Admin_ebayMaster_LU_part_relation_motherboard_get_options : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialDatabase();
        }
    }

    public override void InitialDatabase()
    {
        base.InitialDatabase();
        
        
        switch (ReqCmd)
        {
            case "GetVideoOptions":
                int videoGroupID = EbaySettings.relation_motherboard_video_group_id;
                ResponseJSon(videoGroupID);

                break;
            case "GetAudioOptions":
                int audioGroupID = EbaySettings.relation_motherboard_audio_group_id;
                ResponseJSon(audioGroupID);

                break;
            case "GetNetworkOptions":
                int networkGroupID = EbaySettings.relation_motherboard_network_group_id;
                ResponseJSon(networkGroupID);

                break;
        }
    }

    void ResponseJSon(int group_id)
    {
        DataTable dt;
        System.Text.StringBuilder sb;
        string sql = @"Select p.product_serial_no, p.product_ebay_name 
                from tb_product p inner join tb_part_group_detail pd on p.product_serial_no=pd.product_serial_no
                    where p.split_line=0 and p.tag=1 and pd.showit=1 and pd.part_group_id='{0}' order by p.product_name asc";

        dt = Config.ExecuteDataTable(string.Format(sql, group_id));
        sb = new System.Text.StringBuilder();
        sb.Append("[");
        foreach (DataRow dr in dt.Rows)
        {
            sb.Append(string.Format(@"{{name:""{0}"", value:""{1}""}},"
                , string.Format("{0}:::{1}"
                    , int.Parse(dr["product_serial_no"].ToString()).ToString("00000")
                    , dr["product_ebay_name"].ToString().Replace("\"", ""))
                , dr["product_serial_no"].ToString()));
        }
        sb.Remove(sb.Length - 1, 1);
        sb.Append("]");
        Response.Clear();
        Response.Write(sb.ToString());
        Response.End();
    }

    public string ReqCmd
    {
        get { return Util.GetStringSafeFromQueryString(Page, "cmd"); }
    }


}