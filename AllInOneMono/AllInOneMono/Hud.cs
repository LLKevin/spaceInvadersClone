/**
 * 
 * Description:The purpose of this is to draw and update the game hud. 
 * This is to be displayed the game is being played.
 * Created by Kevin Lucas on Dec 18, 2018
 * 
 * */
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

namespace KLFinalGame
{
    // just need to do highscore
    // restart game 
    class Hud : DrawableGameComponent
    {
        private SpriteFont font;
        public SpriteBatch spriteBatch;
        public static int score;
        public static int lives;
        public static int highScoredDisplay;
        public bool gameOverExist;
        public bool saveHighScore;

        HighScore h;
       // public List<int> highScores;
        private Game1 g;
        public static bool lifeCheck;

        public Hud(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.spriteBatch = spriteBatch;
            g = game as Game1;
            font = g.Content.Load<SpriteFont>("File");
          
            gameOverExist = false;
            lifeCheck = false;
            saveHighScore = false;
            score = 0;

            h = new HighScore(game);
            h.loadSavedScore();
            h.highScores.Sort();
            if (h.highScores.Count != 0)
            {
                highScoredDisplay = h.highScores.Last();
            }
        }
        public override void Draw(GameTime gameTime)
        {

            //display lives, score, and Highscore.
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Score" + " " + score , new Vector2(20, 20), Color.White);
            spriteBatch.DrawString(font, "Lives" + " " + lives, new  Vector2 (20, 680), Color.White);
            spriteBatch.DrawString(font, "HighScore" + " " + highScoredDisplay, new Vector2(600, 20), Color.White);
            if (gameOverExist)
            {
                spriteBatch.DrawString(font, "Game Over", new Vector2(30, 800), Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public override void Update(GameTime gameTime)
        {
            KeyboardState keyState = Keyboard.GetState();   
           
            //check if the play has any lives left.
            if (lives == 0 && saveHighScore == false)  
            {
                if (score > 0)
                {
                    h.score = score;
                    h.SaveHighScore();
                    saveHighScore = true;
                }
            }
            //check if the player has hit the escape key to record the ending game state.
            if (g.gameStateActive == false)
            {
                if (score > 0)
                {
                    h.score = score;
                    h.SaveHighScore();
                    saveHighScore = true;
                }
            }

            base.Update(gameTime);
        }
    }
}
