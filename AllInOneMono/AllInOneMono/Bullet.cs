/**
 * Description: Used to create bullets witch can be shoot by both enemies and the player.
 * this will handle the all collision using the provided collision class.
 * Create by Kevin Lucas on Dec 14 2018
 * 
 * **/
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
    class Bullet : DrawableGameComponent
    {
        //Bullet Image
        public Texture2D bulletTexture; //bullet image
        //Size of the bullet texture
        const int BULLETWIDTH = 15;
        const int BULLETHEIGHT = 15;
        //bullet Travel Speed
        const int SPEED = 5;

        //Enemy Death effect
        private SoundEffect destroyedEnemyShipSound;
       
        public static bool playerDead;
        
        //checks if the bullet is visable/ Alive
        bool isAlive = false;
        bool isAliveEnemy = false;
        public bool IsAlive{ get => isAlive; }
        public bool IsAliveEnemy { get => isAliveEnemy; } 
        //checks if player or enemy is shooting
        public bool isPlayer = false;
        public bool isEnemy = false;
        //
        public bool enemyShotBarrier;


        //Bullet
        public Rectangle enemyBullet;
        public Rectangle bullet;

        //set the collison 
        Sides collisionEnemy;
        Sides collisionPlayer;
        Sides collisionBarrierPlayer;
        Sides collisionBarrierEnemy;

        SpriteBatch SpriteBatch;

        //collection Of bullets        
        public List<Rectangle> bulletRididBodyCollection;
        public List<Rectangle> enemyBulletCollection;

        public bool playerDeathCheck;

        //bool to check whom is firing 
        bool firing = false;
        bool enemyfiring = false;
        Game1 g;

        public Bullet(Game game, SpriteBatch spriteBatch, Vector2 startLocation) : base(game)
        {
            g = game as Game1;
            this.SpriteBatch = spriteBatch;
            bulletTexture = g.Content.Load<Texture2D>("spaceMissiles_018");
           
            //bullet is alive
            isAlive = true;
            //live enemy bullet
            isAliveEnemy = true;

            playerDeathCheck = false;
            //
            enemyShotBarrier = false;


            playerDead = false;

            destroyedEnemyShipSound = g.Content.Load<SoundEffect>("atari_boom2"); 
           
            //bullet Location
            bullet = new Rectangle((int)startLocation.X + 14, (int)startLocation.Y - 15, BULLETWIDTH, BULLETHEIGHT);
            
            enemyBullet = bullet;

            //might need to remove --- player
            enemyBulletCollection = new List<Rectangle>();
            bulletRididBodyCollection = new List<Rectangle>();
            if (isEnemy)
            {
                //if it is an enemy shooting 
                enemyBulletCollection.Add(enemyBullet);
            }
            else
            {
                //if it is a player shooting
                bulletRididBodyCollection.Add(bullet);
            }         
        }
        public override void Draw(GameTime gameTime)
        {
            //based on who is shooting the bullet it will draw the bullet from the object that has shoot it.
            // this will be used to determine the collision with another object
            if (isAlive && g.gameStateActive == true)
            {              
                SpriteBatch.Begin();
                if (!isEnemy && !enemyShotBarrier)
                {
                    SpriteBatch.Draw(bulletTexture, bullet, Color.Red);
                  //  SpriteBatch.DrawRectangle(bullet, Color.Red);
                }
                SpriteBatch.End();
            }
            if(isAliveEnemy && g.gameStateActive == true)
                {
                if (isEnemy)
                {
                    SpriteBatch.Begin();
                    SpriteBatch.Draw(bulletTexture, enemyBullet, Color.Blue);
                  //  SpriteBatch.DrawRectangle(enemyBullet, Color.Blue);
                    SpriteBatch.End();
                }
            }           
            base.Draw(gameTime);
        }
        public override void Update(GameTime gameTime)
        {
            int height = GraphicsDevice.Viewport.Height;

            //bullet management for Player
            if (isAlive && isEnemy == false && enemyShotBarrier == false)//check if the bullet is alive
            {
                firing = true;
                if (bullet.Y < 0)
                {
                    isAlive = false;
                    firing = false;
                    enemyShotBarrier = false;
                    bulletRididBodyCollection.Remove(bullet);
                }
               
                bullet.Y -= SPEED;
            }
            //Bullet management for Enemy
             if( isAlive && isEnemy == true)
            {
                enemyfiring = true;
                if (enemyBullet.Y < 0)
                {
                    //not firing 
                    enemyfiring = false;
                    isAliveEnemy = false;
                    enemyShotBarrier = false;
                    enemyBulletCollection.Remove(enemyBullet);
                }              
                enemyBullet.Y += SPEED;
            }

             //checks if the enemy and the player bullet has been collide.
            if (firing == true && isEnemy == false && g.gameStateActive == true)
            {
                for (int i = 0; i < EnemyManager.enemiesRef.Count; i++)
                {
                    collisionEnemy = bullet.CheckCollisions(EnemyManager.enemiesRef[i].enemy);
                    if ((collisionEnemy & Sides.BOTTOM) == Sides.BOTTOM)
                    {
                        isAlive = false;
                        bulletRididBodyCollection.Remove(bullet);
                        EnemyManager.enemiesRef[i].isAlive = false;
                        EnemyManager.enemiesRef.RemoveAt(i);                   
                        destroyedEnemyShipSound.Play(0.1f,0.0f,0.0f);//play a death animation sound
                        Hud.score += 10; //increase the score by 10 point if you kill an enemy
                        firing = false;//only check collision if firing 
                       // break; //stop checking for collision
                    }
                }
            }
            //checks if the enemy bullet has collided with the player
            if (enemyfiring == true && isEnemy == true && g.gameStateActive == true)
            {
                collisionPlayer = Player.player.CheckCollisions(enemyBullet);
                if ((collisionPlayer & Sides.TOP) == Sides.TOP)
                {
                    if (playerDeathCheck == false && Hud.lives > 0)
                    {
                        destroyedEnemyShipSound.Play(0.1f, 0.0f, 0.0f);//play a death animation sound
                        enemyBulletCollection.Remove(enemyBullet);//remove the enemy bull hitbox
                        isAliveEnemy = false;// kill off the enemy bullet
                        enemyfiring = false; // set off the enemy firing flag to prevent multiple hitbox taking effect at a single time.
                        playerDead = true; //flag the player has died
                        Hud.lives--; //remove a life
                        playerDeathCheck = true; // break the collison death check.
                    }
                }

            }
            // this is the collision for the shields if the player bullet hits the shield
            for (int k = 0; k < barrier.shield.Count; k++)
            {
                collisionBarrierPlayer = bullet.CheckCollisions(barrier.shield[k]);

                if ((collisionBarrierPlayer & Sides.BOTTOM) == Sides.BOTTOM)
                {
                    //remove the player's bullet from the collection
                    isAlive = false;
                    bulletRididBodyCollection.Remove(bullet);
                    barrier.shield.RemoveAt(k);
                    firing = false;
                    break;
                }
            }

            // Collision for the enemies bullet with the barrier

            if (enemyfiring == true  && enemyShotBarrier == false && g.gameStateActive == true)
            {
                for (int z = 0; z < barrier.shield.Count; z++)
                {
                    collisionBarrierEnemy = enemyBullet.CheckCollisions(barrier.shield[z]);
                    if ((collisionBarrierEnemy & Sides.TOP) == Sides.TOP)
                    {
                        enemyBulletCollection.Remove(enemyBullet);//remove the enemy bull hitbox
                        isAliveEnemy = false;// kill off the enemy bullet
                        isEnemy = false;
                        enemyShotBarrier = true;
                        enemyfiring = false; // set off the enemy firing flag to prevent multiple hitbox taking effect at a single time.
                        barrier.shield.RemoveAt(z);
                        break;
                        
                    }

                }
              
            }
     
        }
    }
}


///https://opengameart.org/content/atari-booms
/// dklon credit for explosion effect

