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
using Microsoft.Xna.Framework.Input.Touch;

namespace Wisielec
{
    public static class TouchManager
    {

        private static TouchCollection CurrentTouches = TouchPanel.GetState();
        private static TouchCollection PreviousTouches;

        public static void Update(GameTime gameTime)
        {
            PreviousTouches = CurrentTouches;
            CurrentTouches = TouchPanel.GetState();
        }

        public static List<TouchLocation> GetTouches()
        {
            List<TouchLocation> touches = new List<TouchLocation>();
            if (CurrentTouches.Count == 0)
            {
                return touches;
            }
            else
            {
                foreach(var touch in CurrentTouches)
                {
                    if (!PreviousTouches.Contains(touch))
                    {
                        touches.Add(touch);
                    }
                }
            }
            return touches;
        }
    }
}