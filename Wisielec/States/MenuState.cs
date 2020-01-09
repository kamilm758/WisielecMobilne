using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Wisielec.States
{
    public class DataObject
    {
        public string userId { get; set; }
        public string id { get; set; }
        public string title { get; set; }
        public string completed { get; set; }
    }

    class MenuState : IComponent
    {
        private Game1 game;
        private Dictionary<string, Texture2D> tekstury = new Dictionary<string, Texture2D>();
        private Dictionary<string, Rectangle> rectangles = new Dictionary<string, Rectangle>();
        private Dictionary<string, Color> colors = new Dictionary<string, Color>();
        private Vector2 windowSize;
        private SpriteFont menuFont;
        int licznik = 0;
        string slowo="";
        public MenuState(Game1 game)
        {
            this.game = game;
            windowSize = new Vector2(game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);
            LoadContent();
        }

        private void LoadContent()
        {
            tekstury.Add("background", game.Content.Load<Texture2D>("background"));
            tekstury.Add("nowaGra", game.Content.Load<Texture2D>("NowaGra"));
            rectangles.Add("nowaGra", new Rectangle((int)windowSize.X / 2-((int)windowSize.X / 5)/2
                , (int)(2 * windowSize.Y / 5), (int)windowSize.X / 5, (int)windowSize.Y / 8));
            colors.Add("nowaGra", Color.White);
            menuFont = game.Content.Load<SpriteFont>("fontMenu");
            slowo = GetWord();
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(tekstury["background"], new Rectangle(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height), Color.White);
            spriteBatch.Draw(tekstury["nowaGra"],rectangles["nowaGra"] ,colors["nowaGra"]);
            spriteBatch.DrawString(menuFont,slowo,new Vector2(0, 0), Color.Black);
            spriteBatch.DrawString(menuFont, licznik.ToString(),new Vector2(300, 0), Color.Black);
        }

        public void Update(GameTime gameTime)
        {
            TouchManager.Update(gameTime);
            ResetColors();
            CheckTouchesOptions();
        }

        private void ResetColors()
        {
            colors["nowaGra"] = Color.White;
        }

        private void CheckTouchesOptions()
        {
            foreach (var touch in TouchManager.GetTouches())
            {
                if (rectangles["nowaGra"].Intersects(new Rectangle((int)touch.Position.X, (int)touch.Position.Y, 1, 1)))
                {
                    colors["nowaGra"] = Color.Red;
                    licznik++;
                    break;
                }
            }
        }

        public string GetWord()
        {
            string URL = "https://jsonplaceholder.typicode.com/todos/1";

            DataObject dataObject = new DataObject();
            string response = "";
            using (var client = new WebClient() { UseDefaultCredentials = true })
            {
                response = client.DownloadString(URL);
            }

            dataObject = JsonConvert.DeserializeObject<DataObject>(response);
            return dataObject.title;
        }
    }
}