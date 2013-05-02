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
    class Spawner
    {
        public Color color = Color.Black;
        public Rectangle boundingBox;
        public Texture2D texture;
        public EnemyManager enemyManager;
        public static Random rand = new Random();

        Camera cam;

        public Spawner(EnemyManager em, Vector2 position, Camera cam, Texture2D tex)
        {
            enemyManager = em;
            count = rand.Next(350)+50;
            this.cam = cam;
            texture = tex;
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width,texture.Height);
        }
        int count = 0;
        public void update()
        {
            count--;
            if (count <= 0 && boundingBox.Center.X > (int)cam.Pos.X - 400 && boundingBox.Center.X < (int)cam.Pos.X + 400)
            {
                enemyManager.addEnemy(new Vector2(boundingBox.X + boundingBox.Width/2 - 15, boundingBox.Y + boundingBox.Height/2 - 15));
                count = rand.Next(500);
            }
        }
        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, boundingBox, Color.White);
        }
    }
}
