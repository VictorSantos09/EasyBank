using EasyBankWeb.Entities;
using EasyBankWeb.Services;

namespace EasyBankWeb.Repository
{
    public class CreditCardRepository
    {
        private List<CreditCard> CreditCards { get; set; }

        public CreditCardRepository()
        {
            CreditCards = new List<CreditCard>();
        }

        public List<CreditCard> GetCreditCard()
        {
            return CreditCards;
        }

        public void AddCreditCard(CreditCard creditCard)
        {
            CreditCards.Add(creditCard);
        }
    }
}