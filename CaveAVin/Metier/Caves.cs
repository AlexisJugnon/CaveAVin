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
        /// Supprime tous les caves
        /// </summary>
        public void Vider()
        {
            caves.Clear();
        }

        /// <summary>
        /// Ajoute un cave à la liste
        /// </summary>
        /// <param name="p"></param>
        /// <exception cref="Exception">Si le cave existe déjà</exception>
        public void Ajouter(Cave p)
        {
            if (caves.Contains(p))
                throw new Exception("Le cave existe déjà");
            caves.Add(p);
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
