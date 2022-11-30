using EasyBankWeb.Entities;

namespace EasyBankWeb.Repository
{
    public class UserRepository
    {
        private List<UserEntity> Users { get; set; }

        public UserRepository()
        {
            Users = new List<UserEntity>();
        }

        public List<UserEntity> GetUsers()
        {
            return Users;
        }

        public void AddUser(UserEntity user)
        {
            Users.Add(user);
        }

        public UserEntity? GetUserById(int id)
        {
            return Users.Find(x => x.Id == id);
        }

        public void RemoveUser(UserEntity user)
        {
            Users.Remove(user);
        }
    }
}