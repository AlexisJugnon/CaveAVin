using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Cru : Modification
    {
        #region attributs
        private int id;
        private string nomCru;
        private List<Bouteille> bouteilles = new List<Bouteille>();
        #endregion

        #region propriétés
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public string NomCru
        {
            get
            {
                return nomCru;
            }
            set
            {
                nomCru = value;
            }
        }
        #endregion

        #region opérations
        /// <summary>
        /// Créer un cru
        /// </summary>
        /// <param name="n">(facultatif) le nom du cru</param>
        public Cru(string n = "")
        {
            nomCru = n;
        }
        /// <summary>
        /// Donne le cru au bouteille
        /// </summary>
        /// <param name="b">liste de bouteille ayant le même cru</param>
        public void Ajouter(Bouteilles b)
        {
            foreach (Bouteille bou in b.Lister())
            {
                bou.Cru = this;
                Ajouter(bou);
            }
        }

        /// <summary>
        /// ajoute les bouteille ou le cru est modifié
        /// </summary>
        /// <param name="b">bouteille ou on change le cru</param>
        public void Ajouter(Bouteille b)
        {
            bouteilles.Add(b);

        }


        /// <summary>
        /// supprime le cru d'une bouteille
        /// </summary>
        /// <param name="b"> la bouteille où on doit retirer le cru</param>
        public void Supprimer(Bouteille b)
        {
            bouteilles.Remove(b);
        }
      

        /// <summary>
        /// Fournis la liste de bouteille possédée
        /// </summary>
        /// <returns></returns>
        public Bouteille[] Lister()
        {
            return bouteilles.ToArray();
        }
        #endregion
    }
}
