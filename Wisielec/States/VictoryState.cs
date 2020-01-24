using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Wisielec.API;
using Wisielec.Button;

namespace Wisielec.States
{
    class VictoryState : IComponent
    {
        private Game1 game;
        private Rectangle hangmanRectangle;
        private Vector2 windowSize;
        private SpriteFont resultFont;
        private Vector2 resultVector;
        private TextButton backToMenu;
        private TextButton playAgainButton;
        private SpriteFont buttonLabelFont;
        private string playerName;
        private WordAPI word;
        private APICommunicator communicator;
        private bool success = false;

        public VictoryState(Game1 game,string playerName)
        {
            this.game = game;
            buttonLabelFont = game.Content.Load<SpriteFont>("ButtonLabelFont");
            resultFont = game.Content.Load<SpriteFont>("ResultInformationFont");
            windowSize = new Vector2(game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);
            hangmanRectangle = new Rectangle((int)windowSize.X / 27, (int)windowSize.Y / 12, (int)windowSize.X / 4, (int)(windowSize.Y / 1.2));
            this.playerName = playerName;
            communicator = new APICommunicator(game);
            LoadContent();
        }

        private void LoadContent()
        {
            resultVector = new Vector2(windowSize.X / 2 - (int)resultFont.MeasureString(game.GetActivity().Resources.GetString(Resource.String.resultWin)).X / 2
                , windowSize.Y / 5 - (int)resultFont.MeasureString(game.GetActivity().Resources.GetString(Resource.String.resultWin)).Y / 2);

            backToMenu = new TextButton(game.GetActivity().Resources.GetString(Resource.String.backToMenuButtonLabel)
                ,buttonLabelFont,(int)(3*windowSize.X / 14), (int)(13 * windowSize.Y / 15));
            playAgainButton = new TextButton(game.GetActivity().Resources.GetString(Resource.String.playAgainButtonLabel), buttonLabelFont
                , (int)(10 * windowSize.X / 14), (int)(13 * windowSize.Y / 15));

            //wczytywanie w czasie wpisywania nazwy gracza w nowym wątku
            ThreadStart ts = new ThreadStart(GetWordFromApi);
            Thread newThread = new Thread(ts);
            newThread.Start();
        }


        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Dictionary<string, Texture2D> textures)
        {
            spriteBatch.DrawString(resultFont, game.GetActivity().Resources.GetString(Resource.String.resultWin), resultVector, Color.White);
            spriteBatch.Draw(textures["8"], hangmanRectangle, Color.White);
            spriteBatch.DrawString(buttonLabelFont, playAgainButton.GetButtonLabel(), playAgainButton.GetVectorPosition(), Color.White);
            spriteBatch.DrawString(buttonLabelFont, backToMenu.GetButtonLabel(), backToMenu.GetVectorPosition(), Color.White);
            spriteBatch.Draw(textures["captainAmerica"], new Rectangle(3*(int)windowSize.X / 5, (int)windowSize.Y / 8, (int)windowSize.X / 4, 4*(int)windowSize.Y / 5), Color.White);
        }

        public void Update(GameTime gameTime)
        {
            CheckTouchesOptions();
        }

        private void CheckTouchesOptions()
        {
            foreach (var touch in TouchManager.GetTouches())
            {
                if (playAgainButton.GetHitbox().Intersects(new Rectangle((int)touch.Position.X, (int)touch.Position.Y, 1, 1)))
                {
                    if(success)
                        game.SetCurrentState(new GameState(game,playerName,word));
                }

                if (backToMenu.GetHitbox().Intersects(new Rectangle((int)touch.Position.X, (int)touch.Position.Y, 1, 1)))
                {
                    //wyświetlenie rankingu
                    game.SetCurrentState(new MenuState(game));
                }
            }
        }

        private void GetWordFromApi()
        {
            word = communicator.GetWord();
            success = true;
        }
    }
}