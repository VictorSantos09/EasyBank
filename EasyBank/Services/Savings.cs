using EasyBank.Crosscutting;
using EasyBank.Entities;
using System.Windows.Input;

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
                        SavingSteps(savings, userID, users);
                        break;

                    case "2":
                        PrintBenefits(savings, userID);
                        break;
                    case "3":
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
        public void AddSavingToList(int userID, double userValueInvested, List<Savings> savings, double taxesValue)
        {
            var saving = new Savings(userID, userValueInvested, UserValidator.ID_AUTOINCREMENT(savings), taxesValue);

            savings.Add(saving);
        }
        public double ChooseExtraValue(List<User> users, int userID)
        {
            var value = 0.0;
            while (true)
            {
                Console.WriteLine("Digite a quantia: ");

                value = Convert.ToDouble(Console.ReadLine());

                var user = users.Find(x => x.Id == userID);

                return value;
            }
        }
        public double ChooseValue(List<User> users, int userID)
        {
            while (true)
            {
                Console.WriteLine("1 - 100");
                Console.WriteLine("2 - 150");
                Console.WriteLine("3 - 200");
                Console.WriteLine("4 - 250");
                Console.WriteLine("5 - Outro Valor");
                Console.Write("Digite: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        return 100.00;
                    case "2":
                        return 150.00;
                    case "3":
                        return 200.00;
                    case "4":
                        return 250.00;

                    case "5":
                        return ChooseExtraValue(users, userID);

                    default:
                        Message.ErrorGeneric("Opção Indisponível");
                        break;
                }
            }
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
        public void SavingSteps(List<Savings> savings, int userID, List<User> users)
        {
            var user = users.Find(x => x.Id == userID);
            while (Console.ReadKey().Key != ConsoleKey.Q)
            {
                Console.WriteLine("Clique Q a qualquer momento para sair");

                if (HasExistentSaving(savings, userID) == true)
                {
                    Message.ErrorThread("Você já contém uma poupança");
                    break;
                }

                else
                {
                    var Value = ChooseValue(users, userID);

                    if (Value > user.CurrentAccount)
                        Message.ErrorGeneric("Valor maior que em conta");

                    else
                    {
                        var taxesValue = CalculateTaxes(Value);

                        if (ConfirmApplySaving() == true)
                        {
                            AddSavingToList(userID, Value, savings, taxesValue);
                            TransferMoneyToSavings(users, savings, userID, Value);
                        }
                    }
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
        public void TransferMoneyToSavings(List<User> users, List<Savings> savings, int userID, double investMoneyValue)
        {
            var user = users.Find(x => x.Id == userID);
            var saving = savings.Find(x => x.OwnerID == userID);

            user.CurrentAccount = -investMoneyValue;
            saving.Value += investMoneyValue;
            saving.StartValue = investMoneyValue;

        }
        public void InsertMoney(List<User> users, List<Savings> savings, int userID)
        {
            var user = users.Find(x => x.Id == userID);
            var saving = savings.Find(x => x.OwnerID == userID);

            var value = ChooseValue(users, userID);

            if (UserHasEnoughMoney(value, users, userID) == true)
            {
                user.CurrentAccount = -value;
                saving.Value += value;
            }
        }
        public void RescueMoney()
        {
            // Implement
            // Remover o dinheiro total ou parcial da poupança para a conta corrente
        }
        public bool UserHasEnoughMoney(double value, List<User> users, int userID)
        {
            var user = users.Find(x => x.Id == userID);

            if (value > user.CurrentAccount)
            {
                Message.ErrorGeneric("Valor maior que em conta");
                return false;
            }

            return true;
        }
    }
}