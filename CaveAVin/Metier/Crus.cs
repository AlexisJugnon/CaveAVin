using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Crus
    {
        private List<Cru> crus = new List<Cru>();

        #region opérations

        /// <summary>
        /// Ajoute un cru à la liste
        /// </summary>
        /// <param name="p"></param>
        /// <exception cref="Exception">Si le cru existe déjà</exception>
        public void Ajouter(Cru p)
        {
            if (crus.Contains(p))
                throw new Exception("Le cru existe déjà");
            crus.Add(p);
        }

        public void Ajouter(Bouteille b)
        {
            b.Cru.Ajouter(b);
        }

        public void supprimer(Cru p)
        {
            crus.Remove(p);
        }

        /// <summary>
        /// Fournit l'ensemble des crus
        /// </summary>
        /// <returns>un tableau des crus</returns>
        public Cru[] Lister()
        {
            return crus.ToArray();
        }
        #endregion
    }
}
