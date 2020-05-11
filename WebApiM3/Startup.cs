using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using WebApiM3.Context;
using WebApiM3.Entities;
using WebApiM3.Helpers;
using WebApiM3.Models;
using WebApiM3.Services;

namespace WebApiM3
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
            //IhostedServices
           // services.AddTransient<Microsoft.Extensions.Hosting.IHostedService, IHotedServiceExample>();

            //IhostedServices Con Entity framework
            services.AddTransient<Microsoft.Extensions.Hosting.IHostedService, ConsumeScopedService>();

            //AutoMapper
            services.AddAutoMapper(Configuration => {
                Configuration.CreateMap<Autor, AutorDTO>(); //Origen, Destino
                Configuration.CreateMap<AutorCreacionDTO, Autor>();
            },  typeof(Startup));

            //Filtros Personalizados.
            services.AddScoped<MiFiltroAccion>();

            //Entity Framework
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString")));


            services.AddControllers()
                .AddNewtonsoftJson();


            //Quitar error de referencia Ciclica.
            services.AddMvcCore().AddNewtonsoftJson(options =>

                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
              

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
