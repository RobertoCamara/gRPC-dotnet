using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using POC.GrpcService.ApiClient.Services;
using System;

namespace POC.GrpcService.ApiClient
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

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "POC.GrpcService.ApiClient", Version = "v1" });
            }).AddSwaggerGenNewtonsoftSupport();

            services.AddScoped<IGreeterService, GreeterService>();
            services.AddScoped<ICalculatorService, CalculatorService>();

            services.AddGrpcClient<Calculate.CalculateClient>((services, options) =>
            {
                var calculateApi = "https://localhost:5001";
                options.Address = new Uri(calculateApi);
            });

            services.AddGrpcClient<Greeter.GreeterClient>((services, options) =>
            {
                var greeterApi = "https://localhost:5001";                
                options.Address = new Uri(greeterApi);
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "POC.GrpcService.ApiClient v1"));
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
