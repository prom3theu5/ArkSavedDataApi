using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;
using Microsoft.Framework.DependencyInjection;
using ArkPlayerDataApi.Models;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.Runtime;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;

namespace ArkPlayerDataApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IApplicationEnvironment app)
        {
            Configuration = new ConfigurationBuilder(app.ApplicationBasePath)
            .AddJsonFile("settings.json")
            .AddEnvironmentVariables()
            .Build();
        }

        public IConfiguration Configuration { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().Configure<MvcOptions>(options => {

                var jsonFormatter = options.OutputFormatters
                    .OfType<JsonOutputFormatter>()
                    .First();

                jsonFormatter.SerializerSettings.ReferenceLoopHandling =
                    ReferenceLoopHandling.Ignore;
                jsonFormatter.SerializerSettings.PreserveReferencesHandling =
                    PreserveReferencesHandling.None;
            });

            services.AddSingleton<IPlayerRepository, PlayerRepository>();
            services.Configure<ApplicationSettings>(Configuration.GetConfigurationSection("ApplicationSettings"));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
