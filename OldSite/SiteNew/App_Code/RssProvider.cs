using System;
using System.Web;

/// <summary>
/// RssProvider 的摘要说明
/// </summary>
public class RssProvider : IHttpHandler
{
    public System.Web.Routing.RequestContext RequestContext { get; set; }

    private bool HaveParamter = false;
    public RssProvider(System.Web.Routing.RequestContext context)
    {
        this.RequestContext = context;
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    public void ProcessRequest(HttpContext context)
    {
        DealParamter(context);
    }


    private void DealParamter(HttpContext context)
    {
        var computer = this.RequestContext.RouteData.Values["computer"].ToString();
        var mfp = this.RequestContext.RouteData.Values["mfp"].ToString();
        var sku = 0;//
        try
        {
            sku = Int32.Parse(this.RequestContext.RouteData.Values["sku"].ToString());
        }
        catch { }//context.Server.Execute("/?type=" + type + "&num=" + num);
                 //context.Response.Write("Type:" + type + "<br/>Number:" + num + "<br/>");
                 //context.Response.Write("根据上面的条件设置响应头，输出需要的数据");
        if (computer.ToLower() == "computer")
        {
            if (mfp.ToLower() == "system")
            {
                context.Server.Execute(string.Concat("~/detail_sys.aspx?sku=", sku));  // system 
            }
            else if (mfp.ToLower() != "parts_detail" && sku > 0)
            {
                context.Server.Execute(string.Concat("~/detail_part.aspx?sku=", sku));  // part 
            }
        }
        else if (computer.ToLower()== "computers")
        {
            context.Server.Execute(string.Concat("~/list_part.aspx?catename=", mfp));
            context.Response.End();
        }
        else if(computer .ToLower() == "brand")
        {
            context.Server.Execute(string.Concat("~/list_brand.aspx?brand=", mfp));
            context.Response.End();
        }

        ////string brand = this.RequestContext.RouteData.Values["brand"].ToString();
        ////if (!string.IsNullOrEmpty(brand))
        ////{
        ////    context.Server.Execute(string.Concat("~/detail_part.aspx?sku=", sku)); // brand 
        ////}
    }
}