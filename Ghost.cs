using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame_5___Making_a_Class
{
    public class Ghost
    {
        private List<Texture2D> _textures;
        private Vector2 _speed;
        private Rectangle _location;
        private int _textureIndex; //Control what texture is drawn to the screen
        private SpriteEffects _direction;
        private float _animationSpeed;
        private float _seconds;

        public Ghost(List<Texture2D> textures, Rectangle location)
        {
            _location = location;
            _textures = textures;
            _speed = Vector2.Zero;
            _textureIndex = 0;
            _direction = SpriteEffects.None;
            _animationSpeed = 0.2f;
            _seconds = 0f;

        }

        public Rectangle Rectangle
        {
            get { return _location; }
        }

        public void Update(GameTime gameTime, MouseState mouseState)
        {
            _speed = Vector2.Zero;

            if (mouseState.X < _location.X)
            {
                _direction = SpriteEffects.FlipHorizontally;
                _speed.X = -1;
            }
            else if (mouseState.X > _location.X)
            {
                _speed.X = 1;
            }
            if (mouseState.Y < _location.Y)
            {
                _speed.Y = -1;
            }
            else if (mouseState.Y > _location.Y)
            {
                _direction = SpriteEffects.None;
                _speed.Y = 1;
            }

            if (mouseState.LeftButton == ButtonState.Released)
            {
                _speed = Vector2.Zero;
                _textureIndex= 0;
                _seconds = 0f;
            }
            else if (_speed != Vector2.Zero)
            {
                //Animation
                _seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (_seconds > _animationSpeed)
                {
                    _seconds = 0;
                    _textureIndex++;
                    if (_textureIndex >= _textures.Count)
                    {
                        _textureIndex = 1;
                    }
                }

               
            }
            _location.Offset(_speed);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_textures[_textureIndex], _location, null,Color.White, 0f, Vector2.Zero, _direction, 1);
        }


    }

}
