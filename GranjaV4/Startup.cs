using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GranjaV4.Data;
using GranjaV4.Models;
using GranjaV4.Services;
using Repositorios;

namespace GranjaV4
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
            var azConstring = Configuration.GetValue<string>("Veterinarios:azStorage");
            services.AddTransient<IVeterinariosRepository, AzureVeterinariosRepository>(
                sr =>
                    new AzureVeterinariosRepository(azConstring)
            );
            var azConstring1 = Configuration.GetValue<string>("Animales:azStorage");
            services.AddTransient<IAnimalesRepository, AzureAnimalesRepository>(
                sr =>
                    new AzureAnimalesRepository(azConstring1)
            );
            var azConstring2 = Configuration.GetValue<string>("Empleados:azStorage");
            services.AddTransient<IEmpleadosRepository, AzureEmpleadosRepository>(
                sr =>
                    new AzureEmpleadosRepository(azConstring2)
            );
            var azConstring3 = Configuration.GetValue<string>("Proveedores:azStorage");
            services.AddTransient<IProveedoresRepository, AzureProveedoresRepository>(
                sr =>
                    new AzureProveedoresRepository(azConstring3)
            );
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
