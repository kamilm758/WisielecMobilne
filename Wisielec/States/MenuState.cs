using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Wisielec.Database;
using Wisielec.Models;

namespace Wisielec.States
{

    class MenuState : IComponent
    {
        private Game1 game;
        private Dictionary<string, Texture2D> tekstury = new Dictionary<string, Texture2D>();
        private Dictionary<string, Rectangle> rectangles = new Dictionary<string, Rectangle>();
        private Vector2 windowSize;
        private SpriteFont menuFont;
        private SpriteFont titleFont;
        List<RankingItem> ranking = new List<RankingItem>();
        public MenuState(Game1 game)
        {
            this.game = game;
            windowSize = new Vector2(game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);
            LoadContent();
        }

        private void LoadContent()
        {
            //ładowanie tekstur
            tekstury.Add("background", game.Content.Load<Texture2D>("background"));
            tekstury.Add("nowaGra", game.Content.Load<Texture2D>("NowaGra"));
            tekstury.Add("ranking", game.Content.Load<Texture2D>("Ranking"));
            //tworzenie kwadratów(pozycje na spriteBatch)
            rectangles.Add("nowaGra", new Rectangle((int)windowSize.X / 2-((int)windowSize.X / 7)/2
                , (int)(4*windowSize.Y / 8), (int)windowSize.X / 6, (int)windowSize.Y / 12));
            rectangles.Add("ranking", new Rectangle((int)windowSize.X / 2 - ((int)windowSize.X / 7) / 2
                , (int)(5*windowSize.Y / 8), (int)windowSize.X / 6, (int)windowSize.Y / 12));
            //fonty
            menuFont = game.Content.Load<SpriteFont>("fontMenu");
            titleFont = game.Content.Load<SpriteFont>("TitleFont");
            //slowo = GetWord();
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(tekstury["background"], new Rectangle(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height), Color.White);
            spriteBatch.DrawString(titleFont, "Wisielec", new Vector2(windowSize.X / 2 - 260, windowSize.Y / 5),Color.White);
            spriteBatch.Draw(tekstury["nowaGra"],rectangles["nowaGra"] ,Color.White);
            spriteBatch.Draw(tekstury["ranking"], rectangles["ranking"], Color.White);
            for(int i = 0; i < ranking.Count; i++)
            {
                spriteBatch.DrawString(menuFont, ranking[i].PlayerName + " id: " + ranking[i].ID
                    , new Vector2(400, i * 100), Color.Black);
            }
        }

        public void Update(GameTime gameTime)
        {
            CheckTouchesOptions();
        }
        private void CheckTouchesOptions()
        {
            foreach (var touch in TouchManager.GetTouches())
            {
                if (rectangles["nowaGra"].Intersects(new Rectangle((int)touch.Position.X, (int)touch.Position.Y, 1, 1)))
                {
                    game.SetCurrentState(new PlayerNameState(game));
                }

                if (rectangles["ranking"].Intersects(new Rectangle((int)touch.Position.X, (int)touch.Position.Y, 1, 1)))
                {
                   //wyświetlenie rankingu
                }
            }
        }
    }
}