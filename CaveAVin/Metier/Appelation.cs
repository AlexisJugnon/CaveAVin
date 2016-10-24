using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Appelation
    {
        
        #region attributs
        private int id;
        private string nomAppelation;
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
        public string NomAppelation
        {
            get
            {
                return nomAppelation;
            }
            set
            {
                nomAppelation = value;
            }
        }
        #endregion
    }
}
