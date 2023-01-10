using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trapped_in_the_dark
{
    internal class Case1
    {
        private const int dimensionx = 5;
        private const int dimensiony = 12;
        private bool nord, est, ouest, sud;
        private int valeurcase;

        public Case1(bool nord, bool est, bool ouest, bool sud, int valeurcase)
        {
            this.Nord = nord;
            this.Est = est;
            this.Ouest = ouest;
            this.Sud = sud;
            this.Valeurcase = valeurcase;
        }

        public bool Nord
        {
            get
            {
                return this.nord;
            }
            set
            {
                this.nord = value;
            }
        }
        public bool Est
        {
            get
            {
                return this.est;
            }
            set
            {
                this.est = value;
            }
        }
        public bool Ouest
        {
            get
            {
                return this.ouest;
            }
            set
            {
                this.ouest = value;
            }
        }
        public bool Sud
        {
            get
            {
                return this.sud;
            }
            set
            {
                this.sud = value;
            }
        }
        public int Valeurcase
        {
            get
            {
                return this.valeurcase;
            }
            set
            {
                this.valeurcase = value;
            }
        }
        public override string ToString()
        {
            return base.ToString();
        }
        public override bool Equals(object obj)
        {
            return obj is Case1 @case1 &&
                   Valeurcase == @case1.Valeurcase;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Valeurcase);
        }
        public static int[,] GenerateurDuTileset()
        {
            int[,] tilesets = new int[4 * dimensionx + 1, 4 * dimensiony + 1];
            Case1[,] tileset = new Case1[dimensionx, dimensiony];
            int[,] result = new int[dimensionx, dimensiony];
            int compteur = 0;
            bool testfin = false;
            int compteurvaleur = 0;
            int pourcentagepiege = 15;
            Random rand = new Random();
            Random rand2 = new Random();
            Random direction = new Random();
            Random solaffichage = new Random();
            Random muraffichage = new Random();
            Random piege = new Random();
            // Initialise un tableau de Cases avec des valeurs différentes et tous leurs murs fermés
            for (int i = 0; i < dimensionx; i++)
            {
                for (int j = 0; j < dimensiony; j++)
                {
                    tileset[i, j] = new Case1(false, false, false, false, compteurvaleur);
                    compteurvaleur++;
                }
            }
            // On ouvre exactement longueur*largeur - 1 murs aléatoires
            while (compteur < dimensionx * dimensiony - 1)
            {
                int randnext = rand.Next(0, dimensionx - 1);
                int rand2next = rand2.Next(0, dimensiony - 1);
                int directionnext = direction.Next(1, 5);
                // Direction aléatoire Nord
                if (directionnext == 1)
                {
                    // On vérifie qu'il n'y a pas déjà un chemin entre les deux cases adjacentes
                    if (tileset[randnext, rand2next] != tileset[randnext, rand2next - 1])
                    {
                        // On change la valeur de toutes les cases qui ont la même valeur que la case au dessus de celle choisie aléatoirement
                        for (int k = 0; k < dimensionx; k++)
                        {
                            for (int l = 0; l < dimensiony; l++)
                            {
                                if (tileset[k, l] == tileset[randnext, rand2next - 1])
                                {
                                    tileset[k, l].Valeurcase = tileset[randnext, rand2next].Valeurcase;
                                }
                            }
                        }
                        // On ouvre les murs Nord de la case choisie aléatoirement et Sud de celle du dessus
                        tileset[randnext, rand2next].Nord = true;
                        tileset[randnext, rand2next - 1].Sud = true;
                        // On incrémente le compteur de murs ouverts de 1
                        compteur++;
                    }
                    if (tileset[0, rand2next] != tileset[0, rand2next - 1])
                    {
                        for (int k = 0; k < dimensionx; k++)
                        {
                            for (int l = 0; l < dimensiony; l++)
                            {
                                if (tileset[k, l] == tileset[0, rand2next - 1])
                                {
                                    tileset[k, l].Valeurcase = tileset[0, rand2next].Valeurcase;
                                }
                            }
                        }
                        tileset[0, rand2next].Nord = true;
                        tileset[0, rand2next - 1].Sud = true;
                        compteur++;
                    }
                    if (tileset[dimensionx, rand2next] != tileset[dimensionx, rand2next - 1])
                    {
                        for (int k = 0; k < dimensionx; k++)
                        {
                            for (int l = 0; l < dimensiony; l++)
                            {
                                if (tileset[k, l] == tileset[dimensionx, rand2next - 1])
                                {
                                    tileset[k, l].Valeurcase = tileset[dimensionx, rand2next].Valeurcase;
                                }
                            }
                        }
                        tileset[dimensionx, rand2next].Nord = true;
                        tileset[dimensionx, rand2next - 1].Sud = true;
                        compteur++;
                    }
                    if (tileset[randnext, dimensiony] != tileset[randnext, dimensiony - 1])
                    {
                        for (int k = 0; k < dimensionx; k++)
                        {
                            for (int l = 0; l < dimensiony; l++)
                            {
                                if (tileset[k, l] == tileset[randnext, dimensiony - 1])
                                {
                                    tileset[k, l].Valeurcase = tileset[randnext, dimensiony].Valeurcase;
                                }
                            }
                        }
                        tileset[randnext, dimensiony].Nord = true;
                        tileset[randnext, dimensiony - 1].Sud = true;
                        compteur++;
                    }
                }
                // Direction aléatoire Ouest
                else if (directionnext == 2)
                {
                    if (tileset[randnext, rand2next] != tileset[randnext + 1, rand2next])
                    {
                        for (int k = 0; k < dimensionx; k++)
                        {
                            for (int l = 0; l < dimensiony; l++)
                            {
                                if (tileset[k, l] == tileset[randnext + 1, rand2next])
                                {
                                    tileset[k, l].Valeurcase = tileset[randnext, rand2next].Valeurcase;
                                }
                            }
                        }
                        tileset[randnext, rand2next].Ouest = true;
                        tileset[randnext + 1, rand2next].Est = true;
                        compteur++;
                    }
                    if (tileset[randnext, 0] != tileset[randnext + 1, 0])
                    {
                        for (int k = 0; k < dimensionx; k++)
                        {
                            for (int l = 0; l < dimensiony; l++)
                            {
                                if (tileset[k, l] == tileset[randnext + 1, 0])
                                {
                                    tileset[k, l].Valeurcase = tileset[randnext, 0].Valeurcase;
                                }
                            }
                        }
                        tileset[randnext, 0].Ouest = true;
                        tileset[randnext + 1, 0].Est = true;
                        compteur++;
                    }
                    if (tileset[randnext, dimensiony - 1] != tileset[randnext + 1, dimensiony - 1])
                    {
                        for (int k = 0; k < dimensionx; k++)
                        {
                            for (int l = 0; l < dimensiony; l++)
                            {
                                if (tileset[k, l] == tileset[randnext + 1, dimensiony - 1])
                                {
                                    tileset[k, l].Valeurcase = tileset[randnext, dimensiony - 1].Valeurcase;
                                }
                            }
                        }
                        tileset[randnext, dimensiony - 1].Ouest = true;
                        tileset[randnext + 1, dimensiony - 1].Est = true;
                        compteur++;
                    }
                    if (tileset[dimensionx - 1, rand2next] != tileset[dimensionx, rand2next])
                    {
                        for (int k = 0; k < dimensionx; k++)
                        {
                            for (int l = 0; l < dimensiony; l++)
                            {
                                if (tileset[k, l] == tileset[dimensionx, rand2next])
                                {
                                    tileset[k, l].Valeurcase = tileset[dimensionx - 1, rand2next].Valeurcase;
                                }
                            }
                        }
                        tileset[dimensionx - 1, rand2next].Ouest = true;
                        tileset[dimensionx, rand2next].Est = true;
                        compteur++;
                    }
                }
                // Direction aléatoire Sud
                else if (directionnext == 3)
                {
                    if (tileset[randnext, rand2next] != tileset[randnext, rand2next + 1])
                    {
                        for (int k = 0; k < dimensionx; k++)
                        {
                            for (int l = 0; l < dimensiony; l++)
                            {
                                if (tileset[k, l] == tileset[randnext, rand2next + 1])
                                {
                                    tileset[k, l].Valeurcase = tileset[randnext, rand2next].Valeurcase;
                                }
                            }
                        }
                        tileset[randnext, rand2next].Sud = true;
                        tileset[randnext, rand2next + 1].Nord = true;
                        compteur++;
                    }
                    if (tileset[0, rand2next] != tileset[0, rand2next + 1])
                    {
                        for (int k = 0; k < dimensionx; k++)
                        {
                            for (int l = 0; l < dimensiony; l++)
                            {
                                if (tileset[k, l] == tileset[0, rand2next + 1])
                                {
                                    tileset[k, l].Valeurcase = tileset[0, rand2next].Valeurcase;
                                }
                            }
                        }
                        tileset[0, rand2next].Sud = true;
                        tileset[0, rand2next + 1].Nord = true;
                        compteur++;
                    }
                    if (tileset[randnext, 0] != tileset[randnext, 0 + 1])
                    {
                        for (int k = 0; k < dimensionx; k++)
                        {
                            for (int l = 0; l < dimensiony; l++)
                            {
                                if (tileset[k, l] == tileset[randnext, 0 + 1])
                                {
                                    tileset[k, l].Valeurcase = tileset[randnext, 0].Valeurcase;
                                }
                            }
                        }
                        tileset[randnext, 0].Sud = true;
                        tileset[randnext, 0 + 1].Nord = true;
                        compteur++;
                    }
                    if (tileset[dimensionx, rand2next] != tileset[dimensionx, rand2next + 1])
                    {
                        for (int k = 0; k < dimensionx; k++)
                        {
                            for (int l = 0; l < dimensiony; l++)
                            {
                                if (tileset[k, l] == tileset[dimensionx, rand2next + 1])
                                {
                                    tileset[k, l].Valeurcase = tileset[dimensionx, rand2next].Valeurcase;
                                }
                            }
                        }
                        tileset[dimensionx, rand2next].Sud = true;
                        tileset[dimensionx, rand2next + 1].Nord = true;
                        compteur++;
                    }
                }
                // Direction aléatoire Est
                else if (directionnext == 4)
                {
                    if (tileset[randnext, rand2next] != tileset[randnext - 1, rand2next])
                    {
                        for (int k = 0; k < dimensionx; k++)
                        {
                            for (int l = 0; l < dimensiony; l++)
                            {
                                if (tileset[k, l] == tileset[randnext - 1, rand2next])
                                {
                                    tileset[k, l].Valeurcase = tileset[randnext, rand2next].Valeurcase;
                                }
                            }
                        }
                        tileset[randnext, rand2next].Est = true;
                        tileset[randnext - 1, rand2next].Ouest = true;
                        compteur++;
                    }
                    if (tileset[dimensionx, rand2next] != tileset[dimensionx - 1, rand2next])
                    {
                        for (int k = 0; k < dimensionx; k++)
                        {
                            for (int l = 0; l < dimensiony; l++)
                            {
                                if (tileset[k, l] == tileset[dimensionx - 1, rand2next])
                                {
                                    tileset[k, l].Valeurcase = tileset[dimensionx, rand2next].Valeurcase;
                                }
                            }
                        }
                        tileset[dimensionx, rand2next].Est = true;
                        tileset[dimensionx - 1, rand2next].Ouest = true;
                        compteur++;
                    }
                    if (tileset[randnext, 0] != tileset[randnext - 1, 0])
                    {
                        for (int k = 0; k < dimensionx; k++)
                        {
                            for (int l = 0; l < dimensiony; l++)
                            {
                                if (tileset[k, l] == tileset[randnext - 1, 0])
                                {
                                    tileset[k, l].Valeurcase = tileset[randnext, 0].Valeurcase;
                                }
                            }
                        }
                        tileset[randnext, 0].Est = true;
                        tileset[randnext - 1, 0].Ouest = true;
                        compteur++;
                    }
                    if (tileset[randnext, dimensiony] != tileset[randnext - 1, dimensiony])
                    {
                        for (int k = 0; k < dimensionx; k++)
                        {
                            for (int l = 0; l < dimensiony; l++)
                            {
                                if (tileset[k, l] == tileset[randnext - 1, dimensiony])
                                {
                                    tileset[k, l].Valeurcase = tileset[randnext, dimensiony].Valeurcase;
                                }
                            }
                        }
                        tileset[randnext, dimensiony].Est = true;
                        tileset[randnext - 1, dimensiony].Ouest = true;
                        compteur++;
                    }
                }
                testfin = true;
                for (int longueur = 1; longueur < dimensionx; longueur++)
                {
                    for (int largeur = 1; largeur < dimensiony; largeur++)
                    {
                        if (tileset[longueur, largeur] != tileset[0, 0])
                            testfin = false;
                    }
                }
            }
            for (int k = 0; k < dimensionx; k++)
            {
                for (int l = 0; l < dimensiony; l++)
                {
                    result[k, l] = tileset[k, l].Valeurcase;
                }
            }

            // On initialise toutes les cases du tileset à 0
            for (int i = 0; i < 4 * dimensionx; i++)
            {
                for (int j = 0; j < 4 * dimensiony; j++)
                {
                    if (piege.Next(0, pourcentagepiege) != 1)
                        tilesets[i, j] = solaffichage.Next(1, 6);
                    else
                        tilesets[i, j] = 13;
                }

            }
            // On change les murs externes en 1
            for (int k = 0; k < 4 * dimensionx; k++)
            {
                for (int l = 0; l < 4 * dimensiony; l++)
                {
                    tilesets[k, 0] = muraffichage.Next(7, 12);
                    tilesets[0, l] = muraffichage.Next(7, 12);
                    tilesets[4 * dimensionx, l] = muraffichage.Next(7, 12);
                    tilesets[k, 4 * dimensiony] = muraffichage.Next(7, 12);
                    tilesets[4 * dimensionx, 4 * dimensiony] = muraffichage.Next(7, 12);
                }
            }
            // On change les murs fermés en 1 selon la logique: une case = 3*3 tiles et entre deux cases un mur de 1*4 (en comptant les coins)
            for (int i = 1; i < dimensionx + 1; i++)
            {
                for (int j = 1; j < dimensiony + 1; j++)
                {
                    //Pour l'Est
                    if (tileset[i - 1, j - 1].Est == false)
                    {
                        tilesets[i * 4, j * 4] = muraffichage.Next(7, 12);
                        tilesets[i * 4 - 1, j * 4] = muraffichage.Next(7, 12);
                        tilesets[i * 4 - 2, j * 4] = muraffichage.Next(7, 12);
                        tilesets[i * 4 - 3, j * 4] = muraffichage.Next(7, 12);
                    }
                    //Pour le Sud
                    if (tileset[i - 1, j - 1].Sud == false)
                    {
                        tilesets[i * 4, j * 4] = muraffichage.Next(7, 12);
                        tilesets[i * 4, j * 4 - 1] = muraffichage.Next(7, 12);
                        tilesets[i * 4, j * 4 - 2] = muraffichage.Next(7, 12);
                        tilesets[i * 4, j * 4 - 3] = muraffichage.Next(7, 12);
                    }
                    //Pour le Nord
                    if (tileset[i - 1, j - 1].Nord == false)
                    {
                        tilesets[(i - 1) * 4, j * 4] = muraffichage.Next(7, 12);
                        tilesets[(i - 1) * 4, j * 4 - 1] = muraffichage.Next(7, 12);
                        tilesets[(i - 1) * 4, j * 4 - 2] = muraffichage.Next(7, 12);
                        tilesets[(i - 1) * 4, j * 4 - 3] = muraffichage.Next(7, 12);
                    }
                    //Pourl'Ouest
                    if (tileset[i - 1, j - 1].Ouest == false)
                    {
                        tilesets[i * 4, (j - 1) * 4] = muraffichage.Next(7, 12);
                        tilesets[i * 4 - 1, (j - 1) * 4] = muraffichage.Next(7, 12);
                        tilesets[i * 4 - 2, (j - 1) * 4] = muraffichage.Next(7, 12);
                        tilesets[i * 4 - 3, (j - 1) * 4] = muraffichage.Next(7, 12);
                    }
                }
            }
            return tilesets;
        }

        //Méthode qui affiche le tileset retourné pour vérifier son fonctionnement
        public static void AfficheTileset(int[,] tileset)
        {
            for (int i = 0; i < tileset.GetLength(0); i++)
            {
                for (int j = 0; j < tileset.GetLength(1); j++)
                    Console.Write(tileset[i, j]);
                Console.WriteLine("\n");
            }
        }
    }
}