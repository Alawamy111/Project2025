using Project2025.Data.Entity;

namespace Project2025.Components.Pages.ChaletComponents
{
    public interface IChaletServices
    {
        Task DeleteAsync(Chalet chalet);
        Task<Chalet?> GetChaletByChaletId(Guid ChaletId);
        Task<List<Chalet>> GetChaletsByOwnerId(Guid OwnerId);
       

        Task<List<Chalet>> GetChalets();
        Task<Chalet> Upsert(Chalet chalet);
    }
}
