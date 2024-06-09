using System.ComponentModel.DataAnnotations;

namespace firstAsp.Models.DTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(3,ErrorMessage ="code has to be minimum 3 caractrs")]
        [MaxLength(3,ErrorMessage ="code has to be maximum 3 caracters")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100,ErrorMessage ="Name has to be 100 caracters max")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
