namespace EasyBankWeb.Entities
{
    public class AutoDebitEntity : BaseEntity
    {
        public bool Activated { get; set; }
        public int OwnerID { get; set; }
        public double Value { get; set; }
        public string Name { get; set; }
        public string? Info { get; set; }
        public AutoDebitEntity(string _name, string _info, int _userID, double _value, int _id)
        {
            Name = _name;
            Info = _info;
            OwnerID = _userID;
            Value = _value;
            Activated = true;
            Id = _id;
        }
        public AutoDebitEntity()
        {

        }
    }
}