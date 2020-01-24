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
using Wisielec.HangmanLogic;
using Wisielec.HangmanSpriteBuilder;
using Wisielec.Keyboard;
using Wisielec.Models;

namespace Wisielec.States
{
    class GameState : IComponent
    {
        private readonly Game1 game;
        private readonly string  playerName;
        private readonly WordAPI word;
        private SpriteFont descriptionFont;
        private SpriteFont informationFont;
        private Vector2 windowSize;
        private readonly ScreenKeyboard keyboard;
        private string definitionPartOne = "";
        private string definitionPartTwo = "";
        private int definitionDivider;
        private readonly HangmanBuilder hangmanBuilder;
        private readonly HangmanGame hangmanGame;
        private int pointsToObtain;
        private bool usedPrompt = false;
        private string informationAboutTakingPrompt="";
        private Random rnd = new Random(); //do losowanie litery do podpowiedzi
        public GameState(Game1 game, string playerName, WordAPI word)
        {
            this.game = game;
            this.playerName = playerName;
            this.word = word;
            this.keyboard = new ScreenKeyboard(game,KeyboardOperatingMode.Game);
            windowSize = new Vector2(game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);
            definitionDivider = (int)windowSize.X / 27;
            hangmanBuilder = new HangmanBuilder(game);
            hangmanGame = new HangmanGame(word.Word);
            //jeśli gracz wygra-> zarobi tyle punktów ile słowo ma liter
            pointsToObtain = hangmanGame.GetRemainingLettersToGuess();
            game.GetActivity().onShake += (o, e) => TakePrompt();
            LoadContent();
        }

        private void LoadContent()
        {
            descriptionFont = game.Content.Load<SpriteFont>("DefinitionFont");
            informationFont = game.Content.Load<SpriteFont>("InformationFont");
            SplitTheDefinition();
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Dictionary<string,Texture2D> textures)
        {
            hangmanBuilder.Draw(spriteBatch, gameTime,textures);
            spriteBatch.DrawString(descriptionFont, definitionPartOne, new Vector2((windowSize.X/2)-(11*definitionPartOne.Length), windowSize.Y/12), Color.White);
            spriteBatch.DrawString(descriptionFont, definitionPartTwo, new Vector2((windowSize.X / 2) - (11 * definitionPartTwo.Length), 2*windowSize.Y/12), Color.White);
            //spriteBatch.DrawString(descriptionFont, word.Word, new Vector2((windowSize.X/2)-(11*word.Word.Length), 4*windowSize.Y/12), Color.White);
            spriteBatch.DrawString(descriptionFont, hangmanGame.GetWordPattern(), new Vector2((windowSize.X / 2) - (11 * word.Word.Length), 5 * windowSize.Y / 12), Color.White);
            spriteBatch.DrawString(informationFont, informationAboutTakingPrompt,
                new Vector2(4 * windowSize.X / 5, 7*windowSize.Y / 8), Color.Red);

            keyboard.Draw(spriteBatch, gameTime);
        }

        public void Update(GameTime gameTime)
        {
            //pobieramy wciśniętą literę
            var clickedLetter = keyboard.GetPressedKeys();
            if (clickedLetter!="") //jeśli jakakolwiek litera została wciśnięta
            {
                keyboard.LockKey(clickedLetter); //blokujemy ją
                var isInWord = hangmanGame.CheckLetterInWord(clickedLetter[0]); //sprawdzamy czy występuje w słowie

                if (!isInWord) //jeśli nie->budujemy wisielca
                {
                    hangmanBuilder.BuildForward();
                }
            }
            var touches = TouchManager.GetTouches();
            keyboard.Update(gameTime, touches);
            hangmanBuilder.Update(gameTime);

            //jeśli graczowi nie zostało ani jedno życie-> gracz przegrywa->wejście do ekranu porażki

            if (hangmanGame.GetLifes() == 0)
            {
                foreach (Delegate d in game.GetActivity().onShake.GetInvocationList())
                {
                    game.GetActivity().onShake -= (EventHandler)d;
                }

                game.GetDatabase().SaveRankingItemsAsync(new RankingItem(playerName,(-1)* pointsToObtain));
                game.SetCurrentState(new DefeatState(game,playerName,word.Word));
            }
            //jeśli nie ma więcej liter do zgadnięcia->gracz wygrywa
            if (hangmanGame.GetRemainingLettersToGuess() == 0)
            {
                foreach (Delegate d in game.GetActivity().onShake.GetInvocationList())
                {
                    game.GetActivity().onShake -= (EventHandler)d;
                }

                game.GetDatabase().SaveRankingItemsAsync(new RankingItem(playerName, pointsToObtain));
                game.SetCurrentState(new VictoryState(game,playerName));
            }

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

        public void TakePrompt()
        {
            if (usedPrompt == true)
                return;
            int random;
            do
            {
                random = rnd.Next(0, word.Word.Length);
            } while (hangmanGame.GetWordPattern()[random] != '?');
            keyboard.LockKey(word.Word[random].ToString());
            hangmanGame.CheckLetterInWord(word.Word[random]);
            usedPrompt = true;
            //wywołanie wątku aby pokazał informację
            ThreadStart threadStart = new ThreadStart(TakePromptInformation);
            Thread t = new Thread(threadStart);
            t.Start();
        }

        public void TakePromptInformation()
        {
            informationAboutTakingPrompt = game.GetActivity().Resources.GetString(Resource.String.informationAboutTakingPrompt);
            Thread.Sleep(3000);
            pointsToObtain = pointsToObtain / 2;
            informationAboutTakingPrompt = "";
        }
    }
}