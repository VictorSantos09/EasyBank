using EasyBankWeb.Entities;

namespace EasyBankWeb.Repository
{
    public class CreditCardRepository
    {
        private List<CreditCardEntity> CreditCards { get; set; }

        public CreditCardRepository()
        {
            CreditCards = new List<CreditCardEntity>();
        }

        public List<CreditCardEntity> GetCreditCard()
        {
            return CreditCards;
        }

        public void AddCreditCard(CreditCardEntity creditCard)
        {
            CreditCards.Add(creditCard);
        }

        public CreditCardEntity? GetCreditCardById(int id)
        {
            return CreditCards.Find(x => x.OwnerID == id);
        }
    }
}