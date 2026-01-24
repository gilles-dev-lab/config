public static void RegisterRoutes(RouteCollection routes)
{
    routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

    // 🔹 Active le routing par attributs
    routes.MapMvcAttributeRoutes();

    // 🔹 Routes existantes (Controller B)
    routes.MapRoute(
        name: "FicheProduit",
        url: "ps-toto/{code}",
        defaults: new { controller = "B", action = "Special" },
        constraints: new { code = @".+--.+" }
    );


    routes.MapRoute(
        name: "Default",
        url: "{controller}/{action}/{id}",
        defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
    );
}


