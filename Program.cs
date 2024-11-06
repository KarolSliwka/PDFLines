using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using Hangfire;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.EntityFrameworkCore;
using PDFLines.Data;

namespace PDFLines
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add ToastNotification
            builder.Services.AddNotyf(config =>
            {
                config.DurationInSeconds = 5;
                config.IsDismissable = true;
                config.Position = NotyfPosition.TopRight;
            });

            builder.Services.AddControllers().AddNewtonsoftJson();

            builder.Services.AddDbContext<TCZNT5000>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("TCZNT5000") ?? throw new InvalidOperationException("Connection string 'TCZNT5000' not found.")));
            builder.Services.AddDbContext<TCZNT58>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("TCZNT58") ?? throw new InvalidOperationException("Connection string 'TCZNT58' not found.")));

            builder.Services.AddControllers(
                options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

            builder.Services.AddScoped<IClaimsTransformation, ClaimsTransformer>();

            builder.Services.AddAuthentication(IISDefaults.AuthenticationScheme);
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("SuperOnly", policy =>
                {
                    policy.AddAuthenticationSchemes("Windows");
                    policy.RequireRole("super");
                });
                options.AddPolicy("AdminOnly", policy =>
                {
                    policy.AddAuthenticationSchemes("Windows");
                    policy.RequireRole("super", "admin");
                });
                options.AddPolicy("VisorsOnly", policy =>
                {
                    policy.AddAuthenticationSchemes("Windows");
                    policy.RequireRole("super", "admin", "visor");
                });
                options.AddPolicy("AllUsers", policy =>
                {
                    policy.AddAuthenticationSchemes("Windows");
                    policy.RequireRole("super", "admin", "visor", "user");
                });
            });

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Contexts
            builder.Services.AddDbContext<TCZNT5000>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TCZNT5000")));
            builder.Services.AddDbContext<TCZNT58>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TCZNT58")));

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseNotyf();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
                await next();
            });

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}