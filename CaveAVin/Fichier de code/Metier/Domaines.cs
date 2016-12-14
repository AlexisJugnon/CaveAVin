using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Domaines
    {
        private List<Domaine> domaines = new List<Domaine>();

        #region opérations

        /// <summary>
        /// Ajoute un domaine à la liste
        /// </summary>
        /// <param name="p"></param>
        /// <exception cref="Exception">Si le domaine existe déjà</exception>
        public void Ajouter(Domaine p)
        {
            if (domaines.Contains(p))
                throw new Exception("Le domaine existe déjà");
            domaines.Add(p);
        }

        public void Ajouter(Bouteille b)
        {
            b.Domaine.Ajouter(b);
        }

        public void supprimer(Domaine p)
        {
            domaines.Remove(p);
        }

        /// <summary>
        /// Fournit l'ensemble des domaines
        /// </summary>
        /// <returns>un tableau des domaines</returns>
        public Domaine[] Lister()
        {
            return domaines.ToArray();
        }
        #endregion
    }
}
