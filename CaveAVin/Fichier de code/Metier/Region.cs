using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Region : Modification
    {
        #region attributs
        private int id;
        private string nomRegion;
        private Pays pays;
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
        public string NomRegion
        {
            get
            {
                return nomRegion;
            }
            set
            {
                nomRegion = value;
            }
        }
        public Pays Pays
        {
            get
            {
                return pays;
            }
            set
            {
                pays = value;
            }
            
        }

        public int IdPays
        {
            get
            {
                int id = 0;
                if (pays != null)
                    id = pays.Id;
                return id;
            }
        }
        #endregion

        #region méthodes

        /// <summary>
        /// attributions de la région aux bouteilles et ajoute les bouteilles où la région est modifiée
        /// </summary>
        /// <param name="b">bouteilles qui vont prendre la région</param>
        public void Ajouter(Bouteilles b)
        {
            foreach (Bouteille bouteille in b.Lister())
            {
                Ajouter(bouteille);
            }
        }

        /// <summary>
        /// attribution de la région à la bouteille et ajoute la bouteille où la région est modifiée
        /// </summary>
        /// <param name="b">la bouteille à modifier</param>
        public void Ajouter(Bouteille b)
        {
            b.Region = this;
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
        /// Supprime la région si la bouteille à modifier a été trouvé.
        /// </summary>
        /// <param name="b">la bouteille ou on doit retirer la région</param>
        public void Supprimer(Bouteille b)
        {
            //recherche la bouteille à supprimer dans la liste interne
            var bouteilleASupprimer = bouteilles.FirstOrDefault(bouteille => bouteille.Id == b.Id);

            //si la bouteille est trouvée
            if (bouteilleASupprimer != null)
            {
                //on supprime la région pour la bouteille
                bouteilleASupprimer.Region = null;
                //on supprime la bouteille de la liste interne
                bouteilles.Remove(bouteilleASupprimer);
            }
        }
        #endregion
    }
}