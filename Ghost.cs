using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
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

        public Ghost(List<Texture2D> textures, Rectangle location)
        {
            _location = location;
            _textures = textures;
            _speed = Vector2.Zero;
            _textureIndex = 0;

        }

        public Rectangle Rectangle
        {
            get { return _location; }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_textures[0], _location, Color.White);
        }


    }

}
