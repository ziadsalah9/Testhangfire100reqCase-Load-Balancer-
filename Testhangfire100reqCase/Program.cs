
using Microsoft.EntityFrameworkCore;
using Testhangfire100reqCase.Background_Services;
using Testhangfire100reqCase.Models;
using Testhangfire100reqCase.orderQueue;

namespace Testhangfire100reqCase
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);




            // Add services to the container.

            builder.Services.AddDbContext<StoreDbcontext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddSingleton<OrderQueue>();
            builder.Services.AddHostedService<OrderProcessor>();
            builder.Services.AddHostedService<ReadOrderWorker>();


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
