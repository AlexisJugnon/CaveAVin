using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Casier : Modification
    {
        #region attributs
        private int id;
        private string nom;
        private int largeurX;
        private int largeurY;
        private Cave cave;
        private List<Bouteille> bouteilles = new List<Bouteille>();
        #endregion

        #region opérations

            public Casier(string nom= "")
            {
            nom = n;
        }
            public void Ajouter(Bouteille b)
            {
                bouteilles.Add(b);
            }

            public void Ajouter(Bouteilles b)
            {
                foreach(Bouteille bt in b.Lister())
                {
                    bt.Casier = this;
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

        #region proriétés
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
        public string Nom
        {
            get
            {
                return nom;
            }
            set
            {
                nom = value;
            }
        }
        public int LargeurX
        {
            get
            {
                return largeurX;
            }
            set
            {
                largeurX = value;
            }
        }
        public int LargeurY
        {
            get
            {
                return largeurY;
            }
            set
            {
                largeurY = value;
            }
        }

        public Cave Cave
        {
            get
            {
                return cave;
            }
            set
            {
                cave = value;
            }
        }

        public int IdCave {
            get
            {
                int id = 0;
                if (cave != null)
                    id = cave.Id;
                return id;
            }
        }
        #endregion
    }
}
