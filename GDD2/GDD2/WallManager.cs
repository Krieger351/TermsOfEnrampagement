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
    class WallManager
    {
        public List<Wall> walls = new List<Wall>();
        public List<CameralWall> camWalls = new List<CameralWall>();

        public WallManager(Camera cam, GraphicsDevice gd)
        {
            walls.Add(new Wall(0, -10, Game1.width, 15));
            walls.Add(new Wall(0, 475, Game1.width, 5));
            walls.Add(new Wall(0, 0, 5, 480));
            walls.Add(new Wall(Game1.width-5, 0, 5, 480));
            walls.Add(new Wall(440, 200, 80, 25));
            camWalls.Add(new CameralWall(cam, gd, CamSide.left));
            camWalls.Add(new CameralWall(cam, gd, CamSide.right));
        }
        public void update()
        {
            foreach (Wall current in walls)
                current.update();

            foreach (CameralWall current in camWalls)
                current.update();
        }
        public void draw(SpriteBatch spriteBatch)
        {
            foreach (Wall current in walls)
            {
                current.draw(spriteBatch);
            }
            foreach (CameralWall current in camWalls)
                current.draw(spriteBatch);
        }
    }
}
