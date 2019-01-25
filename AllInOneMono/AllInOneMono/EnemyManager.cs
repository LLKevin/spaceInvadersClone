/**
 * 
 * Is to control the creation, movement, 
 * and shooting of multiple enemies. 
 * 
 * 
 * Kevin Lucas
 * 
 * **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllInOneMono;
using C3.XNA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PROG2370CollisionLibrary;

namespace KLFinalGame
{
    class EnemyManager : DrawableGameComponent
    {
        Game1 g;
        SpriteBatch spriteBatch;      

        // Contains a list of elements
        public static List<Enemy> enemiesRef;
        Vector2 _scale;

        Enemy enemy;

        //spacing --------
        const int ENEMYSIZE = 35;
        float spacing = 20f;

        //movement speed;
        float speed = 5f;

        float shootingSpeed = 0.85f;

        //bool to change direction       
        public bool moveRight;
        public float movementTimer;
        public Vector2 velocity;

        Enemy farRightEnemy;
        Enemy farLeftEnemy;
        int width;
        float shootTimer;
        public EnemyManager(Game game, SpriteBatch spriteBatch) : base(game)
        {
            g = game as Game1;
            this.spriteBatch = spriteBatch;
            moveRight = true;
            enemiesRef = new List<Enemy>();
            width = GraphicsDevice.Viewport.Width;


    //https://github.com/PartumGames/SpaceInvaders_MonoGame/blob/master/Space_Invaders/Game_Content/EnemyController.cs// adapted adding multiple enemy from here.
            for (int i = 0; i < 11; i++)//col
            {  
               for(int j = 0; j < 5; j++)//rol
                {        
                    _scale = new Vector2(ENEMYSIZE, ENEMYSIZE); //create a scale for the enemy
                    float xPos = ((_scale.X + spacing) * i) + 100f; // set x position with spacing around 
                    float yPos = ((_scale.Y + spacing) * j) + 100f ;
                
                    velocity = new Vector2(speed, 0);
                    
                    enemy = new Enemy(g, spriteBatch, new Vector2(xPos, yPos));                  
                    g.Components.Add(enemy);
                    enemiesRef.Add(enemy);                                       
                }               
            }
        }
        //draws the enemy matrix
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            foreach (Enemy m in enemiesRef)
            {
                spriteBatch.Draw(m.enemyTexture,m.enemy,Color.White);
                //spriteBatch.DrawRectangle(m.enemy, Color.Aquamarine); //Draws HitBoxes around enemies
            }
            spriteBatch.End();

          base.Draw(gameTime);
        }
        public override void Update(GameTime gameTime)
        {
            //movement time based on elapsing time
            movementTimer += (float) gameTime.ElapsedGameTime.TotalSeconds;

            //enables the shooting mechanic for enemies
            shooting(gameTime); 

            //check for hightest enemy location -- far right
            for (int i = 0; i < enemiesRef.Count; i++)
            {
                if (enemiesRef[i] != null)
                {
                    if (i + 1 < enemiesRef.Count)
                    {
                        Enemy tempHolder = enemiesRef[i + 1];
                        if (enemiesRef[i].enemy.X > tempHolder.enemy.X)
                        {
                            farRightEnemy = enemiesRef[i];
                        }
                        else
                        {
                            farRightEnemy = enemiesRef[i + 1];
                        }
                    }
                }
            }

            //used to move the group of enemies based on a timer to allow the "Space invader like effect"
            if (movementTimer > 0.1f)
            {
                foreach (Enemy m in enemiesRef)
                {
                    if (farRightEnemy.enemy.X <= 1100f && moveRight == true)/*(farRightEnemy.enemy.X <= 1100f && moveRight == true)*/
                    {                       
                        m.enemy.X += (int)speed;
                    }
                    else
                    {
                        moveRight = false;
                    }
                    if (m.enemy.X >= 100f && moveRight == false)
                    {
                        m.enemy.X -= (int)speed;
                    }
                    else
                    {
                        moveRight = true;
                    }
                    if (farRightEnemy.enemy.X == 1100f /*|| m.enemy.X == 100f*/)
                    {
                        m.enemy.Y += 10;
                    }

                    velocity.Y = 0;
                    movementTimer = 0;
                }
            }          
            //no enemies --- player has lives left : redraw the group of enemies
            if (enemiesRef.Count == 0 && Hud.lives > 0)
            {
                for (int i = 0; i < 11; i++)//col
                {
                    for (int j = 0; j < 5; j++)//row
                    {
                        //might be required for changing 
                        _scale = new Vector2(ENEMYSIZE, ENEMYSIZE);
                        float xPos = ((_scale.X + 15) * i) + 300f;
                        float yPos = ((_scale.Y + 15) * j) + 100f;

                        velocity = new Vector2(0, 10);
                        enemy = new Enemy(g, spriteBatch, new Vector2(xPos, yPos));
                        g.Components.Add(enemy);
                        enemiesRef.Add(enemy);
                    }
                }
            }
            //if the enemy count drops to a certain level it wll shoot more projectiles && speed increase
            if(enemiesRef.Count < 30)
            {
                shootingSpeed = 0.50f;
                if(enemiesRef.Count == 20)
                {
                    speed = 6.0f ;
                }
            }
            else if (enemiesRef.Count <= 10)
            {
                shootingSpeed = 0.30f;
                if(enemiesRef.Count == 10){
                    speed = speed + 6.8f;
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// used for the shooting application for enemy grid based on a timer.
        /// I decided to make it random to increase the diffculty of the game
        /// </summary>
        /// <param name="gameTime"></param>
        public void shooting(GameTime gameTime)
        {
             shootTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            Random rndSelector = new Random();
            int randomEnemy = rndSelector.Next(0, enemiesRef.Count);

            ///TODO: IF All Enemies are destroyed breaks the game
            if (shootTimer > shootingSpeed)
            {
               Bullet b = new Bullet(g, spriteBatch, new Vector2(enemiesRef[randomEnemy].enemy.X, enemiesRef[randomEnemy].enemy.Y));
               b.isEnemy = true;
               g.Components.Add(b);
               shootTimer = 0;                                
            }
        }
       
    }
}




///https://stackoverflow.com/questions/16914970/how-to-permanently-delete-enemy-sprites-in-xna
/// music : https://opengameart.org/content/hero-immortal ---- Trevor Lentz
