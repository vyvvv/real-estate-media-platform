using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RealEstateMediaPlatform.API.Models.Entities;
using RealEstateMediaPlatform.API.Models.Enums;

namespace RealEstateMediaPlatform.API.Data;

public static class DataSeeder
{
    private const string SeedPassword = "Password123!";

    public static async Task SeedAsync(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        await context.Database.MigrateAsync();

        await EnsureRolesAsync(roleManager);

        var photographyUser = await EnsureUserAsync(
            userManager,
            "admin@recam.com",
            "PhotographyCompany");

        var agentUser1 = await EnsureUserAsync(
            userManager,
            "agent.alex@recam.com",
            "Agent");

        var agentUser2 = await EnsureUserAsync(
            userManager,
            "agent.mia@recam.com",
            "Agent");

        await EnsurePhotographyCompanyAsync(context, photographyUser);
        await EnsureAgentsAsync(context, agentUser1, agentUser2);
        await EnsureAgentPhotographyCompaniesAsync(context, photographyUser, agentUser1, agentUser2);

        var listings = await EnsureListingCasesAsync(context, photographyUser);

        await EnsureAgentListingCasesAsync(context, listings, agentUser1, agentUser2);
        await EnsureCaseContactsAsync(context, listings);
        await EnsureMediaAssetsAsync(context, listings, photographyUser);
    }

    private static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        string[] roles = ["PhotographyCompany", "Agent"];

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

