using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Cave
    {
        #region attributs
        private int id;
        private string nom;
        private string adresse;
        private string piece;
        private List<Casier> casiers = new List<Casier>();
        #endregion

        #region opérations
        public Cave (string n = "")
        {
            nom = n;
        }

        public void Ajouter(Casier c) {
            casiers.Add(c);
        }

        public void Ajouter(Casiers c) {
            foreach(Casier cr in c.Lister())
            {
                cr.Cave = this;
                Ajouter(cr);
            }
        }

        public void Supprimer(Casier c) {
            casiers.Remove(c);
        }

        public Casier[] Lister() {
            return casiers.ToArray();
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
        public string Nom
        {
            get
            {
                return nom;   
            }
            set
            {
                if (value == "")
                    throw new Exception("Cave vide");
                nom = value;
            }
        }
        public string Adresse
        {
            get
            {
                return adresse;
            }
            set
            {
                if (value == "")
                    throw new Exception("Adresse de cave vide");
                adresse = value;
            }
        }
        public string Piece
        {
            get
            {
                return piece;
            }
            set
            {
                if (value == "")
                    throw new Exception("Pièce de cave vide");
                piece = value;
            }
        }
        #endregion
    }
}
