using DesafioConexa.Api.Models;
using DesafioConexa.Api.Interfaces;
using DesafioConexa.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DesafioConexa.Api.Utils;
using DesafioConexa.Api.Util;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using System;

namespace DesafioConexa.Api
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

            services.AddScoped<IPlayListService, PlayListService>();
            services.AddScoped<IWeatherService, OpenWeatherService>();
            services.AddScoped<IStreamingService, SpotifyService>();

            services.Configure<OpenWeatherSettings>(Configuration.GetSection("AppSettings:OpenWeather"));
            services.Configure<SpotifySettings>(Configuration.GetSection("AppSettings:Spotify"));

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "GetPlayList");
            });

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
