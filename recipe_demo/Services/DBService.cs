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
        //各OSのDBConnectionで利用
        public const string RECIPE_DB_FILE_NAME = "RecipeDb.db3";

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
            return await dbConnection.GetAllWithChildrenAsync<Recipe>();
        }

        public async Task<Recipe> GetRecipe(int recipeId)
        {
            return await dbConnection.GetWithChildrenAsync<Recipe>(recipeId);
        }

        public async Task AddRecipe(Recipe recipe)
        {
            await dbConnection.InsertWithChildrenAsync(recipe);
        }

        public async Task UpdateRecipe(Recipe recipe)
        {
            //UpdateWithChildrenの方だと、ItemsとStepsがうまく更新されなかった
            await dbConnection.InsertOrReplaceWithChildrenAsync(recipe);
        }

        public async Task DeleteRecipe(Recipe recipe)
        {
            await dbConnection.DeleteAsync(recipe);
        }
    }
}
