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
    class PlayerManager
    {
        public Dictionary<PlayerIndex, Player> players;
        public Camera camera;

        public PlayerManager(ContentManager Content, BulletManager bm, Camera cam, int numPlayers)
        {
            players = new Dictionary<PlayerIndex, Player>();
            if (GamePad.GetState(PlayerIndex.One).IsConnected && numPlayers > 0)
            {
                players.Add(PlayerIndex.One, new Player(PlayerIndex.One, Content, bm, players, new Vector2(100, 100)));
            }
            if (GamePad.GetState(PlayerIndex.Two).IsConnected && numPlayers > 1)
            {
                players.Add(PlayerIndex.Two, new Player(PlayerIndex.Two, Content, bm, players, new Vector2(100, 200)));
            }
            if (GamePad.GetState(PlayerIndex.Three).IsConnected && numPlayers > 2)
            {
                players.Add(PlayerIndex.Three, new Player(PlayerIndex.Three, Content, bm, players, new Vector2(200, 100)));
            }
            if (GamePad.GetState(PlayerIndex.Four).IsConnected && numPlayers > 3)
            {
                players.Add(PlayerIndex.Four, new Player(PlayerIndex.Four, Content, bm, players, new Vector2(200, 200)));
            }
            camera = cam;
        }

        public void update(WallManager walls)
        {
            int newCamX = 0;
            List<Player> values = Enumerable.ToList(players.Values);
            foreach (Player current in values)
            {
                current.update(walls);
                newCamX += current.boundingBox.X;
            }
            if (players.Count != 0)
                newCamX /= players.Count;
            else
                Game1.state = GameState.GameOver;
            camera.Pos = new Vector2(newCamX, camera.Pos.Y);
        }

        public void draw(SpriteBatch spriteBatch)
        {
            foreach (KeyValuePair<PlayerIndex, Player> current in players)
            {
                current.Value.draw(spriteBatch);
            }
        }
    }
}
