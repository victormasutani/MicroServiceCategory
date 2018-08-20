using MicroServiceCategory.API.Services;
using MicroServiceCategory.API.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;

namespace MicroServiceCategory.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            //Habilitando a geração da documentação do Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("category-v1", new Info { Title = "Category API V1", Version = "v1" });
                c.SwaggerDoc("category-v2", new Info { Title = "Category API V2", Version = "v2" });

                var filePath = Path.Combine(System.AppContext.BaseDirectory, "MicroServiceCategory.API.xml");
                c.IncludeXmlComments(filePath);
            });

            services.AddScoped<ICategoryService, CategoryService>();
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            //Habilitando a geração das páginas de UI do Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/category-v1/swagger.json", "Category API V1");
                c.SwaggerEndpoint("/swagger/category-v2/swagger.json", "Category API V2");
            });

            app.UseWelcomePage("/");
        }
    }
}
