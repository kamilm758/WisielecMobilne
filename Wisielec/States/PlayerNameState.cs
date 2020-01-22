using System.Collections.Generic;
using System.Threading;
using Android.App;
using Android.Content.Res;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RestSharp;
using Wisielec.Keyboard;
using Java.Util;

namespace Wisielec.States
{
    class PlayerNameState : IComponent
    {
        private Game1 game;
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
            this.keyboard = new ScreenKeyboard(game,KeyboardOperatingMode.PlayerName);
            windowSize = new Vector2(game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);
            LoadContent();
        }

        private void LoadContent()
        {
            rectangles.Add("OkRectangle", new Rectangle((int)(windowSize.X / 14), (int)(7 * windowSize.Y / 9), (int)windowSize.X / 12, (int)windowSize.Y / 12));
            font = game.Content.Load<SpriteFont>("fontMenu");
            playerNameFont = game.Content.Load<SpriteFont>("TitleFont");

            //wczytywanie w czasie wpisywania nazwy gracza w nowym wątku
            ThreadStart ts = new ThreadStart(GetWord);
            Thread newThread = new Thread(ts);
            newThread.Start();
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Dictionary<string,Texture2D> textures)
        {
            spriteBatch.DrawString(font, game.GetActivity().Resources.GetString(Resource.String.enterPlayerName)
                ,new Vector2(windowSize.X/3,windowSize.Y/10),Color.White);
            spriteBatch.DrawString(playerNameFont, playerName,
                new Vector2(windowSize.X / 3, 2 * windowSize.Y / 10),Color.White);
            spriteBatch.Draw(textures["OkTexture"], rectangles["OkRectangle"], Color.White);
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
                    {
                        if (word.Results[0].Definition.Length < (int)(2 * windowSize.X / 27))
                        {
                            break;
                        }
                    }
                }
            }
            success = true;
        }
    }
}