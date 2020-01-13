using System.Collections.Generic;
using Android.App;
using Android.Content.Res;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
            font = game.Content.Load<SpriteFont>("fontMenu");
            playerNameFont = game.Content.Load<SpriteFont>("TitleFont");
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(tekstury["background"], new Rectangle(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height), Color.White);
            spriteBatch.DrawString(font, game.GetActivity().Resources.GetString(Resource.String.wpiszNazweGracza)
                ,new Vector2(windowSize.X/3,windowSize.Y/10),Color.White);
            spriteBatch.DrawString(playerNameFont, playerName,
                new Vector2(windowSize.X / 3, 2 * windowSize.Y / 10),Color.White);
            keyboard.Draw(spriteBatch, gameTime);
        }

        public void Update(GameTime gameTime)
        {
            keyboard.Update(gameTime);
            playerName += keyboard.GetPressedKeys();
        }
    }
}