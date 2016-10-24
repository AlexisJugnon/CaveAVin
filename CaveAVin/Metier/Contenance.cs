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

        public void Ajouter(Bouteille b)
        {
            bouteilles.Add(b);
        }

        public void Ajouter(Bouteilles b)
        {
            foreach (Bouteille bt in b.Lister())
            {
                bt.Contenance = this;
                Ajouter(bt);
            }
        }

        public void Supprimer(Bouteille b)
        {
            bouteilles.Remove(b);
        }

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
