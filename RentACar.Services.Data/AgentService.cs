using Microsoft.EntityFrameworkCore;
using RentACar.Data;
using RentACar.Data.Models;
using RentACar.Services.Data.Interfaces;
using RentACar.Web.ViewModels.Agent;

namespace RentACar.Services.Data
{
    public class AgentService : IAgentService
    {
        private readonly RentACarDbContext dbContext;

        public AgentService(RentACarDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> AgentExistsByUserIdAsync(string userId)
        {
            bool result = await this.dbContext
                .Agents
                .AnyAsync(a => a.UserId.ToString() == userId);

            return result;
        }

        public async Task CreateAsync(string userId, BecomeAgentFormModel model)
        {
            Agent newAgent = new Agent()
            {
                Name = model.Name,
                UserId = Guid.Parse(userId)
            };

            await this.dbContext.Agents.AddAsync(newAgent);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<string?> GetAgentIdByUserIdAsync(string userId)
        {
            Agent? agent = await this.dbContext
                .Agents
                .FirstOrDefaultAsync(a => a.UserId.ToString() == userId);
            if (agent == null)
            {
                return null;
            }

            return agent.Id.ToString();
        }
    }
}
