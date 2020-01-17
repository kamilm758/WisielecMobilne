using System.Collections.Generic;
using System.Threading;
using Android.App;
using Android.Content.Res;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RestSharp;
using Wisielec.Keyboard;

namespace Wisielec.States
{
    class PlayerNameState : IComponent
    {
        private Game1 game;
        private Dictionary<string, Texture2D> tekstury = new Dictionary<string, Texture2D>();
        private Dictionary<string, Rectangle> rectangles = new Dictionary<string, Rectangle>();
        private Vector2 windowSize;
        private ScreenKeyboard keyboard;
        private SpriteFont font;
        private SpriteFont playerNameFont;
        private string playerName="";
        private WordAPI word;
        private bool success = false;

        public PlayerNameState(Game1 game)
        {
            this.game = game;
            this.keyboard = new ScreenKeyboard(game);
            windowSize = new Vector2(game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);
            LoadContent();
        }

        private void LoadContent()
        {
            tekstury.Add("background", game.Content.Load<Texture2D>("background"));
            tekstury.Add("OkTexture", game.Content.Load<Texture2D>("Ok"));
            rectangles.Add("OkRectangle", new Rectangle((int)(windowSize.X / 14), (int)(7 * windowSize.Y / 9), (int)windowSize.X / 12, (int)windowSize.Y / 12));
            font = game.Content.Load<SpriteFont>("fontMenu");
            playerNameFont = game.Content.Load<SpriteFont>("TitleFont");

            //wczytywanie w czasie wpisywania nazwy gracza w nowym wątku
            ThreadStart ts = new ThreadStart(GetWord);
            Thread newThread = new Thread(ts);
            newThread.Start();
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(tekstury["background"], new Rectangle(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height), Color.White);
            spriteBatch.DrawString(font, game.GetActivity().Resources.GetString(Resource.String.wpiszNazweGracza)
                ,new Vector2(windowSize.X/3,windowSize.Y/10),Color.White);
            spriteBatch.DrawString(playerNameFont, playerName,
                new Vector2(windowSize.X / 3, 2 * windowSize.Y / 10),Color.White);
            spriteBatch.Draw(tekstury["OkTexture"], rectangles["OkRectangle"], Color.White);
            keyboard.Draw(spriteBatch, gameTime);
        }

        public void Update(GameTime gameTime)
        {
            playerName += keyboard.GetPressedKeys();
            CheckTouchesOptions(gameTime);
        }

        private void CheckTouchesOptions(GameTime gameTime)
        {
            var touches = TouchManager.GetTouches();
            foreach (var touch in touches)
            {
                if (rectangles["OkRectangle"].Intersects(new Rectangle((int)touch.Position.X, (int)touch.Position.Y, 1, 1)))
                {
                    if(success) //czekamy na to aż api pobierze wyraz
                        game.SetCurrentState(new GameState(game,playerName,word));
                }
            }
            keyboard.Update(gameTime,touches);
        }
        //get word with description from API
        public void GetWord()
        {
            var client = new RestClient("https://wordsapiv1.p.rapidapi.com/words/?random=true");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-host", "wordsapiv1.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "456382e8d4msh04e537e61f0f24fp1a2670jsn8bf005196cc2");
            while (true)
            {
                IRestResponse response = client.Execute(request);
                this.word = Newtonsoft.Json.JsonConvert.DeserializeObject<WordAPI>(response.Content);
                if (word.Results != null)
                {
                    if (word.Results[0].Definition != null)
                        break;
                }
            }
            success = true;
        }
    }
}