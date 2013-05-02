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
    class UI
    {
        public Dictionary<PlayerIndex, Player> players;
        public Camera camera;

        public UI (Dictionary<PlayerIndex, Player> p, Camera cam)
        {
            players = p;
            camera = cam;
        }

        public void update()
        { }

        public void draw(SpriteBatch spriteBatch)
        {
            if (players.ContainsKey(PlayerIndex.One))
            {
                spriteBatch.Draw(Game1.blank, new Rectangle(0, 0, (int)(400 * ((double)players[PlayerIndex.One].health / (double)players[PlayerIndex.One].maxHealth)), 10), Color.Green);
                spriteBatch.Draw(Game1.blank, new Rectangle(0, 0, 10, (int)(240 * ((double)players[PlayerIndex.One].health / (double)players[PlayerIndex.One].maxHealth))), Color.Green);
            }
            if (players.ContainsKey(PlayerIndex.Two))
            {
                spriteBatch.Draw(Game1.blank, new Rectangle(800 - (int)(400 * ((double)players[PlayerIndex.Two].health / (double)players[PlayerIndex.Two].maxHealth)), 0, (int)(400 * ((double)players[PlayerIndex.Two].health / (double)players[PlayerIndex.Two].maxHealth)), 10), Color.Red);
                spriteBatch.Draw(Game1.blank, new Rectangle(790, 0, 10, (int)(240 * ((double)players[PlayerIndex.Two].health / (double)players[PlayerIndex.Two].maxHealth))), Color.Red);
            }
            if (players.ContainsKey(PlayerIndex.Three))
            {
                spriteBatch.Draw(Game1.blank, new Rectangle(0, 470, (int)(400 * ((double)players[PlayerIndex.Three].health / (double)players[PlayerIndex.Three].maxHealth)), 10), Color.Yellow);
                spriteBatch.Draw(Game1.blank, new Rectangle(0, 470 - (int)(240 * ((double)players[PlayerIndex.Three].health / (double)players[PlayerIndex.Three].maxHealth)), 10, (int)(240 * ((double)players[PlayerIndex.Three].health / (double)players[PlayerIndex.Three].maxHealth))), Color.Yellow);
            }

            if (players.ContainsKey(PlayerIndex.Four))
            {
                spriteBatch.Draw(Game1.blank, new Rectangle(800 - (int)(400 * ((double)players[PlayerIndex.Four].health / (double)players[PlayerIndex.Four].maxHealth)), 470, (int)(400 * ((double)players[PlayerIndex.Four].health / (double)players[PlayerIndex.Four].maxHealth)), 10), Color.Blue);
                spriteBatch.Draw(Game1.blank, new Rectangle(790, 470 - (int)(240 * ((double)players[PlayerIndex.Four].health / (double)players[PlayerIndex.Four].maxHealth)), 10, (int)(240 * ((double)players[PlayerIndex.Four].health / (double)players[PlayerIndex.Four].maxHealth))), Color.Blue);
            }
            spriteBatch.Draw(Game1.blank, new Rectangle(0, 239, 15, 3), Color.Black);
            spriteBatch.Draw(Game1.blank, new Rectangle(785, 239, 15, 3), Color.Black);
            spriteBatch.Draw(Game1.blank, new Rectangle(399, 0, 3, 15), Color.Black);
            spriteBatch.Draw(Game1.blank, new Rectangle(399, 465, 3, 15), Color.Black);
        }
    }
}
