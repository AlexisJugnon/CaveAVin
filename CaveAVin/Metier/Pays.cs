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
        /// <summary>
        /// Ajoute une nouvelle région à la liste de région
        /// </summary>
        /// <param name="r">Région à ajouter</param>
        public void Ajouter(Region r)
        {
            regions.Add(r);
        }


        /// <summary>
        /// Ajoute un pays à une liste de region
        /// </summary>
        /// <param name="r">Région à attribuer le pays</param>
        public void Ajouter(Regions r)
        {
            foreach (Region cr in r.Lister())
            {
                cr.Pays = this;
                Ajouter(cr);
            }
        }

        /// <summary>
        /// Supprime  une région de la liste des régions
        /// </summary>
        /// <param name="r">Région à supprimer</param>
        
        public void Supprimer(Region r)
        {
            regions.Remove(r);
        }

        /// <summary>
        /// retourne une liste de région
        /// </summary>
        /// <returns>liste de région</returns>
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
