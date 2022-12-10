using EasyBankWeb.Entities;

namespace EasyBankWeb.Repository
{
    public class CreditCardRepository : BaseRepository<CreditCardEntity>
    {
        public CreditCardRepository() : base("CreditCard")
        {

        }
    }
}