// attribute
[AttributeUsage(AttributeTargets.Property)]
public class ConfigKeyAttribute : Attribute
{
    public string Key { get; }
    public ConfigKeyAttribute(string key) => Key = key;
}

// pocos + ajouter interface 
public class SiteSettings : ISiteSettings
{
    [ConfigKey("codeSite")]
    public string CodeSite { get; set; }

    [ConfigKey("culture")]
    public string Culture { get; set; }

    [ConfigKey("Devise")]
    public string Devise { get; set; }

    [ConfigKey("MaxUsers")]
    public int MaxUsers { get; set; }
}

// create generique
public T CreateSettings<T>() where T : new()
{
    var settings = new T();

    foreach (var prop in typeof(T).GetProperties())
    {
        var attr = prop.GetCustomAttribute<ConfigKeyAttribute>();
        if (attr == null)
            continue;

        var raw = _config[attr.Key];
        if (raw == null)
            throw new ConfigurationErrorsException($"Missing key '{attr.Key}' in Web.config.");

        var value = Convert.ChangeType(raw, prop.PropertyType);
        prop.SetValue(settings, value);
    }

    return settings;
}


// Factory 
public class SettingsFactory : ISettingsFactory
{
    public ISiteSettings CreateSiteSettings() => CreateSettings<SiteSettings>();
    public IPathsSettings CreatePathsSettings() => CreateSettings<PathsSettings>();
}


// autofac || en core on supprime tout et on remplace par services.Configure<SiteSettings>(Configuration.GetSection("Site"));

builder.RegisterType<SettingsFactory>()
       .As<ISettingsFactory>()
       .SingleInstance();

builder.Register(c => c.Resolve<ISettingsFactory>().CreateSiteSettings())
       .As<ISiteSettings>()
       .SingleInstance();
