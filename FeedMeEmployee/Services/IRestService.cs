using FeedMeEmployee.Model;

namespace TodoREST.Services
{
    public interface IRestService
    {
        Task<List<Facture>> GetHistoFacture();

        Task<bool> CreateTransaction();
        Task<List<TransactionRecord>> GetAllTransactions();
    }
}
