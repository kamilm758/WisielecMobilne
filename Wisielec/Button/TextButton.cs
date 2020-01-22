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

namespace Wisielec.Button
{
    public class TextButton
    {
        private string buttonLabel;
        private Rectangle hitbox;

        public TextButton(string buttonLabel,SpriteFont labelFont, int x, int y)
        {
            this.buttonLabel = buttonLabel;
            //od razu w konstruktorze ustalamy, że punktem odniesienia jest środek buttona, a nie początek
            hitbox = new Rectangle(x-(int)labelFont.MeasureString(buttonLabel).X/2, y - (int)labelFont.MeasureString(buttonLabel).Y / 2, (int) labelFont.MeasureString(buttonLabel).X,(int)labelFont.MeasureString(buttonLabel).Y);
        }
        //gettery
        #region
        public string GetButtonLabel()
        {
            return buttonLabel;
        }
        public Rectangle GetHitbox()
        {
            return hitbox;
        }
        public Vector2 GetVectorPosition()
        {
            return new Vector2(this.hitbox.X, this.hitbox.Y);
        }
        #endregion
    }
}