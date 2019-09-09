using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore30
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // ASP.NET Core 3.0

            var urlWithAsync = Url.Action("InfoAsync", "Api");  // Will return (invalid):      /Api/InfoAsync
            var urlWithoutAsync = Url.Action("Info", "Api");    // Will return (valid):        /SystemApi/Info

            return Content($"ASP.NET Core 3.0\n" +
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
