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
            Bouteilles liste = new Bouteilles();
            int indexTableau = 0;
            foreach (Bouteille bouteille in liste.Lister())
            {
                if (bouteille.Id == b.Id)
                {
                    liste.Lister().SetValue(b, indexTableau);
                }
                indexTableau += 1;
            }

        }


        /// <summary>
        /// supprime le cru d'une bouteille
        /// </summary>
        /// <param name="b"> la bouteille où on doit retirer le cru</param>
        public void Supprimer(Bouteille b)
        {
            Bouteilles bou = new Bouteilles();

            if (bou.Lister().Contains(b))
            {
                foreach (Bouteille bouteille in bou.Lister())
                {

                    if(bouteille.Id == b.Id)
                    {
                        bouteille.Cru = null;
                    }
                }
            }
        }
        
        /// <summary>
        /// retourne un tableau de bouteille
        /// </summary>
        /// <returns></returns>
        Bouteille[] Modification.Lister()
        {
            Bouteilles liste = new Bouteilles();
            return liste.Lister();
        }

        /// <summary>
        /// Fournis la liste de bouteille possédée
        /// </summary>
        /// <returns></returns>
        Bouteille[] Lister()
        {
            Bouteilles bou = new Bouteilles();
            return bou.Lister();
        }
        #endregion
    }
}
