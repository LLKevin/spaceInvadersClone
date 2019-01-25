/**
 Used to create copys of type enemies
 Created by Kevin Lucas Dec 11,2018
 */
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
using AllInOneMono;

namespace KLFinalGame
{
    class Enemy : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        public Texture2D enemyTexture;
        public Rectangle enemy;

        //enemy width and height
        const int ENEMYWIDTH = 35;
        const int ENEMYHEIGHT = 35;
        
        public Vector2 velocity;
              
        public bool isAlive = false;
      
        public Enemy(Game game, SpriteBatch spriteBatch, Vector2 startingLocation) : base(game)
        {
            this.spriteBatch = spriteBatch;

            //default enemy to alive
            this.isAlive = true;
          
            //location and size of the enemy spawn
            enemy = new Rectangle((int)startingLocation.X, (int)startingLocation.Y, ENEMYWIDTH, ENEMYHEIGHT);
          
            //load the assest --- enemey ship
            Game1 g = game as Game1;
           
            enemyTexture = g.Content.Load<Texture2D>("spaceShips_Enemy");

            this.velocity = new Vector2(startingLocation.X, startingLocation.Y);
            
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }           
    }
}
