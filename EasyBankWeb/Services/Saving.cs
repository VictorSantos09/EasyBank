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
        public Saving(SavingRepository savingRepository, UserRepository userRepository)
        {
            _savingRepository = savingRepository;
            _userRepository = userRepository;
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
        public bool HasExistentSaving(int userID)
        {
            if (_savingRepository.GetSavings().Exists(x => x.OwnerID == userID) == false)
                return false;

            return true;
        }
        public void DiscountMoneyFromUser(int userID, double investMoneyValue)
        {
            var user = _userRepository.GetUsers().Find(x => x.Id == userID);
            user.CurrentAccount -= investMoneyValue;
        }
        public bool SucessInsertMoney(int userID, int value)
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
                        if (SavingHasEnoughMoney(value, userID))
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
        public bool SucessCancelSaving(int userID, string safetyKey)
        {
            var saving = _savingRepository.GetSavingById(userID);

            var userValidator = new UserValidator(_userRepository);

            if (userValidator.IsCorrectSafeyKey(userID, safetyKey) == true)
            {
                _savingRepository.RemoveSavings(saving);
                return true;
            }

            return false;
        }
        public bool InsertAfterRescue(int userID)
        {
            var saving = _savingRepository.GetSavingById(userID);
            var user = _userRepository.GetUsers().Find(x => x.Id == userID);

            if (saving.TaxesValue == 0 && saving.MonthsPassed >= 1)
            {
                var value = 1;  //ChooseValue();

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
        public string PrintBenefits(int userID)
        {
            var saving = _savingRepository.GetSavingById(userID);

            if (saving == null)
                return null;

            return $"Saldo Atual: {saving.Value}\nSaldo Inicial: {saving.StartValue}\nJuros Totais: {saving.TaxesValue}\nTempo Aplicado: {saving.MonthsPassed}";
        }
        public void AddSavings(SavingsDto savingsDto)
        {
            var newSaving = new SavingEntity(savingsDto.OwnerID, savingsDto.Value, IncrementID(), CalculateTaxes(savingsDto.Value, 1));

            _savingRepository.AddSavings(newSaving);
        }
        public List<SavingEntity> GetSavings()
        {
            return _savingRepository.GetSavings();
        }
        public int IncrementID()
        {
            return GeneralValidator.ID_AUTOINCREMENT(_savingRepository.GetSavings());
        }
        public bool CreateNewSaving(SavingsDto savingsDto)
        {
            if (UserHasEnoughMoney(savingsDto.Value, savingsDto.OwnerID))
            {
                DiscountMoneyFromUser(savingsDto.OwnerID, savingsDto.Value);

                AddSavings(savingsDto);

                return true;
            }

            else
                return false;
        }
    }
}