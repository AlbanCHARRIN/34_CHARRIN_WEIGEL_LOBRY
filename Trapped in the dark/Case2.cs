using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trapped_in_the_dark
{
    internal class Case2
    {
        private const int uneCase = 3;
        public const int dimensionX = 10 * uneCase;
        public const int dimensionY = 10 * uneCase;


        private int[,] tableau;

        public Case2(int[,] tableau)
        {
            this.tableau = tableau;
        }

        public int[,] Tableau { get => tableau; set => tableau = value; }



        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public static int[,] GenerateurDuTileset()
        {
            int[,] tileset = new int[dimensionX, dimensionY];

            for (int i = 0; i < dimensionX; i++)
            {
                for (int j = 0; j < dimensionY; j++)
                {
                    tileset[i, j] = 0;
                }
            }
            for (int i = 0; i < dimensionY; i++)
            {
                tileset[i, 0] = 1;
                tileset[0, i] = 1;
                tileset[i, dimensionY - 1] = 1;
                tileset[dimensionX - 1, i] = 1;
            }

            return tileset;

        }

        public static int[,] GenerateTheWall(int[,] tileset)
        {
            int wall = dimensionX + dimensionY;
            Random rand = new Random();
            for (int k = 0; k < dimensionX * dimensionY; k++)
            {
                int positionXWall = rand.Next(1, dimensionX - 1);
                int positionYWall = rand.Next(1, dimensionY - 1);
                tileset[positionXWall, positionYWall] = 0;

                for (int i = 0; i < wall; i++)
                {
                    int ajoutDeMurX = 0;
                    int ajoutDeMurY = 0;
                    int direction = rand.Next(1, 4);
                    if (direction == 1)
                    {
                        ajoutDeMurY = -1;
                    }
                    else if (direction == 2)
                    {
                        ajoutDeMurX = 1;
                    }
                    else if (direction == 3)
                    {
                        ajoutDeMurY = 1;
                    }
                    else if (direction == 4)
                    {
                        ajoutDeMurX = -1;
                    }
                    if (tileset[positionXWall + 1, positionYWall + 1] == 1 || tileset[positionXWall - 1, positionYWall + 1] == 1 || tileset[positionXWall + 1, positionYWall - 1] == 1 || tileset[positionXWall - 1, positionYWall - 1] == 1)
                    {
                        i = wall;
                    }
                    else if (tileset[positionXWall + ajoutDeMurX, positionYWall + ajoutDeMurY] != 1)
                    {
                        tileset[positionXWall, positionYWall] = 1;
                        tileset[positionXWall + ajoutDeMurX, positionYWall + ajoutDeMurY] = 1;
                        positionXWall += ajoutDeMurX;
                        positionYWall += ajoutDeMurY;
                    }

                    else
                    {
                        i = wall;
                    }
                }

            }

            for (int i = 0; i < dimensionX; i++)
            {
                for (int j = 0; j < dimensionY; j++)
                {
                    if (tileset[i, j] == 1)
                        tileset[i, j] = rand.Next(8, 13);
                }
            }

            return tileset;


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
