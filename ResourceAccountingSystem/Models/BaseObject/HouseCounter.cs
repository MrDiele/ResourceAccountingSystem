namespace ResourceAccountingSystem.Models
{
    public class HouseCounter
    {
        public int IdHouse { get; set; }
        public string Address { get; set; }
        public int? SerialNumber { get; set; }
        public decimal? Indication { get; set; }
    }
}