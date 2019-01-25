/**
  Description: The purpose of this class is to display the about scene from the menu.
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
    class AboutScene : GameScene
    {
        public SpriteBatch spriteBatch;
        public SpriteFont font;
        public Game1 g;
        public string aboutSection;

        const int SCREENWIDTH = 1280;
        const int SCREENHEIGHT = 720; // set the screeen dimensions

        public Vector2 position;//used for positioning on the screen

        public AboutScene(Game game, SpriteBatch spriteBatch) : base(game)
        {
            g = game as Game1;
            this.spriteBatch = spriteBatch;
            position = new Vector2(Shared.stage.X / 2, Shared.stage.Y / 2);
            font = g.Content.Load<SpriteFont>("File");
            aboutSection = "About Section " + "\n" + "Kevin Lucas";
        }
        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            //Draw message here
            spriteBatch.Begin();
            spriteBatch.DrawString(font, aboutSection, position, Color.White);
            spriteBatch.End();


            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            
            base.Update(gameTime);
        }
    }
}
