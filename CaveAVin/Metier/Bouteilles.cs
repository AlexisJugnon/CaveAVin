using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Bouteilles
    {
        private List<Bouteille> bouteilles = new List<Bouteille>();

        #region opérations

        /// <summary>
        /// Supprime toute les bouteilles
        /// </summary>
        public void Vider()
        {
            bouteilles.Clear();
        }

        /// <summary>
        /// Ajoute une bouteille à la liste
        /// </summary>
        /// <param name="p"></param>
        /// <exception cref="Exception">Si le produit existe déjà</exception>
        public void Ajouter(Bouteille p)
        {
            if (bouteilles.Contains(p))
                throw new Exception("la bouteille existe déjà");
            bouteilles.Add(p);
        }

        /// <summary>
        /// Fournit l'ensemble des bouteilles
        /// </summary>
        /// <returns>un tableau des bouteilles</returns>
        public Bouteille[] Lister()
        {
            return bouteilles.ToArray();
        }
        #endregion
    }
}
