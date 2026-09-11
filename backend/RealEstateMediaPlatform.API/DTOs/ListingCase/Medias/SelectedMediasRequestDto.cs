using System.ComponentModel.DataAnnotations;

namespace RealEstateMediaPlatform.API.DTOs.ListingCase.Medias
{
    public class SelectedMediasRequestDto
    {
        [Required]
        public List<int> mediaIds { get; set; }
    }
}
