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

namespace Wisielec.HangmanLogic
{
    public class HangmanGame
    {
        private string wordToGuess = "";
        private string wordPattern = "";
        private int remainingLettersToGuess;

        public HangmanGame(string wordToGuess)
        {
            this.wordToGuess = wordToGuess;
            this.wordPattern = wordToGuess;
            remainingLettersToGuess = wordToGuess.Where(c => c != ' ').Where(v=>(int)v!=39).Count();
            //create wordPattern
            var wordPatternArray = wordPattern.ToArray<char>();
            for(int i = 0; i < wordPatternArray.Length; i++)
            {
                if (wordPatternArray[i] == ' ' || (int)wordPatternArray[i] == 39)
                    continue;
                wordPatternArray[i] = '?';
            }
            wordPattern = new string(wordPatternArray);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

        }
        public void Update(GameTime gameTime)
        {

        }

        public string GetWordPattern()
        {
            return wordPattern;
        }
    }
}