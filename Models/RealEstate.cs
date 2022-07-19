namespace Zillow.Models
{
    public class RealEstate
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Desc { get; set; }
        public int? AddressId { get; set; }
        public virtual Address? Address { get; set; }
        public int? CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        public virtual ICollection<Image>? Images { get; set; }
        public virtual ICollection<Contract>? Contracts { get; set; }

    }
}
