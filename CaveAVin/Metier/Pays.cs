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
        private List<Region> regions = new List<Region>();
        #endregion

        #region opérations
        public void Ajouter(Region r)
        {
            regions.Add(r);
        }

        public void Ajouter(Regions r)
        {
            foreach (Region cr in r.Lister())
            {
                cr.Pays = this;
                Ajouter(cr);
            }
        }
        public void Supprimer(Region r)
        {
            regions.Remove(r);
        }
        public Region[] Lister()
        {
            return regions.ToArray() ;
        }

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
