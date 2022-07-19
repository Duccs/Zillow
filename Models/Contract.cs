namespace Zillow.Models
{
    public class Contract
    {
        public int Id { get; set; }
        public TypeEnum? Type { get; set; }
        public decimal? Price { get; set; }
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public int? RealEstateId { get; set; }
        public RealEstate? RealEstate { get; set; }

    }
    public enum TypeEnum
    {
        Buy,
        Sell,
        Rent,
        Rebuild
    }
}
