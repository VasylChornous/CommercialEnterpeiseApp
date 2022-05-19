namespace CommercialEnterprise.Infrastructure.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string KeyWord { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string TelephoneNumber { get; set; }

        public BankCard Card { get; set; }
    }
}
