using AuthenticationService.IoC;
using AuthenticationService.Shared.ValueObject;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AuthenticationService.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            DatabaseSettings = new DatabaseSettings
            {
                ConnectionString = Configuration.GetSection("DatabaseSettings:ConnectionString").Value,
                Database = Configuration.GetSection("DatabaseSettings:Database").Value
            };
        }

        public IConfiguration Configuration { get; }

        public DatabaseSettings DatabaseSettings { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddProjectDependencies();

            services.AddMediatRDependency();            

            services.AddDatabaseDependency(DatabaseSettings);
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
