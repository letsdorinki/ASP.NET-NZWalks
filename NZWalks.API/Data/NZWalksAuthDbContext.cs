using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace NZWalks.API.Data
{
    public class NZWalksAuthDbContext : IdentityDbContext
    {

        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readerRoleId = "0a648764-257f-4f3b-a55b-72ee9a196fde";
            var writerRoleId = "0d7289ba-746b-4a91-a8c9-73498b46f6da";
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                   Id =readerRoleId,
                   ConcurrencyStamp= readerRoleId,
                   Name ="Readre",
                   NormalizedName ="Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id =writerRoleId,
                    ConcurrencyStamp= writerRoleId,
                    Name = "Writer",
                    NormalizedName="Writer".ToUpper()
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
