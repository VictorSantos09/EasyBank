namespace EasyBank
{
    public class Bill // Contas
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public string Name { get; set; }
        public int NumberParcels { get; set; }
        public string? Info { get; set; }
        public void ConstructorBills(List<Bill> bills, int _valueInvoce, string _nameBill, int QtdParcels, string? _infoBill)
        {
            if (_valueInvoce != 0 && _nameBill != null && QtdParcels != 0)
            {
                bills.Add(new Bill
                {
                    Value = _valueInvoce,
                    Name = _nameBill,
                    NumberParcels = QtdParcels,
                    Info = _infoBill
                });
                Validator.ID_AUTOINCREMENT(null, null, 3, bills, null);
            }
        }
        public void RegisterNewBiils(List<Bill> bills, List<Loan> loans)
        {
            string[] data = R_AutoRegisterLoan(loans);
            // Fazer em seguida
            //value
            //nome --------- coloquei para registrar apenas o emprestimo
            //qtdparcelas
            //info
            ConstructorBills(bills, Convert.ToInt32(data[0]), data[2], Convert.ToInt32(data[1]), null);
        }
        public void ViewMonthBills(List<Bill> bills)
        {
            var finalValue = 0;
            for (int i = 0; i < bills.Count; i++)
            {
                if (bills[i].Name != string.Empty)
                {
                    Console.WriteLine($"Item: {bills[i].Name} Descrição: {bills[i].Info} Valor: {bills[i].Value}");
                    finalValue = finalValue + bills[i].Value;
                }
                else if (i == bills.Count)
                {
                    Console.WriteLine($"\nTotal: R$ {finalValue}");
                }
            }
        }
        public string[] R_AutoRegisterLoan(List<Loan> loans)
        {
            string[] data = new string[3];
            for (int i = 0; i < loans.Count; i++)
            {
                if (loans[i].Open == true)
                {
                    data[0] = Convert.ToString(loans[i].Value);
                    data[1] = Convert.ToString(loans[i].Parcels);
                    data[2] = "EMPRÉSTIMO"; // Name to -> ConstructorBills
                    Loan loan = new Loan();
                    loan.Constructor(loans, Convert.ToInt32(data[0]), Convert.ToInt32(data[1]));
                }
            }
            return data;
        }
    }
}