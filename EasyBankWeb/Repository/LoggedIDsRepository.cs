using EasyBankWeb.Entities;
using System.Text.Json;

namespace EasyBankWeb.Repository
{
    public class LoggedIDsRepository
    {
        private readonly string _pathFile;

        public LoggedIDsRepository()
        {
            _pathFile = $@"C:\Users\victo\Desktop\EasyBank\EasyBankWeb\DataBase\{"LoggedIDs"}.json";
        }

        public void Add(int entity)
        {
            var list = GetAll();

            list.Add(entity);

            string json = JsonSerializer.Serialize(list);
            File.WriteAllText(_pathFile, json);
        }
        public List<int> GetAll()
        {
            using (StreamReader r = new StreamReader(_pathFile))
            {
                string json = r.ReadToEnd();

                if (string.IsNullOrWhiteSpace(json))
                {
                    return new List<int>();
                }

                return JsonSerializer.Deserialize<List<int>>(json);
            }
        }
        public int? GetById(int id)
        {
            return GetAll().Find(x => x == id);
        }
        public void Remove(int id)
        {
            var list = GetAll();

            var entity = list.Find(x => x == id);

            if (entity == 0)
                return;

            list.Remove(entity);

            string json = JsonSerializer.Serialize(list);
            File.WriteAllText(_pathFile, json);
        }
        public void Update(int id, int newEntity)
        {
            var list = GetAll();

            var entity = list.Find(x => x == id);

            if (entity == 0)
                return;

            list.Remove(entity);

            list.Add(newEntity);

            string json = JsonSerializer.Serialize(list);
            File.WriteAllText(_pathFile, json);
        }
    }
}
