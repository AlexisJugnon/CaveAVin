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
        /// retourne un tableau de bouteille
        /// </summary>
        /// <returns></returns>
        public Bouteille[] Lister()
        {
            Bouteilles liste = new Bouteilles();
            return liste.Lister();
        }

        /// <summary>
        /// retire l'appelation d'une bouteille
        /// </summary>
        /// <param name="b">la bouteille ou on doit retirer une appelation</param>
        public void Supprimer(Bouteille b)
        {
            Bouteilles liste = new Bouteilles();
            if (liste.Lister().Contains(b))
            {
                foreach (Bouteille bouteille in liste.Lister())
                {
                    if(bouteille.Id == b.Id)
                    {
                        bouteille.Appelation = null;
                    }
                }
            }
        }
        #endregion
    }
}
