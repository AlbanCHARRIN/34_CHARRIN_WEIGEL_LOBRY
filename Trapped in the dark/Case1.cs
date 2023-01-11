using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trapped_in_the_dark
{
    internal class Case1
    {
        private const int dimensionX = 5;
        private const int dimensionY = 12;
        private bool nord, est, ouest, sud;
        private int valeurCase;

        public Case1(bool nord, bool est, bool ouest, bool sud, int valeurcase)
        {
            this.Nord = nord;
            this.Est = est;
            this.Ouest = ouest;
            this.Sud = sud;
            this.ValeurCase = valeurcase;
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
        public int ValeurCase
        {
            get
            {
                return this.valeurCase;
            }
            set
            {
                this.valeurCase = value;
            }
        }
        public override string ToString()
        {
            return base.ToString();
        }
        public override bool Equals(object obj)
        {
            return obj is Case1 @case1 &&
                   ValeurCase == @case1.ValeurCase;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(ValeurCase);
        }
        public static int[,] GenerateurDuTileset()
        {
            int[,] tilesets = new int[4 * dimensionX + 1, 4 * dimensionY + 1];
            Case1[,] tileset = new Case1[dimensionX, dimensionY];
            int[,] result = new int[dimensionX, dimensionY];
            int compteur = 0;
            bool testFin = false;
            int compteurValeur = 0;
            int pourcentagePiege = 15;
            Random rand = new Random();
            Random rand2 = new Random();
            Random direction = new Random();
            Random solAffichage = new Random();
            Random murAffichage = new Random();
            Random piege = new Random();
            // Initialise un tableau de Cases avec des valeurs différentes et tous leurs murs fermés
            for (int i = 0; i < dimensionX; i++)
            {
                for (int j = 0; j < dimensionY; j++)
                {
                    tileset[i, j] = new Case1(false, false, false, false, compteurValeur);
                    compteurValeur++;
                }
            }
            // On ouvre exactement longueur*largeur - 1 murs aléatoires
            while (compteur < dimensionX * dimensionY - 1)
            {
                int randNext = rand.Next(0, dimensionX - 1);
                int rand2Next = rand2.Next(0, dimensionY - 1);
                int directionNext = direction.Next(1, 5);
                // Direction aléatoire Nord
                if (directionNext == 1)
                {
                    // On vérifie qu'il n'y a pas déjà un chemin entre les deux cases adjacentes
                    if (tileset[randNext, rand2Next] != tileset[randNext, rand2Next - 1])
                    {
                        // On change la valeur de toutes les cases qui ont la même valeur que la case au dessus de celle choisie aléatoirement
                        for (int k = 0; k < dimensionX; k++)
                        {
                            for (int l = 0; l < dimensionY; l++)
                            {
                                if (tileset[k, l] == tileset[randNext, rand2Next - 1])
                                {
                                    tileset[k, l].ValeurCase = tileset[randNext, rand2Next].ValeurCase;
                                }
                            }
                        }
                        // On ouvre les murs Nord de la case choisie aléatoirement et Sud de celle du dessus
                        tileset[randNext, rand2Next].Nord = true;
                        tileset[randNext, rand2Next - 1].Sud = true;
                        // On incrémente le compteur de murs ouverts de 1
                        compteur++;
                    }
                    if (tileset[0, rand2Next] != tileset[0, rand2Next - 1])
                    {
                        for (int k = 0; k < dimensionX; k++)
                        {
                            for (int l = 0; l < dimensionY; l++)
                            {
                                if (tileset[k, l] == tileset[0, rand2Next - 1])
                                {
                                    tileset[k, l].ValeurCase = tileset[0, rand2Next].ValeurCase;
                                }
                            }
                        }
                        tileset[0, rand2Next].Nord = true;
                        tileset[0, rand2Next - 1].Sud = true;
                        compteur++;
                    }
                    if (tileset[dimensionX, rand2Next] != tileset[dimensionX, rand2Next - 1])
                    {
                        for (int k = 0; k < dimensionX; k++)
                        {
                            for (int l = 0; l < dimensionY; l++)
                            {
                                if (tileset[k, l] == tileset[dimensionX, rand2Next - 1])
                                {
                                    tileset[k, l].ValeurCase = tileset[dimensionX, rand2Next].ValeurCase;
                                }
                            }
                        }
                        tileset[dimensionX, rand2Next].Nord = true;
                        tileset[dimensionX, rand2Next - 1].Sud = true;
                        compteur++;
                    }
                    if (tileset[randNext, dimensionY] != tileset[randNext, dimensionY - 1])
                    {
                        for (int k = 0; k < dimensionX; k++)
                        {
                            for (int l = 0; l < dimensionY; l++)
                            {
                                if (tileset[k, l] == tileset[randNext, dimensionY - 1])
                                {
                                    tileset[k, l].ValeurCase = tileset[randNext, dimensionY].ValeurCase;
                                }
                            }
                        }
                        tileset[randNext, dimensionY].Nord = true;
                        tileset[randNext, dimensionY - 1].Sud = true;
                        compteur++;
                    }
                }
                // Direction aléatoire Ouest
                else if (directionNext == 2)
                {
                    if (tileset[randNext, rand2Next] != tileset[randNext + 1, rand2Next])
                    {
                        for (int k = 0; k < dimensionX; k++)
                        {
                            for (int l = 0; l < dimensionY; l++)
                            {
                                if (tileset[k, l] == tileset[randNext + 1, rand2Next])
                                {
                                    tileset[k, l].ValeurCase = tileset[randNext, rand2Next].ValeurCase;
                                }
                            }
                        }
                        tileset[randNext, rand2Next].Ouest = true;
                        tileset[randNext + 1, rand2Next].Est = true;
                        compteur++;
                    }
                    if (tileset[randNext, 0] != tileset[randNext + 1, 0])
                    {
                        for (int k = 0; k < dimensionX; k++)
                        {
                            for (int l = 0; l < dimensionY; l++)
                            {
                                if (tileset[k, l] == tileset[randNext + 1, 0])
                                {
                                    tileset[k, l].ValeurCase = tileset[randNext, 0].ValeurCase;
                                }
                            }
                        }
                        tileset[randNext, 0].Ouest = true;
                        tileset[randNext + 1, 0].Est = true;
                        compteur++;
                    }
                    if (tileset[randNext, dimensionY - 1] != tileset[randNext + 1, dimensionY - 1])
                    {
                        for (int k = 0; k < dimensionX; k++)
                        {
                            for (int l = 0; l < dimensionY; l++)
                            {
                                if (tileset[k, l] == tileset[randNext + 1, dimensionY - 1])
                                {
                                    tileset[k, l].ValeurCase = tileset[randNext, dimensionY - 1].ValeurCase;
                                }
                            }
                        }
                        tileset[randNext, dimensionY - 1].Ouest = true;
                        tileset[randNext + 1, dimensionY - 1].Est = true;
                        compteur++;
                    }
                    if (tileset[dimensionX - 1, rand2Next] != tileset[dimensionX, rand2Next])
                    {
                        for (int k = 0; k < dimensionX; k++)
                        {
                            for (int l = 0; l < dimensionY; l++)
                            {
                                if (tileset[k, l] == tileset[dimensionX, rand2Next])
                                {
                                    tileset[k, l].ValeurCase = tileset[dimensionX - 1, rand2Next].ValeurCase;
                                }
                            }
                        }
                        tileset[dimensionX - 1, rand2Next].Ouest = true;
                        tileset[dimensionX, rand2Next].Est = true;
                        compteur++;
                    }
                }
                // Direction aléatoire Sud
                else if (directionNext == 3)
                {
                    if (tileset[randNext, rand2Next] != tileset[randNext, rand2Next + 1])
                    {
                        for (int k = 0; k < dimensionX; k++)
                        {
                            for (int l = 0; l < dimensionY; l++)
                            {
                                if (tileset[k, l] == tileset[randNext, rand2Next + 1])
                                {
                                    tileset[k, l].ValeurCase = tileset[randNext, rand2Next].ValeurCase;
                                }
                            }
                        }
                        tileset[randNext, rand2Next].Sud = true;
                        tileset[randNext, rand2Next + 1].Nord = true;
                        compteur++;
                    }
                    if (tileset[0, rand2Next] != tileset[0, rand2Next + 1])
                    {
                        for (int k = 0; k < dimensionX; k++)
                        {
                            for (int l = 0; l < dimensionY; l++)
                            {
                                if (tileset[k, l] == tileset[0, rand2Next + 1])
                                {
                                    tileset[k, l].ValeurCase = tileset[0, rand2Next].ValeurCase;
                                }
                            }
                        }
                        tileset[0, rand2Next].Sud = true;
                        tileset[0, rand2Next + 1].Nord = true;
                        compteur++;
                    }
                    if (tileset[randNext, 0] != tileset[randNext, 0 + 1])
                    {
                        for (int k = 0; k < dimensionX; k++)
                        {
                            for (int l = 0; l < dimensionY; l++)
                            {
                                if (tileset[k, l] == tileset[randNext, 0 + 1])
                                {
                                    tileset[k, l].ValeurCase = tileset[randNext, 0].ValeurCase;
                                }
                            }
                        }
                        tileset[randNext, 0].Sud = true;
                        tileset[randNext, 0 + 1].Nord = true;
                        compteur++;
                    }
                    if (tileset[dimensionX, rand2Next] != tileset[dimensionX, rand2Next + 1])
                    {
                        for (int k = 0; k < dimensionX; k++)
                        {
                            for (int l = 0; l < dimensionY; l++)
                            {
                                if (tileset[k, l] == tileset[dimensionX, rand2Next + 1])
                                {
                                    tileset[k, l].ValeurCase = tileset[dimensionX, rand2Next].ValeurCase;
                                }
                            }
                        }
                        tileset[dimensionX, rand2Next].Sud = true;
                        tileset[dimensionX, rand2Next + 1].Nord = true;
                        compteur++;
                    }
                }
                // Direction aléatoire Est
                else if (directionNext == 4)
                {
                    if (tileset[randNext, rand2Next] != tileset[randNext - 1, rand2Next])
                    {
                        for (int k = 0; k < dimensionX; k++)
                        {
                            for (int l = 0; l < dimensionY; l++)
                            {
                                if (tileset[k, l] == tileset[randNext - 1, rand2Next])
                                {
                                    tileset[k, l].ValeurCase = tileset[randNext, rand2Next].ValeurCase;
                                }
                            }
                        }
                        tileset[randNext, rand2Next].Est = true;
                        tileset[randNext - 1, rand2Next].Ouest = true;
                        compteur++;
                    }
                    if (tileset[dimensionX, rand2Next] != tileset[dimensionX - 1, rand2Next])
                    {
                        for (int k = 0; k < dimensionX; k++)
                        {
                            for (int l = 0; l < dimensionY; l++)
                            {
                                if (tileset[k, l] == tileset[dimensionX - 1, rand2Next])
                                {
                                    tileset[k, l].ValeurCase = tileset[dimensionX, rand2Next].ValeurCase;
                                }
                            }
                        }
                        tileset[dimensionX, rand2Next].Est = true;
                        tileset[dimensionX - 1, rand2Next].Ouest = true;
                        compteur++;
                    }
                    if (tileset[randNext, 0] != tileset[randNext - 1, 0])
                    {
                        for (int k = 0; k < dimensionX; k++)
                        {
                            for (int l = 0; l < dimensionY; l++)
                            {
                                if (tileset[k, l] == tileset[randNext - 1, 0])
                                {
                                    tileset[k, l].ValeurCase = tileset[randNext, 0].ValeurCase;
                                }
                            }
                        }
                        tileset[randNext, 0].Est = true;
                        tileset[randNext - 1, 0].Ouest = true;
                        compteur++;
                    }
                    if (tileset[randNext, dimensionY] != tileset[randNext - 1, dimensionY])
                    {
                        for (int k = 0; k < dimensionX; k++)
                        {
                            for (int l = 0; l < dimensionY; l++)
                            {
                                if (tileset[k, l] == tileset[randNext - 1, dimensionY])
                                {
                                    tileset[k, l].ValeurCase = tileset[randNext, dimensionY].ValeurCase;
                                }
                            }
                        }
                        tileset[randNext, dimensionY].Est = true;
                        tileset[randNext - 1, dimensionY].Ouest = true;
                        compteur++;
                    }
                }
                testFin = true;
                for (int longueur = 1; longueur < dimensionX; longueur++)
                {
                    for (int largeur = 1; largeur < dimensionY; largeur++)
                    {
                        if (tileset[longueur, largeur] != tileset[0, 0])
                            testFin = false;
                    }
                }
            }
            for (int k = 0; k < dimensionX; k++)
            {
                for (int l = 0; l < dimensionY; l++)
                {
                    result[k, l] = tileset[k, l].ValeurCase;
                }
            }

            // On initialise toutes les cases du tileset à 0
            for (int i = 0; i < 4 * dimensionX; i++)
            {
                for (int j = 0; j < 4 * dimensionY; j++)
                {
                    if (piege.Next(0, pourcentagePiege) != 1)
                        tilesets[i, j] = solAffichage.Next(1, 6);
                    else
                        tilesets[i, j] = 13;
                }

            }
            // On change les murs externes en 1
            for (int k = 0; k < 4 * dimensionX; k++)
            {
                for (int l = 0; l < 4 * dimensionY; l++)
                {
                    tilesets[k, 0] = murAffichage.Next(7, 12);
                    tilesets[0, l] = murAffichage.Next(7, 12);
                    tilesets[4 * dimensionX, l] = murAffichage.Next(7, 12);
                    tilesets[k, 4 * dimensionY] = murAffichage.Next(7, 12);
                    tilesets[4 * dimensionX, 4 * dimensionY] = murAffichage.Next(7, 12);
                }
            }
            // On change les murs fermés en 1 selon la logique: une case = 3*3 tiles et entre deux cases un mur de 1*4 (en comptant les coins)
            for (int i = 1; i < dimensionX + 1; i++)
            {
                for (int j = 1; j < dimensionY + 1; j++)
                {
                    //Pour l'Est
                    if (tileset[i - 1, j - 1].Est == false)
                    {
                        tilesets[i * 4, j * 4] = murAffichage.Next(7, 12);
                        tilesets[i * 4 - 1, j * 4] = murAffichage.Next(7, 12);
                        tilesets[i * 4 - 2, j * 4] = murAffichage.Next(7, 12);
                        tilesets[i * 4 - 3, j * 4] = murAffichage.Next(7, 12);
                    }
                    //Pour le Sud
                    if (tileset[i - 1, j - 1].Sud == false)
                    {
                        tilesets[i * 4, j * 4] = murAffichage.Next(7, 12);
                        tilesets[i * 4, j * 4 - 1] = murAffichage.Next(7, 12);
                        tilesets[i * 4, j * 4 - 2] = murAffichage.Next(7, 12);
                        tilesets[i * 4, j * 4 - 3] = murAffichage.Next(7, 12);
                    }
                    //Pour le Nord
                    if (tileset[i - 1, j - 1].Nord == false)
                    {
                        tilesets[(i - 1) * 4, j * 4] = murAffichage.Next(7, 12);
                        tilesets[(i - 1) * 4, j * 4 - 1] = murAffichage.Next(7, 12);
                        tilesets[(i - 1) * 4, j * 4 - 2] = murAffichage.Next(7, 12);
                        tilesets[(i - 1) * 4, j * 4 - 3] = murAffichage.Next(7, 12);
                    }
                    //Pourl'Ouest
                    if (tileset[i - 1, j - 1].Ouest == false)
                    {
                        tilesets[i * 4, (j - 1) * 4] = murAffichage.Next(7, 12);
                        tilesets[i * 4 - 1, (j - 1) * 4] = murAffichage.Next(7, 12);
                        tilesets[i * 4 - 2, (j - 1) * 4] = murAffichage.Next(7, 12);
                        tilesets[i * 4 - 3, (j - 1) * 4] = murAffichage.Next(7, 12);
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