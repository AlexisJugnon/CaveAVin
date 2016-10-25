using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Millesime
    {
        #region attributs
        private int id;
        private string nomMillesime;
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
        public string NomMillesime
        {
            get
            {
                return nomMillesime;
            }
            set
            {
                nomMillesime = value;
            }
        }
        #endregion;
    }
}
