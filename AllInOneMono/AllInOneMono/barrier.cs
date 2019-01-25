/**
  Description: The purpose of this class is to create and position the barriers
  which will be stored inside a list to keep track of blocks being hit.
  Created by Kevin Lucas of Jan 16 2019
 */
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

namespace AllInOneMono
{
    class barrier : DrawableGameComponent
    {
        Game1 g;
        SpriteBatch spriteBatch;
        public static List<Rectangle> shield;

        //X & Y Axis for the single block
        public float block_X = 0;
        public float block_Y = 0;
        // set  X & Y Axis for the fullBarrier
        public float barrier_X;
        public float barrier_Y;
        //Space around each black
        public float spacing = 1.5f;
        //Size of each block
        public int blockSize = 15;
        //number of shield in the game;
        const int MAXNUMBEROFSHIELDS = 3;
        int shieldCounter = 0;
        //check if a block is alive
        bool isAlive;
        //newGame -- draw barriers again
       
        public barrier(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            g = game as Game1;
            shield = new List<Rectangle>();

            //set the first barrier position
            barrier_X = 250f;
            barrier_Y = 520f;

            // creates the shields 
            generateBarriers();
        }

        public override void Draw(GameTime gameTime)
        {
            if (isAlive)
            {
                spriteBatch.Begin();
                foreach (Rectangle rect in shield)
                {
                    spriteBatch.FillRectangle(rect, Color.LightGreen);
                }
                spriteBatch.End();
            }

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {

            if (g.gameStateActive == false)
            {
                generateBarriers();
                spriteBatch.Begin();
                foreach (Rectangle rect in shield)
                {
                    spriteBatch.FillRectangle(rect, Color.LightGreen);
                }
                spriteBatch.End();
            }

                base.Update(gameTime);
            
        }
        /// <summary>
        /// Method to create the barrier
        /// </summary>
            public void generateBarriers()
            {
                shieldCounter = 0;
                while (shieldCounter < MAXNUMBEROFSHIELDS)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            isAlive = true; //sets that is alive
                            block_X = ((blockSize + spacing) * i) + barrier_X; //sets the x-cord and size for the block 
                            block_Y = ((blockSize + spacing) * j) + barrier_Y; // set the y-cord and size for the block
                            shield.Add(new Rectangle((int)block_X, (int)block_Y, blockSize, blockSize)); //adds the block to the list.
                        }
                    }
                    barrier_X += 300f; // spaces each barrier container.
                    shieldCounter++;//increase the shield counters up
                }
            }
        }
    }


