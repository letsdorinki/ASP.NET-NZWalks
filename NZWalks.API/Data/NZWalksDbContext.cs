using Microsoft.EntityFrameworkCore;
using NZWalks.API.models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Difficulty> Difficulties { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Walk> Walk { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //seed data for Difficulties 
            //Easy ,Medium ,Hard
            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    ID = Guid.Parse("483e27ce-e678-46d9-b125-1d95eab80963"),
                    Name= "Easy"
                },
                new Difficulty()
                {
                     ID = Guid.Parse("f5de9f4a-1b72-4bde-b87a-4aaadc103a44"),
                    Name= "Medium"

                },
                new Difficulty()
                {
                     ID =Guid.Parse("337f7ec6-93e2-4e65-b520-6cc1fa6996d8") ,
                    Name= "Hard"
                      }



            };
            //seed difficulties to the database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            //seed data for Regions
            var regions = new List<Region>
            {
                new Region()
                {
                    Id = Guid.Parse("568ff32d-e4a8-4ac0-b928-6d10a37ba162"),
                    Name = "Auckland",
                    Code= "AKL",
                    RegionImageUrl="https://media.istockphoto.com/id/2174551157/photo/cyber-security-data-protection-business-technology-privacy-concept.jpg?s=1024x1024&w=is&k=20&c=7OXl5_jOy6XukjgOBa8YePAKDE9ZlpBFDV5rBTCS3Vs="
                },
                 new Region()
                {
                    Id = Guid.Parse("43bb2362-df2f-4411-b074-fccd272dc5bd"),
                    Name = "Northland",
                    Code= "NTL",
                    RegionImageUrl=null
                },
                  new Region()
                {
                    Id = Guid.Parse("dec75c94-5348-43f3-9e15-2bf168f3ab5c"),
                    Name = "Bayof Plenty",
                    Code= "BOP",
                    RegionImageUrl=null 
                },
                   new Region()
                {
                    Id = Guid.Parse("a3f2763f-d544-4a56-ab2b-14c00f9ced44"),
                    Name = "Wellington",
                    Code= "WGN",
                    RegionImageUrl=null
                },
                      new Region()
                {
                    Id = Guid.Parse("e1d9e47d-e0ea-418a-b881-f9d7be198076"),
                    Name = "Nelson",
                    Code= "NSN",
                    RegionImageUrl=null
                },
                       new Region()
                {
                    Id = Guid.Parse("89fcfce3-a998-4f2e-9937-dcbdd7c250ac"),
                    Name = "Southland",
                    Code= "STL",
                    RegionImageUrl=null
                },
            };

            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}