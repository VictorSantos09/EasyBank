using EasyBank.Crosscutting;
using EasyBank.Entities;
using System;
using System.Numerics;

namespace EasyBank.Services
{
    public class Savings : BaseEntity
    {
        public double Value { get; set; }
        public double TaxesValue { get; set; }
        public double StartValue { get; set; }
        public int MonthsPassed { get; set; } = 1;
        public Savings(int userID, double _value, int _id, double _taxesValue, double _startValue)
        {
            OwnerID = userID;
            Value += _value;
            Id = _id;
            TaxesValue += _taxesValue;
            StartValue = _startValue;
        }
        public Savings()
        {

        }
        public void Menu(List<Savings> savings, int userID, List<User> users)
        {
            Console.Clear();
            Console.WriteLine("POUPANÇA\n");
            Console.WriteLine("1 - Investir\n2 - Ver Rendimento\n3 - Resgatar\n4 - Cancelar\n5 - Voltar");
            Console.Write("Digite: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    SavingSteps(savings, userID, users);
                    break;

                case "2":
                    PrintBenefits(savings, userID);
                    MenuInsertMoney(users, savings, userID);
                    Holder.PressAnyKey();
                    break;

                case "3":
                    RescueMoney(users, savings, userID);
                    break;

                case "4":
                    CancelSaving(savings, users, userID);
                    break;

                case "5":
                    break;

                default:
                    Message.ErrorGeneric("Opção indisponivel");
                    break;
            }
        }
        public double CalculateTaxes(double userValueInvested, int monthsPasseds)
        {
            // J = C * I * T
            // M = C + J;

            double c = userValueInvested;
            double i = 0.055;
            int t = monthsPasseds;

            double j = c * i * t;
            //double m = c * (1 + (i * t));

            return j;
        }
        public void AddSavingToList(int userID, double userValueInvested, List<Savings> savings, double taxesValue)
        {
            var saving = new Savings(userID, userValueInvested, UserValidator.ID_AUTOINCREMENT(savings), taxesValue, userValueInvested);

            savings.Add(saving);
        }
        public double ChooseExtraValue(List<User> users, int userID)
        {
            var value = 0.0;
            while (true)
            {
                Console.Write("Digite a quantia: ");

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
            Console.WriteLine("Deseja confirmar o investimento na poupança?\n1 - Sim\n2 - Não");
            Console.Write("Digite: ");
            var confirmed = Console.ReadLine();

            switch (confirmed)
            {
                case "1":
                    return true;

                case "2":
                    return false;

                default:
                    Message.ErrorGeneric();
                    break;
            }
            return false;
        }
        public void MonthlyAction(List<Savings> savings, int userID)
        {
            if (savings != null && savings.Exists(x => x.OwnerID == userID) == true)
            {
                var saving = savings.Find(x => x.OwnerID == userID);
                saving.TaxesValue += CalculateTaxes(saving.TaxesValue, MonthsPassed++);

                saving.Value += saving.TaxesValue;
            }
        }
        public void SavingSteps(List<Savings> savings, int userID, List<User> users)
        {
            var user = users.Find(x => x.Id == userID);
            while (true)
            {
                Console.Clear();

                Console.WriteLine("INVESTIR");

                if (HasExistentSaving(savings, userID) == true)
                {
                    Message.ErrorThread("Você já contém uma poupança", 0);

                    Console.WriteLine("Deseja adicionar mais dinheiro?\n1 - Sim\n2 - Não, sair");
                    Console.Write("Digite: ");

                    if (Console.ReadLine() == "1")
                        InsertMoney(users, savings, userID);
                    break;
                }

                else
                {
                    var value = ChooseValue(users, userID);

                    if (value > user.CurrentAccount)
                        Message.ErrorGeneric("Valor maior que em conta");

                    else
                    {
                        var taxesValue = CalculateTaxes(value, MonthsPassed);

                        if (ConfirmApplySaving() == true)
                        {
                            AddSavingToList(userID, value, savings, taxesValue);
                            DiscountMoneyFromUser(users, userID, value);
                            break;
                        }

                        else
                            break;
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
        public void DiscountMoneyFromUser(List<User> users, int userID, double investMoneyValue)
        {
            var user = users.Find(x => x.Id == userID);
            user.CurrentAccount -= investMoneyValue;
        }
        public void InsertMoney(List<User> users, List<Savings> savings, int userID)
        {
            Console.Clear();

            Console.WriteLine("ADICIONAR DINHEIRO");

            var user = users.Find(x => x.Id == userID);
            var saving = savings.Find(x => x.OwnerID == userID);

            if (saving == null)
                Message.ErrorGeneric("Você não possui investimentos");

            else
            {
                var sucess = InsertAfterRescue(savings, users, userID);

                if (sucess == true)
                    return;

                else
                {
                    var value = ChooseValue(users, userID);

                    if (UserHasEnoughMoney(value, users, userID) == true)
                    {
                        user.CurrentAccount -= value;
                        saving.Value += value;
                    }
                }
            }
        }
        public void RescueMoney(List<User> users, List<Savings> savings, int userID)
        {
            var user = users.Find(x => x.Id == userID);
            var saving = savings.Find(x => x.OwnerID == userID);

            Console.Clear();
            Console.WriteLine("RESGATE\n");

            if (saving != null)
            {
                PrintBenefits(savings, userID);

                Console.WriteLine("Deseja resgatar todo o rendimento?\n1 - Sim\n2 - Não\n3 - Voltar");
                Console.Write("Digite: ");
                var choice = Console.ReadLine();

                var allMoney = 0.0;
                var value = 0.0;

                switch (choice)
                {
                    case "1":
                        TransferAllMoney(savings, userID, users);
                        break;

                    case "2":
                        value = ChooseValue(users, userID);

                        if (SavingHasEnoughMoney(value, savings, userID) == true)
                        {
                            user.CurrentAccount += value;
                            saving.Value -= value;
                        }
                        break;

                    case "3":
                        return;

                    default:
                        Message.ErrorGeneric();
                        break;
                }
            }
            else
                Message.ErrorThread("Nenhuma poupança cadastrada", 1500);
        }
        public bool UserHasEnoughMoney(double value, List<User> users, int userID)
        {
            var user = users.Find(x => x.Id == userID);

            if (value > user.CurrentAccount)
            {
                Message.ErrorGeneric("Saldo indisponível");
                return false;
            }

            return true;
        }
        public bool SavingHasEnoughMoney(double value, List<Savings> savings, int userID)
        {
            var saving = savings.Find(x => x.OwnerID == userID);

            if (value > saving.Value)
            {
                Message.ErrorGeneric("Valor indisponível");
                return false;
            }

            return true;
        }
        public void TransferAllMoney(List<Savings> savings, int userID, List<User> users)
        {
            var saving = savings.Find(x => x.OwnerID == userID);
            var user = users.Find(x => x.Id == userID);

            user.CurrentAccount += saving.Value;

            saving.Value = 0.0;
            saving.StartValue = 0.0;
            saving.TaxesValue = 0.0;
            MonthsPassed = 1;
        }
        public void MenuInsertMoney(List<User> users, List<Savings> savings, int userID)
        {
            if (savings.Find(x => x.Id == userID) != null)
            {
                Console.WriteLine("Você já contém uma poupança, deseja aplicar dinheiro nela?\n1 - Sim\n2 - Não, sair");
                Console.Write("Digite: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        InsertMoney(users, savings, userID);
                        break;

                    case "2":
                        return;

                    default:
                        Message.ErrorGeneric("Opção indisponível");
                        break;
                }
            }
        }
        public void CancelSaving(List<Savings> savings, List<User> users, int userID)
        {
            var saving = savings.Find(x => x.Id == userID);

            Console.Clear();
            Console.WriteLine("Tem certeza que deseja cancelar sua poupança?\n1 - Sim\n2 - Não");
            Console.Write("Digite: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    if (UserValidator.IsCorrectSafeyKey(users, userID) == true)
                    {
                        savings.Remove(saving);
                        Message.ErrorGeneric("Poupança deletada com sucesso");
                    }
                    break;

                case "2":
                    break;

                default:
                    Message.ErrorGeneric();
                    break;
            }

        }
        public bool InsertAfterRescue(List<Savings> savings, List<User> users, int userID)
        {
            var saving = savings.Find(x => x.OwnerID == userID);
            var user = users.Find(x => x.Id == userID);

            if (saving.TaxesValue == 0 && MonthsPassed >= 1)
            {
                var value = ChooseValue(users, userID);

                var taxesValue = CalculateTaxes(value, MonthsPassed);

                if (UserHasEnoughMoney(value, users, userID) == true)
                {
                    user.CurrentAccount -= value;
                    saving.Value += value;
                    saving.TaxesValue += taxesValue;
                }
                return true;
            }
            return false;
        }
    }
}