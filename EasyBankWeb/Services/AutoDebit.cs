using EasyBankWeb.Dto;
using EasyBankWeb.Entities;
using EasyBankWeb.Repository;

namespace EasyBankWeb.Services
{
    public class AutoDebit
    {
        private readonly UserRepository _userRepository;
        private readonly CreditCardRepository _creditCardRepository;
        private readonly BillRepository _billRepository;
        private readonly AutoDebitRepository _autoDebitRepository;

        public AutoDebit(UserRepository userRepository, CreditCardRepository creditCardRepository, BillRepository billRepository, AutoDebitRepository autoDebitRepository)
        {
            _userRepository = userRepository;
            _creditCardRepository = creditCardRepository;
            _billRepository = billRepository;
            _autoDebitRepository = autoDebitRepository;
        }

        public BaseDto DisplaysDebits(int userID)
        {
            var userAutoDebits = _autoDebitRepository.GetAll().FindAll(x => x.OwnerID == userID);

            if (userAutoDebits.Count == 0)
                return new BaseDto("Nenhuma conta cadastrada", 404);

            return new BaseDto("Contas cadastradas", 200, userAutoDebits);
        }

        public BaseDto RegisterAutoDebitProcess(bool confirmed, int userID, double value)
        {
            if (!confirmed)
                return new BaseDto("Solicitação cancelada", 200);

            var user = _userRepository.GetById(userID);

            if (value > user.MonthlyIncome)
                return new BaseDto("Valor maior que em conta", 406);

            return new BaseDto("A conta será paga automaticamente no mesmo dia que sua renda mensal entra em conta, " +
                "descontando assim o valor da fatura da conta corrente.Mas e se não houver saldo?  Sem problemas, assim que entrar saldo suficiente a fatura será paga!", 200);
        }
        public BaseDto Remove(AutoDebitEntity autoDebitEntity)
        {
            var autoDebit = _autoDebitRepository.GetAll().Find(x => x.Equals(autoDebitEntity));

            if (autoDebit != null)
            {
                _autoDebitRepository.Remove(autoDebit.Id);
                return new BaseDto("Deletado com sucesso", 200);
            }

            return new BaseDto("Conta não encontrada", 404);
        }
        public void Add(AutoDebitEntity autoDebitEntity)
        {
            _autoDebitRepository.Add(autoDebitEntity);
        }
    }
}