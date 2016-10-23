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
    class ContenanceDAO:Metier.IContenanceDAO
    {
        private IDbConnection con;
        /// <summary>
        /// Initialise l'objet DAO
        /// </summary>
        /// <param name="c">la connexion à utiliser</param>
        public ContenanceDAO(IDbConnection c)
        {
            Debug.Assert(c != null);
            con = c;
        }

        public Contenance Chercher(int ID)
        {
            throw new NotImplementedException();
        }

        public void Créer(Contenance p)
        {
            throw new NotImplementedException();
        }

        public Contenances Lister()
        {
            throw new NotImplementedException();
        }

        public void Relire(Contenance p)
        {
            throw new NotImplementedException();
        }

        public void Sauver(Contenance p)
        {
            throw new NotImplementedException();
        }

        public void Supprimer(Contenance p)
        {
            throw new NotImplementedException();
        }
    }
}
