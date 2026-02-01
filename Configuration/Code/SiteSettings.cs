public class SiteSettings : ISiteSettings { 
public string CodeSite { get; set; } 
public string Culture { get; set; }
public string Devise { get; set; } 
}
public interface ISiteSettings
{
    string CodeSite { get; }
    string Culture { get; }
    string Devise { get; }
}
