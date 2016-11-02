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
    public class DomaineDAO:Metier.IDomaineDAO
    {
        private IDbConnection con;

        /// <summary>
        /// Initialise l'objet DAO
        /// </summary>
        /// <param name="c">la connexion à utiliser</param>
        public DomaineDAO(IDbConnection c)
        {
            Debug.Assert(c != null);
            con = c;
        }

        public Domaine Chercher(int ID)
        {
            Domaine d = null;

            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM DOMAINE WHERE IdDomaine=" + ID.ToString();
                IDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    d = reader2Domaine(reader);
                }
            }
            finally
            {
                con.Close();
            }

            return d;

        }

        public void Créer(Domaine d)
        {
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "INSERT INTO Domaine(NomDomaine) VALUES('" + d.NomDomaine + "');";
                com.ExecuteNonQuery();
                com.CommandText = "SELECT LAST_INSERT_ID() FROM Domaine;";
                IDataReader reader = com.ExecuteReader();
                int id = 1;
                if (reader.Read())
                    id = Convert.ToInt32(reader[0]);
                d.Id = id;
            }
            finally
            {
                con.Close();
            }
        }

        public Domaines Lister()
        {
            Domaines liste = new Domaines();
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Domaine";
                IDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Domaine d = reader2Domaine(reader);
                    liste.Ajouter(d);
                }
            }
            finally
            {
                con.Close();
            }
            return liste;
        }

        public Domaines Lister(Cave c)
        {
            Domaines liste = new Domaines();
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Domaine WHERE IdCave="+c.Id.ToString();
                IDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Domaine d = reader2Domaine(reader);
                    liste.Ajouter(d);
                }
            }
            finally
            {
                con.Close();
            }
            return liste;
        }

        public void Relire(Domaine d)
        {
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Domaine WHERE IdDomaine=" + d.Id.ToString();
                IDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                    d.NomDomaine = reader["NomDomaine"].ToString();
                }
            }
            finally
            {
                con.Close();
            }
        }

        public void Sauver(Domaine d)
        {
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "UPDATE Domaine SETNomDomaine='" + d.NomDomaine + "' WHERE IdDomaine=" + d.Id.ToString();
                com.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        public void Supprimer(Domaine d)
        {
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "DELETE FROM Domaine WHERE IdDomaine=" + d.Id.ToString();
                com.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        private Domaine reader2Domaine(IDataReader reader)
        {
            Domaine d = new Domaine();
            d.Id = Convert.ToInt32(reader["IdDomaine"]);
            d.NomDomaine = reader["NomDomaine"].ToString();
            return d;
        }
    }

}
