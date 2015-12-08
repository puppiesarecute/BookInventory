using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BookInventory.Startup))]
namespace BookInventory
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
