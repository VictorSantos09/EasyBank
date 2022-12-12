using EasyBankWeb.Crosscutting;
using EasyBankWeb.Entities;
using EasyBankWeb.Repository;

namespace EasyBankWeb.Services
{
    public class Bill
    {
        private readonly BillRepository _billRepository;
        private readonly AutoDebitRepository _autoDebitRepository;

        public Bill(BillRepository billRepository, AutoDebitRepository autoDebitRepository)
        {
            _billRepository = billRepository;
            _autoDebitRepository = autoDebitRepository;
        }

        public void Removes(int userID)
        {
            var bill = _billRepository.GetAll().FindAll(x => x.OwnerID == userID);

            var loanBill = _billRepository.GetAll().Find(x => x.Name == "EMPRÉSTIMO" && x.OwnerID == userID);

            if (loanBill != null)
                //loan.CheckAndRemoveLoan(userID);

                for (int i = 0; i < bill.Count; i++)
                {
                    if (bill[i].RemainParcels <= 1)
                        _billRepository.Remove(bill[i].Id);

                    else
                        bill[i].RemainParcels--;
                }
        }
        public void Removes(BillEntity billEntity)
        {
            _billRepository.Remove(billEntity.Id);
        }
        public bool HasAutoDebitActivated(int userID)
        {
            var autoDebitActivated = _autoDebitRepository.GetAll().FindAll(x => x.OwnerID == userID);

            if (autoDebitActivated.Count == 0)
                return false;

            return true;
        }
        public int IncrementID()
        {
            return GeneralValidator.ID_AUTOINCREMENT(_billRepository.GetAll());
        }
    }
}
