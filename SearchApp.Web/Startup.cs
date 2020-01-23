using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SearchApp.BusinessLayer.Services;
using SearchApp.DataLayer;
using SearchApp.DataLayer.EF;

namespace SearchApp.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
#if RELEASE
            Configuration = configuration;
#else
            Configuration = new ConfigurationBuilder()
            .AddJsonFile($"appsettings.Development.json")
            .Build();
#endif
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
#if DEBUG
            var connection = Configuration["ConnectionString"];
#else
            var connection = Configuration["ConnectionString"];
#endif

            services.AddDbContext<MainContext>(options => // TODO: yep, I know that Web can`t know about DAL. Fix this later
                options.UseSqlServer(connection)
            );

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ISearchService, SearchService>();
            services.AddScoped<IEnginesService, EnginesService>();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Search}/{action=Index}/{id?}");
            });
        }
    }
}
