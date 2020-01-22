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
        private char[] wordPattern;
        private int remainingLettersToGuess;
        private int lifes = 9;

        public HangmanGame(string wordToGuess)
        {
            this.wordToGuess = wordToGuess;
            this.wordPattern = wordToGuess.ToArray<char>();
            remainingLettersToGuess = wordToGuess.Where(c => c != ' ').Where(v=>(int)v!=39).Where(z=>z!='-').Count();
            //create wordPattern
            for(int i = 0; i < wordPattern.Length; i++)
            {
                if (wordPattern[i] == ' ' || (int)wordPattern[i] == 39 || wordPattern[i] == '-')
                    continue;
                wordPattern[i] = '?';
            }
        }

        public bool CheckLetterInWord(char letter)
        {
            int letterToGuessBeforeCheck = this.remainingLettersToGuess;
            for(int i=0;i<wordToGuess.Length;i++)
            {
                if (wordToGuess[i] == letter)
                {
                    remainingLettersToGuess--;
                    wordPattern[i] = letter;
                }
            }

            if (letterToGuessBeforeCheck == remainingLettersToGuess)
            {
                //jeśli danej litery nie było w słowie-> zabieramy jedno życie
                lifes--;
                return false;
            }
            return true;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

        }
        public void Update(GameTime gameTime)
        {

        }

        #region
        public string GetWordPattern()
        {
            return new string(wordPattern);
        }
        public int GetLifes()
        {
            return lifes;
        }
        public int GetRemainingLettersToGuess()
        {
            return remainingLettersToGuess;
        }
        #endregion
    }
}