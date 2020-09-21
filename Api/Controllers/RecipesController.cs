using System;
using System.Threading.Tasks;
using Api.Dtos;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeRepository _recipeRepo;
        public RecipesController(IRecipeRepository recipeRepo)
        {
            _recipeRepo = recipeRepo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecipe(CreateRecipeDto recipeDto)
        {
            if(await _recipeRepo.ExistsAsync(recipeDto.Id))
            {
                return Conflict();
            }

            var recipe = new Recipe
            {
                Id = recipeDto.Id,
                CategoryId = recipeDto.CategoryId,
                Cook = TestData.TestUser,
                DurationInMinutes = recipeDto.DurationInMinutes,
                Ingredients = recipeDto.Ingredients,
                NumberOfPortions = recipeDto.NumberOfPortions,
                Steps = recipeDto.Steps,
                Story = recipeDto.Story,
                Tags = recipeDto.Tags,
                Title = recipeDto.Title,
                UserId = TestData.TestUser.Id
            };

            await _recipeRepo.CreateRecipeAsync(recipe);

            return Ok();
        }
    }
}