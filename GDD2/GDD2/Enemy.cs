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
    class Enemy
    {
        public Texture2D[] texture;
        public Rectangle boundingBox;
        public Player target;
        public Dictionary<PlayerIndex, Player> players;
        public int speed = 2;
        public int health = 20;
        Random rand = new Random();
        Color color = Color.White;

        public Enemy(Texture2D[] td, Player ta, Vector2 position, Dictionary<PlayerIndex, Player> players)
        {
            this.players = players;
            texture = td;
            target = ta;
            boundingBox = new Rectangle((int)position.X, (int)position.Y, 30, 30);
        }

        public void update(List<Wall> walls, BulletManager bm)
        {
            if (!players.ContainsValue(target) && players.Count != 0)
            {
                List<Player> values = Enumerable.ToList(players.Values);
                target = values[rand.Next(players.Count)];
            }
            double angle = Math.Atan2(boundingBox.Center.X - target.boundingBox.Center.X, boundingBox.Center.Y - target.boundingBox.Center.Y);
            boundingBox.Y -= (int)(Math.Cos(angle) * speed); 
            
            if(this.boundingBox.Intersects(target.boundingBox))
            {
                boundingBox.X += 5*(int)(Math.Sin(angle) * speed);
                boundingBox.Y += 5*(int)(Math.Cos(angle) * speed);
                target.health--;
            }

            for (int i = 0; i < bm.targetBullets.Count; i++)
            {
                if (boundingBox.Intersects(bm.targetBullets[i].boundingBox))
                {
                    if (bm.targetBullets[i].direction == Direction.North)
                    {
                        boundingBox.Y -= 5;
                        target = bm.targetBullets[i].target;
                    }
                    if (bm.targetBullets[i].direction == Direction.South)
                    {
                        boundingBox.Y += 5; 
                        target = bm.targetBullets[i].target;
                    }
                    if (bm.targetBullets[i].direction == Direction.East)
                    {
                        boundingBox.X += 5;
                        target = bm.targetBullets[i].target;
                    }
                    if (bm.targetBullets[i].direction == Direction.West)
                    {
                        boundingBox.X -= 5;
                        target = bm.targetBullets[i].target;
                    }
                    bm.killTarget(bm.targetBullets[i]);
                    health -= 5;
                }
            }
            foreach (Wall current in walls)
            {
                if (boundingBox.Intersects(current.boundingBox))
                {
                    if ((int)(Math.Cos(angle) * speed) > 0)
                        boundingBox.Y = current.boundingBox.Y + current.boundingBox.Height + 1;
                    else
                        boundingBox.Y = current.boundingBox.Y - boundingBox.Height - 1;
                }
            }

            boundingBox.X -= (int)(Math.Sin(angle) * speed);
            foreach (Wall current in walls)
            {
                if (boundingBox.Intersects(current.boundingBox))
                {
                    if ((int)(Math.Sin(angle) * speed) > 0)
                        boundingBox.X = current.boundingBox.X + current.boundingBox.Width + 1;
                    else
                        boundingBox.X = current.boundingBox.X - boundingBox.Width - 1;
                }
            }

            if (target.index == PlayerIndex.One)
                color = Color.Green;
            if (target.index == PlayerIndex.Two)
                color = Color.Red;
            if (target.index == PlayerIndex.Three)
                color = Color.Yellow;
            if (target.index == PlayerIndex.Four)
                color = Color.Blue;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture[2], boundingBox, color);
        }
    }
}
