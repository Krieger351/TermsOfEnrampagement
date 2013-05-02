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
    class EnemyManager
    {
        public List<Enemy> enemys = new List<Enemy>();
        public Texture2D[] texture;
        public Dictionary<PlayerIndex, Player> players;

        public EnemyManager(ContentManager Content, Dictionary<PlayerIndex, Player> p)
        {
            texture = new Texture2D[4];
            texture[2] = Content.Load<Texture2D>("Enemy/South");
            players = p;
        }
        public void update(List<Wall> walls, BulletManager bm)
        {
            for (int i = 0; i < enemys.Count; i++ )
            {
                enemys[i].update(walls, bm);
                if (enemys[i].health <= 0)
                {
                    enemys.Remove(enemys[i]);
                    i--;
                }
            }
        }
        public void draw(SpriteBatch spriteBatch)
        {
            foreach (Enemy current in enemys)
            {
                current.draw(spriteBatch);
            }
        }
        Random rand = new Random();
        public void addEnemy(Vector2 position)
        {
            List<Player> values = Enumerable.ToList(players.Values);
            enemys.Add(new Enemy(texture, values[rand.Next(players.Count)], position, players));
        }
    }
}
