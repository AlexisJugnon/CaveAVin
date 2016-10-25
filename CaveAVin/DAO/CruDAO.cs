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
    class CruDAO : Metier.ICruDAO
    {
        private IDbConnection con;
        /// <summary>
        /// Initialise l'objet DAO
        /// </summary>
        /// <param name="c">la connexion à utiliser</param>
        public CruDAO(IDbConnection c)
        {
            Debug.Assert(c != null);
            con = c;
        }

        public Cru Chercher(int ID)
        {
            throw new NotImplementedException();
        }

        public void Créer(Cru p)
        {
            throw new NotImplementedException();
        }

        public Crus Lister()
        {
            throw new NotImplementedException();
        }

        public void Relire(Cru p)
        {
            throw new NotImplementedException();
        }

        public void Sauver(Cru p)
        {
            throw new NotImplementedException();
        }

        public void Supprimer(Cru p)
        {
            throw new NotImplementedException();
        }
        private Cru reader2Cru(IDataReader reader)
        {
            return null;
        }
    }
}
