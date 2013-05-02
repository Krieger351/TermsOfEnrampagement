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
    class Player
    {
        public Direction state = Direction.North;

        public PlayerIndex index;
        public Color color = Color.Black;
        public Rectangle boundingBox;
        public Dictionary<Direction,Texture2D> texture;
        public int speed = 3;
        public int cooldown = 0;
        public int health = 50;
        public int maxHealth = 50;

        public Item item = null;

        public Dictionary<PlayerIndex, Player> players;
        public Player target;

        public BulletManager bulletManager;

        public Player(PlayerIndex i, ContentManager Content, BulletManager bm, Dictionary<PlayerIndex, Player> p, Vector2 position)
        {

            index = i;
            string ps = "";
            if (index == PlayerIndex.One)
            {
                ps = "Player1";
            } 
            if (index == PlayerIndex.Two)
            {
                ps = "Player2";
            }
            if (index == PlayerIndex.Three)
            {
                ps = "Player3";
            }
            if (index == PlayerIndex.Four)
            {
                ps = "Player4";
            }
            texture = new Dictionary<Direction, Texture2D>();
            texture.Add(Direction.North, Content.Load<Texture2D>(ps + "/North"));
            texture.Add(Direction.South, Content.Load<Texture2D>(ps + "/South"));
            texture.Add(Direction.East, Content.Load<Texture2D>(ps + "/East"));
            texture.Add(Direction.West, Content.Load<Texture2D>(ps + "/West"));

            target = this;

            players = p;

            bulletManager = bm;

            if (index == PlayerIndex.One)
                color = Color.Green;
            if (index == PlayerIndex.Two)
                color = Color.Red;
            if (index == PlayerIndex.Three)
                color = Color.Yellow;
            if (index == PlayerIndex.Four)
                color = Color.Blue;
            boundingBox = new Rectangle(50, 50, 20,30);
        }
        public void update(WallManager wm)
        {
            if (this.health <= 0)
            {
                if (item != null)
                {
                    item.boundingBox.X = this.boundingBox.X;
                    item.boundingBox.Y = this.boundingBox.Y;
                    item.pickedUp = false;
                }
                players.Remove(this.index);
            }

            if (boundingBox.X < 100 && item != null)
                Game1.state = GameState.Win;

            #region movement
            float x = GamePad.GetState(index).ThumbSticks.Left.X;
            float y = GamePad.GetState(index).ThumbSticks.Left.Y;
            int curSpeed = speed;
            if (x == 0 && y == 0)
                curSpeed = 0;
            double angle = Math.Atan2(y,x);   
            boundingBox.Y -= (int)(curSpeed * Math.Sin(angle));
            foreach (Wall current in wm.walls)
            {
                if (boundingBox.Intersects(current.boundingBox))
                {
                    if (y > 0)
                        boundingBox.Y = current.boundingBox.Y + current.boundingBox.Height + 1;
                    else
                        boundingBox.Y = current.boundingBox.Y - boundingBox.Height - 1;
                }
            }
            boundingBox.X += (int)(Math.Cos(angle) * curSpeed);
            foreach (Wall current in wm.walls)
            {
                if (boundingBox.Intersects(current.boundingBox))
                {
                    if (x < 0)
                        boundingBox.X = current.boundingBox.X + current.boundingBox.Width + 1;
                    else
                        boundingBox.X = current.boundingBox.X - boundingBox.Width - 1;
                }
            }
            foreach (CameralWall current in wm.camWalls)
            {
                if (boundingBox.Intersects(current.boundingBox))
                {
                    if (x < 0)
                        boundingBox.X = current.boundingBox.X + current.boundingBox.Width + 1;
                    else
                        boundingBox.X = current.boundingBox.X - boundingBox.Width - 1;
                }
            }
            #endregion
            #region Attacks
            float lookX = GamePad.GetState(index).ThumbSticks.Right.X;
            float lookY = GamePad.GetState(index).ThumbSticks.Right.Y;
            if (Math.Abs(lookX) > Math.Abs(lookY))
            {
                if (lookX > 0)
                    this.state = Direction.East;
                if (lookX < 0)
                    this.state = Direction.West;
            }
            else
            {
                if (lookY > 0)
                    this.state = Direction.North;
                if (lookY < 0)
                    this.state = Direction.South;
            }
            cooldown--;

            if (GamePad.GetState(index).IsButtonDown(Buttons.RightTrigger) && cooldown <= 0)
            {
                cooldown = 20;
                bulletManager.shootTarget(target, this.state, new Vector2(boundingBox.Center.X, boundingBox.Center.Y));
            }

            #region Target
            if (GamePad.GetState(index).IsButtonDown(Buttons.A))
            {
                if(players.ContainsKey(PlayerIndex.One))
                    target = players[PlayerIndex.One];
            }
            if (GamePad.GetState(index).IsButtonDown(Buttons.B) && players.ContainsKey(PlayerIndex.Two))
            {
                if (players.ContainsKey(PlayerIndex.Two))
                    target = players[PlayerIndex.Two];
            }
            if (GamePad.GetState(index).IsButtonDown(Buttons.Y) && players.ContainsKey(PlayerIndex.Three))
            {
                if (players.ContainsKey(PlayerIndex.Three))
                    target = players[PlayerIndex.Three];
            }
            if (GamePad.GetState(index).IsButtonDown(Buttons.X) && players.ContainsKey(PlayerIndex.Four))
            {
                if (players.ContainsKey(PlayerIndex.Four))
                    target = players[PlayerIndex.Four];
            }
            if (!players.ContainsValue(target))
                target = this;
            #endregion

            #endregion
        }
        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture[this.state] , boundingBox, Color.White);
        }
    }
}
