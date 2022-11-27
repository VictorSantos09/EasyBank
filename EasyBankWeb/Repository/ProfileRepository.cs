using EasyBankWeb.Entities;
using EasyBankWeb.Services;

namespace EasyBankWeb.Repository
{
    public class ProfileRepository
    {
        private List<ProfileEntity> Profile { get; set; }

        public ProfileRepository()
        {
            Profile = new List<ProfileEntity>();
        }

        public List<ProfileEntity> GetProfile()
        {
            return Profile;
        }

        public void AddProfile(ProfileEntity profile)
        {
            Profile.Add(profile);
        }

        public void RemoveProfile(ProfileEntity profileEntity)
        {
            Profile.Remove(profileEntity);
        }

        internal void AddProfile(Profile profile)
        {
            throw new NotImplementedException();
        }
    }
}
