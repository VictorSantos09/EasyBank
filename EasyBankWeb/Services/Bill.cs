﻿using EasyBankWeb.Entities;
using EasyBankWeb.Repository;

namespace EasyBankWeb.Services
{
    public class Bill
    {
        private readonly LoanRepository _loanRepository;
        private readonly BillRepository _billRepository;
        private readonly AutoDebitRepository _autoDebitRepository;
        private readonly Loan loan;

        public Bill(LoanRepository loanRepository, BillRepository billRepository, AutoDebitRepository autoDebitRepository, Loan loan)
        {
            _loanRepository = loanRepository;
            _billRepository = billRepository;
            _autoDebitRepository = autoDebitRepository;
            this.loan = loan;
        }

        public void RemoveBills(int userID)
        {
            var bill = _billRepository.GetBill().FindAll(x => x.OwnerID == userID);

            var loanBill = _billRepository.GetBill().Find(x => x.Name == "EMPRÉSTIMO" && x.OwnerID == userID);

            if (loanBill != null)
                //loan.CheckAndRemoveLoan(userID);

                for (int i = 0; i < bill.Count; i++)
                {
                    if (bill[i].RemainParcels <= 1)
                        _billRepository.RemoveBill(bill[i]);

                    else
                        bill[i].RemainParcels--;
                }
        }
        public void RemoveAutoDebits(BillEntity billEntity)
        {
            _billRepository.RemoveBill(billEntity);
        }
        public bool HasAutoDebitActivated(int userID)
        {
            var autoDebitActivated = _autoDebitRepository.GetAutoDebits().FindAll(x => x.OwnerID == userID);

            if (autoDebitActivated.Count == 0)
                return false;

            return true;
        }
    }
}
