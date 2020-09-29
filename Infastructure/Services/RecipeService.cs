using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;

namespace Infastructure.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepo;
        private readonly IUserRepository _userRepo;
        private readonly IGroupRepository _groupRepo;
        public RecipeService(IRecipeRepository recipeRepo, IUserRepository userRepo, IGroupRepository groupRepo)
        {
            _groupRepo = groupRepo;
            _userRepo = userRepo;
            _recipeRepo = recipeRepo;
        }

        public async Task<IReadOnlyList<Recipe>> GetRecipesForGroupAsync(Guid groupId)
        {
            var group = await _groupRepo.GetGroupByIdAsync(groupId);
            if (group == null)
            {
                throw new Exception("Group does not exist.");
            }

            return await _recipeRepo.GetRecipesByIdsAsync(group.RecipeIds);
        }

        public async Task<IReadOnlyList<Recipe>> GetRecipesForUserAsync(Guid userId)
        {
            var user = await _userRepo.GetUser(userId);
            if (user == null)
            {
                throw new Exception("User does not exist.");
            }

            var groups = await _groupRepo.GetGroupsByIds(user.GroupIds);

            var recipeIds = new List<Guid>();
            foreach(Group group in groups)
            {
                recipeIds.AddRange(group.RecipeIds);
            }
            recipeIds.Distinct();

            return await _recipeRepo.GetRecipesByIdsAsync(recipeIds);
        }
    }
}