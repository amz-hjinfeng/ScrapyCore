using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScrapyCore.Apis;
using ScrapyCore.Core;
using ScrapyCore.Core.Platform;
using ScrapyCore.Core.Platform.System;
using System.Reflection;

namespace ScrapyCore.Utralisks.WebHosting
{
    public class Startup
    {
        public static ISystemController SystemController { get; set; }
        private Bootstrap bootstrap = Bootstrap.DefaultInstance;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var assembly = typeof(ApiConst).GetTypeInfo().Assembly;
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddApplicationPart(assembly); ;
            services.AddSingleton(x => SystemController.MessagePipline.Entrance);
            services.AddSingleton(x => bootstrap.GetCachedFromVariableSet("HeartbeatCache"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
