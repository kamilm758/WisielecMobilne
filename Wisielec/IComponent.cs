using System;
using System.Collections;
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

namespace Wisielec
{
    public interface IComponent
    {
        void Draw(SpriteBatch spriteBatch, GameTime gameTime, Dictionary<string,Texture2D> textures);
        void Update(GameTime gameTime);

    }
}