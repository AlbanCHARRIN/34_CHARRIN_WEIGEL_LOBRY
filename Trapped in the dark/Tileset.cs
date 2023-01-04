using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trapped_in_the_dark
{
    internal class Tileset
    {
        private int dimension = 6;

        public Tileset(int dimension)
        {
            this.Dimension = dimension;
        }

        public int Dimension 
        {
            get
            {
                return this.Dimension;
            }
            set
            {
                this.Dimension = value;
            }
        }
        public int[,] GenerateurDuTileset(int dimension)
        {
            Case[,] tileset = new Case[dimension,dimension];
            int compteur = 0;
            int compteurvaleur = 0;
            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    tileset[i, j] = new Case(false, false, false, false, compteurvaleur);
                    compteurvaleur++;
                }
            }
            while (compteur < dimension*dimension-1)
            {
                Random rand = new Random();
                Random rand2 = new Random();
                Random direction = new Random();
                int randnext = rand.Next(0, dimension -1 );
                int rand2next = rand2.Next(0, dimension -1);
                int directionnext = direction.Next(0,4);
                if (directionnext == 1)
                {
                    if (tileset[randnext, rand2next] != tileset[randnext - 1, rand2next])
                    {
                        tileset[randnext - 1, rand2next].Valeurcase = tileset[randnext, rand2next].Valeurcase;
                        tileset[randnext, rand2next].Nord = true;
                        tileset[randnext - 1, rand2next].Sud = true;
                        compteur++;
                    }
                }
                else if (directionnext == 2)
                {
                    if (tileset[randnext, rand2next] != tileset[randnext, rand2next + 1])
                    {
                        tileset[randnext - 1, rand2next + 1].Valeurcase = tileset[randnext, rand2next].Valeurcase;
                        tileset[randnext - 1, rand2next].Est = true;
                        tileset[randnext - 1, rand2next + 1].Ouest = true;
                        compteur++;
                    }
                }
                else if (directionnext == 3)
                {
                    if (tileset[randnext, rand2next] != tileset[randnext + 1, rand2next])
                    {
                        tileset[randnext + 1 , rand2next].Valeurcase = tileset[randnext, rand2next].Valeurcase;
                        tileset[randnext, rand2next].Sud = true;
                        tileset[randnext + 1, rand2next].Nord = true;
                        compteur++;
                    }
                }
                else if (directionnext == 4)
                {
                    if (tileset[randnext, rand2next] != tileset[randnext, rand2next - 1])
                    {
                        tileset[randnext - 1, rand2next - 1].Valeurcase = tileset[randnext, rand2next].Valeurcase;
                        tileset[randnext - 1, rand2next].Ouest = true;
                        tileset[randnext - 1, rand2next - 1].Est = true;
                        compteur++;
                    }
                }
            }
            return new int[1, 1] { { 2 } };
        }
    }
}
