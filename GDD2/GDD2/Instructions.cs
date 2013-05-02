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
    class Instructions
    {
        List<Texture2D> screens;
        int curScreen = 0;

        public Instructions(ContentManager Content)
        {
            screens = new List<Texture2D>();
            screens.Add(Content.Load<Texture2D>("Instructions\\Controls"));
            screens.Add(Content.Load<Texture2D>("Instructions\\chars"));
        }

        public void update()
        {
            if (Game1.singlePress(Buttons.A))
                curScreen++;
            if (curScreen == screens.Count)
                Game1.state = GameState.StartMenu;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(screens[curScreen], new Rectangle(0,0,800,480), Color.White);
        }
    }
}
