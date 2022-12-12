namespace EasyBankWeb.Entities
{
    public class AdressEntity
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Neiborhood { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string? HouseComplement { get; set; }
    }
}
