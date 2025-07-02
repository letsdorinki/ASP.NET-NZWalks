using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.models.DTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(3,ErrorMessage ="Code has to be minimum of 3 chracters")]
        [MaxLength(4, ErrorMessage = "Code has to be maximum of 4 chracters")]

        public string? Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Code has to be maximum of 100 chracters")]
        public string? Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
