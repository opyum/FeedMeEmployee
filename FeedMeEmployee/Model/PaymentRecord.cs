namespace FeedMe.CrossCutting
{
    public record class PaymentRecord
    {
        public int UserId { get; set; }
        public double AmountToPay { get; set; }
        public int UserBankAccountId { get; set; }

    }

    public record PaymentRequest
    {
        public int UserBankAccountId { get; set; }
        public double AmountToPay { get; set; }
        public int ReceiverBankAccountId { get; set; }
    }
}
