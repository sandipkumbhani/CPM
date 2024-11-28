using CPM.UI.Application.Interface;
using CPM.UI.Application.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPM.UI.Application.Extension
{
    public static class ServiceExtension
    { 
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            
            services.AddScoped<IRegisterServices, RegisterServices>();
            services.AddScoped<ILoginServices, LoginServices>();
            services.AddScoped<IClinicServices, ClinicServices>();
            services.AddScoped<IDoctorServices, DoctorServices>();
            services.AddScoped<IEmailSendService, EmailSendService>();
            services.AddScoped<ISkillServices, SkillServices>();
            services.AddScoped<IPasswordHasher, PasswordHasherService>();
            services.AddScoped<IPatientServices, PatientServices>();
            services.AddScoped<IPatientDiagnosisServices, PatientDiagnosisServices>();
            return services;
        }
    }
}
