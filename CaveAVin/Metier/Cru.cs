using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Cru
    {
        #region attributs
        private int id;
        private string nomCru;
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
        public string NomCru
        {
            get
            {
                return nomCru;
            }
            set
            {
                nomCru = value;
            }
        }
        #endregion
    }
}
