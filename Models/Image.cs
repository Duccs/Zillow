using System.ComponentModel.DataAnnotations.Schema;

namespace Zillow.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string? ImagePath { get; set; }
        [NotMapped]
        public IFormFile Img { get; set; }
        public int? RealEstateId { get; set; }
        public virtual RealEstate? RealEstate { get; set; }
    }
}
