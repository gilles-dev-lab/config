public static class DependencyConfig
{
    public static IContainer Container { get; private set; }

    public static void Register()
    {
        var builder = new ContainerBuilder();

        // Controllers du site
        builder.RegisterControllers(typeof(MvcApplication).Assembly);

        // Module commun
        builder.RegisterModule<TdvModule>(); 

        // Settings POCO
        builder.RegisterInstance(SettingsFactory.CreateSiteSettings()).AsSelf().SingleInstance();
        builder.RegisterInstance(SettingsFactory.CreatePathsSettings()).AsSelf().SingleInstance();
        builder.RegisterInstance(SettingsFactory.CreateAppSettings()).AsSelf().SingleInstance();

        Container = builder.Build();
        DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));
    }
}
