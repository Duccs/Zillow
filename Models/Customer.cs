namespace Zillow.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? AddressId { get; set; }
        public virtual Address? Address { get; set; }
        public string? Phone { get; set; }
        public virtual ICollection<Contract>? Contracts { get; set; }
    }
}
