namespace FeedMeEmployee.Model
{
    public sealed class TransactionRecord
    {
        public int Id { get; set; }
        public DateTime DateHeure { get; set; }
        public int ClientId { get; set; }
        public int RestoId { get; set; }
        public decimal Montant { get; set; }
        public bool CheckedByResto { get; set; }
    }
}
