
using AngularApp2.Server.Service;
using Azure.Storage.Blobs;
using System.Buffers;

namespace AngularApp2.Server;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.
            Configure<CustomSettings>(builder.Configuration.GetSection("MyCustomOptions"));

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddSingleton<IBlobService, BlobService>();
        builder.Services.AddSingleton(_ => new BlobServiceClient(builder.Configuration.GetConnectionString("blobConnection")));


        var app = builder.Build();

        app.UseCors(cfg =>
        {
            cfg.AllowAnyHeader()
            .AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader();
        });

        app.UseDefaultFiles();
        app.UseStaticFiles();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.MapFallbackToFile("/index.html");

        app.Run();

    }
}
