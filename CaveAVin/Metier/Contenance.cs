using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Contenance : Modification
    {
        #region attributs
        private int id;
        private int valeur;
        private List<Bouteille> bouteilles = new List<Bouteille>();
        #endregion

        #region fonctions

        /// <summary>
        /// Ajoute une nouvelle bouteille
        /// </summary>
        /// <param name="b">Bouteille à ajouter</param>
        public void Ajouter(Bouteille b)
        {
            bouteilles.Add(b);
        }

        /// <summary>
        /// ajoute la contenance à une liste de bouteille
        /// </summary>
        /// <param name="b">liste de bouteille à modifier la contenace</param>
        public void Ajouter(Bouteilles b)
        {
            foreach (Bouteille bt in b.Lister())
            {
                bt.Contenance = this;
                Ajouter(bt);
            }
        }

        /// <summary>
        /// retire une bouteille de la liste de bouteille
        /// </summary>
        /// <param name="b">Bouteille à retirer</param>
        public void Supprimer(Bouteille b)
        {
            bouteilles.Remove(b);
        }

        /// <summary>
        /// retourne la lsite de bouteille
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

        public int Valeur
        {
            get
            {
                return valeur;
            }
            set
            {
                valeur = value;
            }
        }

        #endregion
    }
}
