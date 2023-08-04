namespace RentACar.Services.Data.Interfaces
{
    using RentACar.Web.ViewModels.Agent;

    public interface IAgentService
    {
        Task<bool> AgentExistsByUserIdAsync(string userId);

        Task CreateAsync(string userId, BecomeAgentFormModel model);

        Task<string?> GetAgentIdByUserIdAsync(string userId);
    }
}
