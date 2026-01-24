using App_Start
namespace tdv
{
  public class Global : System.Web.HttpApplication
  {
    protected void Application_Start() {
      AppBootstrapper.Initialize();
    }
  }
}