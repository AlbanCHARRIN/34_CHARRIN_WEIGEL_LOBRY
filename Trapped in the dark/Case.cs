using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trapped_in_the_dark
{
    internal class Case
    {
        private const int dimension = 6;
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
                return this.Nord;
            }
            set
            {
                this.Nord = value;
            }
        }
        public bool Est 
        { 
            get
            {
                return this.Est;
            }
            set
            {
                this.Est = value;
            }
        }
        public bool Ouest 
        {
            get
            {
                return this.Ouest;
            }
            set
            {
                this.Ouest = value;
            }
        }
        public bool Sud 
        {
            get
            {
                return this.Sud;
            }
            set
            {
                this.Sud = value;
            }
        }
        public int Valeurcase 
        {
            get
            {
                return this.Valeurcase;
            }
            set
            {
                this.Valeurcase = value;
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
        public int[,] GenerateurDuTileset()
        {
            int[,] tilesets = new int[4 * dimension + 2, 4 * dimension + 2];
            Case[,] tileset = new Case[dimension, dimension];
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
            while (compteur < dimension * dimension - 1)
            {
                Random rand = new Random();
                Random rand2 = new Random();
                Random direction = new Random();
                int randnext = rand.Next(0, dimension - 1);
                int rand2next = rand2.Next(0, dimension - 1);
                int directionnext = direction.Next(0, 4);
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
                        tileset[randnext + 1, rand2next].Valeurcase = tileset[randnext, rand2next].Valeurcase;
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
            for (int i = 0; i < tilesets.GetLength(0); i++)
            {
                for (int j = 0; j < tilesets.GetLength(1); j++)
                    tilesets[i, j] = 0;
            }
            for (int i = 1; i < tileset.GetLength(0); i++)
            {
                for (int j = 1; j < tileset.GetLength(1); j++)
                {
                    if (tileset[i - 1, j - 1].Est == true)
                    {
                        tilesets[i * 3 + 1, j * 3 + 1] = 1;
                        tilesets[i * 3 + 1, j * 3 + 2] = 1;
                        tilesets[i * 3 + 1, j * 3 + 3] = 1;
                        tilesets[i * 3 + 1, j * 3 + 4] = 1;
                    }
                    if (tileset[i - 1, j - 1].Sud == true)
                    {
                        tilesets[i * 3 + 1, j * 3 + 1] = 1;
                        tilesets[i * 3 + 2, j * 3 + 1] = 1;
                        tilesets[i * 3 + 3, j * 3 + 1] = 1;
                        tilesets[i * 3 + 4, j * 3 + 1] = 1;
                    }
                    if (tileset[i - 1, j - 1].Nord == true)
                    {
                        tilesets[i * 3 + 1, j * 3 - 1] = 1;
                        tilesets[i * 3 + 2, j * 3 - 1] = 1;
                        tilesets[i * 3 + 3, j * 3 - 1] = 1;
                        tilesets[i * 3 + 4, j * 3 - 1] = 1;
                    }
                    if (tileset[i - 1, j - 1].Ouest == true)
                    {
                        tilesets[i * 3 - 1, j * 3 + 1] = 1;
                        tilesets[i * 3 - 1, j * 3 + 2] = 1;
                        tilesets[i * 3 - 1, j * 3 + 3] = 1;
                        tilesets[i * 3 - 1, j * 3 + 4] = 1;
                    }
                }
            }
            return tilesets;
        }
    }
}
