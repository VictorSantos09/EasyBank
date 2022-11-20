using EasyBank.Crosscutting;
using EasyBank.Entities;

namespace EasyBank.Services
{
    public class Savings : EntidadeBase
    {
        public double Value { get; set; }
        public double TaxesValue { get; set; }
        public double StartValue { get; set; }
        public static List<Savings> _Savings { get; set; }
        public Savings(int userID, double _value, int _id, double _taxesValue)
        {
            OwnerID = userID;
            Value += _value;
            Id = _id;
            TaxesValue += _taxesValue;
        }
        public Savings()
        {

        }
        public void Menu(List<Savings> savings, int userID, List<User> users)
        {
            Console.WriteLine("POUPANÇA\n");
            Console.WriteLine("1 - Investir\n2 - Ver Rendimento\n3 - Voltar\nDigite: ");
            var choice = Console.ReadLine();

            while (choice != "1" || choice != "2")

                switch (Console.ReadLine())
                {
                    case "1":
                        SavingStagesSteps(savings, userID, users);
                        break;

                    case "2":
                        PrintBenefits(savings, userID);
                        break;
                    case "3":
                        return;
                        break;

                    default:
                        Message.ErrorGeneric("Opção indisponivel");
                        break;
                }
        }
        public double CalculateTaxes(double userValueInvested)
        {
            var mainTaxPrice = 1.25;
            var incrementer = 0.27;

            return (userValueInvested * mainTaxPrice) * incrementer;
        }
        public void AddSavingToList(int userID, double userValueInvested, List<Savings> savings, double taxesAmount)
        {
            var saving = new Savings(userID, userValueInvested, UserValidator.ID_AUTOINCREMENT(savings), taxesAmount);

            savings.Add(saving);
        }
        public double ChooseAmount(List<User> users, int userID)
        {
            var value = 0.0;
            while (true)
            {
                Console.WriteLine("Quanto você deseja investir mensalmente?\nDigite: ");

                value = Convert.ToDouble(Console.ReadLine());

                var user = users.Find(x => x.Id == userID);

                if (value > user.CurrentAccount)
                    Message.ErrorGeneric("Valor maior que em conta");

                break;
            }
            return value;
        }
        public bool ConfirmApplySaving()
        {
            Console.WriteLine("Deseja confirmar o investimento na poupança?\n1 - Sim\n2 - Não\nDigite: ");
            var confirmed = Console.ReadLine();

            while (confirmed != "1" || confirmed != "2")
                Message.ErrorGeneric("Opção inválida");

            if (confirmed == "1")
                return true;

            return false;
        }
        public void MonthlyAction(List<Savings> savings, int userID)
        {
            var saving = savings.Find(x => x.OwnerID == userID);

            saving.Value += CalculateTaxes(saving.Value);
        }
        public void SavingStagesSteps(List<Savings> savings, int userID, List<User> users)
        {
            if (HasExistentSaving(savings, userID) == true)
                Message.ErrorThread("Você já contém uma poupança");

            else
            {
                var amount = ChooseAmount(users, userID);

                var taxesAmount = CalculateTaxes(amount);

                if (ConfirmApplySaving() == true)
                {
                    AddSavingToList(userID, amount, savings, taxesAmount);
                    TransferMoneyToSavings(users, savings, userID, amount);
                }
            }
        }
        public bool HasExistentSaving(List<Savings> savings, int userID)
        {
            if (savings.Find(x => x.OwnerID == userID) == null)
                return false;

            return true;
        }
        public void PrintBenefits(List<Savings> savings, int userID)
        {
            var saving = savings.Find(x => x.OwnerID == userID);

            if (saving == null)
                Message.ErrorGeneric("Nenhum rendimento encontrado");

            else
            {
                Console.WriteLine($"Saldo Atual: {saving.Value}");
                Console.WriteLine($"Saldo Inicial: {saving.StartValue}");
                Console.WriteLine($"Juros Totais: {saving.TaxesValue}");
            }
        }
        public void TransferMoneyToSavings(List<User> users, List<Savings> savings, int userID, double investMoneyAmount)
        {
            var user = users.Find(x => x.Id == userID);
            var saving = savings.Find(x => x.OwnerID == userID);

            user.CurrentAccount = -investMoneyAmount;
            saving.Value += investMoneyAmount;
            saving.StartValue = investMoneyAmount;

        }
    }
}