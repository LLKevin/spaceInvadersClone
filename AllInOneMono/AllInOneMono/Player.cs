/**
 * 
 * Description: The purpose of this class is to control all movement,
 *              shooting, and death animation of the player
 * Created by Kevin Lucas on Dec 18, 2018
 * 
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PROG2370CollisionLibrary;
using C3.XNA;
using Microsoft.Xna.Framework.Audio;
using AllInOneMono;

namespace KLFinalGame
{
    class Player : DrawableGameComponent
    {
        //used to animate the player death animation
        List<Rectangle> deathAnimationCollection;
        Game1 g;
      
        //check if the player is alive
        bool isAlive;

        //Private -- sounding effect
        private SoundEffect playerShootingSound;
        private Texture2D explosionTexture;


        SpriteBatch spriteBatch;
        Texture2D playerTexture;


 
        public static Rectangle player;
        
        Vector2 velocity;

        //maximum screen width;
        public int maxWidth;   

        //size of the player with scaling
        const float SCALE = 0.6f;
        const int STANDFRAMEWIDTH  = 80;     //all values from spritesheet.txt
        const int STANDFRAMEHEIGHT = 55;

        //location of the Player
        const int PLAYERLOCATIONAXIS_X = 620;
        const int PLAYERLOCATIONAXIS_Y = 650;
        // Player's movement speed 
        const int SPEED = 5;

        //total amount of bullets
       // const int MAXBULLETS = 3; --- remove?

        // used to keep track the amount of time elapsed to limit the amout of bullets on the screen at a given time.
        float timeElapsed;

       public static bool deathAnimation;
       public bool LockMovement = false;

        bool removeDeathAnimation;


        int currentFrame = 0;
        int currentFrameCounter =0;
        const int MAXFAMES = 5;


        public Player(Game game, SpriteBatch spriteBatch, Texture2D playerTexture, int screenWidth) : base(game)
        {
            deathAnimation = true;
            this.spriteBatch = spriteBatch;
            this.playerTexture = playerTexture;
            this.maxWidth = screenWidth;
            this.g = game as Game1;

            //Load the Sound effect
            playerShootingSound = g.Content.Load<SoundEffect>("laser1");

            //used for movement
            velocity = new Vector2(PLAYERLOCATIONAXIS_X, PLAYERLOCATIONAXIS_Y);
            //use to position the player
            player = new Rectangle(PLAYERLOCATIONAXIS_X, PLAYERLOCATIONAXIS_Y, (int)(SCALE * STANDFRAMEWIDTH), STANDFRAMEHEIGHT);

            //check if player is alive
            this.isAlive = true;
            //starting amount of lives
            Hud.lives = 3;

            removeDeathAnimation = false;

            //explosionTexture\
            explosionTexture = g.Content.Load<Texture2D>("explosions");

            //add animation frames
            deathAnimationCollection = new List<Rectangle>();
            deathAnimationCollection.Add(new Rectangle(396, 434, (int)(SCALE * STANDFRAMEWIDTH), STANDFRAMEHEIGHT));//0
            deathAnimationCollection.Add(new Rectangle(396, 236, (int)(SCALE * STANDFRAMEWIDTH), STANDFRAMEHEIGHT));//1
            deathAnimationCollection.Add(new Rectangle(368, 896, (int)(SCALE * STANDFRAMEWIDTH), STANDFRAMEHEIGHT));//2
            deathAnimationCollection.Add(new Rectangle(364, 698, (int)(SCALE * STANDFRAMEWIDTH), STANDFRAMEHEIGHT));//3
            deathAnimationCollection.Add(new Rectangle(330, 500, (int)(SCALE * STANDFRAMEWIDTH), STANDFRAMEHEIGHT));//3
            deathAnimationCollection.Add(new Rectangle(330, 302, (int)(SCALE * STANDFRAMEWIDTH), STANDFRAMEHEIGHT));//3
            deathAnimationCollection.Add(new Rectangle(330, 104, (int)(SCALE * STANDFRAMEWIDTH), STANDFRAMEHEIGHT));//3
            deathAnimationCollection.Add(new Rectangle(298, 792, (int)(SCALE * STANDFRAMEWIDTH), STANDFRAMEHEIGHT));//3
           
        }
        public override void Draw(GameTime gameTime)
        {
            //draws the player's ship to the bottom of the screen
            if (isAlive && !Bullet.playerDead)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(playerTexture, player, null, Color.White, 0f, new Vector2(0), SpriteEffects.FlipVertically, 0);
              //  spriteBatch.DrawRectangle(player, Color.Yellow); //hitbox
                spriteBatch.End();
            }
            else if (Hud.lives >= 0 && removeDeathAnimation == false)
            {
                
                DeathAnimation(gameTime);
                deathAnimation = false;
            }          
            base.Draw(gameTime);                   
        }

        public override void Update(GameTime gameTime)
        {        
            velocity.X = 0;        

            KeyboardState keyState = Keyboard.GetState();
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
           
            //moves the play left
            if (keyState.IsKeyDown(Keys.A) || keyState.IsKeyDown(Keys.Left))
            {
                if (player.X > 100)
                    velocity.X = -SPEED;
            }
            //moves the play right
          
            if (keyState.IsKeyDown(Keys.D) || keyState.IsKeyDown(Keys.Right))
            {
                if (player.X < maxWidth - 100)
                    velocity.X = SPEED;
            }
            
            if (keyState.IsKeyDown(Keys.Space) && isAlive)
            {
                //TODO: Add Shooting Sound
                if (timeElapsed > 0.45f && isAlive)
                {
                   Bullet b = new Bullet(g, spriteBatch, new Vector2(player.X, player.Y));
                    b.isPlayer = true;
                    g.Components.Add(b);
                    timeElapsed = 0;

                    //sound plays when shooting
                    playerShootingSound.Play(0.25f,0.0f,0.0f);
                }     
            }
            //TODO: Player Collision 
            if (Bullet.playerDead == true) 
            {
                this.isAlive = false;

                currentFrameCounter++;
                if(currentFrameCounter > MAXFAMES)
                {
                    currentFrameCounter = 0;
                    currentFrame++;
                }
                if(currentFrame >= deathAnimationCollection.Count -1)
                {
                    currentFrame = 0;
                }

                if (Hud.lives > 0 && deathAnimation == false)
                {
                    this.isAlive = true;  
                }
                else if (Hud.lives == 0 && deathAnimation == false)
                {
                    removeDeathAnimation = true;
                }

            }

            player.X = player.X + (int)velocity.X;

            base.Update(gameTime);
        }
        public void DeathAnimation(GameTime gameTime)
        {
           
            do
            {
                deathAnimation = true;
                spriteBatch.Begin();
                spriteBatch.Draw(explosionTexture, player, 
                    deathAnimationCollection.ElementAt(currentFrame), Color.White, 0f, new Vector2(0), SpriteEffects.None, 0f);
                spriteBatch.End();
            } while (currentFrameCounter > MAXFAMES);

            deathAnimation = false;



        }
    }
}





//laser reference ----- credit dklon
//https://opengameart.org/content/laser-fire