using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Contenances
    {
        private List<Contenance> contenances = new List<Contenance>();

        #region opérations

        /// <summary>
        /// Supprime tous les contenances
        /// </summary>
        public void Vider()
        {
            contenances.Clear();
        }

        /// <summary>
        /// Ajoute un contenance à la liste
        /// </summary>
        /// <param name="p"></param>
        /// <exception cref="Exception">Si le contenance existe déjà</exception>
        public void Ajouter(Contenance p)
        {
            if (contenances.Contains(p))
                throw new Exception("Le contenance existe déjà");
            contenances.Add(p);
        }

        /// <summary>
        /// Fournit l'ensemble des contenances
        /// </summary>
        /// <returns>un tableau des contenances</returns>
        public Contenance[] Lister()
        {
            return contenances.ToArray();
        }
        #endregion
    }
}
