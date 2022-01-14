using Dapper;
using Dominio;
using Dominio.IRepository.Dapper;
using Dominio.IRepository.EF;
using EFContext;
using Escola_ExercicioBancoDeDados.Controllers;
using Escola_ExercicioBancoDeDados.Repository;
using Escola_ExercicioBancoDeDados.Service;
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
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Escola_ExercicioBancoDeDados
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
            var connectionString = Configuration.GetConnectionString("AppAcademy");

            services.AddControllers();
            services.AddDbContext<EFContext.Context>(
    configuration =>
    {
        configuration.UseOracle(connectionString,
            opt =>
            {
                opt.MigrationsAssembly("Escola_ExercicioBancoDeDados");
            });
    });
            SqlMapper.AddTypeHandler(new OracleGuidTypeHandler());
            SqlMapper.RemoveTypeMap(typeof(Guid));
            SqlMapper.RemoveTypeMap(typeof(Guid?));

            services.AddScoped<IUnityOfWork, UnityOfWork>();

            services.AddSingleton<IAlunoRepositoryDapper, DapperContext.Repository.AlunoRepository>();
            services.AddSingleton<IAlunoRepositoryEF, EFContext.Repository.AlunoRepository>();

            services.AddTransient<AlunoService>();
            /*services.AddTransient<CursoService>();
            services.AddTransient<MateriaService>();
            services.AddTransient<TurmaService>();
            services.AddTransient<ProfessorService>();*/

            services.AddTransient<AlunoRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Escola_ExercicioBancoDeDados", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Escola_ExercicioBancoDeDados v1"));
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
    public class OracleGuidTypeHandler : SqlMapper.TypeHandler<Guid>
    {
        public override void SetValue(IDbDataParameter parameter, Guid guid)
        {
            parameter.Value = guid.ToString();
        }

        public override Guid Parse(object value)
        {
            return new Guid((string)value);
        }
    }
}
