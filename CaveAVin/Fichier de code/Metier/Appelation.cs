using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Appelation : Modification
    {
        
        #region attributs
        private int id;
        private string nomAppelation;
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
        public string NomAppelation
        {
            get
            {
                return nomAppelation;
            }
            set
            {
                nomAppelation = value;
            }
        }
        #endregion

        #region opérations

        /// <summary>
        /// Créer une appelation
        /// </summary>
        /// <param name="n">(facultatif) le nom de l'appelation</param>
        public Appelation(string n = "")
        {
            nomAppelation = n;
        }

        /// <summary>
        /// attribut l'appelation au bouteilles
        /// </summary>
        /// <param name="b">bouteille qui vont prendre l'appelation</param>
        public void Ajouter(Bouteilles b)
        {
            foreach(Bouteille bou in b.Lister())
            {
                bou.Appelation = this;
                Ajouter(bou);
            }
        }

        /// <summary>
        /// Modifie les bouteilles ou l'appelation à changer
        /// </summary>
        /// <param name="b">la bouteille à modifier</param>
        public void Ajouter(Bouteille b)
        {
            bouteilles.Add(b);

        }


        /// <summary>
        /// retourne un tableau de bouteille
        /// </summary>
        /// <returns></returns>
        public Bouteille[] Lister()
        {
            return bouteilles.ToArray();
        }

        /// <summary>
        /// retire l'appelation d'une bouteille
        /// </summary>
        /// <param name="b">la bouteille ou on doit retirer une appelation</param>
        public void Supprimer(Bouteille b)
        {
            bouteilles.Remove(b);
        }
        #endregion
    }
}
