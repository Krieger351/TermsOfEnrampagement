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
    class BulletManager
    {
        public List<TargetBullet> targetBullets;
        public List<SwordBullet> swordBullets;

        Texture2D texture;

        public BulletManager(Texture2D tex)
        {
            texture = tex;
            targetBullets = new List<TargetBullet>();
            swordBullets = new List<SwordBullet>();
        }

        public void update()
        {
            foreach (SwordBullet current in swordBullets)
                current.update();
            for (int x = 0; x < targetBullets.Count; x++)
            {
                targetBullets[x].update();
                if (targetBullets[x].life < 0)
                {
                    killTarget(targetBullets[x]);
                    x--;
                }
            }
            for (int x = 0; x < swordBullets.Count; x++)
            {
                swordBullets[x].update();
                if (swordBullets[x].life < 0)
                {
                    killSword(swordBullets[x]);
                    x--;
                }
            }
        }
        public void draw(SpriteBatch spriteBatch)
        {
            foreach (SwordBullet current in swordBullets)
                current.draw(spriteBatch);
            foreach (TargetBullet current in targetBullets)
                current.draw(spriteBatch);
        }
        public void shootTarget(Player target, Direction direction, Vector2 position)
        {
            targetBullets.Add(new TargetBullet(target, texture, direction, position));
        }

        public void killTarget(TargetBullet t)
        {
            targetBullets.Remove(t);
        }
        public void shootSword(Direction direction, Vector2 position)
        {
            swordBullets.Add(new SwordBullet(direction, position));
        }

        public void killSword(SwordBullet t)
        {
            swordBullets.Remove(t);
        }
    }
}
