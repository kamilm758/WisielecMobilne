using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Wisielec.Models;

namespace Wisielec.Database
{
    public class SqliteDatabase
    {
        readonly SQLiteAsyncConnection _database;
        public SqliteDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<RankingItem>().Wait();
        }

        public Task<List<RankingItem>> GetRankingItemsAsync()
        {
            return _database.Table<RankingItem>().ToListAsync();
        }

        public Task<int> SaveRankingItemsAsync(RankingItem rankingItem)
        {
            return _database.InsertAsync(rankingItem);
        }

    }
}