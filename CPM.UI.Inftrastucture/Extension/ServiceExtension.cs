using CPM.UI.Domain.Interfaces;
using CPM.UI.Inftrastucture.Provider;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CPM.UI.Inftrastucture.Extension
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddEfcoreInfrastrucureService(this IServiceCollection services)
        {
            services.AddScoped<IRegisterAdaptor, RegisterAdaptor>();
            services.AddScoped<ILoginAdaptor, LoginAdaptor>();
            services.AddScoped<IClinicAdaptor, ClinicAdaptor>();
            services.AddScoped<IDoctorAdaptor, DoctorAdaptor>();
            services.AddScoped<ISkillAdaptor, SkillAdaptor>();
            services.AddScoped<IPatientAdaptor, PatientAdaptor>();
            services.AddScoped<IPatientDiagnosisAdaptor, PatientDiagnosisAdaptor>();
            return services;
        }
    }
}
    