namespace EasyBank
{
    public class ArrayClassOfAutoDebit
    {
        public int ID { get; set; }
        public int OwnerID { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public string OwnerName { get; set; } // Ignorar
        public float Value { get; set; }
        public ArrayClassOfAutoDebit(string _name, string _info,
            string _ownerName, float _value)
        {
            Name = _name;
            Info = _info;
            OwnerName = _ownerName;
            Value = _value;
        }
    }
}
