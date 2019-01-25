/**
 * 
 * Description: The purpose of this page is to 
 * provide insructions of how to play the game.
 * Created by Kevin Lucas on Dec 18, 2018
 * 
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

namespace AllInOneMono
{
    public class HelpScene : GameScene
    {
        public SpriteBatch spriteBatch;
        public Texture2D helpTex;
        const int SCREENWIDTH = 1280;
        const int SCREENHEIGHT = 720;

        public SpriteFont font;

        public string title;
        public string description;
        public string controlInstruction;
        public HelpScene(Game game): base(game)
        {
            Game1 g = (Game1)game;
            this.spriteBatch = g.spriteBatch;
            helpTex = g.Content.Load<Texture2D>("ControlFinalGameProg");


            font = g.Content.Load<SpriteFont>("file");

             title = "Help";
             description = "The Objective of the game is to destory the enemy ships to score points \n" +
                                   " without getting destroyed yourself.";
             controlInstruction = "To move the player left can be done by pressing the 'A' or left arrow key. \n" +
                                        " To move the player right can be done by pressing the 'd' or right arrow key.\n" +
                                        " The player can shoot by pressing the space key. (see picture)";

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, title,new Vector2(500f,80f), Color.White);
            spriteBatch.Draw(helpTex,new Rectangle(100,100,300,300),Color.White);
            spriteBatch.DrawString(font, description, new Vector2(500f, 140f), Color.White);
            spriteBatch.DrawString(font, controlInstruction, new Vector2(500f, 200f), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
