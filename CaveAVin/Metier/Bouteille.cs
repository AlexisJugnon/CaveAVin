using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Bouteille
    {
        #region attributs
        private int id;
        private Boolean bue;
        private string texte;
        private int posX;
        private int posY;
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
        public Boolean Bue
        {
            get
            {
                return bue;
            }
            set
            {
                bue = value;
            }
        }
        public string Texte
        {
            get
            {
                return texte;
            }
            set
            {
                texte = value;
            }
        }
        public int PosX
        {
            get
            {
                return posX;
            }
            set
            {
                posX = value;
            }
        }
        public int PosY
        {
            get
            {
                return posY;
            }
            set
            {
                posY = value;
            }
        }
        #endregion
    }
}
