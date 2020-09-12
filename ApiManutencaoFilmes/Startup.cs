using ApiManutencaoFilmes.Context;
using ApiManutencaoFilmes.DTOs.Mappings;
using ApiManutencaoFilmes.Extensions;
using ApiManutencaoFilmes.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ApiManutencaoFilmes {
    public class Startup {

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {

            //Configura e adiciona o perfil de mapeamento
            var mappingConfig = new MapperConfiguration(mc => {
                mc.AddProfile(new MappingProfile());
            });

            //Cria o mapeamento
            IMapper mapper = mappingConfig.CreateMapper();

            //Registra o serviço de mapeamento
            services.AddSingleton(mapper);

            //Configura o acesso e uso do DB
            services.AddDbContext<ApiManutencaoFilmesContext>(options =>
                     options.UseSqlServer(Configuration.GetConnectionString("ApiManutencaoFilmesContext")));

            //Configura a Autenticação e Autorização na aplicação por login
            services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApiManutencaoFilmesContext>()
                    .AddDefaultTokenProviders();

            //Configura e valida a autenticação pelo JWT Bearer
            services.AddAuthentication(
                JwtBearerDefaults.AuthenticationScheme).
                AddJwtBearer(options =>
                    options.TokenValidationParameters = new TokenValidationParameters {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidAudience = Configuration["TokenConfiguration:Audience"],
                        ValidIssuer = Configuration["TokenConfiguration:Issuer"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    });

            //Configura para os controladores evitarem erro de referência ciclíca
            services.AddControllers()
                    .AddNewtonsoftJson(options => {
                        options.SerializerSettings.ReferenceLoopHandling
                        = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    });

            //Configura o uso do CORS para permitir acesso de outras origens
            services.AddCors(options => {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });

            //Configura a utilização de injeção de dependência do repositório
            services.AddScoped(typeof(IDataRepository<>), typeof(DataRepository<>));

            // In production, the Angular files will be served from this directory
            //services.AddSpaStaticFiles(configuration =>
            //{
            //    configuration.RootPath = "ClientApp/dist";
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {

            if (env.IsDevelopment()) {
                //Identifica diversas Exceções síncronas e assíncronas do pipeline e gera um HTML com erros
                app.UseDeveloperExceptionPage();
            } else {
                //Permite uso do protocolo HSTS para maior segurança
                app.UseHsts();
            }

            //Permite tratamento de erros globais detalhados devolvidos em um objeto (ErrorDetails)
            app.ConfigureExceptionHandler();

            //Utiliza o CORS configurado no ConfigureServices
            app.UseCors("CorsPolicy");

            //Redireciona automaticamente requests de HTTP para HTTPS (Segurança)
            app.UseHttpsRedirection();

            //Permite uso de arquivos estáticos no caminho do request atual
            app.UseStaticFiles();

            //Permite uso do middleware de arquivos estáticos no caminho do request atual em SPA
            //app.UseSpaStaticFiles();

            //Adiciona middleware de roteamento
            app.UseRouting();

            //Adiciona o middleware da autenticação
            app.UseAuthentication();

            //Adiciona o middleware da autorização
            app.UseAuthorization();

            //Utiliza o Endpoints sem rota definida
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });

            //app.UseSpa(spa =>
            //{
            //    // To learn more about options for serving an Angular SPA from ASP.NET Core,
            //    // see https://go.microsoft.com/fwlink/?linkid=864501

            //    spa.Options.SourcePath = "ClientApp";

            //    if (env.IsDevelopment())
            //    {
            //        spa.UseAngularCliServer(npmScript: "start");
            //    }
            //});
        }
    }
}
