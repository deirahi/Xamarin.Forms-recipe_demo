using System;
using System.IO;
using SQLite;
using recipe_demo.Services;
using recipe_demo.iOS.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(DbConnection))]

namespace recipe_demo.iOS.Services
{
    public class DbConnection : IDbConnection
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(folderPath, DBService.RECIPE_DB_FILE_NAME);

            return new SQLiteAsyncConnection(path);
        }
    }
}
