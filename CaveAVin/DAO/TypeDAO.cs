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
    class TypeDAO:Metier.ITypeDAO
    {
        private IDbConnection con;
        /// <summary>
        /// Initialise l'objet DAO
        /// </summary>
        /// <param name="c">la connexion à utiliser</param>
        public TypeDAO(IDbConnection c)
        {
            Debug.Assert(c != null);
            con = c;
        }

        public Metier.Type Chercher(int ID)
        {
            throw new NotImplementedException();
        }

        public void Créer(Metier.Type p)
        {
            throw new NotImplementedException();
        }

        public Types Lister()
        {
            throw new NotImplementedException();
        }

        public void Relire(Metier.Type p)
        {
            throw new NotImplementedException();
        }

        public void Sauver(Metier.Type p)
        {
            throw new NotImplementedException();
        }

        public void Supprimer(Metier.Type p)
        {
            throw new NotImplementedException();
        }
        private Metier.Type reader2Type(IDataReader reader)
        {
            return null;
        }
    }
}
