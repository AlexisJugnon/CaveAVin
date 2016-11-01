using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class BDD
    {
        
        private static BDD instance;
        private BDD()
        {
            
            con = new MySqlConnection();
        }
        public static BDD Instance
        {
            get
            {
                if (instance == null)
                    instance = new BDD();
                return instance;
            }
        }

        /// <summary>
        /// Obtient la connexion à la base de données
        /// </summary>
        public IDbConnection Connexion
        {
            get
            {
                return con;
            }
        }

        private IDbConnection con;

    }
}
