
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealEstateMediaPlatform.API.Models;
using RealEstateMediaPlatform.API.Models.Entities;

namespace RealEstateMediaPlatform.API.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        // DbSet properties for your entities
        public DbSet<Agent> Agents { get; set; }
        public DbSet<ListingCase> ListingCases { get; set; }
        public DbSet<MediaAsset> MediaAssets { get; set; }
        public DbSet<PhotographyCompany> PhotographyCompanies { get; set; }
        public DbSet<AgentListingCase> AgentListingCases { get; set; }
        public DbSet<AgentPhotographyCompany> AgentPhotographyCompanies { get; set; }
        public DbSet<CaseContact> CaseContacts { get; set; }

        protected override void OnModelCreating(ModelBuilder moduleBuilder)
        {
            base.OnModelCreating(moduleBuilder);
            moduleBuilder.Entity<AgentListingCase>().HasKey(a => new { a.AgentId, a.ListingCaseId });

            moduleBuilder.Entity<AgentPhotographyCompany>().HasKey(a => new { a.AgentId, a.PhotographyCompanyId });

            moduleBuilder.Entity<CaseContact>().HasKey(c => c.ContactId);

            moduleBuilder.Entity<MediaAsset>()
                .HasOne(m => m.ListingCase)
                .WithMany()
                .HasForeignKey(m => m.ListingCaseId)
                .OnDelete(DeleteBehavior.Restrict);

            moduleBuilder.Entity<MediaAsset>()
                .HasOne(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            moduleBuilder.Entity<CaseContact>()
                .HasOne(c => c.ListingCase)
                .WithMany()
                .HasForeignKey(c => c.ListingCaseId)
                .OnDelete(DeleteBehavior.Restrict);

            moduleBuilder.Entity<AgentListingCase>()
                .HasOne(a => a.Agent)
                .WithMany(a => a.AgentListingCases)
                .HasForeignKey(a => a.AgentId)
                .OnDelete(DeleteBehavior.Restrict);

            moduleBuilder.Entity<AgentListingCase>()
                .HasOne(a => a.ListingCase)
                .WithMany(l => l.AgentListingCases)
                .HasForeignKey(a => a.ListingCaseId)
                .OnDelete(DeleteBehavior.Restrict);

            moduleBuilder.Entity<Agent>()
                .HasOne(a => a.User)
                .WithOne()
                .HasForeignKey<Agent>(a => a.Id)
                .OnDelete(DeleteBehavior.Restrict);

            moduleBuilder.Entity<PhotographyCompany>()
                .HasOne<ApplicationUser>()
                .WithOne()
                .HasForeignKey<PhotographyCompany>(p => p.Id)
                .OnDelete(DeleteBehavior.Restrict);

                moduleBuilder.Entity<AgentPhotographyCompany>()
                .HasOne(ap => ap.Agent)
                .WithMany(a => a.AgentPhotographyCompanies)
                .HasForeignKey(ap => ap.AgentId);

            moduleBuilder.Entity<AgentPhotographyCompany>()
                .HasOne(ap => ap.PhotographyCompany)
                .WithMany(p => p.AgentPhotographyCompanies)
                .HasForeignKey(ap => ap.PhotographyCompanyId);


        }

    }

}
