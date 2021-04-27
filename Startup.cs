using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Repository;
using TarefasApi.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace TarefasApi
{
  
    public class Startup
    {
          public IConfiguration Configuration {get; set;}
    
        public Startup(IConfiguration configuration){
                Configuration = configuration;
    }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

             var key = Encoding.ASCII.GetBytes("umtokenmuitograndeparanaoserdescoberto");
            services.AddAuthentication(options =>{
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(options =>{
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters{
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer =false,
                    ValidateAudience = false,
                };
            });


           // services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("BDTarefas"));
            services.AddDbContext<DataContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("Heroku")));

            services.AddTransient<ITarefaRepository, TarefaRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
     
         
        }
    }
}
