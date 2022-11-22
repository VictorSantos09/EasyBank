﻿namespace EasyBank.Entities
{
    public class Bill : BaseEntity
    {
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
        public void RemoveBills(List<Bill> bills, int userID, List<Loan> loans, List<User> users)
        {
            var bill = bills.FindAll(x => x.OwnerID == userID);

            var loanBill = bills.Find(x => x.Name == "EMPRÉSTIMO" && x.OwnerID == userID);

            if (loanBill != null)
                Loan.CheckAndRemoveLoan(loans, users, userID);

            for (int i = 0; i < bill.Count; i++)
            {
                if (bill[i].RemainParcels <= 1)
                    bills.Remove(bill[i]);

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
    }
}