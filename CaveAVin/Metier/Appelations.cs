using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Appelations
    {
        private List<Appelation> appelations = new List<Appelation>();

        #region opérations

        /// <summary>
        /// Ajoute un appelation à la liste
        /// </summary>
        /// <param name="p"></param>
        /// <exception cref="Exception">Si le appelation existe déjà</exception>
        public void Ajouter(Appelation p)
        {
            if (appelations.Contains(p))
                throw new Exception("Le appelation existe déjà");
            appelations.Add(p);
        }

        public void Ajouter(Bouteille b) { }

        public void supprimer(Appelation p) { }

        /// <summary>
        /// Fournit l'ensemble des appelations
        /// </summary>
        /// <returns>un tableau des appelations</returns>
        public Appelation[] Lister()
        {
            return appelations.ToArray();
        }
        #endregion
    }
}
