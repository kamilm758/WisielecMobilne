using System.Collections.Generic;
using System.Linq;
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
            var isPlayername = _database.Table<RankingItem>().Where(i => i.PlayerName == rankingItem.PlayerName).ToListAsync().Result;
            if (isPlayername.Count>0)
            {
                isPlayername[0].Score += rankingItem.Score;
                return _database.UpdateAsync(isPlayername[0]);
            }
            else
            {
                return _database.InsertAsync(rankingItem);
            }
        }
    }
}