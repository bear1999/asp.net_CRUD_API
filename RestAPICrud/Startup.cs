using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using RestAPICrud.Servcies.Employee;
using RestAPICrud.Models;
using System.Text;
using FluentValidation.AspNetCore;
using RestAPICrud.Helpers;
using RestAPICrud.Auth;
using Microsoft.AspNetCore.Authorization;

namespace RestAPICrud
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        //private readonly ICustomAuthorize _customAuthorize;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "CRUD API", Version = "v1" });
            });
            //services.AddControllersWithViews();
            services.AddRazorPages();
            //Add Db Connect String
            services.AddDbContextPool<EmployeeContext>(options => options.UseSqlServer(Configuration.GetConnectionString("EmployeeContextConnectionString")));

            //Add Empployee Interface, Service
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUploadFile, UploadFile>();
            services.AddScoped<IAuthorizationHandler, IsDeleteHandler>();

            services.AddAuthorization(x =>
            {
                x.AddPolicy("Staff", policy =>
                    policy.RequireRole("Admin", "User"));
                x.AddPolicy("isDelete", policy =>
                    policy.Requirements.Add(new IsDelete(false)));
            });

            //Config appsettings.json
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.UTF8.GetBytes(appSettings.SerectKey);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "NgocSy",
                    ValidAudience = "NgocSy",
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            services.AddControllers()
                .AddNewtonsoftJson(options => //Json to Claims
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddFluentValidation(x =>
                {
                    x.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                    x.RegisterValidatorsFromAssemblyContaining<Startup>();
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //For JWT
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                //For Controller
                endpoints.MapDefaultControllerRoute();
                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CRUD API");
            });
        }
    }
}
