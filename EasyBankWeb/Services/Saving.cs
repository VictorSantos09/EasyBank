using EasyBankWeb.Crosscutting;
using EasyBankWeb.Dto;
using EasyBankWeb.Entities;
using EasyBankWeb.Repository;

namespace EasyBankWeb.Services
{
    public class Saving
    {
        private readonly SavingRepository _savingRepository;
        private readonly UserRepository _userRepository;
        private readonly UserValidator userValidator;
        public Saving(SavingRepository savingRepository, UserRepository userRepository)
        {
            _savingRepository = savingRepository;
            _userRepository = userRepository;
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
                    Holder.PressAnyKey();
                    break;

                case "3":
                    //RescueMoney(userID);
                    break;

                case "4":
                    //CancelSaving(userID);
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
            //var saving = new SavingEntity(userID, userValueInvested, IncrementID(), taxesValue, userValueInvested);

            //_savingRepository.AddSavings(saving);
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
        public double ChooseValue()
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

                saving.TaxesValue += CalculateTaxes(saving.TaxesValue, saving.MonthsPassed);

                saving.Value += saving.TaxesValue;
            }
        }
        public bool SavingSteps(int userID)
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
                        //InsertMoney(userID);
                        break;
                }

                else
                {
                    var value = ChooseValue();

                    if (value > user.CurrentAccount)
                        return false;

                    else
                    {
                        var saving = _savingRepository.GetSavingById(userID);

                        var taxesValue = CalculateTaxes(value, saving.MonthsPassed);

                        if (ConfirmApplySaving() == true)
                        {
                            AddSavingToList(userID, value, taxesValue);
                            DiscountMoneyFromUser(userID, value);
                            return true;
                        }

                        else
                            break;
                    }
                }
            }
            return false;
        }
        public bool HasExistentSaving(int userID)
        {
            if (_savingRepository.GetSavings().Exists(x => x.OwnerID == userID) == false)
                return false;

            return true;
        }
        public string PrintBenefits(int userID)
        {
            var saving = _savingRepository.GetSavingById(userID);

            if (saving == null)
                return null;


            return $"saldo Atual:{saving.Value}\nsaldo inicial: {saving.StartValue}\njuros totais: {saving.TaxesValue}";
        }
        public void DiscountMoneyFromUser(int userID, double investMoneyValue)
        {
            var user = _userRepository.GetUsers().Find(x => x.Id == userID);
            user.CurrentAccount -= investMoneyValue;
        }
        public bool InsertMoney(int userID, int value)
        {
            var user = _userRepository.GetUsers().Find(x => x.Id == userID);

            if (!_savingRepository.GetSavings().Exists(x => x.OwnerID == userID))
                return false;

            if (UserHasEnoughMoney(value, userID) == true)
            {
                var saving = _savingRepository.GetSavingById(userID);

                user.CurrentAccount -= value;
                saving.Value += value;

                return true;
            }

            return false;
        }
        public bool RescueMoney(int userID, bool rescueAllMoney, int value)
        {
            var user = _userRepository.GetUsers().Find(x => x.Id == userID);
            var saving = _savingRepository.GetSavingById(userID);

            if (saving != null)
            {
                var caseOption = 0;

                if (rescueAllMoney == true)
                    caseOption = 1;

                else
                    caseOption = 2;

                switch (caseOption)
                {
                    case 1:
                        TransferAllMoney(userID);
                        return true;

                    case 2:
                        if (SavingHasEnoughMoney(value, userID) == true)
                        {
                            user.CurrentAccount += value;
                            saving.Value -= value;
                            return true;
                        }
                        break;
                }
            }
            return false;
        }
        public bool UserHasEnoughMoney(double value, int userID)
        {
            var user = _userRepository.GetUsers().Find(x => x.Id == userID);

            if (value > user.CurrentAccount)
                return false;

            return true;
        }
        public bool SavingHasEnoughMoney(double value, int userID)
        {
            var saving = _savingRepository.GetSavingById(userID);

            if (value > saving.Value)
            {
                //Message.ErrorGeneric("Valor indisponível");
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
        public void CancelSaving(int userID, string safetyKey)
        {
            var saving = _savingRepository.GetSavingById(userID);

            if (userValidator.IsCorrectSafeyKey(userID, safetyKey) == true)
            {
                _savingRepository.RemoveSavings(saving);
            }
        }
        public bool InsertAfterRescue(int userID)
        {
            var saving = _savingRepository.GetSavingById(userID);
            var user = _userRepository.GetUsers().Find(x => x.Id == userID);

            if (saving.TaxesValue == 0 && saving.MonthsPassed >= 1)
            {
                var value = ChooseValue();

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
            //var savings = new SavingEntity(savingsDto.OwnerID, savingsDto.Value, savingsDto.Id, savingsDto.TaxesValue, savingsDto.StartValue);
            //_savingRepository.AddSavings(savings); // Verificar se é necessario ou como resolver erro, ao executar diz ser nulo
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