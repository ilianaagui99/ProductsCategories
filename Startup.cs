using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductsyCategories.Models;
using Microsoft.EntityFrameworkCore;

using  Microsoft.AspNetCore.Server.Kestrel.Core;


namespace ProductsyCategories
{
            public class Startup
            {
                public Startup(IConfiguration configuration)
                {
                    Configuration = configuration;
                }

                public IConfiguration Configuration { get; }
                public void ConfigureServices (IServiceCollection services)
                {
                    services.AddSession();
                    services.AddMvc(option => option.EnableEndpointRouting = false);
                    services.AddDbContext<MyContext>(options => options.UseMySql (Configuration["DBInfo:ConnectionString"]));
                    services.Configure<KestrelServerOptions>(options =>
                    {
                        options.Limits.MaxRequestBodySize = 6291456; // if don't set default value is: 30 MB
                    });
                }
                public void Configure (IApplicationBuilder app, IWebHostEnvironment env)
                {
                    if (env.IsDevelopment())
                    {
                        app.UseDeveloperExceptionPage();
                    }
                    app.UseRouting();
                    app.UseSession(); // must be before UseMvc()
                    app.UseStaticFiles();
                    app.UseMvc();
                }
            }
        }
