[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(ASP.App_Start.Combres), "PreStart")]
namespace ASP.App_Start {
	using System.Web.Routing;
	using global::Combres;
	
    public static class Combres {
        public static void PreStart() {
            RouteTable.Routes.AddCombresRoute("Combres");
        }
    }
}