using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScrapyCore.Core;
using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;

namespace ScrapyCore.HeartOfSwarm
{
    public class Startup : IStartup
    {
        private Bootstrap bootstrap;
        public Startup(IConfiguration configuration)
        {
            bootstrap = new Bootstrap();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //services.AddSingleton(x => bootstrap.GetCachedFromVariableSet("default-cache"));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var builder = new ContainerBuilder();
            builder.RegisterInstance(bootstrap.GetCachedFromVariableSet("HeartbeatCache"));
            builder.Populate(services);
            var applicationContainer = builder.Build();
            return new AutofacServiceProvider(applicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        //{
        //    if (env.IsDevelopment())
        //    {
        //        app.UseDeveloperExceptionPage();
        //    }
        //    app.UseMvc();
        //}

        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app.UseDeveloperExceptionPage();
            app.UseMvc();

        }
    }
}
