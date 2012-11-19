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
using Microsoft.Xna.Framework.Storage;
# endregion

namespace DragonTears
{
    class GameManager
    {
        public enum etat{
            Lancement,
            Menu,
            InGame,
            Pause,
            Credits
        };
        public etat Etat;
        bool pauseactive;

        public GameManager()
        {
            Etat = etat.Lancement;
        }

        public void Update(KeyboardState clavier, Menu menu)
        {
            if (clavier.IsKeyDown(Keys.Escape))
                pauseactive = true;

            if (clavier.IsKeyUp(Keys.Escape) && pauseactive)
            {
                if (Etat == etat.InGame)
                {
                    Etat = etat.Pause;
                    menu.mode = Menu.Mode.Pause;
                }

                pauseactive = false;
            }
        }
    }
}
