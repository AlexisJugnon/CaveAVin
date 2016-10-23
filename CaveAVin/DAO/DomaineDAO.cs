using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metier;

namespace DAO
{
    public class DomaineDAO:Metier.IDomaineDAO
    {
        private IDbConnection con;
        /// <summary>
        /// Initialise l'objet DAO
        /// </summary>
        /// <param name="c">la connexion à utiliser</param>
        public DomaineDAO(IDbConnection c)
        {
            Debug.Assert(c != null);
            con = c;
        }

        public Domaine Chercher(int ID)
        {
            throw new NotImplementedException();
        }

        public void Créer(Domaine p)
        {
            throw new NotImplementedException();
        }

        public Domaines Lister()
        {
            throw new NotImplementedException();
        }

        public void Relire(Domaine p)
        {
            throw new NotImplementedException();
        }

        public void Sauver(Domaine p)
        {
            throw new NotImplementedException();
        }

        public void Supprimer(Domaine p)
        {
            throw new NotImplementedException();
        }
    }
}
