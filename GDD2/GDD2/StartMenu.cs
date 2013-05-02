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
    class StartMenu
    {
        public enum MenuState { ONE, TWO, THREE, FOUR }
        public Game1 game;
        public MenuState state = MenuState.ONE;
        public Texture2D title;
        Texture2D p1, p2, p3, p4;

        public StartMenu(Game1 game)
        {
            this.game = game;
            title = game.Content.Load<Texture2D>("Title");
            p1 = game.Content.Load<Texture2D>("Player1/South");
            p2 = game.Content.Load<Texture2D>("Player2/South");
            p3 = game.Content.Load<Texture2D>("Player3/South");
            p4 = game.Content.Load<Texture2D>("Player4/South");
        }
        public void update()
        {
            if (Game1.singlePress(Buttons.Start))
                Game1.state = GameState.Instructions;

            if (Game1.singlePress(Buttons.A))
            {
                if (state == MenuState.ONE)
                    game.startGame(1);
                if (state == MenuState.TWO)
                    game.startGame(2);
                if (state == MenuState.THREE)
                    game.startGame(3);
                if (state == MenuState.FOUR)
                    game.startGame(4);
                Game1.state = GameState.Game;
            }

            float lookX = GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X;
            float lookY = GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y;
            if (Math.Abs(lookX) > Math.Abs(lookY))
            {
                if (lookX > 0)
                {
                    if(state == MenuState.ONE && GamePad.GetState(PlayerIndex.Two).IsConnected)
                        state = MenuState.TWO;
                    if (state == MenuState.THREE && GamePad.GetState(PlayerIndex.Four).IsConnected)
                        state = MenuState.FOUR;
                }
                if (lookX < 0)
                {
                    if (state == MenuState.TWO)
                        state = MenuState.ONE;
                    if (state == MenuState.FOUR)
                        state = MenuState.THREE;
                }
            }
            else
            {
                if (lookY > 0)
                {
                    if (state == MenuState.FOUR)
                        state = MenuState.TWO;
                    if (state == MenuState.THREE)
                        state = MenuState.ONE;
                }
                if (lookY < 0)
                {
                    if (state == MenuState.TWO && GamePad.GetState(PlayerIndex.Four).IsConnected)
                        state = MenuState.FOUR;
                    if (state == MenuState.ONE && GamePad.GetState(PlayerIndex.Three).IsConnected)
                        state = MenuState.THREE;
                }
            }
        }
        public void draw(SpriteBatch spriteBatch, int width)
        {
            spriteBatch.Draw(title, Vector2.Zero, Color.White);

            Color color = Color.White;
            if (state == MenuState.ONE)
                color = Color.White;
            else
                color = Color.Gray;
            if (!GamePad.GetState(PlayerIndex.One).IsConnected)
                color = Color.Black;
            spriteBatch.Draw(Game1.blank, new Rectangle(width / 2 - 250, 310, 200, 70), color);
            spriteBatch.DrawString(Game1.font, "One", new Vector2(width / 2 - 250, 315), Color.Green);
            spriteBatch.Draw(p1, new Rectangle(width / 2 - 100, 315, p1.Width,p1.Height), Color.White);
            
            if (state == MenuState.TWO)
                color = Color.White;
            else
                color = Color.Gray;
            if (!GamePad.GetState(PlayerIndex.Two).IsConnected)
                color = Color.Black;
            spriteBatch.Draw(Game1.blank, new Rectangle(width / 2 + 50, 310, 200, 70), color);
            spriteBatch.DrawString(Game1.font, "Two", new Vector2(width / 2 + 50, 315), Color.Red);
            spriteBatch.Draw(p2, new Rectangle(width / 2 + 200, 315, p2.Width, p2.Height), Color.White);

            if (state == MenuState.THREE)
                color = Color.White;
            else
                color = Color.Gray;
            if (!GamePad.GetState(PlayerIndex.Three).IsConnected)
                color = Color.Black;
            spriteBatch.Draw(Game1.blank, new Rectangle(width / 2 - 250, 400, 200, 70), color);
            spriteBatch.DrawString(Game1.font, "Three", new Vector2(width / 2 - 250, 405), Color.Yellow);
            spriteBatch.Draw(p3, new Rectangle(width / 2 - 100, 405, p3.Width, p3.Height), Color.White);

            if (state == MenuState.FOUR)
                color = Color.White;
            else
                color = Color.Gray;
            if (!GamePad.GetState(PlayerIndex.Four).IsConnected)
                color = Color.Black;
            spriteBatch.Draw(Game1.blank, new Rectangle(width / 2 + 50, 400, 200, 70), color);
            spriteBatch.DrawString(Game1.font, "Four", new Vector2(width / 2 + 50, 405), Color.Blue);
            spriteBatch.Draw(p4, new Rectangle(width / 2 + 200, 405, p4.Width, p4.Height), Color.White);
        }

    }
}
