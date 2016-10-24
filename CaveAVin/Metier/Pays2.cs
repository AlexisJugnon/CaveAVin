using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Pays2
    {
        private List<Pays> pays = new List<Pays>();

        #region opérations

        /// <summary>
        /// Ajoute un pays à la liste
        /// </summary>
        /// <param name="p"></param>
        /// <exception cref="Exception">Si le pays existe déjà</exception>
        public void Ajouter(Pays p)
        {
            if (pays.Contains(p))
                throw new Exception("Le pays existe déjà");
            pays.Add(p);
        }

        public void Ajouter(Region r)
        {
            r.Pays.Ajouter(r);
        }

        public void supprimer(Pays p)
        {
            pays.Remove(p);
        }

        /// <summary>
        /// Fournit l'ensemble des payss
        /// </summary>
        /// <returns>un tableau des payss</returns>
        public Pays[] Lister()
        {
            return pays.ToArray();
        }
        #endregion
    }
}
