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
            tekstury.Add("q", game.Content.Load<Texture2D>("letters/q"));
            tekstury.Add("w", game.Content.Load<Texture2D>("letters/w"));
            tekstury.Add("e", game.Content.Load<Texture2D>("letters/e"));
            tekstury.Add("r", game.Content.Load<Texture2D>("letters/r"));
            tekstury.Add("t", game.Content.Load<Texture2D>("letters/t"));
            tekstury.Add("y", game.Content.Load<Texture2D>("letters/y"));
            tekstury.Add("u", game.Content.Load<Texture2D>("letters/u"));
            tekstury.Add("i", game.Content.Load<Texture2D>("letters/i"));
            tekstury.Add("o", game.Content.Load<Texture2D>("letters/o"));
            tekstury.Add("p", game.Content.Load<Texture2D>("letters/p"));

            tekstury.Add("a", game.Content.Load<Texture2D>("letters/a"));
            tekstury.Add("s", game.Content.Load<Texture2D>("letters/s"));
            tekstury.Add("d", game.Content.Load<Texture2D>("letters/d"));
            tekstury.Add("f", game.Content.Load<Texture2D>("letters/f"));
            tekstury.Add("g", game.Content.Load<Texture2D>("letters/g"));
            tekstury.Add("h", game.Content.Load<Texture2D>("letters/h"));
            tekstury.Add("j", game.Content.Load<Texture2D>("letters/j"));
            tekstury.Add("k", game.Content.Load<Texture2D>("letters/k"));
            tekstury.Add("l", game.Content.Load<Texture2D>("letters/l"));

            tekstury.Add("z", game.Content.Load<Texture2D>("letters/z"));
            tekstury.Add("x", game.Content.Load<Texture2D>("letters/x"));
            tekstury.Add("c", game.Content.Load<Texture2D>("letters/c"));
            tekstury.Add("v", game.Content.Load<Texture2D>("letters/v"));
            tekstury.Add("b", game.Content.Load<Texture2D>("letters/b"));
            tekstury.Add("n", game.Content.Load<Texture2D>("letters/n"));
            tekstury.Add("m", game.Content.Load<Texture2D>("letters/m"));
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
            spriteBatch.Draw(tekstury["q"], rectangles["q"], colors["q"]);
            spriteBatch.Draw(tekstury["w"], rectangles["w"], colors["w"]);
            spriteBatch.Draw(tekstury["e"], rectangles["e"], colors["e"]);
            spriteBatch.Draw(tekstury["r"], rectangles["r"], colors["r"]);
            spriteBatch.Draw(tekstury["t"], rectangles["t"], colors["t"]);
            spriteBatch.Draw(tekstury["y"], rectangles["y"], colors["y"]);
            spriteBatch.Draw(tekstury["u"], rectangles["u"], colors["u"]);
            spriteBatch.Draw(tekstury["i"], rectangles["i"], colors["i"]);
            spriteBatch.Draw(tekstury["o"], rectangles["o"], colors["o"]);
            spriteBatch.Draw(tekstury["p"], rectangles["p"], colors["p"]);
            //drugi rząd
            spriteBatch.Draw(tekstury["a"], rectangles["a"], colors["a"]);
            spriteBatch.Draw(tekstury["s"], rectangles["s"], colors["s"]);
            spriteBatch.Draw(tekstury["d"], rectangles["d"], colors["d"]);
            spriteBatch.Draw(tekstury["f"], rectangles["f"], colors["f"]);
            spriteBatch.Draw(tekstury["g"], rectangles["g"], colors["g"]);
            spriteBatch.Draw(tekstury["h"], rectangles["h"], colors["h"]);
            spriteBatch.Draw(tekstury["j"], rectangles["j"], colors["j"]);
            spriteBatch.Draw(tekstury["k"], rectangles["k"], colors["k"]);
            spriteBatch.Draw(tekstury["l"], rectangles["l"], colors["l"]);
            //trzeci rząd
            spriteBatch.Draw(tekstury["z"], rectangles["z"], colors["z"]);
            spriteBatch.Draw(tekstury["x"], rectangles["x"], colors["x"]);
            spriteBatch.Draw(tekstury["c"], rectangles["c"], colors["c"]);
            spriteBatch.Draw(tekstury["v"], rectangles["v"], colors["v"]);
            spriteBatch.Draw(tekstury["b"], rectangles["b"], colors["b"]);
            spriteBatch.Draw(tekstury["n"], rectangles["n"], colors["n"]);
            spriteBatch.Draw(tekstury["m"], rectangles["m"], colors["m"]);
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