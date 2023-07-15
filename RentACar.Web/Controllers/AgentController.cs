﻿namespace RentACar.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using RentACar.Services.Data.Interfaces;
    using RentACar.Web.Infastructure.Extensions;
    using RentACar.Web.ViewModels.Agent;

    [Authorize]
    public class AgentController : Controller
    {
        private readonly IAgentService agentService;

        public AgentController(IAgentService agentService)
        {
            this.agentService = agentService;
        }

        [HttpGet]
        public async Task<IActionResult> Become()
        {
            string? userId = this.User.GetId();
            bool isAgent = await this.agentService.AgentExistsByUserIdAsync(userId);
            if (isAgent)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Become(BecomeAgentFormModel model)
        {
            string? userId = this.User.GetId();
            bool isAgent = await this.agentService.AgentExistsByUserIdAsync(userId);
            if (isAgent)
            {
                return RedirectToAction("Index", "Home");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            try
            {
                await this.agentService.Create(userId, model);

            }
            catch (Exception)
            {

                throw new Exception("An unexpected error occured!");
                return this.RedirectToAction("Index", "Home");
            }

            return this.RedirectToAction("Index", "Home");
        }

    }
}
