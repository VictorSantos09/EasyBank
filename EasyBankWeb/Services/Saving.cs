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
            // Calculo de juros simples

            var c = userValueInvested;

            var i = Convert.ToDouble(5) / 100;

            var n = monthsPasseds;

            var j = c * i * n;

            return j + c;
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
        public (string, int) NewSavingProcess(int userID, SavingsDto savingsDto)
        {
            if (!IsExistentUser(userID))
                return ("Usuario não encontrado", 404);

            if (HasExistentSaving(userID))
                return ("Poupança já existente", 400);

            if (CreateNewSaving(savingsDto))
                return ("Poupança criada com sucesso", 200);

            return ("Saldo insuficiente", 400);
        }
        public bool HasExistentSaving(int userID)
        {
            return _savingRepository.GetSavings().Exists(x => x.OwnerID == userID);
        }
        public void DiscountMoneyFromUser(int userID, double investMoneyValue)
        {
            var user = _userRepository.GetUsers().Find(x => x.Id == userID);
            user.CurrentAccount -= investMoneyValue;
        }
        public (string, int) InsertMoneyProcess(int userID, int value)
        {
            var user = _userRepository.GetUsers().Find(x => x.Id == userID);

            if (!_savingRepository.GetSavings().Exists(x => x.OwnerID == userID))
                return ("Não há poupança existente", 404);

            if (UserHasEnoughMoney(value, userID))
            {
                var saving = _savingRepository.GetSavingById(userID);

                user.CurrentAccount -= value;
                saving.Value += value;

                return ("Valor inserido com sucesso", 200);
            }

            return ("Saldo insuficiente", 400);
        }
        public BaseDto RescueMoneyProcess(int userID, bool rescueAllMoney, int value)
        {
            var user = _userRepository.GetUsers().Find(x => x.Id == userID);
            var saving = _savingRepository.GetSavingById(userID);

            if (saving == null)
                return new BaseDto("Usuario não contem poupança existente", 404);

            var caseOption = 0;

            if (rescueAllMoney == true)
                caseOption = 1;

            else
                caseOption = 2;

            switch (caseOption)
            {
                case 1:
                    TransferAllMoney(userID);
                    return new BaseDto("Saldo resgatado com sucesso", 200);

                case 2:
                    if (SavingHasEnoughMoney(value, userID))
                    {
                        user.CurrentAccount += value;
                        saving.Value -= value;

                        return new BaseDto("Saldo resgatado com sucesso", 200);
                    }
                    break;
            }
            return new BaseDto("Quantia indisponivel", 400);
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
        public (string, int) CancelSavingProcess(int userID, string safetyKey)
        {
            var saving = _savingRepository.GetSavingById(userID);

            var userValidator = new UserValidator(_userRepository);

            if (userValidator.IsCorrectSafeyKey(userID, safetyKey) == true)
            {
                _savingRepository.RemoveSavings(saving);
                return ("Poupança deletada com sucesso", 200);
            }

            return ("Solicitação cancelada", 200);
        }
        public bool InsertAfterRescue(int userID)
        {
            var saving = _savingRepository.GetSavingById(userID);
            var user = _userRepository.GetUsers().Find(x => x.Id == userID);

            if (saving.TaxesValue == 0 && saving.MonthsPassed >= 1)
            {
                var value = 1;

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
        public BaseDto PrintBenefits(int userID)
        {
            var saving = _savingRepository.GetSavingById(userID);

            if (saving == null)
                return new BaseDto("Nenhuma poupança registrada", 404);

            return new BaseDto($"Valor: {saving.Value}\nJuros: {saving.TaxesValue}\nTempo Investido: {saving.MonthsPassed}");
        }
        public void AddSavings(SavingsDto savingsDto)
        {
            var newSaving = new SavingEntity(savingsDto.OwnerID, savingsDto.Value, IncrementID(), CalculateTaxes(savingsDto.Value, 1), true);

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
        public void RemoveSaving(SavingEntity savingEntity)
        {
            _savingRepository.RemoveSavings(savingEntity);
        }
        public bool IsExistentUser(int userID)
        {
            return _userRepository.GetUsers().Exists(x => x.Id == userID);
        }
    }
}