using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Contenance
    {
        #region attributs
        private int id;
        private int valeur;
        #endregion

        #region propriétés
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public int Valeur
        {
            get
            {
                return valeur;
            }
            set
            {
                valeur = value;
            }
        }
        #endregion
    }
}
