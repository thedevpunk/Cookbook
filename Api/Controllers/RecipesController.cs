using System;
using System.Collections.Generic;
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
        private readonly IRecipeService _recipeService;
        private readonly IRecipeRepository _recipeRepo;
        public RecipesController(IRecipeService recipeService, IRecipeRepository recipeRepo)
        {
            _recipeRepo = recipeRepo;
            _recipeService = recipeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes()
        {
            var user = TestData.TestUser;

            var recipes = await _recipeService.GetRecipesForUserAsync(user.Id);

            return Ok(recipes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetRecipe(Guid id)
        {
            var user = TestData.TestUser;

            var recipe = await _recipeRepo.GetRecipeByIdAsync(id);

            if (recipe == null)
            {
                return BadRequest("Not available");
            }

            return Ok(recipe);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecipe(CreateRecipeDto recipeDto)
        {
            if (await _recipeRepo.ExistsAsync(recipeDto.Id))
            {
                return Conflict("Recipe already exists!");
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