using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trapped_in_the_dark
{
    internal class Case
    {
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
    }
}
