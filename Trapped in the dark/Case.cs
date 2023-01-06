using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trapped_in_the_dark
{
    internal class Case
    {
        private const int dimensionx = 12;
        private const int dimensiony = 18;
        private bool nord, est, ouest, sud;
        private int valeurcase;

        public Case(bool nord, bool est, bool ouest, bool sud, int valeurcase)
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
            return obj is Case @case &&
                   Valeurcase == @case.Valeurcase;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Valeurcase);
        }
        public static int[,] GenerateurDuTileset()
        {
            int[,] tilesets = new int[4 * dimensionx + 1, 4 * dimensiony + 1];
            Case[,] tileset = new Case[dimensionx, dimensiony];
            int[,] result = new int[dimensionx, dimensiony];
            int compteur = 0;
            int compteurvaleur = 0;
            Random rand = new Random();
            Random rand2 = new Random();
            Random direction = new Random();
            Random solaffichage = new Random();
            Random muraffichage = new Random();
            for (int i = 0; i < dimensionx; i++)
            {
                for (int j = 0; j < dimensiony; j++)
                {
                    tileset[i, j] = new Case(false, false, false, false, compteurvaleur);
                    compteurvaleur++;
                }
            }
            while (compteur < dimensiony * dimensionx - 1)
            {
                int randnext = rand.Next(1, dimensionx);
                int rand2next = rand2.Next(1, dimensiony);
                int directionnext = direction.Next(1, 4);
                if (directionnext == 1)
                {
                    if (tileset[randnext, rand2next] != tileset[randnext, rand2next - 1])
                    {
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
                        tileset[randnext, rand2next].Sud = true;
                        tileset[randnext, rand2next - 1].Nord = true;
                        compteur++;
                    }
                }
                else if (directionnext == 2)
                {
                    if (tileset[randnext - 1, rand2next] != tileset[randnext, rand2next])
                    {
                        for (int k = 0; k < dimensionx; k++)
                        {
                            for (int l = 0; l < dimensiony; l++)
                            {
                                if (tileset[k, l] == tileset[randnext, rand2next])
                                {
                                    tileset[k, l].Valeurcase = tileset[randnext, rand2next].Valeurcase;
                                }
                            }
                        }
                        tileset[randnext, rand2next].Est = true;
                        tileset[randnext - 1, rand2next].Ouest = true;
                        compteur++;
                    }
                }
                else if (directionnext == 3)
                {
                    if (tileset[randnext, rand2next - 1] != tileset[randnext, rand2next])
                    {
                        for (int k = 0; k < dimensionx; k++)
                        {
                            for (int l = 0; l < dimensiony; l++)
                            {
                                if (tileset[k, l] == tileset[randnext, rand2next])
                                {
                                    tileset[k, l].Valeurcase = tileset[randnext, rand2next].Valeurcase;
                                }
                            }
                        }
                        tileset[randnext, rand2next].Sud = true;
                        tileset[randnext, rand2next - 1].Nord = true;
                        compteur++;
                    }
                }
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
                        tileset[randnext - 1, rand2next].Est = true;
                        tileset[randnext, rand2next].Ouest = true;
                        compteur++;
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
            for (int i = 0; i < 4 * dimensionx; i++)
            {
                for (int j = 0; j < 4 * dimensiony; j++)
                    tilesets[i, j] = solaffichage.Next(0, 3);
            }
            for (int k = 0; k < 4 * dimensionx; k++)
            {
                for (int l = 0; l < 4 * dimensiony; l++)
                {
                    tilesets[k, 0] = muraffichage.Next(3, 6);
                    tilesets[0, l] = muraffichage.Next(3, 6);
                    tilesets[4 * dimensionx, l] = muraffichage.Next(3, 6);
                    tilesets[k, 4 * dimensiony] = muraffichage.Next(3, 6);
                    tilesets[4 * dimensionx, 4 * dimensiony] = muraffichage.Next(3, 6);
                }
            }
            for (int i = 1; i < dimensionx + 1; i++)
            {
                for (int j = 1; j < dimensiony + 1; j++)
                {
                    if (tileset[i - 1, j - 1].Est == false)
                    {
                        tilesets[i * 4, j * 4] = muraffichage.Next(3, 6);
                        tilesets[i * 4 - 1, j * 4] = muraffichage.Next(3, 6);
                        tilesets[i * 4 - 2, j * 4] = muraffichage.Next(3, 6);
                        tilesets[i * 4 - 3, j * 4] = muraffichage.Next(3, 6);
                    }
                    if (tileset[i - 1, j - 1].Sud == false)
                    {
                        tilesets[i * 4, j * 4] = muraffichage.Next(3, 6);
                        tilesets[i * 4, j * 4 - 1] = muraffichage.Next(3, 6);
                        tilesets[i * 4, j * 4 - 2] = muraffichage.Next(3, 6);
                        tilesets[i * 4, j * 4 - 3] = muraffichage.Next(3, 6);
                    }
                    if (tileset[i - 1, j - 1].Nord == false)
                    {
                        tilesets[(i - 1) * 4, j * 4] = muraffichage.Next(3, 6);
                        tilesets[(i - 1) * 4, j * 4 - 1] = muraffichage.Next(3, 6);
                        tilesets[(i - 1) * 4, j * 4 - 2] = muraffichage.Next(3, 6);
                        tilesets[(i - 1) * 4, j * 4 - 3] = muraffichage.Next(3, 6);
                    }
                    if (tileset[i - 1, j - 1].Ouest == false)
                    {
                        tilesets[i * 4, (j - 1) * 4] = muraffichage.Next(3, 6);
                        tilesets[i * 4 - 1, (j - 1) * 4] = muraffichage.Next(3, 6);
                        tilesets[i * 4 - 2, (j - 1) * 4] = muraffichage.Next(3, 6);
                        tilesets[i * 4 - 3, (j - 1) * 4] = muraffichage.Next(3, 6);
                    }
                }
            }
            return tilesets;
        }

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