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
    class Menu
    {
        #region Declaration
        Texture2D fond, selecteur;
        int hauteur, largeur, choix;
        public enum Langue
        {
            Francais,
            Anglais
        };
        Langue langue;
        public enum Mode
        {
            Menu,
            Option,
            ChoixSexe,
            Solo,
            Multi,
            Pause
        };
        public Mode mode;
        public enum Son
        {
            On, 
            Off
        };
        public Son sound;
        bool clavierhaut, clavierbas, clavierentrer, changement;
        string solo, multi, option, reprendre, quitter, son, sonetat, language, nomlangue, plangue, pson, nouvjeu, charge, homme, fille, questionsexe;
        SpriteFont font;
        Rectangle rectselecteur;
        #endregion

        public Menu(GameWindow window)
        {
            hauteur = window.ClientBounds.Height;
            largeur = window.ClientBounds.Width;

            #region Booleen clavier
            clavierhaut = false;
            clavierbas = false;
            clavierentrer = false;
            changement = false;
            #endregion

            choix = 1;
            rectselecteur = new Rectangle(10, 60, 40, 30);
            langue = Langue.Francais;
            mode = Mode.Menu;
            sound = Son.On;
            
        }

        public void LoadContent(ContentManager content)
        {
            fond = content.Load<Texture2D>("fond");
            selecteur = content.Load<Texture2D>("selecteur");
            font = content.Load<SpriteFont>("SpriteFont1");
        }

        public void Update(KeyboardState clavier, GameManager gameManager, Game1 game1, MapManager mapManager, GestionJeu jeu)
        {
            #region langue
            if (langue == Langue.Francais)
            {
                solo = "Solo";
                multi = "Multijoueur";
                option = "Options";
                quitter = "Quitter";
                son = "Son: ";
                language = "Langue: ";
                nomlangue = "Francais";
                nouvjeu = "Nouvelle partie";
                charge = "Charger partie"; 
                homme = "Homme"; 
                fille = "Femme"; 
                questionsexe = "Que voulez-vous être?";
                reprendre = "Reprendre";
            }
            else
            {
                solo = "Solo";
                multi = "Multiplayer";
                option = "Settings";
                quitter = "Exit";
                son = "Sound: ";
                language = "Language: ";
                nomlangue = "English";
                nouvjeu = "New game";
                charge = "Load game";
                homme = "Male";
                fille = "Female";
                questionsexe = "What do you want to be?";
                reprendre = "Resume";
            }
            plangue = language + nomlangue;
            #endregion

            #region Son
            if (sound == Son.On)
                sonetat = "On";
            else
                sonetat = "Off";
            pson = son + sonetat;
            #endregion

            #region menu
            if (mode == Mode.Menu)
            {
                #region positionchoix
                if (!clavierhaut)
                {
                    if (clavier.IsKeyDown(Keys.Up) && !clavier.IsKeyDown(Keys.Down))
                    {
                        clavierhaut = true;
                    }
                }

                if (clavier.IsKeyUp(Keys.Up) && !clavier.IsKeyDown(Keys.Down) && clavierhaut)
                {
                    choix--;
                    clavierhaut = false;
                    if (choix <= 0)
                        choix = 4;
                }

                if (!clavierbas)
                {
                    if (!clavier.IsKeyDown(Keys.Up) && clavier.IsKeyDown(Keys.Down))
                    {
                        clavierbas = true;
                    }
                }

                if (!clavier.IsKeyDown(Keys.Up) && clavier.IsKeyUp(Keys.Down) && clavierbas)
                {
                    choix++;
                    clavierbas = false;
                    if (choix >= 5)
                        choix = 1;
                }
                rectselecteur = new Rectangle(10, 20 + choix * 40, 40, 30);
                #endregion

                #region validation
                if (clavier.IsKeyDown(Keys.Enter))
                {
                    clavierentrer = true;
                }
                if (clavierentrer && clavier.IsKeyUp(Keys.Enter))
                {
                    if (choix == 1)
                    {
                        changement = true;
                        rectselecteur = new Rectangle(10, 60, 40, 30);
                        choix = 1;
                        mode = Mode.Solo;
                    }
                    if (choix == 3)
                    {
                        choix = 1;
                        rectselecteur = new Rectangle(10, 60, 40, 30);
                        changement = true;
                        mode = Mode.Option;
                    }
                    if (choix == 4)
                        game1.Exit();
                }
                #endregion
            }
            #endregion

            #region option
            if (mode == Mode.Option)
            {
                #region positionchoix
                if (!clavierhaut)
                {
                    if (clavier.IsKeyDown(Keys.Up) && !clavier.IsKeyDown(Keys.Down))
                    {
                        clavierhaut = true;
                    }
                }

                if (clavier.IsKeyUp(Keys.Up) && !clavier.IsKeyDown(Keys.Down) && clavierhaut)
                {
                    choix--;
                    clavierhaut = false;
                    if (choix <= 0)
                        choix = 2;
                }

                if (!clavierbas)
                {
                    if (!clavier.IsKeyDown(Keys.Up) && clavier.IsKeyDown(Keys.Down))
                    {
                        clavierbas = true;
                    }
                }

                if (!clavier.IsKeyDown(Keys.Up) && clavier.IsKeyUp(Keys.Down) && clavierbas)
                {
                    choix++;
                    clavierbas = false;
                    if (choix >= 3)
                        choix = 1;
                }
                rectselecteur = new Rectangle(10, 20 + choix * 40, 40, 30);
                #endregion

                #region Validation
                if (mode == Mode.Option)
                {
                    if (clavier.IsKeyDown(Keys.Enter))
                    {
                        clavierentrer = true;
                    }

                    if (clavier.IsKeyUp(Keys.Enter) && clavierentrer)
                    {
                        if (choix == 1 && !changement)
                        {
                            if (sound == Son.On)
                                sound = Son.Off;
                            else
                                sound = Son.On;
                        }

                        if (choix == 2)
                        {
                            if (langue == Langue.Francais)
                                langue = Langue.Anglais;
                            else
                                langue = Langue.Francais;
                        }
                        clavierentrer = false;
                    }
                }
                #endregion

                if (clavier.IsKeyDown(Keys.Escape))
                {
                    if (gameManager.Etat == GameManager.etat.Menu)
                    {
                        mode = Mode.Menu;
                    }
                    if (gameManager.Etat == GameManager.etat.Pause)
                    {
                        mode = Mode.Pause;
                    }
                    rectselecteur = new Rectangle(10, 60, 40, 30);
                    choix = 1;
                }

                if (clavier.IsKeyUp(Keys.Enter))
                    changement = false;
            }
            #endregion

            #region Solo
            if (mode == Mode.Solo)
            {
                #region positionchoix
                if (!clavierhaut)
                {
                    if (clavier.IsKeyDown(Keys.Up) && !clavier.IsKeyDown(Keys.Down))
                    {
                        clavierhaut = true;
                    }
                }

                if (clavier.IsKeyUp(Keys.Up) && !clavier.IsKeyDown(Keys.Down) && clavierhaut)
                {
                    choix--;
                    clavierhaut = false;
                    if (choix <= 0)
                        choix = 2;
                }

                if (!clavierbas)
                {
                    if (!clavier.IsKeyDown(Keys.Up) && clavier.IsKeyDown(Keys.Down))
                    {
                        clavierbas = true;
                    }
                }

                if (!clavier.IsKeyDown(Keys.Up) && clavier.IsKeyUp(Keys.Down) && clavierbas)
                {
                    choix++;
                    clavierbas = false;
                    if (choix >= 3)
                        choix = 1;
                }
                rectselecteur = new Rectangle(10, 20 + choix * 40, 40, 30);
                #endregion

                #region Validation
                if (mode == Mode.Solo)
                {
                    if (clavier.IsKeyDown(Keys.Enter))
                    {
                        clavierentrer = true;
                    }

                    if (clavier.IsKeyUp(Keys.Enter) && clavierentrer)
                    {
                        if (choix == 1 && !changement)
                        {
                            jeu.NouveauJeu(mapManager);
                            gameManager.Etat = GameManager.etat.InGame;
                        }

                        if (choix == 2)
                        {
                            
                        }
                        clavierentrer = false;
                    }
                }
                #endregion

                if (clavier.IsKeyDown(Keys.Escape))
                {
                    mode = Mode.Menu;
                    rectselecteur = new Rectangle(10, 60, 40, 30);
                    choix = 1;
                }

                if (clavier.IsKeyUp(Keys.Enter))
                    changement = false;
            }
            #endregion

            #region Choix Sexe

            #endregion

            #region Pause
            if (mode == Mode.Pause)
            {
                #region positionchoix
                if (!clavierhaut)
                {
                    if (clavier.IsKeyDown(Keys.Up) && !clavier.IsKeyDown(Keys.Down))
                    {
                        clavierhaut = true;
                    }
                }

                if (clavier.IsKeyUp(Keys.Up) && !clavier.IsKeyDown(Keys.Down) && clavierhaut)
                {
                    choix--;
                    clavierhaut = false;
                    if (choix <= 0)
                        choix = 3;
                }

                if (!clavierbas)
                {
                    if (!clavier.IsKeyDown(Keys.Up) && clavier.IsKeyDown(Keys.Down))
                    {
                        clavierbas = true;
                    }
                }

                if (!clavier.IsKeyDown(Keys.Up) && clavier.IsKeyUp(Keys.Down) && clavierbas)
                {
                    choix++;
                    clavierbas = false;
                    if (choix >= 4)
                        choix = 1;
                }
                rectselecteur = new Rectangle(10, 20 + choix * 40, 40, 30);
                #endregion

                #region validation
                if (clavier.IsKeyDown(Keys.Enter))
                {
                    clavierentrer = true;
                }
                if (clavierentrer && clavier.IsKeyUp(Keys.Enter))
                {
                    if (choix == 1)
                    {
                        gameManager.Etat = GameManager.etat.InGame;
                        mode = Mode.Pause;
                    }
                    if (choix == 2)
                    {
                        choix = 1;
                        rectselecteur = new Rectangle(10, 60, 40, 30);
                        changement = true;
                        mode = Mode.Option;
                    }
                    if (choix == 3)
                        game1.Exit();

                    clavierentrer = false;
                }
                #endregion
            }
            #endregion
        }

        public void Draw(SpriteBatch spriteBatch)
        { 
            spriteBatch.Draw(fond, new Rectangle(0,0, largeur, hauteur), Color.White);

            #region Menu
            if (mode == Mode.Menu)
            {
                spriteBatch.DrawString(font, solo, new Vector2(60, 60), Color.Silver);
                spriteBatch.DrawString(font, multi, new Vector2(60, 100), Color.Silver);
                spriteBatch.DrawString(font, option, new Vector2(60, 140), Color.Silver);
                spriteBatch.DrawString(font, quitter, new Vector2(60, 180), Color.Silver);
            }
            #endregion

            #region Option
            if (mode == Mode.Option)
            {
                spriteBatch.DrawString(font, pson, new Vector2(60, 60), Color.Silver);
                spriteBatch.DrawString(font, plangue, new Vector2(60, 100), Color.Silver);
            }
            #endregion

            #region Solo
            if (mode == Mode.Solo)
            {
                spriteBatch.DrawString(font, nouvjeu, new Vector2(60, 60), Color.Silver);
                spriteBatch.DrawString(font, charge, new Vector2(60, 100), Color.Silver);
            }
            #endregion

            #region Pause
            if (mode == Mode.Pause)
            {
                spriteBatch.DrawString(font, reprendre, new Vector2(60, 60), Color.Silver);
                spriteBatch.DrawString(font, option, new Vector2(60, 100), Color.Silver);
                spriteBatch.DrawString(font, quitter, new Vector2(60, 140), Color.Silver);
            }
            #endregion

            spriteBatch.Draw(selecteur, rectselecteur, Color.White);
        }
    }
}