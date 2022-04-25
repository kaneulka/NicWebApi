using Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Repository;
using Repository.CabinetRepository;
using Repository.DoctorRepository;
using Repository.PatientRepository;
using Repository.PatientSiteRepository;
using Repository.SitetRepository;
using Repository.SpecializationRepository;
using Service;
using Service.CabinetService;
using Service.DoctorService;
using Service.PatientService;
using Service.SiteService;
using Service.SpecializationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModel;

namespace WebApi
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
            services.AddCors();
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //Repositories
            services.AddScoped(typeof(IRepository<Cabinet>), typeof(CabinetRepository));
            services.AddScoped(typeof(IRepository<Doctor>), typeof(DoctorRepository));
            services.AddScoped(typeof(IRepository<Patient>), typeof(PatientRepository));
            services.AddScoped(typeof(IPatientSiteRepository), typeof(PatientSiteRepository));
            services.AddScoped(typeof(IRepository<Site>), typeof(SiteRepository));
            services.AddScoped(typeof(IRepository<Specialization>), typeof(SpecializationRepository));

            //Servises
            services.AddTransient<ICabinetService, CabinetService>();
            services.AddTransient<IService<DoctorViewModel>, DoctorService>();
            services.AddTransient<IService<PatientViewModel>, PatientService>();
            services.AddTransient<ISiteService, SiteService>();
            services.AddTransient<ISpecializationService, SpecializationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseCors(builder => builder.AllowAnyOrigin());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
