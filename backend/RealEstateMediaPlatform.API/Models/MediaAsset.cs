using RealEstateMediaPlatform.API.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateMediaPlatform.API.Models
{
    public class MediaAsset
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "int")]
        public MediaAssetType MediaType {  get; set; }   
        public string MediaUrl { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.Now;
        public bool IsSelect { get; set; } = false;
        public bool IsHero { get; set; }=false;
        public int ListingCaseId { get; set; }

        [ForeignKey("ListingCaseId")]
        public ListingCase ListingCase { get; set; }
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public bool IsDeleted { get; set; }= false;


    }
}
