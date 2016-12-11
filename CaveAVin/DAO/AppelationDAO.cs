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
    public class AppelationDAO:Metier.IAppelationDAO
    {
        private IDbConnection con;

        /// <summary>
        /// Initialise l'objet DAO
        /// </summary>
        /// <param name="c">la connexion à utiliser</param>
        public AppelationDAO(IDbConnection c)
        {
            Debug.Assert(c != null);
            con = c;
        }

        public Appelation Chercher(int ID)
        {
            Appelation a = null;

            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Appelation WHERE IdAppelation=" + ID.ToString();
                IDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    a = reader2Appelation(reader);
                }
            }
            finally
            {
                con.Close();
            }

            return a;

        }

        public Appelation Chercher(string nom)
        {
            Appelation a = null;

            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Appelation WHERE NomAppelation='" + nom + "'";
                IDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    a = reader2Appelation(reader);
                }
            }
            finally
            {
                con.Close();
            }

            return a;

        }

        public void Créer(Appelation a)
        {
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "INSERT INTO Appelation(NomAppelation) VALUES('" + a.NomAppelation + "');";
                com.ExecuteNonQuery();
                com.CommandText = "SELECT LAST_INSERT_ID() FROM Appelation;";
                IDataReader reader = com.ExecuteReader();
                int id = 1;
                if (reader.Read())
                    id = Convert.ToInt32(reader[0]);
                a.Id = id;
            }
            finally
            {
                con.Close();
            }
        }

        public void Créer(string nom)
        {
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "INSERT INTO Appelation(NomAppelation) VALUES('" + nom + "');";
                com.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        public Appelations Lister()
        {
            Appelations liste = new Appelations();
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Appelation";
                IDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Appelation a = reader2Appelation(reader);
                    liste.Ajouter(a);
                }
            }
            finally
            {
                con.Close();
            }
            return liste;
        }

        public void Relire(Appelation a)
        {
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Appelation WHERE IdAppelation=" + a.Id.ToString();
                IDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                    a.NomAppelation = reader["NomAppelation"].ToString();
                }
            }
            finally
            {
                con.Close();
            }
        }

        public void Sauver(Appelation a)
        {
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "UPDATE Appelation SETNomAppelation='" + a.NomAppelation + "' WHERE IdAppelation=" + a.Id.ToString();
                com.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        public void Supprimer(Appelation a)
        {
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "DELETE FROM Appelation WHERE IdAppelation=" + a.Id.ToString();
                com.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        private Appelation reader2Appelation(IDataReader reader)
        {
            Appelation a = new Appelation();
            a.Id = Convert.ToInt32(reader["IdAppelation"]);
            a.NomAppelation = reader["NomAppelation"].ToString();
            return a;
        }
    }
}