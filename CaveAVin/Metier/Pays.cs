using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Pays
    {
        #region attributs
        private int id;
        private string nomPays;
        #endregion

        #region opérations
        public void Ajouter(Region r) { }
        public void Ajouter(Regions r) { }
        public void Supprimer(Region r) { }
        public Region[] Lister() { return null; }
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
        public string NomPays
        {
            get
            {
                return nomPays;
            }
            set
            {
                nomPays = value;
            }
        }
        #endregion
    }
}
