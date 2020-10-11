using System.Collections.Generic;
using System.Threading.Tasks;
using recipe_demo.Models;

namespace recipe_demo.Services
{
    public interface IDbService
    {
        Task<IEnumerable<Recipe>> GetRecipesAsync();

        Task<Recipe> GetRecipe(int recipe_id);

        Task AddRecipe(Recipe recipe);

        Task UpdateRecipe(Recipe recipe);

        Task DeleteRecipe(Recipe recipe);

    }
}
