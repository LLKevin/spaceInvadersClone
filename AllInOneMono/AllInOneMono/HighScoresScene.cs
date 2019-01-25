/**
 * 
 *  Description: This is to display 
 *  all the highscores recorded in the game 
 *  Created by Kevin Lucas on Dec 18 2018
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
using Microsoft.Xna.Framework.Media;

namespace AllInOneMono
{
    class HighScoresScene : GameScene
    {

        public SpriteBatch spriteBatch;
        public SpriteFont font;
        public Game1 g;

        public List<int> highScores;
        const int SCREENWIDTH = 1280;
        const int SCREENHEIGHT = 720;
        public int spacing = 50;

     
        public HighScoresScene(Game game) : base(game)
        {
            g = game as Game1;
            this.spriteBatch = g.spriteBatch;

            highScores = new List<int>();
             
            font = g.Content.Load<SpriteFont>("File");
            HighScore h = new HighScore(g);
            h.loadSavedScore();
            if (h.highScores.Count > 0)
            {           
                highScores = h.highScores;
                highScores.Reverse();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            for(int i = highScores.Count - 1; i >=0; i--)
            {
                spriteBatch.DrawString(font, highScores[i].ToString(), new Vector2(100, i * 30), Color.White);
              
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }
    }
}
