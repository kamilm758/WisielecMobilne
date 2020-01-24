using System.Collections.Generic;
using System.Threading;
using Android.App;
using Android.Content.Res;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RestSharp;
using Wisielec.Keyboard;
using Java.Util;
using System;
using Wisielec.API;

namespace Wisielec.States
{
    class PlayerNameState : IComponent
    {
        private Game1 game;
        private Dictionary<string, Rectangle> rectangles = new Dictionary<string, Rectangle>();
        private APICommunicator communicator;
        private Vector2 windowSize;
        private ScreenKeyboard keyboard;
        private SpriteFont font;
        private SpriteFont playerNameFont;
        private SpriteFont informationFont;
        private string playerName="";
        private WordAPI word=null;
        private bool success = false;
        private Color OkButtonColor = Color.Red;

        public PlayerNameState(Game1 game)
        {
            this.game = game;
            communicator = new APICommunicator(game);
            this.keyboard = new ScreenKeyboard(game,KeyboardOperatingMode.PlayerName);
            windowSize = new Vector2(game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);
            LoadContent();
        }

        private void LoadContent()
        {
            rectangles.Add("OkRectangle", new Rectangle((int)(windowSize.X / 14), (int)(7 * windowSize.Y / 9), (int)windowSize.X / 12, (int)windowSize.Y / 12));
            font = game.Content.Load<SpriteFont>("fontMenu");
            informationFont = game.Content.Load<SpriteFont>("InformationFont");
            playerNameFont = game.Content.Load<SpriteFont>("TitleFont");

            //wczytywanie w czasie wpisywania nazwy gracza w nowym wątku
            ThreadStart ts = new ThreadStart(GetWordFromApi);
            Thread newThread = new Thread(ts);
            newThread.Start();
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Dictionary<string,Texture2D> textures)
        {
            spriteBatch.DrawString(font, game.GetActivity().Resources.GetString(Resource.String.enterPlayerName)
                ,new Vector2(windowSize.X/3,windowSize.Y/10),Color.White);

            spriteBatch.DrawString(informationFont, game.GetActivity().Resources.GetString(Resource.String.fetchApiInformation)+communicator.GetFetchProgress().ToString()+"%"
                , new Vector2(4*windowSize.X / 5, windowSize.Y / 10), Color.White);

            spriteBatch.DrawString(playerNameFont, playerName,
                new Vector2(windowSize.X /2-playerNameFont.MeasureString(playerName).X/2 , 2 * windowSize.Y / 10),Color.White);
            spriteBatch.Draw(textures["OkTexture"], rectangles["OkRectangle"], OkButtonColor);
            spriteBatch.Draw(textures["drawingArrow"], new Rectangle((int)windowSize.X / 15, 5 * (int)windowSize.Y / 9, (int)windowSize.X / 5, (int)windowSize.Y / 9), Color.White);

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

        private void GetWordFromApi()
        {
            word = communicator.GetWord();
            success = true;
            OkButtonColor = Color.White;
        }
    }
}