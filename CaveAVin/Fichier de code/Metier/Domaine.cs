using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Domaine : Modification
    {
        #region attributs
        private int id;
        private string nomDomaine;
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
        public string NomDomaine
        {
            get
            {
                return nomDomaine;
            }
            set
            {
                if (value == "")
                    throw new Exception("Domaine vide");
                nomDomaine = value;
            }
        }
        #endregion

        #region opérations

        /// <summary>
        /// Créer un Domaine
        /// </summary>
        /// <param name="n">(facultatif) le nom du domaine</param>
        public Domaine(string n = "")
        {
            nomDomaine = n;
        }

        /// <summary>
        /// attribut le domaine aux bouteilles
        /// </summary>
        /// <param name="b">bouteilles qui vont recevoir le domaine</param>
        public void Ajouter(Bouteilles b)
        {
            foreach(Bouteille bouteille in b.Lister())
            {
                bouteille.Domaine = this;
                Ajouter(bouteille);
            }
        }

        /// <summary>
        /// Modifie le domaine de la bouteille
        /// </summary>
        /// <param name="b">bouteille où l'on doit modifier le domaine</param>
        public void Ajouter(Bouteille b)
        {
            bouteilles.Add(b);   
        }

        /// <summary>
        /// retourne le tableau des bouteilles
        /// </summary>
        /// <returns></returns>
        public Bouteille[] Lister()
        {
            return bouteilles.ToArray();
        }

        /// <summary>
        /// Supprime le domaine de la bouteille
        /// </summary>
        /// <param name="b"></param>
        public void Supprimer(Bouteille b)
        {
            bouteilles.Remove(b);
        }
        #endregion
    }
}
