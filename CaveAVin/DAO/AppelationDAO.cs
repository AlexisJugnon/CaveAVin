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
    class AppelationDAO:Metier.IAppelationDAO
    {
        private IDbConnection con;
        /// <summary>
        /// Initialise l'objet DAO
        /// </summary>
        /// <param name="c">la connexion à utiliser</param>
        public AppelationDAO(IDbConnection c)
        {
            Debug.Assert(c != null);
            con = c;
        }

        public Appelation Chercher(int ID)
        {
            throw new NotImplementedException();
        }

        public void Créer(Appelation p)
        {
            throw new NotImplementedException();
        }

        public Appelations Lister()
        {
            throw new NotImplementedException();
        }

        public void Relire(Appelation p)
        {
            throw new NotImplementedException();
        }

        public void Sauver(Appelation p)
        {
            throw new NotImplementedException();
        }

        public void Supprimer(Appelation p)
        {
            throw new NotImplementedException();
        }
    }
}
