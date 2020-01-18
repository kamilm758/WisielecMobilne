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
        private Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
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
            textures.Add("1", game.Content.Load<Texture2D>("hangman/1"));
            textures.Add("2", game.Content.Load<Texture2D>("hangman/2"));
            textures.Add("3", game.Content.Load<Texture2D>("hangman/3"));
            textures.Add("4", game.Content.Load<Texture2D>("hangman/4"));
            textures.Add("5", game.Content.Load<Texture2D>("hangman/5"));
            textures.Add("6", game.Content.Load<Texture2D>("hangman/6"));
            textures.Add("7", game.Content.Load<Texture2D>("hangman/7"));
            textures.Add("8", game.Content.Load<Texture2D>("hangman/8"));
            textures.Add("9", game.Content.Load<Texture2D>("hangman/9"));
            textures.Add("10", game.Content.Load<Texture2D>("hangman/10"));
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
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