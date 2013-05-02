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
    class Wall
    {
        public Rectangle boundingBox;

        public Wall(int x, int y, int w, int h)
        {
            boundingBox = new Rectangle(x, y, w, h);
        }
        public void update()
        {
        }
        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.blank, boundingBox, Color.Black);
        }
    }
}
