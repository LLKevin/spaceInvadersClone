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
    class HowToPlayScene : GameScene
    {
        public SpriteBatch spriteBatch;
        public SpriteFont font;
        public Game1 g;
        public string aboutSection;
        public string controlInstruction;


        const int SCREENWIDTH = 1280;
        const int SCREENHEIGHT = 720;


        public Vector2 position;
        public HowToPlayScene(Game game) : base(game)
        {

            g = game as Game1;
            this.spriteBatch = g.spriteBatch;
            font = g.Content.Load<SpriteFont>("File");

            aboutSection = "How to Play";
            controlInstruction = "To move the player left can be done by pressing the 'A' or left arrow key. \n" +
                                        " To move the player right can be done by pressing the 'd' or right arrow key.\n" +
                                        " The player can shoot by pressing the space key. (see picture)";


            position = new Vector2(Shared.stage.X / 2, Shared.stage.Y / 2);         
        }

        public override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin();
            spriteBatch.DrawString(font, aboutSection, new Vector2(500f, 80f), Color.White);        
            spriteBatch.DrawString(font, controlInstruction, new Vector2(500f, 200f), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
