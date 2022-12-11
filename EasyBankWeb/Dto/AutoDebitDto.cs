namespace EasyBankWeb.Dto
{
    public class AutoDebitDto
    {
        public double Value { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public AutoDebitDto(double value, string name, string type)
        {
            Value = value;
            Name = name;
            Type = type;
        }
    }
}
