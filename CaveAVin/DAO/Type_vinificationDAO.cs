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
    class Type_vinificationDAO:Metier.IType_vinificationDAO
    {
        private IDbConnection con;
        /// <summary>
        /// Initialise l'objet DAO
        /// </summary>
        /// <param name="c">la connexion à utiliser</param>
        public Type_vinificationDAO(IDbConnection c)
        {
            Debug.Assert(c != null);
            con = c;
        }

        public Type_vinification Chercher(int ID)
        {
            throw new NotImplementedException();
        }

        public void Créer(Type_vinification p)
        {
            throw new NotImplementedException();
        }

        public Type_vinifications Lister()
        {
            throw new NotImplementedException();
        }

        public void Relire(Type_vinification p)
        {
            throw new NotImplementedException();
        }

        public void Sauver(Type_vinification p)
        {
            throw new NotImplementedException();
        }

        public void Supprimer(Type_vinification p)
        {
            throw new NotImplementedException();
        }
    }
}
