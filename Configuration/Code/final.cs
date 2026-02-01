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
public static class SettingsBuilder {
public static T Build<T>() where T : new() { var settings = new T(); var config = ConfigurationManager.AppSettings; foreach (var prop in typeof(T).GetProperties()) { var attr = prop.GetCustomAttribute<ConfigKeyAttribute>(); if (attr == null) continue; var raw = config[attr.Key]; if (raw == null) throw new ConfigurationErrorsException($"Missing key '{attr.Key}' in Web.config."); var value = Convert.ChangeType(raw, prop.PropertyType); prop.SetValue(settings, value); } return settings; }}



// autofac
builder.Register(c => SettingsBuilder.Build<SiteSettings>())
       .As<ISiteSettings>()
       .SingleInstance();

builder.Register(c => SettingsBuilder.Build<PathsSettings>())
       .As<IPathsSettings>()
       .SingleInstance();

