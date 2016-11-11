using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
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
        private BDD()
        {
            init();
        }

        private void init()
        {
            con = new MySqlConnection();
            con.ConnectionString = ConnectionString;
        }

        public static BDD Instance
        {
            get
            {
                if (instance == null)
                {
                    try
                    {
                        using (Stream flux = new FileStream("params.dat", FileMode.Open))
                        {
                            IFormatter formater = new BinaryFormatter();
                            instance = (BDD)formater.Deserialize(flux);
                            instance.init();
                        }
                    }
                    catch
                    {
                        instance = new BDD();
                    }
                }
                return instance;
            }
        }

        public static void Sauver()
        {
            IFormatter formater = new BinaryFormatter();
            using (Stream flux = new FileStream("params.dat", FileMode.Create))
            {
                formater.Serialize(flux, instance);
            }
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

        public string Serveur
        {
            get
            {
                return serveur;
            }

            set
            {
                con.Close();
                serveur = value;
                con.ConnectionString = ConnectionString;
            }
        }

        public string Database
        {
            get
            {
                return database;
            }

            set
            {
                con.Close();
                database = value;
                con.ConnectionString = ConnectionString;
            }
        }

        public string Login
        {
            get
            {
                return login;
            }

            set
            {
                con.Close();
                login = value;
                con.ConnectionString = ConnectionString;
            }
        }

        public string Pass
        {
            get
            {
                return pass;
            }

            set
            {
                con.Close();
                pass = value;
                con.ConnectionString = ConnectionString;
            }
        }

        private string ConnectionString
        {
            get
            {
                return "Database=WineFinder;DataSource=137.74.233.210;User Id=user; Password=user";
            }
        }

        private string serveur;
        private string database;
        private string login;
        private string pass;

        [NonSerialized]
        private IDbConnection con;

    }
}
