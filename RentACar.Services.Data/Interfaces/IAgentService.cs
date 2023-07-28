using RentACar.Web.ViewModels.Agent;

namespace RentACar.Services.Data.Interfaces
{
	public interface IAgentService
	{
		Task<bool> AgentExistsByUserIdAsync(string userId);

		Task CreateAsync (string userId, BecomeAgentFormModel model);

		Task<string?> GetAgentIdByUserIdAsync(string userId);
	}
}
