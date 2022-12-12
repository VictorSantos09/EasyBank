namespace EasyBankWeb.Entities
{
    public class BillEntity : BaseEntity
    {
        public int OwnerID { get; set; }
        public double Value { get; set; }
        public double ValueParcel { get; set; }
        public string Name { get; set; }
        public int NumberParcels { get; set; }
        public string? Info { get; set; }
        public int RemainParcels { get; set; }
        public AutoDebitEntity AutoDebitEntity { get; set; }
        public BillEntity(double _valueInvoce, string _nameBill, int QtdParcels, string? _infoBill, int userID, double _ValueParcel, bool _autoDebit)
        {
            Value = _valueInvoce;
            Name = _nameBill;
            NumberParcels = QtdParcels;
            Info = _infoBill;
            OwnerID = userID;
            ValueParcel = _ValueParcel;
            AutoDebitEntity.Activated = _autoDebit;
        }
        public BillEntity()
        {

        }
    }
}