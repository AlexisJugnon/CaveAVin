using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Casier
    {
        #region attributs
        private int id;
        private string nom;
        private int largeurX;
        private int largeurY;
        #endregion

        #region proriétés
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
        public string Nom
        {
            get
            {
                return nom;
            }
            set
            {
                nom = value;
            }
        }
        public int LargeurX
        {
            get
            {
                return largeurX;
            }
            set
            {
                largeurX = value;
            }
        }
        public int LargeurY
        {
            get
            {
                return largeurY;
            }
            set
            {
                largeurY = value;
            }
        }
        #endregion
    }
}
