using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Regions
    {
        private List<Region> regions = new List<Region>();

        #region opérations

        /// <summary>
        /// Ajoute un region à la liste
        /// </summary>
        /// <param name="p"></param>
        /// <exception cref="Exception">Si le region existe déjà</exception>
        public void Ajouter(Region p)
        {
            if (regions.Contains(p))
                throw new Exception("Le region existe déjà");
            regions.Add(p);
        }

        public void Ajouter(Bouteille b)
        {
            b.Casier.Ajouter(b);
        }

        public void Supprimer(Region p)
        {
            regions.Remove(p);
        }

        /// <summary>
        /// Fournit l'ensemble des regions
        /// </summary>
        /// <returns>un tableau des regions</returns>
        public Region[] Lister()
        {
            return regions.ToArray();
        }
        #endregion
    }
}
