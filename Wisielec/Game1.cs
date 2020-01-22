using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Wisielec.States;
using System.IO;
using System;
using Wisielec.Database;
using System.Collections.Generic;

namespace Wisielec
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        Texture2D background;
        IComponent currentState;
        Activity1 activity;
        SqliteDatabase database = new SqliteDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
            , "ranking.db3"));

        

        public Game1(Activity1 activity)
        {
            graphics = new GraphicsDeviceManager(this);
            this.activity = activity;
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = true;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            currentState = new MenuState(this);
            background = Content.Load<Texture2D>("background");

            // TODO: use this.Content to load your game content here

            textures.Add("1", Content.Load<Texture2D>("hangman/1"));
            textures.Add("2", Content.Load<Texture2D>("hangman/2"));
            textures.Add("3", Content.Load<Texture2D>("hangman/3"));
            textures.Add("4", Content.Load<Texture2D>("hangman/4"));
            textures.Add("5", Content.Load<Texture2D>("hangman/5"));
            textures.Add("6", Content.Load<Texture2D>("hangman/6"));
            textures.Add("7", Content.Load<Texture2D>("hangman/7"));
            textures.Add("8", Content.Load<Texture2D>("hangman/8"));
            textures.Add("9", Content.Load<Texture2D>("hangman/9"));
            textures.Add("10", Content.Load<Texture2D>("hangman/10"));

            textures.Add("OkTexture", Content.Load<Texture2D>("Ok"));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            // TODO: Add your update logic here
            currentState.Update(gameTime);
            TouchManager.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.BurlyWood);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, (int)GraphicsDevice.Viewport.Width, (int)GraphicsDevice.Viewport.Height), Color.White);
            currentState.Draw(spriteBatch, gameTime,textures);
            spriteBatch.End();

            base.Draw(gameTime);
        }
        //settery, gettery
        #region
        public Activity1 GetActivity()
        {
            return activity;
        } 

        public SqliteDatabase GetDatabase()
        {
            return database;
        }

        public void SetCurrentState(IComponent state)
        {
            this.currentState = state;
        }
        #endregion
    }
}
