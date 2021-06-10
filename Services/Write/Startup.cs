using System.Reflection;
using MassTransit;
using MassTransit.Definition;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Write.Data;
using Write.Interfaces;
using Write.Services;

namespace Write
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
            services.AddMassTransit(options =>
            {
                // OPTIONAL FOR ALL CONSUMERS (All microservices has to have this line):
                //
                // options.AddConsumers(Assembly.GetEntryAssembly());

                // OPTIONAL FOR CERTAIN CONSUMERS:
                //
                // options.AddConsumer<ArticleAddedConsumer>();
                // options.AddConsumer<UpdateArticleSubscriber>();
                // options.AddConsumer<RemoveArticleSubscriber>();

                options.UsingRabbitMq((context, configuration) => 
                {
                    configuration.Host(Configuration["EventBusSettings:HostAddress"]);
                    // OPTIONAL:
                    //
                    // configuration.ConfigureEndpoints(context, 
                        // new KebabCaseEndpointNameFormatter("write", false));

                    // configuration.ReceiveEndpoint("article-queue", configure => 
                    // {
                    //     configure.ConfigureConsumer<ArticleAddedConsumer>(context);
                    // });
                });
            });
            services.AddMassTransitHostedService();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Write", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Write v1"));
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
