using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// Summary description for DropDownListAdapter
/// </summary>

public class DropDownListAdapter : System.Web.UI.WebControls.Adapters.WebControlAdapter
{
    ArrayList m_viewstates = new ArrayList();

    protected override void RenderContents(HtmlTextWriter writer)
    {

        DropDownList list = this.Control as DropDownList;

        string currentOptionGroup;
        List<string> renderedOptionGroups = new List<string>();

        foreach (ListItem item in list.Items)
        {
            if (item.Attributes["OptionGroup"] == null)
            {
                RenderListItem(item, writer);
            }
            else
            {
                currentOptionGroup = item.Attributes["OptionGroup"];

                if (renderedOptionGroups.Contains(currentOptionGroup))
                {
                    RenderListItem(item, writer);
                }
                else
                {
                    if (renderedOptionGroups.Count > 0)
                    {
                        RenderOptionGroupEndTag(writer);
                    }

                    RenderOptionGroupBeginTag(currentOptionGroup, writer);
                    renderedOptionGroups.Add(currentOptionGroup);

                    RenderListItem(item, writer);
                }
            }
        }

        if (renderedOptionGroups.Count > 0)
        {
            RenderOptionGroupEndTag(writer);
        }
    }

    private void RenderOptionGroupBeginTag(string name, HtmlTextWriter writer)
    {
        writer.WriteBeginTag("optgroup");
        writer.WriteAttribute("label", name);
        writer.Write(HtmlTextWriter.TagRightChar);
        writer.WriteLine();
    }

    private void RenderOptionGroupEndTag(HtmlTextWriter writer)
    {
        writer.WriteEndTag("optgroup");
        writer.WriteLine();
    }

    private void RenderListItem(ListItem item, HtmlTextWriter writer)
    {
        writer.WriteBeginTag("option");
        writer.WriteAttribute("value", item.Value, true);

        if (item.Selected)
        {
            writer.WriteAttribute("selected", "selected", false);
        }

        foreach (string key in item.Attributes.Keys)
        {
            writer.WriteAttribute(key, item.Attributes[key]);
        }

        writer.Write(HtmlTextWriter.TagRightChar);
        HttpUtility.HtmlEncode(item.Text, writer);
        writer.WriteEndTag("option");
        writer.WriteLine();
    }
    protected override object SaveAdapterViewState()
    {
        //return base.SaveAdapterViewState();
        if (Page != null)
        {
            DropDownList list = this.Control as DropDownList;
            object[] l_viewState = new object[list.Items.Count];

            int i = 0;
            for (int j = 0; j < list.Items.Count; j++)
            {
                l_viewState[i] = list.Items[j].Attributes["OptionGroup"];
                i += 1;
            }
            l_viewState[i-1] = base.SaveAdapterViewState();
            return l_viewState;
        }
        else
            return base.SaveAdapterViewState();
    }

    protected override void LoadAdapterViewState(object state)
    {
        if (Page != null)
        {
            m_viewstates.Add(state);
            base.LoadAdapterViewState(m_viewstates[m_viewstates.Count - 1]);
        }
        else
            base.LoadAdapterViewState(state);
    }

    protected override void OnPreRender(EventArgs e)
    {
        // base.OnPreRender(e);
        if (Page != null)
        {
            if (m_viewstates != null && m_viewstates.Count > 1)
            {
                DropDownList list = this.Control as DropDownList;

                int count = m_viewstates.Count;
                if (list.Items.Count < count)
                {
                    count = list.Items.Count;
                }
                for (int i = 0; i < count; i++)
                {
                    list.Items[i].Attributes["OptionGroup"] = m_viewstates[i].ToString();
                }
            }
          
        } 
        base.OnPreRender(e);
    }
}
