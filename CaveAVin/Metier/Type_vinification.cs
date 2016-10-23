using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Type_vinification
    {
        #region attributs
        private int id;
        private string nomVinif;
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
        public string NomVinif
        {
            get
            {
                return nomVinif;
            }
            set
            {
                nomVinif = value;
            }
        }
        #endregion
    }
}
