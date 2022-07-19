namespace Zillow.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public virtual ICollection<Customer>? Customers { get; set; }
        public virtual ICollection<RealEstate>? RealEstates { get; set; }
    }
}
