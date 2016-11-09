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
