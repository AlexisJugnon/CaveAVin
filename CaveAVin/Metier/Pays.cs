using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Pays
    {
        #region attributs
        private int id;
        private string nomPays;
        private List<Bouteille> bouteilles = new List<Bouteille>();
        #endregion

        #region opérations
        /// <summary>
        /// Créer un pays
        /// </summary>
        /// <param name="n">(facultatif) le nom du cru</param>
        public Pays(string n = "")
        {
            nomPays = n;
        }
        /// <summary>
        /// Donne le pays au bouteille
        /// </summary>
        /// <param name="b">liste de bouteille ayant le même pays</param>
        public void Ajouter(Bouteilles b)
        {
            foreach (Bouteille bou in b.Lister())
            {
                bou.Pays = this;
                Ajouter(bou);
            }
        }

        /// <summary>
        /// ajoute les bouteille ou le pays est modifié
        /// </summary>
        /// <param name="b">bouteille ou on change le pays</param>
        public void Ajouter(Bouteille b)
        {
            bouteilles.Add(b);

        }


        /// <summary>
        /// supprime le pays d'une bouteille
        /// </summary>
        /// <param name="b"> la bouteille où on doit retirer le pays</param>
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
        public string NomPays
        {
            get
            {
                return nomPays;
            }
            set
            {
                nomPays = value;
            }
        }
        #endregion
    }
}
