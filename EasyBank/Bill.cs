namespace EasyBank
{
    public class Bill // Contas
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public string Name { get; set; }
        public int NumberParcels { get; set; }
        public string? Info { get; set; }
        public int OwnerID { get; set; }
        public Bill(double _valueInvoce, string _nameBill, int QtdParcels, string? _infoBill, int userID)
        {
            Value = _valueInvoce;
            Name = _nameBill;
            NumberParcels = QtdParcels;
            Info = _infoBill;
            OwnerID = userID;
        }
        public Bill()
        {

        }
    }
}