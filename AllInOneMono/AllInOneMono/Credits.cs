/***
 
 Description: used to display the credit screen.

 */
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
    class Credits : GameScene
    {
        public SpriteBatch spriteBatch;
        public SpriteFont font; // font 
        public Game1 g;
        public string creditTitle; // the header of the credit.
        public string credits; // the body of the credit section

        const int SCREENWIDTH = 1280;
        const int SCREENHEIGHT = 720;

        public Vector2 position;
        public Credits(Game game) : base(game)
        {
            g = game as Game1;
            this.spriteBatch = g.spriteBatch;

            position = new Vector2(Shared.stage.X / 2, Shared.stage.Y / 2);

            font = g.Content.Load<SpriteFont>("File");

            creditTitle = "Credits ";
            credits = "Game art is accredited to Kenney.nl \n" +
                "Sound effects is accredited to dklon\n" +
                "Background music is accredited to Trevor Lentz" ;
        }

        public override void Draw(GameTime gameTime)
        {
            //Draw message here
            spriteBatch.Begin();
            spriteBatch.DrawString(font, creditTitle, new Vector2((SCREENWIDTH/2)-100,position.Y - 100), Color.White);
            spriteBatch.DrawString(font, credits, new Vector2(position.X - 100,position.Y) , Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
