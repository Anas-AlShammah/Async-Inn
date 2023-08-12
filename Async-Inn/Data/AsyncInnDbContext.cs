using Async_Inn.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn.Data
{
    public class AsyncInnDbContext : IdentityDbContext<ApplicationUser>
    {
        public AsyncInnDbContext(DbContextOptions options): base(options)
        {
            
        }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<HotelRoom> hotelRooms { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Hotel>()
            //    .HasMany(e => e.Rooms)
            //    .WithMany(e => e.Hotels)
            //    .UsingEntity<HotelRoom>();
            modelBuilder.Entity<Hotel>()
          .HasMany(h => h.HotelRooms)
          .WithOne(hr => hr.Hotel)
          .HasForeignKey(hr => hr.HotelId);
            modelBuilder.Entity<HotelRoom>().HasKey(hr => new { hr.HotelId, hr.RoomNumber });




            SeedRole(modelBuilder, "DistrictManager", "create", "update", "delete");
            SeedRole(modelBuilder, "PropertyManager");
            SeedRole(modelBuilder, "Agent");

            var hasher = new PasswordHasher<ApplicationUser>();
            var districtManagerUser = new ApplicationUser
            {
                Id = "1", 
                UserName = "adminM",
                NormalizedUserName = "ADMINM", 
                Email = "districtmanager@example.com",
                PhoneNumber = "1234567890",
                NormalizedEmail = "DISTRICTMANAGER@EXAMPLE.COM", 
                EmailConfirmed = true,
                LockoutEnabled = false
            };
            districtManagerUser.PasswordHash = hasher.HashPassword(districtManagerUser, "Admin!23");

            modelBuilder.Entity<ApplicationUser>().HasData(districtManagerUser);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "districtmanager", 
                    UserId = districtManagerUser.Id
                }
            );
  


            modelBuilder.Entity<Hotel>().HasData(
                new Hotel { Id = 1, Name = "Hilton Downtown", StreetAddress = "123 Main St", City = "Seattle", State = "Washington", Country = "United States", Phone = 12345678 },
                new Hotel { Id = 2, Name = "Marriott Waterfront", StreetAddress = "456 Pine St", City = "Seattle", State = "Washington", Country = "United States", Phone = 9876543 },
                new Hotel { Id = 3, Name = "Sheraton Grand", StreetAddress = "789 Olive St", City = "Los Angeles", State = "California", Country = "United States", Phone = 55555555 }
            );

           
            modelBuilder.Entity<Room>().HasData(
                new Room { Id = 1, Name = "Standard Room", Layout = 1 },
                new Room { Id = 2, Name = "Deluxe Suite", Layout = 2 },
                new Room { Id = 3, Name = "Penthouse Suite", Layout = 3 }
            );
            modelBuilder.Entity<HotelRoom>().HasData(
       new HotelRoom { HotelId = 1, RoomId = 1, RoomNumber = 101, Rate = 150.0m, PetFriendly = true },
       new HotelRoom { HotelId = 1, RoomId = 2, RoomNumber = 102, Rate = 180.0m, PetFriendly = false },
       new HotelRoom { HotelId = 2, RoomId = 1, RoomNumber = 201, Rate = 160.0m, PetFriendly = true }
      
             );

            modelBuilder.Entity<Amenity>().HasData(
                new Amenity { Id = 1, Name = "Free Wi-Fi" },
                new Amenity { Id = 2, Name = "Swimming Pool" },
                new Amenity { Id = 3, Name = "Fitness Center" }
            );
        }
        private int nextId = 1; // we need this to generate a unique id on our own
        private void SeedRole(ModelBuilder modelBuilder, string roleName, params string[] permissions)
        {
            var role = new IdentityRole
            {
                Id = roleName.ToLower(),
                Name = roleName,
                NormalizedName = roleName.ToUpper(),
                ConcurrencyStamp = Guid.Empty.ToString()
            };

            modelBuilder.Entity<IdentityRole>().HasData(role);

            // Go through the permissions list (the params) and seed a new entry for each
            var roleClaims = permissions.Select(permission =>
            new IdentityRoleClaim<string>
            {
                Id = nextId++,
                RoleId = role.Id,
                ClaimType = "permissions", // This matches what we did in Startup.cs
                ClaimValue = permission
            }).ToArray();

            modelBuilder.Entity<IdentityRoleClaim<string>>().HasData(roleClaims);
        }
    }
}
