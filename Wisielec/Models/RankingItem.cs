using SQLite;

namespace Wisielec.Models
{
    public class RankingItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string PlayerName { get; set; }
        public int Score { get; set; }

        public RankingItem(string playerName, int score)
        {
            this.PlayerName = playerName;
            this.Score = score;
        }
        public RankingItem() { }
    }
}