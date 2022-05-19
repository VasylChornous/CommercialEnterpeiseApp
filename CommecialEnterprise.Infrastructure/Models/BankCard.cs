namespace CommercialEnterprise.Infrastructure.Models
{
    public class BankCard
    {
        public int Id { get; set; }
        public long Number { get; set; }
        public string Date { get; set; }
        public int CVV { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
