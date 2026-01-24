//---- Implémentation ---//
// Dans .NET Core, 
// les classes de configuration ne sont pas injectées via interface, mais via :
// IOptions<SiteSettings> on injecte donc directement les classes plutôt que 
// de créer des interfaces qui seront supprimées par la suite
// container.RegisterInstance(SettingsFactory.CreateSiteSettings()); 
// container.RegisterInstance(SettingsFactory.CreatePathsSettings()); 
// container.RegisterInstance(SettingsFactory.CreateAppSettings()); 
// container.RegisterInstance(SettingsPaiement.CreateAppSettings()); 
//  Modifier load All par loadTdVModules()... et 
// builder.RegisterModule<TdvModule>(); 
//public class SiteAModule : Module { protected override void Load(ContainerBuilder builder) { builder.RegisterModule<ListProductsModule>(); builder.RegisterModule<ProductsModule>(); builder.RegisterModule<PaiementModule>(); } } 

// /models/HomeViewModel
// public class HomeVm
// {
//     public string Devise { get; } 
//     public HomeVm(SiteSettings settings) 
//     { 
//         Devise = settings.Devise; 
//     }
// }

// // Controller 
// public class HomeController : Controller
// {
//     private readonly SiteSettings _site;

//     public HomeController(SiteSettings site)
//     {
//         _site = site;
//     }

//     public ActionResult Index()
//     {
//         var vm = new HomeVm(_site);
//         return View(vm);
//     }
// }


// // View (Index.cshtml)
// /*
//     @model www.terdav.com.HomeVm

//     Devise du site : @Model.Devise
// */

public static class SettingsFactory
{
    // Hydratation des POCOs
    public static SiteSettings CreateSiteSettings() => new SiteSettings
    {
        CodeSite = Get("codeSite"),
        Culture = Get("culture"),
        Devise = Get("Devise")
    };

    public static PathsSettings CreatePathsSettings() => new PathsSettings
    {
        Destinations = int.Parse(Get("destinations")),
        DefaultPath = Get("defaultPath")
    };

    public static AppSettings CreateAppSettings() => new AppSettings
    {
        ApiUrl = Get("ApiUrl")
    };

    private static string Get(string key) =>
        ConfigurationManager.AppSettings[key]
        ?? throw new ConfigurationErrorsException($"Missing key: {key}");
}

/*
          _____                   _______                   _____                    _____          
         /\    \                 /::\    \                 /\    \                  /\    \         
        /::\    \               /::::\    \               /::\    \                /::\    \        
       /::::\    \             /::::::\    \             /::::\    \              /::::\    \       
      /::::::\    \           /::::::::\    \           /::::::\    \            /::::::\    \      
     /:::/\:::\    \         /:::/~~\:::\    \         /:::/\:::\    \          /:::/\:::\    \     
    /:::/  \:::\    \       /:::/    \:::\    \       /:::/__\:::\    \        /:::/__\:::\    \    
   /:::/    \:::\    \     /:::/    / \:::\    \     /::::\   \:::\    \      /::::\   \:::\    \   
  /:::/    / \:::\    \   /:::/____/   \:::\____\   /::::::\   \:::\    \    /::::::\   \:::\    \  
 /:::/    /   \:::\    \ |:::|    |     |:::|    | /:::/\:::\   \:::\____\  /:::/\:::\   \:::\    \ 
/:::/____/     \:::\____\|:::|____|     |:::|    |/:::/  \:::\   \:::|    |/:::/__\:::\   \:::\____\
\:::\    \      \::/    / \:::\    \   /:::/    / \::/   |::::\  /:::|____|\:::\   \:::\   \::/    /
 \:::\    \      \/____/   \:::\    \ /:::/    /   \/____|:::::\/:::/    /  \:::\   \:::\   \/____/ 
  \:::\    \                \:::\    /:::/    /          |:::::::::/    /    \:::\   \:::\    \     
   \:::\    \                \:::\__/:::/    /           |::|\::::/    /      \:::\   \:::\____\    
    \:::\    \                \::::::::/    /            |::| \::/____/        \:::\   \::/    /    
     \:::\    \                \::::::/    /             |::|  ~|               \:::\   \/____/     
      \:::\    \                \::::/    /              |::|   |                \:::\    \         
       \:::\____\                \::/____/               \::|   |                 \:::\____\        
        \::/    /                 ~~                      \:|   |                  \::/    /        
         \/____/                                           \|___|                   \/____/         
                                                                                                    
*/

// Appsetings
{
  "AppSettings": {
    "ApiUrl": "https://api.local"
  },
  "SiteSettings": {
    "CodeSite": "FR",
    "Culture": "fr-FR",
    "Devise": "EUR"
  },
  "PathsSettings": {
    "Destinations": 5,
    "DefaultPath": "/tmp"
  }
}
// les dépendances 
builder
    .Services 
    .AddOptions<AppSettings>() 
    .Bind(builder.Configuration.GetSection("AppSettings")) 
    .ValidateDataAnnotations()
    .ValidateOnStart();

// Pocos
// > ne changent pas

// Factory 
// supprimée
/* Elle n’est plus nécessaire, car :
- le binder hydrate automatiquement les POCOs
- la validation se fait automatiquement
- l’injection se fait automatiquement

checker EspaceClient var settings = config.GetSection("AppSettings").Get<AppSettings>() ?? throw new Exception("Missing AppSettings section");
> inutile ??

// Appel dans une classe
public class MyService { 
    private readonly AppSettings _settings; 
    public MyService(IOptions<AppSettings> options) 
    { 
        _settings = options.Value; 
    } 
}
*/