using EasyBankWeb.Crosscutting;
using EasyBankWeb.Dto;
using EasyBankWeb.Repository;

namespace EasyBankWeb.Entities
{
    public class Bill : BaseEntity
    {
        private readonly BillRepository _billRepository;

        public Bill(Bill bill)
        {
            _bill = bill;
        }
        public double Value { get; set; }
        public double ValueParcel { get; set; }
        public string Name { get; set; }
        public int NumberParcels { get; set; }
        public string? Info { get; set; }
        public int RemainParcels { get; set; }
        public Bill(double _valueInvoce, string _nameBill, int QtdParcels, string? _infoBill, int userID, double _ValueParcel, bool _autoDebit)
        {
            Value = _valueInvoce;
            Name = _nameBill;
            NumberParcels = QtdParcels;
            Info = _infoBill;
            OwnerID = userID;
            ValueParcel = _ValueParcel;
            AutoDebit autoDebit = new AutoDebit();
            autoDebit.Activated = _autoDebit;
        }
        public Bill()
        {

        }
        public void RemoveBills(int userID)
        {
            var bill = _billRepository.GetBill().FindAll(x => x.OwnerID == userID);

            var loanBill = _billRepository.GetBill().Find(x => x.Name == "EMPRÉSTIMO" && x.OwnerID == userID);

            if (loanBill != null)
                
                Loan.CheckAndRemoveLoan(userID);

            for (int i = 0; i < bill.Count; i++)
            {
                if (bill[i].RemainParcels <= 1)
                    _billRepository.GetBill().Remove(bill[i]);

                else
                    bill[i].RemainParcels--;
            }
        }
        public void RemoveAutoDebits(List<Bill> bills, Bill bill)
        {
            bills.Remove(bill);
        }
        public bool HasAutoDebitActivated(List<AutoDebit> autoDebits, int userID)
        {
            var autoDebitActivated = autoDebits.FindAll(x => x.OwnerID == userID);

            if (autoDebitActivated.Count == 0)
                return false;

            return true;
        }
        public void AddBill(BillDto billDto)
        {
            var bill = new Bill();
            _billRepository.AddBill(bill);
        }
            public List<Bill> GetBill()
        {
            return _billRepository.GetBill();
        } 
        public int IncrementID()
        {
            return GeneralValidator.ID_AUTOINCREMENT(_billRepository.GetBill());
        }
    }
}