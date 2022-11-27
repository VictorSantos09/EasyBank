using EasyBankWeb.Crosscutting;
using EasyBankWeb.Dto;
using EasyBankWeb.Repository;
using EasyBankWeb.Entities;

namespace EasyBankWeb.Entities
{
    public class Bill : BaseEntity
    {
        private readonly BillRepository _billRepository;
        private readonly LoanRepository _loanRepository;

        public Bill(BillRepository billRepository, LoanRepository loanRepository)
        {
            _billRepository = billRepository;
            _loanRepository = loanRepository;
        }
       
        
        public Bill()
        {

        }
        public void RemoveBills(int userID)
        {
            var loan = new Loan();

            var bill = _loanRepository.GetLoan().FindAll(x => x.OwnerID == userID);

            var loanBill = _billRepository.GetBill().Find(x => x.Name == "EMPRÉSTIMO" && x.OwnerID == userID);

            if (loanBill != null)
                
                loan.CheckAndRemoveLoan(userID);

            for (int i = 0; i < bill.Count; i++)
            {
                if (bill[i].RemainParcels <= 1)
                    _billRepository.GetBill().Remove(bill[i]);

                else
                    bill[i].RemainParcels--;
            }
        }
        public void RemoveAutoDebits(Bill bill)
        {
            _billRepository.GetBill().Remove(bill);
        }
        public bool HasAutoDebitActivated(int userID)
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
            public List<BillEntity> GetBill()
        {
            return _billRepository.GetBill();
        } 
        public int IncrementID()
        {
            return GeneralValidator.ID_AUTOINCREMENT(_billRepository.GetBill());
        }
    }
}