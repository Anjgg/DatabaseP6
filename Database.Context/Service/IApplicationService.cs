using Database.Context.Model;

namespace Database.Context.Service
{
    public interface IApplicationService
    {
        Task<List<Ticket>> GetProduitsAsync();

        Task<int> AddAsync(List<ApiModel> requete); 
        
        Task<int> DeleteAllAsync();    
    }
}