namespace EasyBank
{
    public class Bill // Contas
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public string Name { get; set; }
        public int NumberParcels { get; set; }
        public string? Info { get; set; }
        public int OwnerIndex { get; set; }
        public int OwnerID { get; set; }
        public void ConstructorBills(List<Bill> bills, double _valueInvoce, string _nameBill, int QtdParcels, string? _infoBill, int userID, int userIndex)
        {
            if (_valueInvoce != 0 && _nameBill != null && QtdParcels != 0)
            {
                bills.Add(new Bill
                {
                    Value = _valueInvoce,
                    Name = _nameBill,
                    NumberParcels = QtdParcels,
                    Info = _infoBill,
                    OwnerIndex = userIndex,
                    OwnerID = userID,
                });
                Validator.ID_AUTOINCREMENT(null, null, 3, bills, null);
            }
        }
        public void ViewMonthBills(List<Bill> bills)
        {
            var finalValue = 0.0;
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
    }
}