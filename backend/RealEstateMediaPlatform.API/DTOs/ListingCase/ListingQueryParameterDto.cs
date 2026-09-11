namespace RealEstateMediaPlatform.API.DTOs.ListingCase
{
    public class ListingQueryParameterDto
    {
        //Search Keyword
        public string SearchTerm { get; set; } = "";
        //Price filters
        public double? MinPrice {  get; set; }
        public double? MaxPrice { get; set; }

        //Address filters
        public string City { get; set; } = "";
        public string State { get; set; } = "";

        //Pagination
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        //Sorting
        public string OrderBy { get; set; } = "CreatedAt";
        public bool OrderByDescending { get; set; } = true;

        // Validation
        public int ValidatedPageNumber => PageNumber < 1 ? 1 : PageNumber;
        public int ValidatedPageSize => PageSize < 1 ? 10 : (PageSize > 100 ? 100 : PageSize);


    }
}
