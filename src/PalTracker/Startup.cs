using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace PalTracker
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var message = Configuration.GetValue<string>("WELCOME_MESSAGE","Welcome not configured");
          if (string.IsNullOrEmpty(message))          
           {              
               throw new ApplicationException("WELCOME_MESSAGE not configured.");
           }
           services.AddSingleton(sp => new WelcomeMessage(message));

//            PORT
// MEMORY_LIMIT
// CF_INSTANCE_INDEX
// CF_INSTANCE_ADDR
            
            services.AddSingleton(sp=> new CloudFoundryInfo( Configuration.GetValue("PORT","the port not configured "),
                                                             Configuration.GetValue("MEMORY_LIMIT","the MEMORY_LIMIT not configured "),
                                                             Configuration.GetValue("CF_INSTANCE_INDEX","the CF_INSTANCE_INDEX not configured "),
                                                             Configuration.GetValue("CF_INSTANCE_ADDR","the CF_INSTANCE_ADDR not configured ")       )    );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
