using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RealEstateMediaPlatform.API.Enums;
using RealEstateMediaPlatform.API.Models;
using System.Reflection.Emit;



namespace RealEstateMediaPlatform.API.Data
{
    public class RealEstateDbContext : IdentityDbContext<User, Role, String>
    {
        public DbSet<Agent> Agents { get; set; }
        public RealEstateDbContext(DbContextOptions<RealEstateDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }


        public DbSet<PhotographyCompany> PhotographyCompanies { get; set; }
        public DbSet<ListingCase> ListingCases { get; set; }
        public DbSet<CaseContact> CaseContacts { get; set; }
        public DbSet<MediaAsset> MediaAssets { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Agent>().ToTable("Agents");
            builder.Entity<PhotographyCompany>().ToTable("PhotographyCompanies");

            builder.Entity<ListingCase>()
            .HasMany(l => l.Agents)
            .WithMany(a => a.ListingCases)
            .UsingEntity<Dictionary<string, object>>(
            "AgentListingCase",
            j => j.HasOne<Agent>().WithMany().HasForeignKey("AgentId").OnDelete(DeleteBehavior.Restrict),
            j => j.HasOne<ListingCase>().WithMany().HasForeignKey("ListingCaseId").OnDelete(DeleteBehavior.Restrict)
                 );

            builder.Entity<ListingCase>()
                .Property(x => x.Latitude)
                .HasPrecision(9, 6);

            builder.Entity<ListingCase>()
                .Property(x => x.Longitude)
                .HasPrecision(9, 6);

            builder.Entity<Agent>()
           .HasMany(a => a.PhotographyCompanys)
           .WithMany(p => p.Agents)
           .UsingEntity<Dictionary<string, object>>(
           "AgentPhotographyCompany",
           j => j.HasOne<PhotographyCompany>().WithMany().HasForeignKey("PhotographyCompanyId").OnDelete(DeleteBehavior.Restrict),
           j => j.HasOne<Agent>().WithMany().HasForeignKey("AgentId").OnDelete(DeleteBehavior.Restrict)
            );

            builder.Entity<MediaAsset>().HasOne(m => m.ListingCase).WithMany(l => l.MediaAssets)
                .HasForeignKey(m => m.ListingCaseId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<MediaAsset>().HasOne(m => m.User).WithMany().HasForeignKey(m => m.UserId).OnDelete(DeleteBehavior.Restrict);

            SeedRoles(builder);
        }

        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<Role>().HasData(
                new Role
                {
                    Id = "1",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "seed-role-admin"

                },

                new Role
                {
                    Id = "2",
                    Name = "Agent",
                    NormalizedName = "AGENT",
                    ConcurrencyStamp = "seed-role-agent"
                },

                new Role
                {
                    Id = "3",
                    Name = "PhotographyCompany",
                    NormalizedName = "PHOTOGRAPHYCOMPANY",
                    ConcurrencyStamp = "seed-role-photographycompany"
                }
                );
        }
    }


}

