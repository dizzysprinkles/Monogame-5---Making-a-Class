using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Monogame_5___Making_a_Class
{
    enum Screen
    {
        Title,
        House,
        End
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        List<Texture2D> ghostTextures;
        Texture2D titleBackground, mainBackground, endBackground, marioTexture;
        MouseState currentMouseState, prevMouseState;
        Random generator;
        Screen screen;
        Rectangle window;
        
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            window = new Rectangle(0,0,800,600);

            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.PreferredBackBufferWidth = window.Width;

            generator = new Random();
            ghostTextures = new List<Texture2D>();
            //screen = Screen.Title;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            ghostTextures.Add(Content.Load<Texture2D>("Images/boo-stopped"));

            for (int i = 1; i < 8; i++)
            {
                ghostTextures.Add(Content.Load<Texture2D>("Images/boo-move-" +i));
            }

            titleBackground = Content.Load<Texture2D>("Images/haunted-title");
            endBackground = Content.Load<Texture2D>("Images/haunted-end-screen");
            mainBackground = Content.Load<Texture2D>("Images/haunted-background");
        }

        protected override void Update(GameTime gameTime)
        {
            currentMouseState = Mouse.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            prevMouseState = currentMouseState;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(mainBackground, window, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
