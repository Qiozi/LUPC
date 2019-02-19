using System;

public partial class Q_Admin_product_helper_part_view_info_3 : PageBase
{
    public string Token = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Token = System.Guid.NewGuid().ToString();
            Config.ExecuteNonQuery(string.Format(@"insert into tb_exchange 
	( Pwd, ExchangeType, Source, Regdate)
	values
	( '{0}', 1, 'WebManage', now())", Token));
            InitialDatabase();
        }
    }

    public override void InitialDatabase()
    {
        base.InitialDatabase();
    }
}
