using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Caves
    {
        private List<Cave> caves = new List<Cave>();

        #region opérations

        /// <summary>
        /// Ajoute un cave à la liste
        /// </summary>
        /// <param name="p"></param>
        /// <exception cref="Exception">Si le cave existe déjà</exception>
        public void Ajouter(Cave c)
        {
            if (caves.Contains(c))
                throw new Exception("Le cave existe déjà");
            caves.Add(c);
        }

        public void Ajouter(Casier c)
        {
            c.Cave.Ajouter(c);
        }

        public void Supprimer(Cave c)
        {
            caves.Remove(c);
        }

        /// <summary>
        /// Fournit l'ensemble des caves
        /// </summary>
        /// <returns>un tableau des caves</returns>
        public Cave[] Lister()
        {
            return caves.ToArray();
        }
        #endregion
    }
}
