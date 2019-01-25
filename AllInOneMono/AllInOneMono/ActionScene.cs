/**
  Description: The purpose of this class is 
  to display the actions which will be the scene where the game is played in.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C3.XNA;
using KLFinalGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace AllInOneMono
{
    public class ActionScene : GameScene
    {
        const int SCREENWIDTH = 1280;
        const int SCREENHEIGHT = 720;

        private SpriteBatch spriteBatch;
        private EnemyManager em;
        private Hud h;
        private Player p;
        private Texture2D playerTexture;
        private barrier barrier;


        public Game1 g;
            
        public ActionScene (Game game, SpriteBatch spriteBatch): base(game)
        {
            //Creation of game objects.

              g = (Game1)game;
             this.spriteBatch = spriteBatch;

             playerTexture = g.Content.Load<Texture2D>("spaceShips_009");

             p = new Player(g, spriteBatch, playerTexture, SCREENWIDTH);
            Components.Add(p);

            em = new EnemyManager(g, spriteBatch);
            Components.Add(em);

            h = new Hud(g, spriteBatch);
            Components.Add(h);

            barrier = new barrier(g, spriteBatch);
            Components.Add(barrier);
        }

        public override void Update(GameTime gameTime)
        {         
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            //set background colour.
            GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);
        }
    }
}
