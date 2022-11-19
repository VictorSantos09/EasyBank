namespace EasyBank.Entities
{
    public class AutoDebit
    {
        public int Id { get; set; }
        public int OwnerID { get; set; }
        public bool Activated { get; set; }
    }
}