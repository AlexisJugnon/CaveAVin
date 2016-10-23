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
        #endregion

        #region opérations
        public void Ajouter(Casier r) { }
        public void Ajouter(Casiers r) { }
        public void Supprimer(Casier r) { }
        public Casier[] Lister() { return null; }
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
                piece = value;
            }
        }
        #endregion
    }
}
