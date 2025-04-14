using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;

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
        MouseState mouseState;
        KeyboardState keyboardState;
        Random generator;
        Screen screen;
        Rectangle window;
        //Ghost ghost1;
        List<Ghost> ghosts;
        SpriteFont instructionFont;
        
        
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            screen = Screen.Title;
            ghosts = new List<Ghost>();
            window = new Rectangle(0,0,800,600);

            IsMouseVisible = false;

            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.PreferredBackBufferWidth = window.Width;

            generator = new Random();
            ghostTextures = new List<Texture2D>();

            base.Initialize();
            
            for (int i = 0; i < 20; i++)
            {
                ghosts.Add(new Ghost(ghostTextures, new Rectangle(generator.Next(0, 700), generator.Next(0, 500), 40, 40)));
            }

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
            marioTexture = Content.Load<Texture2D>("Images/mario");
            instructionFont = Content.Load<SpriteFont>("Fonts/InstructionFont");
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (screen == Screen.Title)
            {
                if (keyboardState.IsKeyDown(Keys.Enter))
                    screen = Screen.House;

            }
            else if (screen == Screen.House)
            {
                for (int i = 0; i < 20; i++)
                {
                    ghosts[i].Update(gameTime, mouseState, ghosts);
                    if (ghosts[i].Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed)
                       screen = Screen.End;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
           
            if (screen == Screen.Title)
            {
                _spriteBatch.Draw(titleBackground, window, Color.White);
                _spriteBatch.DrawString(instructionFont, "The game ends when you click on a ghost!", new Vector2(10, 200), Color.White);
            }
               
            else if (screen == Screen.House)
            {
                _spriteBatch.Draw(mainBackground, window, Color.White);
                for (int i = 0; i < 20; i++)
                {
                    ghosts[i].Draw(_spriteBatch);
                }
            }
            else
                _spriteBatch.Draw(endBackground, window, Color.White);

            _spriteBatch.Draw(marioTexture, new Rectangle(mouseState.Position.X, mouseState.Position.Y, 20, 20), Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
