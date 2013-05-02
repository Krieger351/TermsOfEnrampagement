using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GDD2
{

    enum Direction { North, South, East, West };
    public enum GameState { StartMenu, Instructions, Game, GameOver, Pause, Win }
    
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public static Texture2D blank;
        public static Texture2D ground;
        public static SpriteFont font;

        public static GamePadState oldState;
        public static GamePadState newState;

        public static GameState state = GameState.StartMenu;

        static Dictionary<String, SoundEffect> sounds;

        public static int width = 5000;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Camera cam;

        StartMenu startMenu;
        Instructions instructions;

        PlayerManager players;
        WallManager walls;
        EnemyManager enemies;
        BulletManager bullets;
        Spawner[] spawn;
        UI ui;
        Item item;

        Texture2D gameover;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            cam = new Camera(GraphicsDevice.Viewport.Width / 2, Game1.width - (GraphicsDevice.Viewport.Width / 2));
            cam.Pos = new Vector2(0, GraphicsDevice.Viewport.Height / 2);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            blank = Content.Load<Texture2D>("blank");
            font = Content.Load<SpriteFont>("font");
            gameover = Content.Load<Texture2D>("GameOver");

            startMenu = new StartMenu(this);
            instructions = new Instructions(Content);

            sounds = new Dictionary<String, SoundEffect>();

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            
            oldState = newState;
            newState = GamePad.GetState(PlayerIndex.One);
            
            #region Start Update
            if (state == GameState.StartMenu)
                startMenu.update();
            #endregion
            
            else if (state == GameState.Instructions)
            {
                instructions.update();
            }

            #region Game Update
            else if (state == GameState.Game)
            {
                walls.update();
                players.update(walls);
                bullets.update();
                enemies.update(walls.walls, bullets);
                item.update();
                foreach (Spawner cur in spawn)
                    cur.update();
            }
            #endregion

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Blue);

            if (state == GameState.StartMenu)
            {
                spriteBatch.Begin();
                startMenu.draw(spriteBatch, GraphicsDevice.Viewport.Width);
            }
            else if (state == GameState.Instructions)
            {
                spriteBatch.Begin();
                instructions.draw(spriteBatch);
            }
            #region Gamme Draw
            else if (state == GameState.Game)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone, null, cam.get_transformation(graphics.GraphicsDevice));
                spriteBatch.Draw(ground, new Vector2(0, 0), new Rectangle(0, 0, Game1.width, GraphicsDevice.Viewport.Height), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                walls.draw(spriteBatch);
                foreach (Spawner cur in spawn)
                    cur.draw(spriteBatch);
                item.draw(spriteBatch);
     
                enemies.draw(spriteBatch);
                bullets.draw(spriteBatch);
                players.draw(spriteBatch);
                
                spriteBatch.End();
                
                spriteBatch.Begin();
                ui.draw(spriteBatch);
            }
            #endregion
            else if (state == GameState.GameOver)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(gameover, Vector2.Zero, Color.White);
                spriteBatch.DrawString(Game1.font, "Game Over", new Vector2(GraphicsDevice.Viewport.Width/2-144, GraphicsDevice.Viewport.Height/2+100), Color.White);
            }
            else if (state == GameState.Win)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(gameover, Vector2.Zero, Color.White);
                spriteBatch.DrawString(Game1.font, "You Win", new Vector2(GraphicsDevice.Viewport.Width / 2 - 144, GraphicsDevice.Viewport.Height / 2 + 100), Color.White);
            }
            
            spriteBatch.End();
            
            base.Draw(gameTime);
        }

        public void startGame(int p)
        {
            ground = Content.Load<Texture2D>("ground");

            Texture2D fireball = Content.Load<Texture2D>("Fireball"); ;
            bullets = new BulletManager(fireball);

            walls = new WallManager(cam, GraphicsDevice);
            players = new PlayerManager(Content, bullets, cam, p);

            enemies = new EnemyManager(Content, players.players);

            spawn = new Spawner[10];

            Texture2D spawner = Content.Load<Texture2D>("spawner");

            for (int i = 0; i < 10; i++)
            {
                int y = 10;
                if((i+1)%2 == 1)
                    y = 420;
                spawn[i] = new Spawner(enemies, new Vector2((800 * i) + 10, y), cam, spawner);
            }

            ui = new UI(players.players, cam);
            item = new Item(new Vector2(Game1.width - 100 ,215) , players, Content.Load<Texture2D>("Treasure"));
        }

        public static void Play(string sound)
        {
            sounds[sound].Play();
        }
        public static bool singlePress(Buttons b)
        {
            bool ret = false;
            if (oldState.IsButtonUp(b) && newState.IsButtonDown(b))
                ret = true;
            return ret;
        }

    }
}
