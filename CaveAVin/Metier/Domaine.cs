using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Domaine
    {
        #region attributs
        private int id;
        private string nomDomaine;
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
        public string NomDomaine
        {
            get
            {
                return nomDomaine;
            }
            set
            {
                nomDomaine = value;
            }
        }
        #endregion
    }
}
