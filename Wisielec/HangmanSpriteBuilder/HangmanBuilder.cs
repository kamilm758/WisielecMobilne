using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Wisielec.HangmanSpriteBuilder
{
    class HangmanBuilder : IComponent
    {
        private Game1 game;
        private Rectangle hangmanRectangle;
        private int buildLevel = 1;
        private Vector2 windowSize;

        public HangmanBuilder(Game1 game)
        {
            this.game = game;
            windowSize = new Vector2(game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);
            hangmanRectangle = new Rectangle((int)windowSize.X / 27,(int) windowSize.Y / 12,(int) windowSize.X / 4,(int)(windowSize.Y / 1.2));
            LoadContent();
        }
        private void LoadContent()
        {

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Dictionary<string,Texture2D> textures)
        {
            spriteBatch.Draw(textures[buildLevel.ToString()], hangmanRectangle, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void BuildForward()
        {
            buildLevel++;
        }
    }
}