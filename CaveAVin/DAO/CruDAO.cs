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
    public class CruDAO : Metier.ICruDAO
    {
        private IDbConnection con;
        /// <summary>
        /// Initialise l'objet DAO
        /// </summary>
        /// <param name="c">la connexion à utiliser</param>
        public CruDAO(IDbConnection c)
        {
            Debug.Assert(c != null);
            con = c;
        }

        /// <summary>
        /// Cherche un cru en fonction de son id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Cru Chercher(int Id)
        {
            Cru cru = null;

            if (con != null)
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                try
                {
                    IDbCommand com = con.CreateCommand();
                    com.CommandText = "SELECT * FROM Cru WHERE IdCru=" + Id.ToString();
                    IDataReader reader = com.ExecuteReader();

                    if (reader.Read())
                    {
                        cru = reader2Cru(reader);
                    }
                }
                finally
                {
                    con.Close();
                } 
            }

            return cru;

        }

        /// <summary>
        /// Cherche un cru en fonction de son id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Cru Chercher(string nom)
        {
            Cru cru = null;

            if (con != null)
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                try
                {
                    IDbCommand com = con.CreateCommand();
                    com.CommandText = "SELECT * FROM Cru WHERE NomCru='" + nom + "'";
                    IDataReader reader = com.ExecuteReader();

                    if (reader.Read())
                    {
                        cru = reader2Cru(reader);
                    }
                }
                finally
                {
                    con.Close();
                }
            }

            return cru;

        }

        /// <summary>
        /// Insert un nouveau cru
        /// </summary>
        /// <param name="cru">Cru à insérer en base</param>
        public void Créer(Cru cru)
        {
            if (con != null)
            {
                if(con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                try
                {
                    var command = con.CreateCommand();
                    command.CommandText = String.Format("INSERT INTO Cru(NomCru) VALUES('{0}');", cru.NomCru);
                    command.ExecuteNonQuery();
                    command.CommandText = "SELECT LAST_INSERT_ID() FROM Cru;";
                    IDataReader reader = command.ExecuteReader();
                    int id = 1;
                    if (reader.Read())
                        Int32.TryParse(reader[0].ToString(), out id);
                    cru.Id = id;
                }
                finally
                {
                    con.Close();
                } 
            }
        }

        /// <summary>
        /// Insert un nouveau cru
        /// </summary>
        /// <param name="cru">Cru à insérer en base</param>
        public void Créer(string nom)
        {
            if (con != null)
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                try
                {
                    var command = con.CreateCommand();
                    command.CommandText = String.Format("INSERT INTO Cru(NomCru) VALUES('{0}');", nom);
                    command.ExecuteNonQuery();
                }
                finally
                {
                    con.Close();
                }
            }
        }

        /// <summary>
        /// Liste tous les crus de la base
        /// </summary>
        /// <returns>Crus contenu en base</returns>
        public Crus Lister()
        {
            var crus = new Crus();

            if (con != null)
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                try
                {
                    var command = con.CreateCommand();
                    command.CommandText = "SELECT * FROM Cru";
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var cru = reader2Cru(reader);
                        crus.Ajouter(cru);
                    }
                }
                finally
                {
                    con.Close();
                }
            }

            return crus;
        }

        /// <summary>
        /// Reprend les données d'un cru en base
        /// </summary>
        /// <param name="cru">Cru à reprendre</param>
        public void Relire(Cru cru)
        {
            if (con != null)
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                try
                {
                    var command = con.CreateCommand();
                    command.CommandText = String.Format("SELECT * FROM CRU WHERE IdCru={0}", cru.Id);
                    var reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        cru.NomCru = reader["NomCru"].ToString();
                    }
                }
                finally
                {
                    con.Close();
                } 
            }
        }

        /// <summary>
        /// Sauvegarde un cru déjà existant en base
        /// </summary>
        /// <param name="cru">Cru à sauvegarder</param>
        public void Sauver(Cru cru)
        {
            if (con != null)
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                try
                {
                    var command = con.CreateCommand();
                    command.CommandText = String.Format("UPDATE Millesime SET NomCru='{0}' WHERE IdCru={1}", cru.NomCru, cru.Id);
                    command.ExecuteNonQuery();
                }
                finally
                {
                    con.Close();
                } 
            }
        }

        /// <summary>
        /// Retire un cru de la base
        /// </summary>
        /// <param name="p">Cru à supprimer</param>
        public void Supprimer(Cru p)
        {
            if (con != null)
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                try
                {
                    var command = con.CreateCommand();
                    command.CommandText = String.Format("DELETE FROM Cru WHERE IdCru={0}", p.Id);
                    command.ExecuteNonQuery();
                }
                finally
                {
                    con.Close();
                } 
            }
        }

        /// <summary>
        /// Converti une ligne de retour de requête en un cru
        /// </summary>
        /// <param name="reader">Reader representant une ligne de réponse</param>
        /// <returns>Cru initialisé</returns>
        private Cru reader2Cru(IDataReader reader)
        {
            var cru = new Cru();
            int id;
            if(Int32.TryParse(reader["IdCru"].ToString(), out id))
            {
                cru.Id = id;
            }
            cru.NomCru = reader["NomCru"].ToString();
            return cru;
        }
    }
}
