using MapsterMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace RsCase.Employee.Api
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
            services.AddDbContext<EmployeeDbContext>(options => options.UseInMemoryDatabase(databaseName: "EmployeeDb"));

            #region DI Registration
            services.AddSingleton<IMapper, Mapper>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            #endregion

            #region Auth
            services.AddAuthentication("token")
                .AddJwtBearer("token", options =>
                {
                    options.Authority = "https://localhost:5001";
                    options.TokenValidationParameters.ValidateAudience = false;

                    options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthConstants.RoleAdmin, policy =>
                    policy.RequireClaim("scope", AuthConstants.ReadScope, AuthConstants.WriteScope));

                options.AddPolicy(AuthConstants.RoleUser, policy =>
                    policy.RequireClaim("scope", AuthConstants.ReadScope));
            });


            #endregion

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RsCase.Employee.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RsCase.Employee.Api v1"));
            }
            app.InitializeSampleData();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
