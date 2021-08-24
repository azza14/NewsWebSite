using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(NewsWebSite.Areas.Identity.IdentityHostingStartup))]
namespace NewsWebSite.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}