using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IRecipeService
    {
         Task<IReadOnlyList<Recipe>> GetRecipesForGroupAsync(Guid groupId);

         Task<IReadOnlyList<Recipe>> GetRecipesForUserAsync(Guid userId);
    }
}