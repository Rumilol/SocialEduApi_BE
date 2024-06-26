using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialEduApi.Data;
using SocialEduApi.Models.Identity;

namespace SocialEduApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var dataSource = builder.Configuration.GetValue<string>("Data_source");

            builder.Services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlite(dataSource));

            builder.Services.AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
            });
            builder.Services.AddAuthorization();
            builder.Services.AddControllers();

            builder.Services.AddCors(options => options.AddPolicy("all", policy => {
                policy.AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            }));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseHttpsRedirection();
            app.MapIdentityApi<ApplicationUser>();
            app.UseAuthorization();
            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var rolemanager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                context.Database.Migrate();
                await DbInitializer.Initialize(userManager, context, rolemanager);
            }
            app.UseCors("all");
            app.UseDeveloperExceptionPage();


            app.Run();
        }

    }
}
