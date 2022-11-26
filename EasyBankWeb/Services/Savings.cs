using EasyBankWeb.Crosscutting;
using EasyBankWeb.Dto;
using EasyBankWeb.Entities;
using EasyBankWeb.Repository;

namespace EasyBankWeb.Services
{
    public class Savings
    {
        private readonly SavingRepository _savingRepository;
        private readonly UserRepository _userRepository;
        private readonly SavingsDto _savingsDto;
        private readonly UserValidator userValidator;
        public Savings(SavingRepository savingRepository, UserRepository userRepository, UserValidator userValidator)
        {
            _savingRepository = savingRepository;
            _userRepository = userRepository;
            this.userValidator = userValidator;
        }
        public Savings()
        {

        }
        public void Menu(int userID)
        {
            Console.Clear();
            Console.WriteLine("POUPANÇA\n");
            Console.WriteLine("1 - Investir\n2 - Ver Rendimento\n3 - Resgatar\n4 - Cancelar\n5 - Voltar");
            Console.Write("Digite: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    SavingSteps(userID);
                    break;

                case "2":
                    PrintBenefits(userID);
                    MenuInsertMoney(userID);
                    Holder.PressAnyKey();
                    break;

                case "3":
                    RescueMoney(userID);
                    break;

                case "4":
                    CancelSaving(userID);
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
        public void AddSavingToList(int userID, double userValueInvested, double taxesValue)
        {
            var saving = new SavingEntity(userID, userValueInvested, IncrementID(), taxesValue, userValueInvested);

            _savingRepository.AddSavings(saving);
        }
        public double ChooseExtraValue()
        {
            var value = 0.0;
            while (true)
            {
                Console.Write("Digite a quantia: ");

                value = Convert.ToDouble(Console.ReadLine());

                return value;
            }
        }
        public double ChooseValue(int userID)
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
                        return ChooseExtraValue();

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
        public void MonthlyAction(int userID)
        {
            if (_savingRepository != null && _savingRepository.GetSavings().Exists(x => x.OwnerID == userID) == true)
            {
                var saving = _savingRepository.GetSavings().Find(x => x.OwnerID == userID);

                saving.TaxesValue += CalculateTaxes(saving.TaxesValue, _savingsDto.MonthsPassed);

                saving.Value += saving.TaxesValue;
            }
        }
        public void SavingSteps(int userID)
        {
            var user = _userRepository.GetUsers().Find(x => x.Id == userID);

            while (true)
            {
                if (HasExistentSaving(userID) == true)
                {
                    Message.GeneralThread("Você já contém uma poupança", 0);

                    Console.WriteLine("Deseja adicionar mais dinheiro?\n1 - Sim\n2 - Não, sair");
                    Console.Write("Digite: ");

                    if (Console.ReadLine() == "1")
                        InsertMoney(userID);
                    break;
                }

                else
                {
                    var value = ChooseValue(userID);

                    if (value > user.CurrentAccount)
                        Message.ErrorGeneric("Valor maior que em conta");

                    else
                    {
                        var taxesValue = CalculateTaxes(value, _savingsDto.MonthsPassed);

                        if (ConfirmApplySaving() == true)
                        {
                            AddSavingToList(userID, value, taxesValue);
                            DiscountMoneyFromUser(userID, value);
                            break;
                        }

                        else
                            break;
                    }
                }
            }
        }
        public bool HasExistentSaving(int userID)
        {
            if (_savingRepository.GetSavings().Find(x => x.OwnerID == userID) == null)
                return false;

            return true;
        }
        public void PrintBenefits(int userID)
        {
            var saving = _savingRepository.GetSavingById(userID);

            if (saving == null)
                Message.GeneralThread("Nenhum rendimento encontrado");

            else
            {
                Console.WriteLine($"Saldo Atual: {saving.Value}");
                Console.WriteLine($"Saldo Inicial: {saving.StartValue}");
                Console.WriteLine($"Juros Totais: {saving.TaxesValue}");
            }
        }
        public void DiscountMoneyFromUser(int userID, double investMoneyValue)
        {
            var user = _userRepository.GetUsers().Find(x => x.Id == userID);
            user.CurrentAccount -= investMoneyValue;
        }
        public void InsertMoney(int userID)
        {
            var user = _userRepository.GetUsers().Find(x => x.Id == userID);

            if (_savingsDto == null)
                Message.GeneralThread("Você não possui investimentos");

            else
            {
                var sucess = InsertAfterRescue(userID);

                if (sucess == true)
                    return;

                else
                {
                    var value = ChooseValue(userID);

                    if (UserHasEnoughMoney(value, userID) == true)
                    {
                        user.CurrentAccount -= value;
                        _savingsDto.Value += value;
                    }
                }
            }
        }
        public void RescueMoney(int userID)
        {
            var user = _userRepository.GetUsers().Find(x => x.Id == userID);
            var saving = _savingRepository.GetSavingById(userID);

            Console.Clear();
            Console.WriteLine("RESGATE\n");

            if (saving != null)
            {
                PrintBenefits(userID);

                Console.WriteLine("Deseja resgatar todo o rendimento?\n1 - Sim\n2 - Não\n3 - Voltar");
                Console.Write("Digite: ");
                var choice = Console.ReadLine();

                var allMoney = 0.0;
                var value = 0.0;

                switch (choice)
                {
                    case "1":
                        TransferAllMoney(userID);
                        break;

                    case "2":
                        value = ChooseValue(userID);

                        if (SavingHasEnoughMoney(value, userID) == true)
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
                Message.GeneralThread("Nenhuma poupança cadastrada", 1500);
        }
        public bool UserHasEnoughMoney(double value, int userID)
        {
            var user = _userRepository.GetUsers().Find(x => x.Id == userID);

            if (value > user.CurrentAccount)
            {
                Message.ErrorGeneric("Saldo indisponível");
                return false;
            }

            return true;
        }
        public bool SavingHasEnoughMoney(double value, int userID)
        {
            var saving = _savingRepository.GetSavingById(userID);

            if (value > saving.Value)
            {
                Message.ErrorGeneric("Valor indisponível");
                return false;
            }

            return true;
        }
        public void TransferAllMoney(int userID)
        {
            var saving = _savingRepository.GetSavingById(userID);
            var user = _userRepository.GetUsers().Find(x => x.Id == userID);

            user.CurrentAccount += saving.Value;

            saving.Value = 0.0;
            saving.StartValue = 0.0;
            saving.TaxesValue = 0.0;
            saving.MonthsPassed = 1;
        }
        public void MenuInsertMoney(int userID)
        {
            if (_savingRepository.GetSavings().Find(x => x.Id == userID) != null)
            {
                Console.WriteLine("Você já contém uma poupança, deseja aplicar dinheiro nela?\n1 - Sim\n2 - Não, sair");
                Console.Write("Digite: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        InsertMoney(userID);
                        break;

                    case "2":
                        return;

                    default:
                        Message.ErrorGeneric("Opção indisponível");
                        break;
                }
            }
        }
        public void CancelSaving(int userID)
        {
            var saving = _savingRepository.GetSavingById(userID);

            Console.Clear();
            Console.WriteLine("Tem certeza que deseja cancelar sua poupança?\n1 - Sim\n2 - Não");
            Console.Write("Digite: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    if (userValidator.IsCorrectSafeyKey(userID) == true)
                    {
                        _savingRepository.RemoveSavings(saving);
                        Message.SuccessfulGeneric("Poupança deletada com sucesso");
                    }
                    break;

                case "2":
                    break;

                default:
                    Message.ErrorGeneric();
                    break;
            }

        }
        public bool InsertAfterRescue(int userID)
        {
            var saving = _savingRepository.GetSavingById(userID);
            var user = _userRepository.GetUsers().Find(x => x.Id == userID);

            if (saving.TaxesValue == 0 && saving.MonthsPassed >= 1)
            {
                var value = ChooseValue(userID);

                var taxesValue = CalculateTaxes(value, saving.MonthsPassed);

                if (UserHasEnoughMoney(value, userID) == true)
                {
                    user.CurrentAccount -= value;
                    saving.Value += value;
                    saving.TaxesValue += taxesValue;
                }
                return true;
            }
            return false;
        }
        public void AddSavings(SavingsDto savingsDto)
        {
            var savings = new SavingEntity(savingsDto.OwnerID, savingsDto.Value, savingsDto.Id, savingsDto.TaxesValue, savingsDto.StartValue);
            _savingRepository.AddSavings(savings);
        }
        public List<SavingEntity> GetSavings()
        {
            return _savingRepository.GetSavings();
        }
        public int IncrementID()
        {
            return GeneralValidator.ID_AUTOINCREMENT(_savingRepository.GetSavings());
        }
    }
}