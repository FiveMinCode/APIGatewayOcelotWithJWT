
using Auth.Shared;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace OcelotGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddJsonFile("OcelotConfig.json", false, false);

            builder.Services.AddOcelot(builder.Configuration);
            builder.Services.AddJwtAuthentication();
            builder.Services.AddCors();

            var app = builder.Build();
            app.UseCors(option => option
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("Content-Dispostion"));

            app.UseCors(option => option.AllowCredentials());


           app.UseOcelot();

            app.UseAuthentication();
            app.UseAuthorization();

            app.Run();
        }
    }
}
