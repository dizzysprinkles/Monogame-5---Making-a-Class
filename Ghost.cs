﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Monogame_5___Making_a_Class
{
    public class Ghost
    {
        private List<Texture2D> _textures;
        private Random _generator;
        private Vector2 _speed;
        private Rectangle _location;
        private int _textureIndex; //Control what texture is drawn to the screen
        private SpriteEffects _direction;
        private float _animationSpeed;
        private float _seconds;
        private float _opacity;

        public Ghost(List<Texture2D> textures, Rectangle location)
        {
            _location = location;
            _textures = textures;
            _speed = Vector2.Zero;
            _textureIndex = 0;
            _direction = SpriteEffects.None;
            _animationSpeed = 0.2f;
            _seconds = 0f;
            _generator = new Random();
            _opacity = 1f;
            

        }

        public Rectangle Bounds
        {
            get { return _location; }
            set { _location = value; }
        }

        public void Update(GameTime gameTime, MouseState mouseState, List<Ghost> ghosts)
        {
            foreach (Ghost ghost in ghosts)
            {
                if (_location.Intersects(ghost.Bounds) && _location != ghost.Bounds)
                    Respawn();
            }
            
            _speed = Vector2.Zero;

            if (mouseState.X < _location.X)
            {
                _direction = SpriteEffects.FlipHorizontally;
                _speed.X = -1;
            }
            else if (mouseState.X > _location.X)
            {
                _direction = SpriteEffects.None;
                _speed.X = 1;
            }
            if (mouseState.Y < _location.Y)
            {
                _speed.Y = -1;
            }
            else if (mouseState.Y > _location.Y)
            {
                _speed.Y = 1;
            }

            if (mouseState.LeftButton == ButtonState.Released)
            {
                _speed = Vector2.Zero;
                _opacity = 0.3f;
                _textureIndex= 0;
                _seconds = 0f;
            }
            else if (_speed != Vector2.Zero)
            {
                _opacity = 1f;
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
        

        // Collision methods that return if the collision is true or not.
        public bool Contains(Point player) //Contains mouse
        {
            return _location.Contains(player);
        }
        //public bool Intersects(Rectangle player) //intersects a rectangle
        //{
        //    return _location.Intersects(player);
        //}

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_textures[_textureIndex], _location, null,Color.White * _opacity, 0f, Vector2.Zero, _direction, 1);
        }

        public void Respawn()
        {
            _location = new Rectangle(_generator.Next(0, 700), _generator.Next(0, 450), _location.Width, _location.Height);
        }


    }

}
