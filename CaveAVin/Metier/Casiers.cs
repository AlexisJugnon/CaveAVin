using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Casiers
    {
        private List<Casier> casiers = new List<Casier>();

        #region opérations
        /// <summary>
        /// Ajoute un casier à la liste
        /// </summary>
        /// <param name="p"></param>
        /// <exception cref="Exception">Si le casier existe déjà</exception>
        public void Ajouter(Casier p)
        {
            if (casiers.Contains(p))
                throw new Exception("Le casier existe déjà");
            casiers.Add(p);
        }

        public void Ajouter(Bouteille b)
        {
            b.Casier.Ajouter(b);
        }

        public void Supprimer(Casier c)
        {
            casiers.Remove(c);
        }

        /// <summary>
        /// Fournit l'ensemble des casiers
        /// </summary>
        /// <returns>un tableau des casiers</returns>
        public Casier[] Lister()
        {
            return casiers.ToArray();
        }

        public void Vider()
        {
            casiers.Clear();
        }
        #endregion
    }
}
