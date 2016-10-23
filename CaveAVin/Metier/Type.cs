using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Type
    {
        #region attributs
        private int id;
        private string nomType;
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

        public string NomType
        {
            get
            {
                return nomType;
            }
            set
            {
                nomType = value;
            }
        }
        #endregion
    }
}
