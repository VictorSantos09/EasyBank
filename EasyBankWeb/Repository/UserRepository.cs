using EasyBankWeb.Entities;

namespace EasyBankWeb.Repository
{
    public class UserRepository : BaseRepository<UserEntity>
    {
        public UserRepository() : base("User")
        {

        }
    }
}