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
    class CaveDAO:Metier.ICaveDAO
    {
        private IDbConnection con;
        /// <summary>
        /// Initialise l'objet DAO
        /// </summary>
        /// <param name="c">la connexion à utiliser</param>
        public CaveDAO(IDbConnection c)
        {
            Debug.Assert(c != null);
            con = c;
        }

        public Cave Chercher(int ID)
        {
            throw new NotImplementedException();
        }

        public void Créer(Cave p)
        {
            throw new NotImplementedException();
        }

        public Caves Lister()
        {
            throw new NotImplementedException();
        }

        public void Relire(Cave p)
        {
            throw new NotImplementedException();
        }

        public void Sauver(Cave p)
        {
            throw new NotImplementedException();
        }

        public void Supprimer(Cave p)
        {
            throw new NotImplementedException();
        }
        private Cave reader2Cave(IDataReader reader)
        {
            return null;
        }
    }
}
