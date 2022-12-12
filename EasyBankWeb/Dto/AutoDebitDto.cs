namespace EasyBankWeb.Dto
{
    public class AutoDebitDto
    {
        public double Value { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool Confirmed { get; set; }
        public int OwnerID { get; set; }

        public AutoDebitDto(double value, string name, string type, bool confirmed, int ownerID)
        {
            Value = value;
            Name = name;
            Type = type;
            Confirmed = confirmed;
            OwnerID = ownerID;
        }
    }
}
