using System.Collections.Generic;
using System.Threading.Tasks;
using recipe_demo.Models;
using recipe_demo.Services;
using recipe_demo.ViewModels;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;

namespace recipe_demo.Services
{
    public class DBService:IDbService
    {
        private SQLiteAsyncConnection dbConnection;

        public DBService(IDbConnection db)
        {
            dbConnection = db.GetConnection();
            dbConnection.CreateTableAsync<Recipe>();
            dbConnection.CreateTableAsync<Item>();
            dbConnection.CreateTableAsync<Step>();
        }

        public async Task<IEnumerable<Recipe>> GetRecipesAsync()
        {
            return await dbConnection.Table<Recipe>().ToListAsync();
        }

        public async Task<Recipe> GetRecipe(int recipe_id)
        {
            return await dbConnection.GetWithChildrenAsync<Recipe>(recipe_id);
        }

        public async Task AddRecipe(Recipe recipe)
        {
            await dbConnection.InsertWithChildrenAsync(recipe);
        }

        public async Task UpdateRecipe(Recipe recipe)
        {
            await dbConnection.UpdateWithChildrenAsync(recipe);
        }

        //お試しで、削除だけrecursivelyをtrueにしてみた
        public async Task DeleteRecipe(Recipe recipe)
        {
            await dbConnection.DeleteAsync(recipe, true);
        }
    }
}
