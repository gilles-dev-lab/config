/*
App_Start
│
├── AppBootstrapper.cs 
├── DependencyConfig.cs 
├── MvcConfig.cs
├── RouteConfig.cs
├── FilterConfig.cs 
└── BundleConfig.cs
*/

public static class AppBootstrapper
{
    public static void Initialize()
    {
        // 1. Configurer Autofac
        DependencyConfig.Register();

        // 2. Configurer MVC
        AreaRegistration.RegisterAllAreas();
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        RouteConfig.RegisterRoutes(RouteTable.Routes);
        BundleConfig.RegisterBundles(BundleTable.Bundles);
    }
}