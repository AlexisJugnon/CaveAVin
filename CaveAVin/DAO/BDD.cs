using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using System.Data;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace DAO
{
    /// <summary>
    /// Connexion à la base de données
    /// </summary>

    public class BDD
    {
        private static BDD instance;
        private string database = "WineFinder";
        private string serveur = "137.74.233.210";
        private string login = "user";
        private string pass = "user";

        private BDD()
        {
            init();
        }

        public static BDD Instance
        {
            get
            {
                if (instance == null)
                {
                    try
                    {
                        instance.init();
                    }
                    catch
                    {
                        instance = new BDD();
                    }
                }
                return instance;
            }
        }

        private void init()
        {
            con = new MySql.Data.MySqlClient.MySqlConnection();
            con.ConnectionString = ConnectionString;
        }


        public bool Connectée
        {
            get
            {
                return con != null && con.State == ConnectionState.Open;
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

        private string ConnectionString
        {
            get
            {
                return "Database=" + database + ";DataSource=" + serveur + ";User Id=" + login + "; Password=" + pass;
            }
        }
        private IDbConnection con;

    }
}
