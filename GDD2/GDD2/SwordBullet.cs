using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GDD2
{
    class SwordBullet
    {
        public Color color = Color.Black;
        public Rectangle boundingBox;
        public Texture2D texture;
        public int speed = 15;
        public int life = 2;
        public Direction direction;

        public SwordBullet(Direction dir, Vector2 position)
        {
            direction = dir;

            boundingBox = new Rectangle((int)position.X-10, (int)position.Y-10, 30, 30);
        }

        public void update()
        {
            if (direction == Direction.North)
                boundingBox.Y -= speed;
            if (direction == Direction.South)
                boundingBox.Y += speed;
            if (direction == Direction.East)
                boundingBox.X += speed;
            if (direction == Direction.West)
                boundingBox.X -= speed;
            life--;  
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.blank, boundingBox, Color.Black);
        }
    }
}
