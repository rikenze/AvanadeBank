namespace AvanadeBank.API.Entities
{
    public class BankAccount
    {
        public int Id { get; set; }

        public string OwnerName { get; set; } = default!;

        public decimal Balance { get; set; }
    }
}
