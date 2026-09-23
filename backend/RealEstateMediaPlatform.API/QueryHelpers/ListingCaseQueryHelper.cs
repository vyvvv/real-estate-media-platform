using RealEstateMediaPlatform.API.DTOs.ListingCase;
using RealEstateMediaPlatform.API.Models;


namespace RealEstateMediaPlatform.API.QueryHelpers
{
    public static class ListingCaseQueryHelper
    {
        // Apply filters to the listing cases query
        public static IQueryable<ListingCase> ApplyFilters(IQueryable<ListingCase> query, ListingQueryParameterDto queryParams)
        {

            var initialCount = query.Count();
            Console.WriteLine($"Initial count: {initialCount}");
            // Search functionality - search in title and description only
            if (!string.IsNullOrEmpty(queryParams.SearchTerm))
            {
                string keyword = queryParams.SearchTerm.Trim().ToLower();

                query = query.Where(lc =>
                    lc.Title.ToLower().Contains(keyword) ||
                    lc.Description.ToLower().Contains(keyword) ||
                    lc.Street.ToLower().Contains(keyword) ||
                    lc.City.ToLower().Contains(keyword) ||
                    lc.State.ToLower().Contains(keyword) || 
                    lc.Postcode.ToString().Contains(keyword));

                var afterSearchCount = query.Count();
                Console.WriteLine($"After search filter: {afterSearchCount}");
            }

            // Price filtering
            if (queryParams.MinPrice.HasValue)
            {
                query = query.Where(lc => lc.Price >= queryParams.MinPrice);
            }
            if (queryParams.MaxPrice.HasValue)
            {
                query = query.Where(lc => lc.Price <= queryParams.MaxPrice);
            }

            // Location filtering
            // City and State filters are already covered by SearchTerm
            // These are kept here for potential future use (e.g., advanced search or precise filtering)
            if (!string.IsNullOrEmpty(queryParams.City))
            {
                query = query.Where(lc => lc.City.Contains(queryParams.City));
            }
            if (!string.IsNullOrEmpty(queryParams.State))
            {
                query = query.Where(lc => lc.State.Contains(queryParams.State));
            }

            return query;
        }

        // Apply sorting to the listing cases query
        public static IQueryable<ListingCase> ApplySorting(IQueryable<ListingCase> query, ListingQueryParameterDto queryParams)
        {
            switch (queryParams.OrderBy.ToLower())
            {
                case "title":
                    if (queryParams.OrderByDescending)
                    {
                        return query.OrderByDescending(lc => lc.Title);
                    }
                    else
                    {
                        return query.OrderBy(lc => lc.Title);
                    }

                case "price":
                    if (queryParams.OrderByDescending)
                    {
                        return query.OrderByDescending(lc => lc.Price);
                    }
                    else
                    {
                        return query.OrderBy(lc => lc.Price);
                    }

                case "city":
                    if (queryParams.OrderByDescending)
                    {
                        return query.OrderByDescending(lc => lc.City);
                    }
                    else
                    {
                        return query.OrderBy(lc => lc.City);
                    }

                default: // "createdate"
                    if (queryParams.OrderByDescending)
                    {
                        return query.OrderByDescending(lc => lc.CreatedAt);
                    }
                    else
                    {
                        return query.OrderBy(lc => lc.CreatedAt);
                    }
            }
        }

        
        // Apply pagination to the listing cases query
        public static IQueryable<ListingCase> ApplyPaging(IQueryable<ListingCase> query, ListingQueryParameterDto queryParams)
        {
            return query
                .Skip((queryParams.ValidatedPageNumber - 1) * queryParams.ValidatedPageSize)
                .Take(queryParams.ValidatedPageSize);
        }
    }
}