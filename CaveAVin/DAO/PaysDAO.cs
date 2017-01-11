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
    public class PaysDAO:Metier.IPaysDAO
    {
        private IDbConnection con;
        /// <summary>
        /// Initialise l'objet DAO
        /// </summary>
        /// <param name="c">la connexion à utiliser</param>
        public PaysDAO(IDbConnection c)
        {
            Debug.Assert(c != null);
            con = c;
        }

        public Pays Chercher(int ID)
        {
            Pays c = null;

            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Pays WHERE IdPays=" + ID.ToString();
                IDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    c = reader2Pays(reader);
                }
            }
            finally
            {
                con.Close();
            }

            return c;
        }

        public Pays Chercher(string nom)
        {
            Pays c = null;

            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Pays WHERE NomPays='" + nom + "'";
                IDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    c = reader2Pays(reader);
                }
            }
            finally
            {
                con.Close();
            }

            return c;
        }

        public void Créer(Pays c)
        {
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "INSERT INTO Pays(NomPays) VALUES('" + c.NomPays + "');";
                com.ExecuteNonQuery();
                com.CommandText = "SELECT LAST_INSERT_ID() FROM Pays;";
                IDataReader reader = com.ExecuteReader();
                int id = 1;
                if (reader.Read())
                    id = Convert.ToInt32(reader[0]);
                c.Id = id;
            }
            finally
            {
                con.Close();
            }
        }


        public void Créer(string nom)
        {
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "INSERT INTO Pays(NomPays) VALUES('" + nom + "');";
                com.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        public Pays2 Lister()
        {
            Pays2 liste = new Pays2();
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Pays";
                IDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Pays c = reader2Pays(reader);
                    liste.Ajouter(c);
                }
            }
            finally
            {
                con.Close();
            }
            return liste;
        }

        public void Relire(Pays c)
        {
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Pays WHERE IdPays=" + c.Id.ToString();
                IDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                    c.NomPays = reader["NomPays"].ToString();
                }
            }
            finally
            {
                con.Close();
            }
        }

        public void Sauver(Pays c)
        {
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "UPDATE Pays SETNomPays='" + c.NomPays + "' WHERE IdPays=" + c.Id.ToString();
                com.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        public void Supprimer(Pays c)
        {
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "DELETE FROM Pays WHERE IdPays=" + c.Id.ToString();
                com.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        private Pays reader2Pays(IDataReader reader)
        {
            Pays c = new Pays();
            c.Id = Convert.ToInt32(reader["IdPays"]);
            c.NomPays = reader["NomPays"].ToString();
            return c;
        }
    }
}
