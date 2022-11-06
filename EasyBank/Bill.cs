namespace EasyBank
{
    public class Bill : EntidadeBase
    {
        public double Value { get; set; }
        public double ValueParcel { get; set; }
        public string Name { get; set; }
        public int NumberParcels { get; set; }
        public string? Info { get; set; }
        public int OwnerID { get; set; }
        public AutoDebit AutoDebit { get; set; }
        public Bill(double _valueInvoce, string _nameBill, int QtdParcels, string? _infoBill, int userID, double _ValueParcel, bool _autoDebit)
        {
            Value = _valueInvoce;
            Name = _nameBill;
            NumberParcels = QtdParcels;
            Info = _infoBill;
            OwnerID = userID;
            ValueParcel = _ValueParcel;
            AutoDebit.Activated = _autoDebit;
        }
        public Bill()
        {

        }
        public void RemoveBills(List<Bill> bills, int userID)
        {
            var bill = bills.Find(x => x.OwnerID == userID);

            if (bill.NumberParcels <= 1)
            {
                bills.Remove(bill);
            }
            else
            {
                bill.NumberParcels--;
            }
        }
        public void RemoveAutoDebits(List<Bill> bills, Bill bill)
        {
            bills.Remove(bill);
        }
        public bool HasAutoDebitActivated(List<AutoDebit> autoDebits, int userID)
        {
            var autoDebitActivated = autoDebits.FindAll(x => x.OwnerID == userID && x.Activated == true);

            if (autoDebitActivated == null)
                return false;

            return true;

        }

    }
}