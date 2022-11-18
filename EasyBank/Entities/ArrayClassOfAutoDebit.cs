namespace EasyBank.Entities
{
    public class ArrayClassOfAutoDebit
    {
        public int ID { get; set; }
        public int OwnerID { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public float Value { get; set; }
        public ArrayClassOfAutoDebit(string _name, string _info,
            int _ownerID, float _value)
        {
            Name = _name;
            Info = _info;
            OwnerID = _ownerID;
            Value = _value;
        }
    }
}