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
    public class MillesimeDAO : Metier.IMillesimeDAO
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
            Millesime m = null;

            if (con.State != ConnectionState.Open)
                con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Millesime WHERE IdMillesime=" + ID.ToString();
                IDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    m = reader2Millesime(reader);
                }
            }
            finally
            {
                con.Close();
            }

            return m;

        }

        public Millesime Chercher(int nom, string car)
        {
            Millesime m = null;

            if (con.State != ConnectionState.Open)
                con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Millesime WHERE NomMillesime='" + nom + "'";
                IDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    m = reader2Millesime(reader);
                }
            }
            finally
            {
                con.Close();
            }

            return m;

        }

        public void Créer(Millesime p)
        {
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "INSERT INTO Millesime(NomMillesime) VALUES('" + p.NomMillesime + "');";
                com.ExecuteNonQuery();
                com.CommandText = "SELECT LAST_INSERT_ID() FROM Millesime;";
                IDataReader reader = com.ExecuteReader();
                int id = 1;
                if (reader.Read())
                    id = Convert.ToInt32(reader[0]);
                p.Id = id;
            }
            finally
            {
                con.Close();
            }
        }

        public void Créer(int val)
        {
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "INSERT INTO Millesime(NomMillesime) VALUES('" + val + "');";
                com.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        public Millesimes Lister()
        {
            Millesimes liste = new Millesimes();
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Millesime";
                IDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Millesime m = reader2Millesime(reader);
                    liste.Ajouter(m);
                }
            }
            finally
            {
                con.Close();
            }
            return liste;
        }

        public void Relire(Millesime p)
        {
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Millesime WHERE IdMillesime=" + p.Id.ToString();
                IDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                    p.NomMillesime = reader["NomMillesime"].ToString();
                }
            }
            finally
            {
                con.Close();
            }
        }

        public void Sauver(Millesime p)
        {
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "UPDATE Millesime SETNomMillesime='" + p.NomMillesime + "' WHERE IdMillesime=" + p.Id.ToString();
                com.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        public void Supprimer(Millesime p)
        {
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "DELETE FROM Millesime WHERE IdMillesime=" + p.Id.ToString();
                com.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        private Millesime reader2Millesime(IDataReader reader)
        {
            Millesime m = new Millesime();
            m.Id = Convert.ToInt32(reader["IdMillesime"]);
            m.NomMillesime = reader["NomMillesime"].ToString();
            return m;
        }
    }
}
