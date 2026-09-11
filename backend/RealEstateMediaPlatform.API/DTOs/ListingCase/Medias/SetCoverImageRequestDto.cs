using System.ComponentModel.DataAnnotations;

namespace RealEstateMediaPlatform.API.DTOs.ListingCase.Medias
{
    public class SetCoverImageRequestDto
    {
        [Required]
        public int ImageId { get; set; }
    }
}
