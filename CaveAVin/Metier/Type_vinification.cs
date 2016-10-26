using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Type_vinification : Modification
    {
        #region attributs
        private int id;
        private string nomVinif;
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
        public string NomVinif
        {
            get
            {
                return nomVinif;
            }
            set
            {
                nomVinif = value;
            }
        }
        #endregion

        #region méthodes
        /// <summary>
        /// attributions du type de vinification aux bouteilles et ajoute les bouteilles où le type de vinification est modifié
        /// </summary>
        /// <param name="b">bouteilles qui vont prendre le type de vinification</param>
        public void Ajouter(Bouteilles b)
        {
            foreach (Bouteille bouteille in b.Lister())
            {
                Ajouter(bouteille);
            }
        }

        /// <summary>
        /// attribution du type de vinification à la bouteille et ajoute la bouteille où le type de vinification est modifié
        /// </summary>
        /// <param name="b">la bouteille à modifier</param>
        public void Ajouter(Bouteille b)
        {
            b.Type_vinification = this;
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
        /// Supprime le type vinification si la bouteille à modifier a été trouvé.
        /// </summary>
        /// <param name="b">la bouteille ou on doit retirer le type de vinification</param>
        public void Supprimer(Bouteille b)
        {
            //recherche la bouteille à supprimer dans la liste interne
            var bouteilleASupprimer = bouteilles.FirstOrDefault(bouteille => bouteille.Id == b.Id);

            //si la bouteille est trouvée
            if (bouteilleASupprimer != null)
            {
                //on supprime le type de vinification pour la bouteille
                bouteilleASupprimer.Type_vinification = null;
                //on supprime la bouteille de la liste interne
                bouteilles.Remove(bouteilleASupprimer);
            }
        }
        #endregion
    }
}
