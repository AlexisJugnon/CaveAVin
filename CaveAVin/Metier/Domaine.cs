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
                nomDomaine = value;
            }
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
            Bouteilles liste = new Bouteilles();
            int indexTableau = 0;
            foreach(Bouteille bouteille in liste.Lister())
            {
                if(bouteille.Id == b.Id )
                {
                    liste.Lister().SetValue(b,indexTableau);
                }
                indexTableau += 1;
            }
           
        }

        /// <summary>
        /// retourne le tableau des bouteilles
        /// </summary>
        /// <returns></returns>
        public Bouteille[] Lister()
        {
            Bouteilles liste  = new Bouteilles();
            return liste.Lister();
        }

        /// <summary>
        /// Supprime le domaine de la bouteille
        /// </summary>
        /// <param name="b"></param>
        public void Supprimer(Bouteille b)
        {
            Bouteilles liste = new Bouteilles();
            if (liste.Lister().Contains(b))
            {
                foreach (Bouteille bouteille in liste.Lister())
                {
                    if(bouteille.Id == b.Id)
                    {
                        bouteille.Domaine = null;
                    }

                }
            }
        }
        #endregion
    }
}
