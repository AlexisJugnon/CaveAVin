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
        private String nomMillesime;
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
        public String NomMillesime
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
