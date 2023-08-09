using Async_Inn.Data;
using Async_Inn.Interfaces;
using Async_Inn.Models;
using Async_Inn.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers().AddNewtonsoftJson(options =>
       options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
     );
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                // There are other options like this
            })
 .AddEntityFrameworkStores<AsyncInnDbContext>();

            builder.Services.AddTransient<IUserService, IdentityUserService>();


            builder.Services.AddScoped < IHotel,HotelService>();
            builder.Services.AddScoped<IHoteRoom, HotelRoomRepository>();
            builder.Services.AddTransient<IRoomcs, RoomService>();
            builder.Services.AddTransient<IAmenity, AmenityService>();
            builder.Services.AddDbContext<AsyncInnDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }
            );
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}