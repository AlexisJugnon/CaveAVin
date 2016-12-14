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
    public class CaveDAO:Metier.ICaveDAO
    {
        private IDbConnection con;
        /// <summary>
        /// Initialise l'objet DAO
        /// </summary>
        /// <param name="c">la connexion à utiliser</param>
        public CaveDAO(IDbConnection c)
        {
            Debug.Assert(c != null);
            con = c;
        }

        public Cave Chercher(int ID)
        {
            Cave c = null;

            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Cave WHERE IdCave=" + ID.ToString();
                IDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    c = reader2Cave(reader);
                }
            }
            finally
            {
                con.Close();
            }

            return c;

        }

        public void Créer(Cave c)
        {
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "INSERT INTO Cave(NomCave) VALUES('" + c.Nom + "');";
                com.ExecuteNonQuery();
                com.CommandText = "SELECT LAST_INSERT_ID() FROM Cave;";
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

        public Caves Lister()
        {
            Caves liste = new Caves();
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Cave";
                IDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Cave c = reader2Cave(reader);
                    liste.Ajouter(c);
                }
            }
            finally
            {
                con.Close();
            }
            return liste;
        }

        public void Relire(Cave c)
        {
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Cave WHERE IdCave=" + c.Id.ToString();
                IDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                    c.Nom = reader["NomCave"].ToString();
                }
            }
            finally
            {
                con.Close();
            }
        }

        public void Sauver(Cave c)
        {
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "UPDATE Cave SETNomCave='" + c.Nom + "' WHERE IdCave=" + c.Id.ToString();
                com.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        public void Supprimer(Cave c)
        {
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "DELETE FROM Cave WHERE IdCave=" + c.Id.ToString();
                com.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        private Cave reader2Cave(IDataReader reader)
        {
            Cave c = new Cave();
            c.Id = Convert.ToInt32(reader["IdCave"]);
            c.Nom = reader["NomCave"].ToString();
            c.Adresse= reader["Adresse"].ToString();
            c.Piece= reader["Piece"].ToString();
            return c;
        }
    }
}
