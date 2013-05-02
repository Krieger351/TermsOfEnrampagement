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
    class Item
    {
        public Rectangle boundingBox;
        public PlayerManager pm;
        Texture2D texture;
        public bool pickedUp = false;


        public Item(Vector2 pos, PlayerManager pm, Texture2D tex)
        {
            boundingBox = new Rectangle((int)pos.X, (int)pos.Y, 50, 50);
            this.pm = pm;
            texture = tex;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            if (!pickedUp)
                spriteBatch.Draw(texture, boundingBox, Color.White);
        }

        public void update()
        {
            foreach (Player cur in pm.players.Values)
            {
                if (cur.boundingBox.Intersects(boundingBox))
                {
                    cur.item = this;
                    pickedUp = true;
                }
            }
        }
    }
}
