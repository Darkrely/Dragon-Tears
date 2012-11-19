#region Using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;
# endregion

namespace DragonTears
{
    class CollisionManager
    {
        

        public CollisionManager()
        {

        }

        public void CollisionPersoDeplacement(MapManager mapManager, PersonnageJouable perso)
        {
            perso.blochaut = false;
            perso.blocbas = false;
            perso.blocgauche = false;
            perso.blocdroit = false;

            foreach(List<Rectangle> rectangleligne in mapManager.collision)
            {
                foreach (Rectangle rectangle in rectangleligne)
                {
                    if(rectangle.Intersects(perso.collisionhaut))
                    {
                        perso.blochaut = true;
                    }
                    if (rectangle.Intersects(perso.collisionbas))
                    {
                        perso.blocbas = true;
                    }
                    if (rectangle.Intersects(perso.collisiongauche))
                    {
                        perso.blocgauche = true;
                    }
                    if (rectangle.Intersects(perso.collisiondroite))
                    {
                        perso.blocdroit = true;
                    }
                }
            }
        }
    }
}
