using System;
using System.IO;
using System.Text;

public partial class Q_Admin_UC_main_menus : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MenuHelper mh = new MenuHelper();
            //Literal1.Text = mh.LoadMenuNew().ToString();
            string filename = Server.MapPath("/export/menu.htm");
            bool IsCreate = false;
            string content  = "";
            if (!File.Exists(filename))
            {
                IsCreate = true;
            }
            else
            {
                FileInfo fi = new FileInfo(filename);
                if (fi.LastWriteTime.ToString("yyyyMMdd") != DateTime.Now.ToString("yyyyMMdd"))
                {
                    IsCreate = true;
                }
                else
                {
                    StreamReader sr = new StreamReader(filename);
                    content = sr.ReadToEnd();
                    sr.Close();
                }
            }
            if (IsCreate)
            {
                content = mh.LoadMenu().ToString();
                StreamWriter sw = new StreamWriter(filename, false, Encoding.UTF8);
                sw.Write(content);
                sw.Close();
            }
            this.lbl_menu_string.Text = content;
        }
    }

}
