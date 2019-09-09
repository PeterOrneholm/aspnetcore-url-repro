using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore22
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // ASP.NET Core 2.2

            var urlWithAsync = Url.Action("InfoAsync", "Api");  // Will return (valid):        /SystemApi/Info
            var urlWithoutAsync = Url.Action("Info", "Api");    // Will return (invalid):        

            return Content($"ASP.NET Core 2.2\n" +
                           $"UrlWithAsyncSuffix: {urlWithAsync}\n" +
                           $"UrlWithoutAsyncSuffix: {urlWithoutAsync}\n");
        }
    }

    [Route("/SystemApi/")]
    [ApiController]
    public class ApiController : Controller
    {
        [HttpGet("Info")]
        public async Task<string> InfoAsync()
        {
            return await Task.FromResult("Hello world!");
        }
    }
}
