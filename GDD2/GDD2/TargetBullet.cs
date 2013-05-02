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
    class TargetBullet
    {
        public Color color = Color.Black;
        public Rectangle boundingBox;
        public Texture2D texture;
        public int speed = 10;
        public int life = 100;

        public Direction direction;

        public Player target;

        public TargetBullet(Player t, Texture2D tex, Direction dir, Vector2 position)
        {
            target = t;
            texture = tex;
            direction = dir;
            boundingBox = new Rectangle((int)position.X, (int)position.Y, tex.Width*2, tex.Height*2);

            if (t.index == PlayerIndex.One)
                color = Color.Green;
            if (t.index == PlayerIndex.Two)
                color = Color.Red;
            if (t.index == PlayerIndex.Three)
                color = Color.Yellow;
            if (t.index == PlayerIndex.Four)
                color = Color.Blue;
            
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
            if (direction == Direction.North)
                spriteBatch.Draw(texture, boundingBox, null, color, -(float)Math.PI/2, Vector2.Zero, SpriteEffects.None, 0f);
            if (direction == Direction.South)
                spriteBatch.Draw(texture, boundingBox, null, color, (float)Math.PI / 2, Vector2.Zero, SpriteEffects.None, 0f);
            if (direction == Direction.East)
                spriteBatch.Draw(texture, boundingBox, color);
            if (direction == Direction.West)
                spriteBatch.Draw(texture, boundingBox, null, color, (float)Math.PI, Vector2.Zero, SpriteEffects.None, 0f);
        }
    }
}
