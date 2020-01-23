using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Wisielec.Models;

namespace Wisielec.States
{
    public class RankingState : IComponent
    {
        private Game1 game;
        private List<RankingItem> rankingItems = new List<RankingItem>();
        private SpriteFont rankingTitleFont;
        private SpriteFont rankingItemsFont;
        private List<Vector2> vectorsForStrings = new List<Vector2>();
        private Vector2 windowSize;

        public RankingState(Game1 game)
        {
            this.game = game;
            windowSize = new Vector2(game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);
            LoadContent();
        }

        private void LoadContent()
        {
            rankingTitleFont = game.Content.Load<SpriteFont>("RankingTitleFont");
            rankingItemsFont = game.Content.Load<SpriteFont>("RankingItemsFont");
            GetRankingItemsFromDatabase();
            //vector dla tytułu rankingu
            vectorsForStrings.Add(new Vector2(windowSize.X / 2 - rankingTitleFont.MeasureString(game.GetActivity().Resources.GetString(Resource.String.rankingTitle)).X/2, windowSize.Y / 8));
            //dla 10 najlepszych wyników-> wypisz na ekran
            rankingItems = rankingItems.OrderByDescending(i => i.Score).ToList();
            for(int i = 0; i < rankingItems.Count; i++)
            {
                vectorsForStrings.Add(new Vector2(windowSize.X / 2 - rankingItemsFont.MeasureString(rankingItems[i].PlayerName).X / 2, (i+4)*windowSize.Y / 14));
                if (i == 8)
                    break;
            }
        }
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Dictionary<string, Texture2D> textures)
        {
            spriteBatch.DrawString(rankingTitleFont, game.GetActivity().Resources.GetString(Resource.String.rankingTitle),vectorsForStrings[0], Color.White);
            for (int i = 1; i < vectorsForStrings.Count; i++)
            {
                spriteBatch.DrawString(rankingItemsFont,i.ToString()+"."+rankingItems[i - 1].PlayerName + ": " + rankingItems[i - 1].Score, vectorsForStrings[i], Color.White);
            }
        }

        public void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                game.SetCurrentState(new MenuState(game));
        }

        public void GetRankingItemsFromDatabase()
        {
            rankingItems = game.GetDatabase().GetRankingItemsAsync().Result;
        }
    }
}