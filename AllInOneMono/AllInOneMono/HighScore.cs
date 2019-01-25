/***
 
 Description: The purpose of this class is to
              save and load the highscores of
 
 
 
 */ 

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KLFinalGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace AllInOneMono
{

    class HighScore : DrawableGameComponent
    {
        //holds the name and the highscore.
        public List<int> highScores;
        public List<string> name;

        public string fileName = @"highScoreLst";


        Boolean highScoreListNotFound;
        public int score;

        public HighScore(Game game) : base(game)
        {
            //creation of the list of highscores
            highScores = new List<int>(); 

           if(highScoreListNotFound)
            {
                highScores.Add(score);
            }
            else
            {
                if (highScores.Count > 10) ;
                {
                    highScores.Remove(0);
                }
            }

        }
        public void SaveHighScore()
        {
            using (StreamWriter writer = new StreamWriter(fileName,append:true))
            {           
                writer.WriteLine(score);           
                writer.Close();
            }
        }
        public void loadSavedScore()
        {
            try
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    while(!reader.EndOfStream)
                    {
                        //read the file of highscores
                        highScores.Add(int.Parse(reader.ReadLine()));
                    }
                    reader.Close();

                }
                highScoreListNotFound = false;
            }
            catch(FileNotFoundException ex)
            {
                highScoreListNotFound =  true;
            }           
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
