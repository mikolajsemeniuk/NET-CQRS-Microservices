using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Read.Data;
using Read.Interfaces;
using Read.Services;
using Read.Subscribers;

namespace Read
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
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            // services.AddMassTransit(options =>
            // {
            //     // OPTIONAL FOR ALL CONSUMERS:
            //     //
            //     // options.AddConsumers(Assembly.GetEntryAssembly());

            //     // OPTIONAL FOR CERTAIN CONSUMERS:
            //     //
            //     options.AddConsumer<ArticleAddedConsumer>();
            //     // options.AddConsumer<UpdateArticleSubscriber>();
            //     // options.AddConsumer<RemoveArticleSubscriber>();

            //     options.UsingRabbitMq((context, configuration) => 
            //     {
            //         configuration.Host(Configuration["EventBusSettings:HostAddress"]);
            //         // OPTIONAL:
            //         //
            //         // configuration.ConfigureEndpoints(context, 
            //             // new KebabCaseEndpointNameFormatter("write", false));

            //         configuration.ReceiveEndpoint("article-queue", configure => 
            //         {
            //             configure.ConfigureConsumer<ArticleAddedConsumer>(context); 
            //             // configure.ConfigureConsumer<UpdateArticleSubscriber>(context); 
            //             // configure.ConfigureConsumer<RemoveArticleSubscriber>(context); 
            //         });
            //     });
            // });
            // services.AddMassTransitHostedService();

            services.AddMassTransit(options =>
            {
                options.AddConsumer<ArticleAddedConsumer>();
                options.AddConsumer<ArticleUpdatedConsumer>();
                options.AddConsumer<ArticleRemovedConsumer>();
                options.UsingRabbitMq((context, configuration) => 
                {
                    configuration.Host(Configuration["EventBusSettings:HostAddress"]);
                    configuration.ReceiveEndpoint("article-queue", configure =>
                    {
                        configure.ConfigureConsumer<ArticleAddedConsumer>(context);
                        configure.ConfigureConsumer<ArticleUpdatedConsumer>(context);
                        configure.ConfigureConsumer<ArticleRemovedConsumer>(context);
                    });
                });
            });
            services.AddMassTransitHostedService();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Read", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Read v1"));
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
