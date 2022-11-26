using EasyBankWeb.Entities;

namespace EasyBankWeb.Repository
{
    public class UserRepository
    {
        private List<User> Users { get; set; }

        public UserRepository()
        {
            Users = new List<User>();
        }

        public List<User> GetUsers()
        {
            return Users;
        }

        public void AddUser(User user)
        {
            Users.Add(user);
        }

        public User? GetUserById(int id)
        {
            return Users.Find(x => x.Id == id);
        }
    }
}