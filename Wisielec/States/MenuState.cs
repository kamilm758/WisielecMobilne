using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Wisielec.Button;
using Wisielec.Database;
using Wisielec.Models;

namespace Wisielec.States
{

    class MenuState : IComponent
    {
        private Game1 game;
        private Vector2 windowSize;
        private SpriteFont titleFont;
        private SpriteFont buttonLabelFont;
        //buttons
        private TextButton newGameButton;
        private TextButton rankingButton;
        public MenuState(Game1 game)
        {
            this.game = game;
            windowSize = new Vector2(game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);
            buttonLabelFont = game.Content.Load<SpriteFont>("ButtonLabelFont");
            newGameButton = new TextButton(game.GetActivity().Resources.GetString(Resource.String.newGameLabelButton),
                buttonLabelFont, (int)windowSize.X / 2, (int)(4 * windowSize.Y / 8));
            rankingButton = new TextButton(game.GetActivity().Resources.GetString(Resource.String.rankingLabelButton),
                buttonLabelFont, (int)windowSize.X / 2, (int)(5 * windowSize.Y / 8));
            LoadContent();
        }

        private void LoadContent()
        {
            //fonty
            titleFont = game.Content.Load<SpriteFont>("TitleFont");
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Dictionary<string,Texture2D> textures)
        {
            spriteBatch.DrawString(titleFont, game.GetActivity().Resources.GetString(Resource.String.ApplicationName)
                , new Vector2(windowSize.X / 2 -(int)titleFont.MeasureString(game.GetActivity().Resources.GetString(Resource.String.ApplicationName)).X/2
                , windowSize.Y / 5- (int)titleFont.MeasureString(game.GetActivity().Resources.GetString(Resource.String.ApplicationName)).Y / 2),Color.White);
            spriteBatch.DrawString(buttonLabelFont, newGameButton.GetButtonLabel(), newGameButton.GetVectorPosition(),Color.White);
            spriteBatch.DrawString(buttonLabelFont, rankingButton.GetButtonLabel(), rankingButton.GetVectorPosition(), Color.White);
        }

        public void Update(GameTime gameTime)
        {
            CheckTouchesOptions();
        }
        private void CheckTouchesOptions()
        {
            foreach (var touch in TouchManager.GetTouches())
            {
                if (newGameButton.GetHitbox().Intersects(new Rectangle((int)touch.Position.X, (int)touch.Position.Y, 1, 1)))
                {
                    game.SetCurrentState(new PlayerNameState(game));
                }

                if (rankingButton.GetHitbox().Intersects(new Rectangle((int)touch.Position.X, (int)touch.Position.Y, 1, 1)))
                {
                    //wyświetlenie rankingu
                }
            }
        }
    }
}