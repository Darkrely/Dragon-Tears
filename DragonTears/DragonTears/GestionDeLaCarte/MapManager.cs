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
    class MapManager
    {
        #region Declaration

        #region Declaration des textures
        Texture2D textureeau;
        Texture2D textureherbe;
        Texture2D texturesolbois;
        Texture2D texturemurbois;
        Texture2D texturesable;
        Texture2D texturesolpierre;
        Texture2D texturemurpierre;
        Texture2D textureeausable;
        Texture2D texturesableeau;
        Texture2D coltext;
        #endregion

        #region Declaration des limites
            bool limitehaute;
            bool limitebasse;
            bool limitegauche;
            bool limitedroite;
        #endregion

        #region Declaration de la liste contenant la carte
        List<List<char>> map;
        List<char> ligne;
        #endregion

        #region Initialisation d'une case
        Case casee;
        #endregion

        #region Declaration des coordonnees
        int xmap;
        int ymap;
        int xmax;
        int ymax;
        #endregion

        #region Blocage
        public List<List<Rectangle>> collision;
        public List<Rectangle> collisionligne;
        bool affichecol;
        bool touchecol;
        #endregion

        #endregion

        public MapManager()
        {
            #region Initialisation de la map sans decalage
            xmap = 0;
            ymap = 0;
            xmax = 0;
            ymax = 0;
            limitehaute = true;
            limitegauche = true;
            limitedroite = false;
            limitebasse = false;
            map = new List<List<char>>();
            ligne = new List<char>();
            collision = new List<List<Rectangle>>();
            collisionligne = new List<Rectangle>();
            affichecol = false;
            touchecol = false;
            #endregion
        }

        public MapManager(int x, int y)
        {
            #region Initialisation de la map avec un decalage
            xmap = -x;
            ymap = -y;
            xmax = 0;
            ymax = 0;
            map = new List<List<char>>();
            ligne = new List<char>();
            collision = new List<List<Rectangle>>();
            collisionligne = new List<Rectangle>();
            affichecol = false;
            touchecol = false;
            #endregion
        }

        public void LoadContent(ContentManager content)
        {
            #region Chargement du décor
            textureeau = content.Load<Texture2D>("Décors\\Mursetsols\\eau");
            textureherbe = content.Load<Texture2D>("Décors\\Mursetsols\\herbe");
            texturesolbois = content.Load<Texture2D>("Décors\\Mursetsols\\solbois");
            texturemurbois = content.Load<Texture2D>("Décors\\Mursetsols\\murbois");
            texturesable = content.Load<Texture2D>("Décors\\Mursetsols\\sable");
            texturesolpierre = content.Load<Texture2D>("Décors\\Mursetsols\\solpierre");
            texturemurpierre = content.Load<Texture2D>("Décors\\Mursetsols\\murpierre");
            textureeausable = content.Load<Texture2D>("Décors\\Mursetsols\\eausable");
            texturesableeau = content.Load<Texture2D>("Décors\\Mursetsols\\sableeau");
            coltext = content.Load<Texture2D>("collision");
            #endregion
        }

        public void Position(int x, int y)
        {
            xmap = -x;
            ymap = -y;
        }

        public void Update(KeyboardState clavier, GameWindow window, PersonnageJouable perso)
        {
            #region miseajourdonnees
            ///////////////////Joueur//////////////////////////////////////
            Rectangle positionjoueur = perso._rectangle;
            ///////////////////Taille fenetre//////////////////////////////
            int largeur = window.ClientBounds.Width;
            int hauteur = window.ClientBounds.Height;
            ///////////////////Touche clavier//////////////////////////////
            bool toucheup = clavier.IsKeyDown(Keys.Up) && !(clavier.IsKeyDown(Keys.Down) && clavier.IsKeyDown(Keys.Left) && clavier.IsKeyDown(Keys.Right));
            bool touchedown = clavier.IsKeyDown(Keys.Down) && !(clavier.IsKeyDown(Keys.Up) && clavier.IsKeyDown(Keys.Left) && clavier.IsKeyDown(Keys.Right));
            bool toucheleft = clavier.IsKeyDown(Keys.Left) && !(clavier.IsKeyDown(Keys.Down) && clavier.IsKeyDown(Keys.Up) && clavier.IsKeyDown(Keys.Right));
            bool toucheright = clavier.IsKeyDown(Keys.Right) && !(clavier.IsKeyDown(Keys.Down) && clavier.IsKeyDown(Keys.Left) && clavier.IsKeyDown(Keys.Up));
            ///////////////////////////////////////////////////////////////
            #endregion

            #region Haut
                if (toucheup)
                {
                    if (ymap < 0 && !perso.dplcmtbas && !perso.blochaut)
                    {
                        ymap++;
                        RectangleMaj(0, 1);
                        limitehaute = false;
                    }
                }

                if (ymap >= 0)
                {
                    limitehaute = true;
                }
                #endregion

            #region Bas
                if (-ymap >= -hauteur + ymax || (ymax < hauteur))
                {
                    limitebasse = true;
                }

                if (touchedown)
                {
                    if (-hauteur + ymax > -ymap && !(ymax < hauteur) && !perso.dplcmthaut && !perso.blocbas)
                    {
                        ymap--;
                        RectangleMaj(0, -1);
                        limitebasse = false;
                    }
                }

                #endregion
               
            #region Gauche
                if (toucheleft)
                {
                    if (xmap < 0 && !perso.dplcmtdroite && !perso.blocgauche)
                    {
                        xmap++;
                        RectangleMaj(1, 0);
                        limitegauche = false;
                    }
                }

                if (xmap >= 0)
                {
                    limitegauche = true;
                }
                #endregion
                
            #region Droite
                if (toucheright)
                {
                    if (xmap > largeur - xmax + 40 && !(xmax < largeur) && !perso.dplcmtgauche && !perso.blocdroit)
                    {
                        xmap--;
                        RectangleMaj(-1, 0);
                        limitedroite = false;
                    }
                }

                if (xmap <= largeur - xmax + 40 || xmax < largeur)
                {
                    limitedroite = true;
                }
                #endregion

            #region affichage collision
            if (clavier.IsKeyDown(Keys.F1))
            {
                touchecol = true;
            }

            if(clavier.IsKeyUp(Keys.F1) && touchecol)
            {
                touchecol = false;
                affichecol = !affichecol;
            }
            #endregion

            perso.Deplacement(limitehaute, limitebasse, limitegauche, limitedroite, clavier, window);
        }

        public void ChargementMap(string adress)
        {
            #region Initialisation des limites de la carte
            xmax = 0;
            ymax = 0;
            #endregion

            #region Initialisation de la liste
            map = new List<List<char>>() { };
            ligne = new List<char>() { };
            collision = new List<List<Rectangle>>() { };
            collisionligne = new List<Rectangle>() { };
            #endregion

            #region Lecture des données de carte
            try
            {
                StreamReader monStreamReader = new StreamReader("Map\\" + adress);
                string line = monStreamReader.ReadLine();

                while (line != null)
                {
                    ymax++;

                    if (xmax < line.Length)
                    {
                        xmax = line.Length;
                    }

                    ligne = new List<char>() { };
                    collisionligne = new List<Rectangle>() { };

                    for (int i = 0; i < line.Length + 20; i++)
                    {
                        if (i < line.Length)
                        {
                            ligne.Add(line[i]);
                            #region Filtrage de la texture
                            if (line[i] == '~')
                            {
                                collisionligne.Add(new Rectangle(40 * i + xmap, 40 * (ymax - 1) + ymap, 40, 40));
                            }

                            else if (line[i] == '_')
                            {

                            }

                            else if (line[i] == 'p')
                            {

                            }

                            else if (line[i] == '.')
                            {

                            }

                            else if (line[i] == '_')
                            {

                            }

                            else if (line[i] == '#')
                            {

                            }

                            else if (line[i] == '=')
                            {
                                collisionligne.Add(new Rectangle(40 * i + xmap, 40 * (ymax - 1) + ymap, 40, 40));
                            }

                            else if (line[i] == '-')
                            {
                                collisionligne.Add(new Rectangle(40 * i + xmap, 40 * (ymax - 1) + ymap, 40, 40));
                            }

                            else if (line[i] == '/')
                            {
                                collisionligne.Add(new Rectangle(40 * i + 20 + xmap, 40 * (ymax - 1) + 20 + ymap, 20, 20));
                            }

                            else if (line[i] == '\\')
                            {
                                collisionligne.Add(new Rectangle(40 * i + xmap, 40 * (ymax - 1) + 20 + ymap, 20, 20));
                            }

                            else
                            {
                                collisionligne.Add(new Rectangle(40 * i + xmap, 40 * (ymax - 1) + ymap, 40, 40));
                            }
                        }
                        else
                        {
                            collisionligne.Add(new Rectangle(40 * i + xmap, 40 * (ymax - 1) + ymap, 40, 40));
                        }
                        #endregion
                    }

                    map.Add(ligne);
                    collision.Add(collisionligne);
                    line = monStreamReader.ReadLine();
                }
                monStreamReader.Close();

                xmax *= 40;
                ymax *= 40;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            #endregion
        }

        public void RectangleMaj(int x, int y)
        {
            for (int i = 0; i < collision.Count; i++)
            {
                for (int j = 0; j < collision[i].Count; j++)
                {
                    collision[i][j] = new Rectangle(collision[i][j].X + x, collision[i][j].Y + y, collision[i][j].Width, collision[i][j].Height);
                }
            }
        }

        public void AffichageMap(SpriteBatch spriteBatch)
        {
            #region Lecture de la liste representant la carte
            for (int i = 0; i < map.Count; i++)
            {
                for (int j = 0; j < map[i].Count; j++)
                {
                    #region Affichage des textures

                    casee = new Case(j * 40 + xmap, i * 40 + ymap, map[i][j]);

                    #region Filtrage de la texture
                    if (casee.text == Case.Typetext.Eau)
                    {
                        spriteBatch.Draw(textureeau, casee.rectangle, Color.White);
                    }

                    if (casee.text == Case.Typetext.Herbe)
                    {
                        spriteBatch.Draw(textureherbe, casee.rectangle, Color.White);
                    }

                    if (casee.text == Case.Typetext.Murbois)
                    {
                        spriteBatch.Draw(texturemurbois, casee.rectangle, Color.White);
                    }

                    if (casee.text == Case.Typetext.Murpierre)
                    {
                        spriteBatch.Draw(texturemurpierre, casee.rectangle, Color.White);
                    }

                    if (casee.text == Case.Typetext.Sable)
                    {
                        spriteBatch.Draw(texturesable, casee.rectangle, Color.White);
                    }

                    if (casee.text == Case.Typetext.SableEau)
                    {
                        spriteBatch.Draw(texturesableeau, casee.rectangle, Color.White);
                    }

                    if (casee.text == Case.Typetext.EauSable)
                    {
                        spriteBatch.Draw(textureeausable, casee.rectangle, Color.White);
                    }

                    if (casee.text == Case.Typetext.Solbois)
                    {
                        spriteBatch.Draw(texturesolbois, casee.rectangle, Color.White);
                    }

                    if (casee.text == Case.Typetext.Solpierre)
                    {
                        spriteBatch.Draw(texturesolpierre, casee.rectangle, Color.White);
                    }
                    #endregion

                    #endregion
                }
            }
            #endregion

            if (affichecol)
            {
                foreach (List<Rectangle> rectligne in collision)
                {
                    foreach (Rectangle rect in rectligne)
                    {
                        spriteBatch.Draw(coltext, rect, Color.White);
                    }
                }
            }
        }
    }
}
