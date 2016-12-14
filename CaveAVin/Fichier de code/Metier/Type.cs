using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Type:Modification
    {
        #region attributs
        private int id;
        private string nomType;
        private List<Bouteille> bouteilles = new List<Bouteille>();
        #endregion

        #region opérations
        /// <summary>
        /// Ajoute une bouteille à la liste e bouteille
        /// </summary>
        /// <param name="b">Bouteille à ajouter</param>
        /// 
        public void Ajouter(Bouteille b)
        {
            bouteilles.Add(b);
        }

        /// <summary>
        /// Ajoute le type à une liste de bouteille
        /// </summary>
        /// <param name="b">liste de bouteille à modifier le type</param>
        public void Ajouter(Bouteilles b)
        {
            foreach (Bouteille bt in b.Lister())
            {
                bt.Type = this;
                Ajouter(bt);
            }
        }

        /// <summary>
        /// Supprime la bouteille de la liste
        /// </summary>
        /// <param name="b">Bouteille à supprimer</param>
        public void Supprimer(Bouteille b)
        {
            bouteilles.Remove(b);
        }

        /// <summary>
        /// Retourne la liste de bouteille
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

        public string NomType
        {
            get
            {
                return nomType;
            }
            set
            {
                nomType = value;
            }
        }
        #endregion
    }
}
