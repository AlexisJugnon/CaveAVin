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
    public class CasierDAO:Metier.ICasierDAO
    {
        private IDbConnection con;
        /// <summary>
        /// Initialise l'objet DAO
        /// </summary>
        /// <param name="c">la connexion à utiliser</param>
        public CasierDAO(IDbConnection c)
        {
            Debug.Assert(c != null);
            con = c;
        }

        public Casier Chercher(int ID)
        {
            throw new NotImplementedException();
        }

        public void Créer(Casier p)
        {
            throw new NotImplementedException();
        }

        public Casiers Lister()
        {
            throw new NotImplementedException();
        }

        public void Relire(Casier p)
        {
            throw new NotImplementedException();
        }

        public void Sauver(Casier p)
        {
            throw new NotImplementedException();
        }

        public void Supprimer(Casier p)
        {
            throw new NotImplementedException();
        }
        private Casier reader2Casier(IDataReader reader)
        {
            return null;
        }
    }
}
