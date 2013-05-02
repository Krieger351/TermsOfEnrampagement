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
    
    enum CamSide { left, right }
    class CameralWall
    {
        public Rectangle boundingBox;
        public CamSide side;
        public Camera cam;
        public int rightOffset;

        public CameralWall(Camera cam, GraphicsDevice gd, CamSide side)
        {
            this.cam = cam;
            this.side = side;
            rightOffset = gd.Viewport.Width;
            boundingBox = new Rectangle(0, 0, 0, gd.Viewport.Height);
        }
        public void update()
        {
            if (side == CamSide.left)
                boundingBox.X = (int)cam.Pos.X - (int)(.5 * rightOffset);
            else
                boundingBox.X = (int)cam.Pos.X + (int)(.5 * rightOffset); ;
        }
        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.blank, boundingBox, Color.Black);
        }
    }
}
