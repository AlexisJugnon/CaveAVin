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
    class PaysDAO:Metier.IPaysDAO
    {
        private IDbConnection con;
        /// <summary>
        /// Initialise l'objet DAO
        /// </summary>
        /// <param name="c">la connexion à utiliser</param>
        public PaysDAO(IDbConnection c)
        {
            Debug.Assert(c != null);
            con = c;
        }

        public Pays Chercher(int ID)
        {
            throw new NotImplementedException();
        }

        public void Créer(Pays p)
        {
            throw new NotImplementedException();
        }

        public Pays Lister()
        {
            throw new NotImplementedException();
        }

        public void Relire(Pays p)
        {
            throw new NotImplementedException();
        }

        public void Sauver(Pays p)
        {
            throw new NotImplementedException();
        }

        public void Supprimer(Pays p)
        {
            throw new NotImplementedException();
        }
        private Pays reader2Pays(IDataReader reader)
        {
            return null;
        }
    }
}
