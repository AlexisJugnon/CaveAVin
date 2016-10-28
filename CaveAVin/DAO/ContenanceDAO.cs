using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metier;
using System.Data;
using System.Diagnostics;

namespace DAO
{
    class ContenanceDAO:Metier.IContenanceDAO
    {
        private IDbConnection con;

        /// <summary>
        /// Initialise l'objet DAO
        /// </summary>
        /// <param name="c">la connexion à utiliser</param>
        public ContenanceDAO(IDbConnection c)
        {
            Debug.Assert(c != null);
            con = c;
        }

        public Contenance Chercher(int ID)
        {
            Contenance c = null;

            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM CONTENANCE WHERE IdContenance=" + ID.ToString();
                IDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    c = reader2Contenance(reader);
                }
            }
            finally
            {
                con.Close();
            }

            return c;

        }

        public void Créer(Contenance c)
        {
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "INSERT INTO Contenance(valeur) VALUES('" + c.Valeur + "');";
                com.ExecuteNonQuery();
                com.CommandText = "SELECT LAST_INSERT_ID() FROM Contenance;";
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

        public Contenances Lister()
        {
            Contenances liste = new Contenances();
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Contenance";
                IDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Contenance c = reader2Contenance(reader);
                    liste.Ajouter(c);
                }
            }
            finally
            {
                con.Close();
            }
            return liste;
        }

        public Contenances Lister(Cave c)
        {
            Contenances liste = new Contenances();
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Contenance WHERE IdContenance=" + c.Id.ToString();
                IDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Contenance cd = reader2Contenance(reader);
                    liste.Ajouter(cd);
                }
            }
            finally
            {
                con.Close();
            }
            return liste;
        }

        public void Relire(Contenance c)
        {
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Contenance WHERE IdContenance=" + c.Id.ToString();
                IDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                   // c.Valeur = reader["valeur"].ToString();  //je sais pas pourquoi cette ligne ne fonctionne pas ? help ?
                }
            }
            finally
            {
                con.Close();
            }
        }

        public void Sauver(Contenance c)
        {
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "UPDATE Contenance SETvaleur='" + c.Valeur + "' WHERE IdContenance=" + c.Id.ToString();
                com.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        public void Supprimer(Contenance c)
        {
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "DELETE FROM Contenance WHERE IdContenance=" + c.Id.ToString();
                com.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        private Contenance reader2Contenance(IDataReader reader)
        {
            Contenance c = new Contenance();
            c.Id = Convert.ToInt32(reader["IdContenance"]);
            //c.Valeur = reader["valeur"].ToString(); //je sais pas pourquoi cette ligne ne fonctionne pas ? help ?
            return c;
        }
    }

}