namespace TreasuryManagementAPI.Models
{
    public class Collateral
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? AssetType { get; set; }
        public decimal? Amount { get; set; }
        public string? Description { get; set; }
        public DateTime? Date { get; set; }

        public int? Value { get; set; }
    }
}
