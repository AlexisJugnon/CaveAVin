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
        public void Ajouter(Bouteille b)
        {
            bouteilles.Add(b);
        }

        public void Ajouter(Bouteilles b)
        {
            foreach (Bouteille bt in b.Lister())
            {
                bt.Type = this;
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
