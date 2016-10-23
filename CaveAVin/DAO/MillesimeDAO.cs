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
    class MillesimeDAO:Metier.IMillesimeDAO
    {
        private IDbConnection con;
        /// <summary>
        /// Initialise l'objet DAO
        /// </summary>
        /// <param name="c">la connexion à utiliser</param>
        public MillesimeDAO(IDbConnection c)
        {
            Debug.Assert(c != null);
            con = c;
        }

        public Millesime Chercher(int ID)
        {
            throw new NotImplementedException();
        }

        public void Créer(Millesime p)
        {
            throw new NotImplementedException();
        }

        public Millesimes Lister()
        {
            throw new NotImplementedException();
        }

        public void Relire(Millesime p)
        {
            throw new NotImplementedException();
        }

        public void Sauver(Millesime p)
        {
            throw new NotImplementedException();
        }

        public void Supprimer(Millesime p)
        {
            throw new NotImplementedException();
        }
    }
}
