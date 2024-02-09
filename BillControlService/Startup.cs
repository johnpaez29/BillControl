using BillBusiness.Handlers;
using BillBusiness.Implementations;
using BillBusiness.Interfaces;
using DataAccess;
using DataAccess.Implements;
using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BillControlService
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
            services.AddCors(service => service.AddPolicy("Policy", builder =>
            {
                builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
            }));

            services.AddControllers();


            AppSettings.ConnectionString = Configuration.GetConnectionString("dbFirebase");

            services.AddScoped<ILog<Log>, LogUserHandler>()
                .AddScoped<ILog<LogBill>, LogBillHandler>()
                .AddScoped<ILogData<Log>, LogUserData>()
                .AddScoped<ILogData<LogBill>, LogBillData>()
                .AddScoped<IServiceData<BillData>, BillServiceData>()
                .AddScoped<IServiceBillData<BillData>, BillServiceData>()
                .AddScoped<IBillHandler, BillHandler>()
                .AddScoped<IServiceData<User>, UserServiceData>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("Policy");

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
