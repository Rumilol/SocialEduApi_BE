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


            builder.Services.AddControllers();
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseHttpsRedirection();

            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();
                
            }

            app.UseDeveloperExceptionPage();

            app.Run();

            
        }

    }
}
