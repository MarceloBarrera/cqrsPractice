﻿using Api.Utils;
using Logic.Dtos;
using Logic.Students;
using Logic.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSingleton(new SessionFactory(Configuration["ConnectionString"]));
            //services.AddScoped<UnitOfWork>();
            services.AddTransient<UnitOfWork>(); //this version has not dispose method that is why Transient

            services.AddTransient<ICommandHandler<EditPersonalInfoCommand>, EditPersonalInfoCommandHandler>();
            services.AddTransient<ICommandHandler<RegisterCommand>, RegisterCommandHandler>();
            services.AddTransient<ICommandHandler<UnregisterCommand>, UnregisterCommandHandler>();
            services.AddTransient<ICommandHandler<EnrollCommand>, EnrollCommandHandler>();
            services.AddTransient<ICommandHandler<TransferCommand>, TransferCommandHandler>();
            services.AddTransient<ICommandHandler<DisenrollCommand>, DisenrollCommandHandler>();

            services.AddTransient<IQueryHandler<GetListQuery, List<StudentDto>>, GetListQueryHandler>();
            services.AddSingleton<Messages>();

        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandler>();
            app.UseMvc();
        }
    }
}
