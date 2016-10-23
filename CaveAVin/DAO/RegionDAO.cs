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
    class RegionDAO:Metier.IRegionDAO
    {
        private IDbConnection con;
        /// <summary>
        /// Initialise l'objet DAO
        /// </summary>
        /// <param name="c">la connexion à utiliser</param>
        public RegionDAO(IDbConnection c)
        {
            Debug.Assert(c != null);
            con = c;
        }

        public Region Chercher(int ID)
        {
            throw new NotImplementedException();
        }

        public void Créer(Region p)
        {
            throw new NotImplementedException();
        }

        public Regions Lister()
        {
            throw new NotImplementedException();
        }

        public void Relire(Region p)
        {
            throw new NotImplementedException();
        }

        public void Sauver(Region p)
        {
            throw new NotImplementedException();
        }

        public void Supprimer(Region p)
        {
            throw new NotImplementedException();
        }
    }
}
