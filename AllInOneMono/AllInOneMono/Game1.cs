using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace AllInOneMono
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        
        //declare all the scenes here
        private StartScene startScene;
        private ActionScene actionScene;
        private HelpScene helpScene;
        private Credits creditScene;
        private HighScoresScene highScoreScene;
        // others....


       //credit
        private AboutScene aboutScene;
        private HowToPlayScene howToPlayScene;

        const int SCREENWIDTH = 1280;
        const int SCREENHEIGHT = 720;
        public static bool getEnemies;
        public Song song;


        public bool gameStateActive = false;


        public Game1()
        {         
            graphics = new GraphicsDeviceManager(this);
            {
                graphics.PreferredBackBufferWidth = SCREENWIDTH;
                graphics.PreferredBackBufferHeight = SCREENHEIGHT;

                Content.RootDirectory = "Content";
            }
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
            Shared.stage = new Vector2(graphics.PreferredBackBufferWidth,
                graphics.PreferredBackBufferHeight);
          
            //initialize other Shared class members

            base.Initialize();
        }

        private void hideAllScenes()
        {
            GameScene gs = null;
            foreach (GameComponent  item in Components)
            {
                if (item is GameScene)
                {
                    gs = (GameScene)item;
                    gs.hide();
                }
               
            }
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {

            song = Content.Load<Song>("Hero Immortal");
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            startScene = new StartScene(this);
            this.Components.Add(startScene);
            startScene.show();

            //create other scenes here and add to component list
            actionScene = new ActionScene(this, spriteBatch);
            this.Components.Add(actionScene);

            highScoreScene = new HighScoresScene(this);
            this.Components.Add(highScoreScene);

            aboutScene = new AboutScene(this, spriteBatch);
            this.Components.Add(aboutScene);


            helpScene = new HelpScene(this);
            this.Components.Add(helpScene);

            howToPlayScene = new HowToPlayScene(this);
            this.Components.Add(howToPlayScene);

            creditScene = new Credits(this);
            this.Components.Add(creditScene);
            // others.....
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            int selectedIndex = 0;
           

            KeyboardState ks = Keyboard.GetState();
          

            if (startScene.Enabled)
            {
                selectedIndex = startScene.Menu.SelectedIndex;
                if (selectedIndex == 0 && ks.IsKeyDown(Keys.Enter))
                {
                    //creates the gamescene when it is entered
                    actionScene = new ActionScene(this, spriteBatch);
                    this.Components.Add(actionScene);
                 
                    hideAllScenes();
                    actionScene.show();             
                    
                    //plays background music
                    MediaPlayer.Play(song);
                    MediaPlayer.IsRepeating = true;
                    MediaPlayer.Volume = 0.1f;
                    gameStateActive = true;
                }
                else if(selectedIndex == 1 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    aboutScene.show();
                }
                else if (selectedIndex == 2 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();                   
                    helpScene.show();
                }
                else if(selectedIndex ==3 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    howToPlayScene.show();
                    
                }
                else if(selectedIndex == 4 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    highScoreScene.show();
                }
                else if (selectedIndex == 5 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    creditScene.show();
                }
                //handle other menu options
                else if (selectedIndex == 6 && ks.IsKeyDown(Keys.Enter))
                {                
                    Exit();
                }
            }

            if (actionScene.Enabled || helpScene.Enabled || aboutScene.Enabled || howToPlayScene.Enabled || creditScene.Enabled || highScoreScene.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {          
                    hideAllScenes();
                    startScene.show();
                    gameStateActive = false;
                    MediaPlayer.Stop();
                }
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CadetBlue);
            base.Draw(gameTime);
        }
    }
}
