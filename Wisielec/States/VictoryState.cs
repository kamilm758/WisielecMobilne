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

namespace Wisielec.States
{
    class VictoryState : IComponent
    {
        private Game1 game;
        private Rectangle hangmanRectangle;
        private Vector2 windowSize;

        public VictoryState(Game1 game)
        {
            this.game = game;
            windowSize = new Vector2(game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);
            hangmanRectangle = new Rectangle((int)windowSize.X / 27, (int)windowSize.Y / 12, (int)windowSize.X / 4, (int)(windowSize.Y / 1.2));
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Dictionary<string, Texture2D> textures)
        {
            spriteBatch.Draw(textures["8"], hangmanRectangle, Color.White);
        }

        public void Update(GameTime gameTime)
        {

        }
    }
}