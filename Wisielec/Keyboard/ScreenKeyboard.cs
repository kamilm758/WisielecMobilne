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
using Microsoft.Xna.Framework.Input.Touch;

namespace Wisielec.Keyboard
{
    class ScreenKeyboard
    {
        private Game1 game;
        private Dictionary<string, Texture2D> tekstury = new Dictionary<string, Texture2D>();
        private Dictionary<string, Rectangle> rectangles = new Dictionary<string, Rectangle>();
        private Dictionary<string, Color> colors = new Dictionary<string, Color>();
        private Vector2 windowSize;
        private List<string> lockedKeys = new List<string>();
        private string buffer = "";
        private KeyboardOperatingMode operatingMode;

        public ScreenKeyboard(Game1 game, KeyboardOperatingMode operatingMode)
        {
            this.game = game;
            this.operatingMode = operatingMode;
            windowSize = new Vector2(game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);
            LoadContent();
        }

        private void LoadContent()
        {
            //tekstury liter
            tekstury.Add("a", game.Content.Load<Texture2D>("letters/a"));
            //kwadraty, pozycje i wielkości
            int letterWidth = (int)windowSize.X / 20;
            int letterHeight = (int)windowSize.Y / 10;
            //pierwszy rząd od góry
            rectangles.Add("q", new Rectangle((int)(5 * windowSize.X / 16), (int)(4*windowSize.Y/8),letterWidth ,letterHeight));
            rectangles.Add("w", new Rectangle((int)(6 * windowSize.X / 16), (int)(4*windowSize.Y/8), letterWidth, letterHeight));
            rectangles.Add("e", new Rectangle((int)(7 * windowSize.X / 16), (int)(4*windowSize.Y/8), letterWidth, letterHeight));
            rectangles.Add("r", new Rectangle((int)(8 * windowSize.X / 16), (int)(4*windowSize.Y/8), letterWidth, letterHeight));
            rectangles.Add("t", new Rectangle((int)(9 * windowSize.X / 16), (int)(4*windowSize.Y/8), letterWidth, letterHeight));
            rectangles.Add("y", new Rectangle((int)(10 * windowSize.X / 16), (int)(4*windowSize.Y/8), letterWidth, letterHeight));
            rectangles.Add("u", new Rectangle((int)(11 * windowSize.X / 16), (int)(4*windowSize.Y/8), letterWidth, letterHeight));
            rectangles.Add("i", new Rectangle((int)(12 * windowSize.X / 16), (int)(4*windowSize.Y/8), letterWidth, letterHeight));
            rectangles.Add("o", new Rectangle((int)(13 * windowSize.X / 16), (int)(4*windowSize.Y/8), letterWidth, letterHeight));
            rectangles.Add("p", new Rectangle((int)(14 * windowSize.X / 16), (int)(4*windowSize.Y/8), letterWidth, letterHeight));
            //drugi rząd
            rectangles.Add("a", new Rectangle((int)(5 * windowSize.X / 16) + letterWidth / 2, (int)(5*windowSize.Y/8), letterWidth, letterHeight));
            rectangles.Add("s", new Rectangle((int)(6 * windowSize.X / 16) + letterWidth / 2, (int)(5*windowSize.Y/8), letterWidth, letterHeight));
            rectangles.Add("d", new Rectangle((int)(7 * windowSize.X / 16) + letterWidth / 2, (int)(5*windowSize.Y/8), letterWidth, letterHeight));
            rectangles.Add("f", new Rectangle((int)(8 * windowSize.X / 16) + letterWidth / 2, (int)(5*windowSize.Y/8), letterWidth, letterHeight));
            rectangles.Add("g", new Rectangle((int)(9 * windowSize.X / 16) + letterWidth / 2, (int)(5*windowSize.Y/8), letterWidth, letterHeight));
            rectangles.Add("h", new Rectangle((int)(10 * windowSize.X / 16) + letterWidth / 2, (int)(5*windowSize.Y/8), letterWidth, letterHeight));
            rectangles.Add("j", new Rectangle((int)(11 * windowSize.X / 16) + letterWidth / 2, (int)(5*windowSize.Y/8), letterWidth, letterHeight));
            rectangles.Add("k", new Rectangle((int)(12 * windowSize.X / 16) + letterWidth / 2, (int)(5*windowSize.Y/8), letterWidth, letterHeight));
            rectangles.Add("l", new Rectangle((int)(13 * windowSize.X / 16) + letterWidth / 2, (int)(5*windowSize.Y/8), letterWidth, letterHeight));
            //trzeci rząd
            rectangles.Add("z", new Rectangle((int)(6 * windowSize.X / 16) + letterWidth, (int)(6*windowSize.Y/8), letterWidth, letterHeight));
            rectangles.Add("x", new Rectangle((int)(7 * windowSize.X / 16) + letterWidth, (int)(6*windowSize.Y/8), letterWidth, letterHeight));
            rectangles.Add("c", new Rectangle((int)(8 * windowSize.X / 16) + letterWidth, (int)(6*windowSize.Y/8), letterWidth, letterHeight));
            rectangles.Add("v", new Rectangle((int)(9 * windowSize.X / 16) + letterWidth, (int)(6*windowSize.Y/8), letterWidth, letterHeight));
            rectangles.Add("b", new Rectangle((int)(10 * windowSize.X / 16) + letterWidth, (int)(6*windowSize.Y/8), letterWidth, letterHeight));
            rectangles.Add("n", new Rectangle((int)(11 * windowSize.X / 16) + letterWidth, (int)(6*windowSize.Y/8), letterWidth, letterHeight));
            rectangles.Add("m", new Rectangle((int)(12 * windowSize.X / 16) + letterWidth, (int)(6*windowSize.Y/8), letterWidth, letterHeight));

            //kolory
            colors.Add("q", Color.White);
            colors.Add("w", Color.White);
            colors.Add("e", Color.White);
            colors.Add("r", Color.White);
            colors.Add("t", Color.White);
            colors.Add("y", Color.White);
            colors.Add("u", Color.White);
            colors.Add("i", Color.White);
            colors.Add("o", Color.White);
            colors.Add("p", Color.White);
            //drugi rząd
            colors.Add("a", Color.White);
            colors.Add("s", Color.White);
            colors.Add("d", Color.White);
            colors.Add("f", Color.White);
            colors.Add("g", Color.White);
            colors.Add("h", Color.White);
            colors.Add("j", Color.White);
            colors.Add("k", Color.White);
            colors.Add("l", Color.White);
            //trzeci rząd
            colors.Add("z", Color.White);
            colors.Add("x", Color.White);
            colors.Add("c", Color.White);
            colors.Add("v", Color.White);
            colors.Add("b", Color.White);
            colors.Add("n", Color.White);
            colors.Add("m", Color.White);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //pierwszy rząd z góry
            spriteBatch.Draw(tekstury["a"], rectangles["q"], colors["q"]);
            spriteBatch.Draw(tekstury["a"], rectangles["w"], colors["w"]);
            spriteBatch.Draw(tekstury["a"], rectangles["e"], colors["e"]);
            spriteBatch.Draw(tekstury["a"], rectangles["r"], colors["r"]);
            spriteBatch.Draw(tekstury["a"], rectangles["t"], colors["t"]);
            spriteBatch.Draw(tekstury["a"], rectangles["y"], colors["y"]);
            spriteBatch.Draw(tekstury["a"], rectangles["u"], colors["u"]);
            spriteBatch.Draw(tekstury["a"], rectangles["i"], colors["i"]);
            spriteBatch.Draw(tekstury["a"], rectangles["o"], colors["o"]);
            spriteBatch.Draw(tekstury["a"], rectangles["p"], colors["p"]);
            //drugi rząd
            spriteBatch.Draw(tekstury["a"], rectangles["a"], colors["a"]);
            spriteBatch.Draw(tekstury["a"], rectangles["s"], colors["s"]);
            spriteBatch.Draw(tekstury["a"], rectangles["d"], colors["d"]);
            spriteBatch.Draw(tekstury["a"], rectangles["f"], colors["f"]);
            spriteBatch.Draw(tekstury["a"], rectangles["g"], colors["g"]);
            spriteBatch.Draw(tekstury["a"], rectangles["h"], colors["h"]);
            spriteBatch.Draw(tekstury["a"], rectangles["j"], colors["j"]);
            spriteBatch.Draw(tekstury["a"], rectangles["k"], colors["k"]);
            spriteBatch.Draw(tekstury["a"], rectangles["l"], colors["l"]);
            //trzeci rząd
            spriteBatch.Draw(tekstury["a"], rectangles["z"], colors["z"]);
            spriteBatch.Draw(tekstury["a"], rectangles["x"], colors["x"]);
            spriteBatch.Draw(tekstury["a"], rectangles["c"], colors["c"]);
            spriteBatch.Draw(tekstury["a"], rectangles["v"], colors["v"]);
            spriteBatch.Draw(tekstury["a"], rectangles["b"], colors["b"]);
            spriteBatch.Draw(tekstury["a"], rectangles["n"], colors["n"]);
            spriteBatch.Draw(tekstury["a"], rectangles["m"], colors["m"]);
        }

        public void Update(GameTime gameTime, List<TouchLocation> touches)
        {
            foreach(var touch in touches)
            {
                foreach(var key in rectangles)
                {
                    if(key.Value.Intersects(new Rectangle((int)touch.Position.X, (int)touch.Position.Y, 1, 1)))
                    {
                        //jeśli klikniemy na jakąś literę
                        if(!lockedKeys.Contains(key.Key))
                        buffer = buffer + key.Key;
                        if (operatingMode == KeyboardOperatingMode.PlayerName)
                        {
                            Thread t = new Thread(new ParameterizedThreadStart(ColorAfterClick));
                            t.Start(key.Key);
                        }
                    }
                }
            }
        }

        public void ColorAfterClick(Object key)
        {

            colors[(string)key] = Color.Red;
            Thread.Sleep(500);
            colors[(string)key] = Color.White;
        }

        public string GetPressedKeys()
        {
            string pom = buffer;
            buffer = "";
            return pom;
        }

        public void LockKey(string keyLabel)
        {
            colors[keyLabel] = Color.Gray;
            lockedKeys.Add(keyLabel);
        }
    }
}