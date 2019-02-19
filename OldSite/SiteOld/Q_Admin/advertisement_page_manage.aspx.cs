using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Q_Admin_advertisement_page_manage : PageBase
{
   const string  ADV_PATH = "/adv_page/";
   const string FILE_PRE = "adv_";
   const string FILE_EXT = ".html";
   const int PageSize = 30;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialDatabase();
            this.AspNetPager1.PageSize = PageSize;
            
        }
    }

    public override void InitialDatabase()
    {
        base.InitialDatabase();

        BindLV(0, PageSize);   
    }

    private DataTable GetFileListInfo(int start_record_index, int page_size, ref int return_record_count)
    {
        DataTable dt = Config.ExecuteDataTable("select id, summary, title, '' file_name, '' date_taken, '' size from tb_advertise_page where is_del=0 order by last_regdate desc limit " + start_record_index + "," + page_size + " ");
        
        //dt.Columns.Add("summary");
        //dt.Columns.Add("file_name");
        //dt.Columns.Add("date_taken");
        //dt.Columns.Add("size");
        //CH.Alert("select id, summary, '' file_name, '' date_taken, '' size from tb_advertise_page where is_del=0 order by last_regdate desc limit " + start_record_index + "," + page_size + " ", this.Literal1);
        DirectoryInfo dir = new DirectoryInfo(Server.MapPath(ADV_PATH));
        FileInfo[] files = dir.GetFiles();
        return_record_count = Config.ExecuteScalarInt32("select count(id) c from tb_advertise_page where is_del=0 ");
        for (int i = 0; i < dt.Rows.Count; i++)
        { 
            DataRow dr = dt.Rows[i];
            bool is_exist_file = false;
            for (int j = 0; j < files.Length; j++)
            {
                int id = GetIDByFilename(files[j].Name);
                if (id.ToString() == dt.Rows[i]["id"].ToString())
                {
                   
                    dr["file_name"] = files[j].Name;
                    dr["date_taken"] = files[j].LastWriteTime;
                    dr["size"] = files[j].Length;
                    is_exist_file = true;
                    //DataTable sdt = Config.ExecuteDataTable("select summary from tb_advertise_page where id='" + id + "'");
                    //if (sdt.Rows.Count == 1)
                    //    dr["summary"] = sdt.Rows[0][0].ToString();
                    //dt.Rows.Add(dr);
                }
            }
            if (! is_exist_file)
                dr["file_name"] = "N/A";
        }
       
        return dt;
    }
    protected void btn_create_file_Click(object sender, EventArgs e)
    {
        string summary = this.txt_summary.Text.Trim();

        if (summary.Length > 0)
        {
            AdvertisePageModel apm = new AdvertisePageModel();
            apm.last_regdate = DateTime.Now;
            apm.regdate = DateTime.Now;
            apm.summary = summary;
            apm.Create();
            CurrentEditID = apm.id;
            StreamWriter sw = new StreamWriter(Server.MapPath(GetFilenameById(apm.id)));
            sw.Write("");
            sw.Close();

            BindLV(this.AspNetPager1.StartRecordIndex-1, this.AspNetPager1.EndRecordIndex);
            this.panel_edit_content.Visible = true;
            this.txt_summary_2.Text = summary;
            this.txt_content.Text = "";
        }
        else
            CH.Alert("请输入描述", this.lv_file_list);

    }

    private void BindLV( int start_record_index, int page_size)
    {
        int return_record_count = 0;
        DataTable dt = GetFileListInfo(start_record_index, page_size, ref return_record_count);
        this.lv_file_list.DataSource = dt;

        this.lv_file_list.DataBind();

        this.AspNetPager1.RecordCount = return_record_count > 1 ? return_record_count  : 1;
        this.AspNetPager1.DataBind();
    }

    #region Properties
    public int CurrentEditID
    {
        get { return (int)ViewState["CurrentEditID"]; }
        set { ViewState["CurrentEditID"] = value; }
    }

    public DataTable FileList
    {
        get { return (DataTable)ViewState["FileList"]; }
        set { ViewState["FileList"] = value; }
    }
    #endregion
    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {
            string summary = this.txt_summary_2.Text.Trim();
            if (CurrentEditID > 0)
            {
                AdvertisePageModel apm = AdvertisePageModel.GetAdvertisePageModel(CurrentEditID);
                apm.last_regdate = DateTime.Now;
                apm.regdate = DateTime.Now;
                apm.summary = summary;
                apm.id = CurrentEditID;
                apm.title = this.txt_title.Text;
                apm.conent = this.txt_content.Text;
                apm.Save();

                StreamWriter sw = new StreamWriter(Server.MapPath(GetFilenameById(apm.id)));
                sw.Write(apm.conent);
                sw.Close();

                BindLV(this.AspNetPager1.StartRecordIndex - 1, this.AspNetPager1.EndRecordIndex);
                this.panel_edit_content.Visible = false;
                CH.Alert(KeyFields.SaveIsOK, this.Literal1);
            }
            else
            {
                CH.Alert("参数错误码，未知编辑ID号", this.Literal1);
            }
            this.panel_edit_content.Visible = false;
        }
        catch (Exception ex)
        {
            CH.Alert(ex.Message, this.Literal1);
        }
    }

    public int GetIDByFilename(string file_name)
    {
        if (file_name.Length > 5)
        {
            int id;
            int.TryParse(file_name.Replace(FILE_PRE, "").Replace(FILE_EXT, ""), out id);
            return id;
        }
        return 0;
    }

    public string GetFilenameById(int id)
    {
        return string.Format("{0}{2}{1}{3}", ADV_PATH, id, FILE_PRE, FILE_EXT);
    }

    protected void lv_file_list_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        try
        {
            int id = GetIDByFilename(e.CommandArgument.ToString());
            switch (e.CommandName)
            {
                case "EditFile":
                    CurrentEditID = id;
                    StreamReader sr = new StreamReader(Server.MapPath(GetFilenameById(id)));
                    this.txt_content.Text = sr.ReadToEnd();
                    sr.Close();
                    sr.Dispose();

                    DataTable sdt = Config.ExecuteDataTable("select summary from tb_advertise_page where id='" + id + "'");
                    if (sdt.Rows.Count == 1)
                        this.txt_summary_2.Text = sdt.Rows[0][0].ToString();


                    this.panel_edit_content.Visible = true;

                    break;
                case "DeleteFile":
                    Config.ExecuteNonQuery("update tb_advertise_page set is_del = 1 where id='" + id.ToString() + "'");
                    File.Delete(Server.MapPath(GetFilenameById(id)));
                    BindLV(this.AspNetPager1.StartRecordIndex - 1, this.AspNetPager1.EndRecordIndex);
                    break;
            }
        }
        catch (Exception ex)
        {
            CH.Alert(ex.Message, this.Literal1);
        }
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindLV(this.AspNetPager1.StartRecordIndex - 1, PageSize );
    }
}
