namespace Zillow.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Desc { get; set; }
        public virtual ICollection<RealEstate>? RealEstates { get; set; }
    }
}
