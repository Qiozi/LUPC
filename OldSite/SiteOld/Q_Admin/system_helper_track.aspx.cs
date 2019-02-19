using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Q_Admin_system_helper_track : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.ValidateLoginRule(Role.Track);
            InitialDatabase();
        }
    }

    #region Methods

    public override void InitialDatabase()
    {
        base.InitialDatabase();
        BindTrackDG();
    }

    private void BindTrackDG()
    {
        this.dg_track_list.DataSource = TrackModel.FindModelsByCount(100);
        this.dg_track_list.DataBind();
    }
    #endregion
}
