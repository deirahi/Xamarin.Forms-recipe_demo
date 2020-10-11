using SQLite;

namespace recipe_demo.Services
{
    public interface IDbConnection
    {
        SQLiteAsyncConnection GetConnection();
    }
}