    private static async Task<ApplicationUser> EnsureUserAsync(
        UserManager<ApplicationUser> userManager,
        string email,
        string role)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user == null)
        {
            user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };

            var createResult = await userManager.CreateAsync(user, SeedPassword);

            if (!createResult.Succeeded)
            {
                var errors = string.Join(", ", createResult.Errors.Select(error => error.Description));
                throw new InvalidOperationException($"Failed to create seed user {email}: {errors}");
            }
        }

        if (!await userManager.IsInRoleAsync(user, role))
        {
            await userManager.AddToRoleAsync(user, role);
        }

        return user;
    }

    private static async Task EnsurePhotographyCompanyAsync(
        ApplicationDbContext context,
        ApplicationUser photographyUser)
    {
        if (await context.PhotographyCompanies.AnyAsync(company => company.Id == photographyUser.Id))
        {
            return;
        }

        context.PhotographyCompanies.Add(new PhotographyCompany
        {
            Id = photographyUser.Id,
            PhotographyCompanyName = "RECAM Studio"
        });

        await context.SaveChangesAsync();
    }

    private static async Task EnsureAgentsAsync(
        ApplicationDbContext context,
        ApplicationUser agentUser1,
        ApplicationUser agentUser2)
    {
        var seedAgents = new[]
        {
            new Agent
            {
                Id = agentUser1.Id,
                AgentFirstName = "Alex",
                AgentLastName = "Chen",
                AvatarUrl = "https://images.unsplash.com/photo-1500648767791-00dcc994a43e",
                CompanyName = "Bright Realty",
                User = agentUser1
            },
            new Agent
            {
                Id = agentUser2.Id,
                AgentFirstName = "Mia",
                AgentLastName = "Taylor",
                AvatarUrl = "https://images.unsplash.com/photo-1494790108377-be9c29b29330",
                CompanyName = "Harbour Estate",
                User = agentUser2
            }
        };

        foreach (var agent in seedAgents)
        {
            if (!await context.Agents.AnyAsync(existingAgent => existingAgent.Id == agent.Id))
            {
                context.Agents.Add(agent);
            }
        }

        await context.SaveChangesAsync();
    }

    private static async Task EnsureAgentPhotographyCompaniesAsync(
        ApplicationDbContext context,
        ApplicationUser photographyUser,
        ApplicationUser agentUser1,
        ApplicationUser agentUser2)
    {
        var relations = new[]
        {
            new AgentPhotographyCompany
            {
                AgentId = agentUser1.Id,
                PhotographyCompanyId = photographyUser.Id
            },
            new AgentPhotographyCompany
            {
                AgentId = agentUser2.Id,
                PhotographyCompanyId = photographyUser.Id
            }
        };

        foreach (var relation in relations)
        {
            var exists = await context.AgentPhotographyCompanies.AnyAsync(existingRelation =>
                existingRelation.AgentId == relation.AgentId &&
                existingRelation.PhotographyCompanyId == relation.PhotographyCompanyId);

            if (!exists)
            {
                context.AgentPhotographyCompanies.Add(relation);
            }
        }

        await context.SaveChangesAsync();
    }

    private static async Task<List<ListingCase>> EnsureListingCasesAsync(
        ApplicationDbContext context,
        ApplicationUser photographyUser)
    {
        var seedListings = new[]
        {
            new ListingCase
            {
                Title = "Modern Family House",
                Description = "A bright family home near the beach with open living spaces.",
                Street = "93 Beach Road",
                City = "Melbourne",
                State = "VIC",
                Postcode = 3000,
                Longitude = 144.9631m,
                Latitude = -37.8136m,
                Price = 800000,
                Bedrooms = 3,
                Bathrooms = 2,
                Garages = 1,
                FloorArea = 180,
                CreatedAt = DateTime.UtcNow.AddDays(-8),
                PropertyType = PropertyType.House,
                SaleCategory = SaleCategory.ForSale,
                ListCaseStatus = ListCaseStatus.Created,
                UserId = photographyUser.Id,
                User = photographyUser
            },
            new ListingCase
            {
                Title = "City Apartment With Views",
                Description = "A low-maintenance apartment close to transport, cafes, and parks.",
                Street = "12 King Street",
                City = "Sydney",
                State = "NSW",
                Postcode = 2000,
                Longitude = 151.2093m,
                Latitude = -33.8688m,
                Price = 980000,
                Bedrooms = 2,
                Bathrooms = 1,
                Garages = 1,
                FloorArea = 92,
                CreatedAt = DateTime.UtcNow.AddDays(-5),
                PropertyType = PropertyType.Unit,
                SaleCategory = SaleCategory.Auction,
                ListCaseStatus = ListCaseStatus.Pending,
                UserId = photographyUser.Id,
                User = photographyUser
            },
            new ListingCase
            {
                Title = "Townhouse Near The Park",
                Description = "A modern townhouse with private courtyard and flexible work-from-home space.",
                Street = "8 Collins Avenue",
                City = "Brisbane",
                State = "QLD",
                Postcode = 4000,
                Longitude = 153.0251m,
                Latitude = -27.4698m,
                Price = 720000,
                Bedrooms = 3,
                Bathrooms = 2,
                Garages = 2,
                FloorArea = 146,
                CreatedAt = DateTime.UtcNow.AddDays(-3),
                PropertyType = PropertyType.Townhouse,
                SaleCategory = SaleCategory.ForRent,
                ListCaseStatus = ListCaseStatus.Delivered,
                UserId = photographyUser.Id,
                User = photographyUser
            }
        };

        foreach (var listing in seedListings)
        {
            if (!await context.ListingCases.AnyAsync(existingListing => existingListing.Title == listing.Title))
            {
                context.ListingCases.Add(listing);
            }
        }

        await context.SaveChangesAsync();

        var titles = seedListings.Select(listing => listing.Title).ToArray();

        return await context.ListingCases
            .Where(listing => titles.Contains(listing.Title))
            .OrderBy(listing => listing.Id)
            .ToListAsync();
    }

    private static async Task EnsureAgentListingCasesAsync(
        ApplicationDbContext context,
        List<ListingCase> listings,
        ApplicationUser agentUser1,
        ApplicationUser agentUser2)
    {
        if (listings.Count == 0)
        {
            return;
        }

        var relations = new List<AgentListingCase>
        {
            new()
            {
                AgentId = agentUser1.Id,
                ListingCaseId = listings[0].Id
            }
        };

        if (listings.Count > 1)
        {
            relations.Add(new AgentListingCase
            {
                AgentId = agentUser2.Id,
                ListingCaseId = listings[1].Id
            });
        }

        if (listings.Count > 2)
        {
            relations.Add(new AgentListingCase
            {
                AgentId = agentUser1.Id,
                ListingCaseId = listings[2].Id
            });
        }

        foreach (var relation in relations)
        {
            var exists = await context.AgentListingCases.AnyAsync(existingRelation =>
                existingRelation.AgentId == relation.AgentId &&
                existingRelation.ListingCaseId == relation.ListingCaseId);

            if (!exists)
            {
                context.AgentListingCases.Add(relation);
            }
        }

        await context.SaveChangesAsync();
    }

    private static async Task EnsureCaseContactsAsync(
        ApplicationDbContext context,
        List<ListingCase> listings)
    {
        foreach (var listing in listings)
        {
            if (await context.CaseContacts.AnyAsync(contact => contact.ListingCaseId == listing.Id))
            {
                continue;
            }

            context.CaseContacts.AddRange(
                new CaseContact
                {
                    FirstName = "Olivia",
                    LastName = "Brown",
                    ProfileUrl = "https://images.unsplash.com/photo-1438761681033-6461ffad8d80",
                    CompanyName = "Bright Realty",
                    Email = "olivia.brown@example.com",
                    PhoneNumber = "0400 111 222",
                    ListingCaseId = listing.Id,
                    ListingCase = listing
                },
                new CaseContact
                {
                    FirstName = "Noah",
                    LastName = "Wilson",
                    ProfileUrl = "https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d",
                    CompanyName = "Harbour Estate",
                    Email = "noah.wilson@example.com",
                    PhoneNumber = "0400 333 444",
                    ListingCaseId = listing.Id,
                    ListingCase = listing
                });
        }

        await context.SaveChangesAsync();
    }

    private static async Task EnsureMediaAssetsAsync(
        ApplicationDbContext context,
        List<ListingCase> listings,
        ApplicationUser photographyUser)
    {
        foreach (var listing in listings)
        {
            if (await context.MediaAssets.AnyAsync(media => media.ListingCaseId == listing.Id))
            {
                continue;
            }

            context.MediaAssets.AddRange(
                new MediaAsset
                {
                    MediaType = MediaType.Photos,
                    MediaUrl = "https://images.unsplash.com/photo-1564013799919-ab600027ffc6",
                    UploadedAt = DateTime.UtcNow.AddDays(-2),
                    IsSelected = true,
                    IsHero = true,
                    IsDeleted = false,
                    ListingCaseId = listing.Id,
                    ListingCase = listing,
                    UserId = photographyUser.Id,
                    User = photographyUser
                },
                new MediaAsset
                {
                    MediaType = MediaType.Photos,
                    MediaUrl = "https://images.unsplash.com/photo-1600585154340-be6161a56a0c",
                    UploadedAt = DateTime.UtcNow.AddDays(-2),
                    IsSelected = true,
                    IsHero = false,
                    IsDeleted = false,
                    ListingCaseId = listing.Id,
                    ListingCase = listing,
                    UserId = photographyUser.Id,
                    User = photographyUser
                },
                new MediaAsset
                {
                    MediaType = MediaType.FloorPlan,
                    MediaUrl = "https://example.com/sample-floor-plan.pdf",
                    UploadedAt = DateTime.UtcNow.AddDays(-1),
                    IsSelected = false,
                    IsHero = false,
                    IsDeleted = false,
                    ListingCaseId = listing.Id,
                    ListingCase = listing,
                    UserId = photographyUser.Id,
                    User = photographyUser
                },
                new MediaAsset
                {
                    MediaType = MediaType.Videography,
                    MediaUrl = "https://example.com/sample-property-video.mp4",
                    UploadedAt = DateTime.UtcNow.AddDays(-1),
                    IsSelected = false,
                    IsHero = false,
                    IsDeleted = false,
                    ListingCaseId = listing.Id,
                    ListingCase = listing,
                    UserId = photographyUser.Id,
                    User = photographyUser
                });
        }

        await context.SaveChangesAsync();
    }
}
