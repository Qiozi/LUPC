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
using LU.Data;

/// <summary>
/// Summary description for MenuHelper
/// </summary>
public class MenuHelper
{
    LU.Data.nicklu2Entities _context;

    public MenuHelper(nicklu2Entities context)
    {
        _context = context;
        //
        // TODO: Add constructor logic here
        //
    }

    public string _nav_menu = string.Empty;

    public string nav_menu
    {
        get
        {
            if (_nav_menu == string.Empty)
            {
                _nav_menu = LoadMenu(_context);

            }
            return _nav_menu;
        }
    }

    public string LoadMenu(nicklu2Entities context)
    {

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<div class=\"menucontainer\"><div class=\"menu\"><ul>");
        DataTable dt = MenuModel.FindMenuTopLevel();
        var parent = MenuModel.MenuModelsByMenuParentSerialNo(context, 0, true);
        for (int i = 0; i < parent.Length; i++)
        {

            sb.Append("<li><a href=\"" + parent[i].menu_child_href + "\">" + parent[i].menu_child_name + "</a>");
            if (parent[i].menu_is_exist_sub == 1)
            {
                var mm = MenuModel.MenuModelsByMenuParentSerialNo(context, parent[i].menu_child_serial_no, true);
                sb.Append("<table><tr><td><ul>");
                for (int j = 0; j < mm.Length; j++)
                {
                    sb.Append("<li><a href=\"" + mm[j].menu_child_href + "\" target=\"" + mm[j].target + "\">" + mm[j].menu_child_name + "</a></li>");
                }
                sb.Append("</ul></td></tr></table>");
            }
            sb.Append("</li>");
        }
        sb.Append("</ul></div></div>");
        return sb.ToString();
    }

    public string LoadMenuNew()
    {

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<ul>");
        DataTable dt = MenuModel.FindMenuTopLevel();
        var parent = MenuModel.MenuModelsByMenuParentSerialNo(_context, 0, true);
        for (int i = 0; i < parent.Length; i++)
        {
            if (i > 6)
                continue;
            sb.Append("<li><a href=\"" + parent[i].menu_child_href + "\" class=\"arc\"><span>" + parent[i].menu_child_name + "</span></a>");
            if (parent[i].menu_is_exist_sub == 1)
            {
                var mm = MenuModel.MenuModelsByMenuParentSerialNo(_context, parent[i].menu_child_serial_no, true);
                sb.Append("<ul class=\"subnav\">");
                for (int j = 0; j < mm.Length; j++)
                {
                    sb.Append("<li><a href=\"" + mm[j].menu_child_href + "\"  target=\"" + mm[j].target + "\"><span>" + mm[j].menu_child_name + "</span></a></li>");
                }
                sb.Append("</ul>");
            }
            sb.Append("</li>");
        }
        sb.Append("<ul>");
        return sb.ToString();
    }
}
