using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metier;
using System.Data;
using System.Diagnostics;

namespace DAO
{
    /// <summary>
    /// Implémentation du DAO produit en utilisant un SGBD
    /// </summary>
    /// <see cref="Bouteille"/> 
    public class BouteilleDAO : Metier.IBouteilleDAO
    {
        private IDbConnection con;
        /// <summary>
        /// Initialise l'objet DAO
        /// </summary>
        /// <param name="c">la connexion à utiliser</param>
        public BouteilleDAO(IDbConnection c)
        {
            Debug.Assert(c != null);
            con = c;
        }

        public Bouteille Chercher(int ID)
        {
            throw new NotImplementedException();
        }

        public void Créer(Bouteille p)
        {
            throw new NotImplementedException();
        }

        public Bouteille Lister()
        {
            throw new NotImplementedException();
        }

        public void Relire(Bouteille p)
        {
            throw new NotImplementedException();
        }

        public void Sauver(Bouteille p)
        {
            throw new NotImplementedException();
        }

        public void Supprimer(Bouteille p)
        {
            throw new NotImplementedException();
        }
    }
}
