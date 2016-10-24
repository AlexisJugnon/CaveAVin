using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Pays2
    {
        private List<Pays> payss = new List<Pays>();

        #region opérations

        /// <summary>
        /// Ajoute un pays à la liste
        /// </summary>
        /// <param name="p"></param>
        /// <exception cref="Exception">Si le pays existe déjà</exception>
        public void Ajouter(Pays p)
        {
            if (payss.Contains(p))
                throw new Exception("Le pays existe déjà");
            payss.Add(p);
        }

        public void Ajouter(Bouteille b) { }

        public void supprimer(Pays p) { }

        /// <summary>
        /// Fournit l'ensemble des payss
        /// </summary>
        /// <returns>un tableau des payss</returns>
        public Pays[] Lister()
        {
            return payss.ToArray();
        }
        #endregion
    }
}
