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
using Wisielec.HangmanSpriteBuilder;
using Wisielec.Keyboard;

namespace Wisielec.States
{
    class GameState : IComponent
    {
        private Game1 game;
        private string playerName;
        private WordAPI word;
        private SpriteFont descriptionFont;
        private Dictionary<string, Texture2D> tekstury = new Dictionary<string, Texture2D>();
        private Dictionary<string, Rectangle> rectangles = new Dictionary<string, Rectangle>();
        private Vector2 windowSize;
        private ScreenKeyboard keyboard;
        private string definitionPartOne = "";
        private string definitionPartTwo = "";
        private int definitionDivider;
        private HangmanBuilder hangmanBuilder;

        public GameState(Game1 game, string playerName, WordAPI word)
        {
            this.game = game;
            this.playerName = playerName;
            this.word = word;
            this.keyboard = new ScreenKeyboard(game);
            windowSize = new Vector2(game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);
            definitionDivider = (int)windowSize.X / 27;
            hangmanBuilder = new HangmanBuilder(game);
            LoadContent();
        }

        private void LoadContent()
        {
            tekstury.Add("background", game.Content.Load<Texture2D>("background"));
            descriptionFont = game.Content.Load<SpriteFont>("DefinitionFont");
            SplitTheDefinition();
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(tekstury["background"], new Rectangle(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height), Color.White);
            spriteBatch.DrawString(descriptionFont, definitionPartOne, new Vector2((windowSize.X/2)-(11*definitionPartOne.Length), windowSize.Y/12), Color.White);
            spriteBatch.DrawString(descriptionFont, definitionPartTwo, new Vector2((windowSize.X / 2) - (11 * definitionPartTwo.Length), 2*windowSize.Y/12), Color.White);
            spriteBatch.DrawString(descriptionFont, word.Word, new Vector2((windowSize.X/2)-(11*word.Word.Length), 4*windowSize.Y/12), Color.White);
            keyboard.Draw(spriteBatch, gameTime);
            hangmanBuilder.Draw(spriteBatch, gameTime);
        }

        public void Update(GameTime gameTime)
        {
            var touches = TouchManager.GetTouches();
            keyboard.Update(gameTime, touches);
            hangmanBuilder.Update(gameTime);
        }
        private void SplitTheDefinition()
        {
            if (definitionDivider >= word.Results[0].Definition.Length)
            {
                definitionPartOne = word.Results[0].Definition;
                return;
            }

            while (!word.Results[0].Definition[definitionDivider].Equals(' '))
                definitionDivider--;

            definitionPartOne =new string(word.Results[0].Definition.Take(definitionDivider).ToArray());
            definitionPartTwo =new string(word.Results[0].Definition
                .TakeLast(word.Results[0].Definition.Length - definitionDivider).ToArray());
        }
    }
}