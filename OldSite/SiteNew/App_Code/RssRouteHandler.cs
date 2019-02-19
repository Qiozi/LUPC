using System;
using System.Web;
using System.Web.Routing;
/// <summary>
/// RssRouteHandler 的摘要说明
/// </summary>
public class RssRouteHandler : IRouteHandler
{
    public RssRouteHandler()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    public IHttpHandler GetHttpHandler(RequestContext requestContext)
    {
        return new RssProvider(requestContext);
    }
}