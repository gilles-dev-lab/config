/*protected void Application_Start()
{
    RouteTable.Routes.MapMvcAttributeRoutes();

    RouteTable.Routes.ConstraintMap.Add(
        "NoFPNoAvisConstraint",
        typeof(NoFPNoAvisConstraint)
    );

    RouteConfig.RegisterRoutes(RouteTable.Routes);
}
*/

using System.Text.RegularExpressions;
using System.Web;
using System.Web.Routing;

public class NoFPNoAvisConstraint : IRouteConstraint
{
    public bool Match(
        HttpContextBase httpContext,
        Route route,
        string parameterName,
        RouteValueDictionary values,
        RouteDirection routeDirection)
    {
        if (!values.ContainsKey(parameterName))
            return false;

        var value = values[parameterName]?.ToString();
        return !string.IsNullOrEmpty(value) && !value.Contains("--");
    }
}

