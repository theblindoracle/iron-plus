using IronPlus.Interfaces;
using IronPlus.Models;
using SQLite;

namespace IronPlus.Services
{
    public class DatabaseService : IDatabaseService
    {
        readonly SQLiteAsyncConnection database;

        public DatabaseService()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "IronPlus.db3");
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Barbell>().Wait();
        }
        public async Task<List<Barbell>> GetBarbellsAsync()
        {
            return await database.Table<Barbell>().ToListAsync();
        }

        public async Task<int> SaveBarbellAsync(Barbell barbell)
        {
            if (barbell.ID != 0)
            {
                return await database.UpdateAsync(barbell);
            }
            else
            {
                return await database.InsertAsync(barbell);
            }
        }

        public async Task<Barbell> GetBarbellAsync(int id)
        {
            return await database.Table<Barbell>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public async Task<int> DeleteBarbellAsync(Barbell barbell)
        {
            return await database.DeleteAsync(barbell);
        }
    }
}
