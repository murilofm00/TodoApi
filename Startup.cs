using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi
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
            services.AddCors(options =>{
            options.AddDefaultPolicy(
                builder =>
                {
                    builder.WithOrigins("http://localhost:19006").WithMethods("PUT", "DELETE", "GET", "POST").AllowAnyHeader();
                });
            });
            services.AddDbContext<TodoContext>(opt =>
                                               opt.UseSqlServer(@"Server=DESKTOP-EJOO4GU\SQLEXPRESS;Database=TodoAPI;Trusted_Connection=True;")
                                               );
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}