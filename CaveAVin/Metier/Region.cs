using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Region
    {
        #region attributs
        private int id;
        private string nomRegion;
        private Pays pays;
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
        public string NomRegion
        {
            get
            {
                return nomRegion;
            }
            set
            {
                nomRegion = value;
            }
        }
        public Pays Pays
        {
            get
            {
                return pays;
            }
            set
            {
                pays = value;
            }
            #endregion
        }
}
