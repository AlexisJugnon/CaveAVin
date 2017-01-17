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
    public class CasierDAO : Metier.ICasierDAO
    {

        private IDbConnection con;

        /// <summary>
        /// Initialise l'objet DAO
        /// </summary>
        /// <param name="c">la connexion à utiliser</param>
        public CasierDAO(IDbConnection c)
        {
            Debug.Assert(c != null);
            con = c;
        }

        public Casier Chercher(int ID)
        {
            Casier c = null;

            if(con.State != ConnectionState.Open)
                con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Casier WHERE IdCasier=" + ID.ToString();
                IDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    c = reader2Casier(reader);
                }
            }
            finally
            {
                con.Close();
            }

            return c;

        }

        public void Créer(Casier c)
        {
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();

                com.CommandText = "SELECT Casier.IdCasier+1 FROM Casier WHERE (Casier.IdCasier + 1) NOT IN (SELECT Casier.IdCasier FROM Casier) ORDER BY IdCasier LIMIT 1";

                IDataReader reader = com.ExecuteReader();
                int id = 0;
                try
                {
                    if (reader.Read())
                    {
                        id = Convert.ToInt32(reader[0]);
                    }

                }
                catch { }
                c.Id = id;
                con.Close();
                con.Open();
                com.CommandText = "INSERT INTO Casier(IdCasier,NomCasier, Largeur_X, Largeur_Y) VALUES('" + (id) + "', '" +c.Nom+ "', '" + c.LargeurX +"', '" + c.LargeurY + "')";
                com.ExecuteNonQuery();

            }
            finally
            {
                con.Close();
            }
        }

        public void Modifier(string nom , int nbLigne, int nbColonne)
        {
                con.Open();
            try {
                IDbCommand com = con.CreateCommand();
                com.CommandText = String.Format("UPDATE `Casier` SET `Largeur_X`={0},`Largeur_Y`={1} WHERE NomCasier='{2}';", nbLigne, nbColonne, nom);
                com.ExecuteNonQuery();

            }
            finally
            {
                con.Close();
            }
        }

        public Casiers Lister()
        {
            Casiers liste = new Casiers();
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Casier";
                IDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Casier c = reader2Casier(reader);
                    liste.Ajouter(c);
                }
            }
            finally
            {
                con.Close();
            }
            return liste;
        }

        public Casiers Lister(Cave c)
        {
            Casiers liste = new Casiers();
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Casier WHERE IdCave="+c.Id.ToString();
                IDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Casier cd = reader2Casier(reader);
                    liste.Ajouter(cd);
                }
            }
            finally
            {
                con.Close();
            }
            return liste;
        }

        public int Nombre()
        {
            int nb = 0;
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Casier;";
                IDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    nb++;
                }
            }
            finally
            {
                con.Close();
            }
            return nb;
        }

        public void Relire(Casier c)
        {
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Casier WHERE IdCasier=" + c.Id.ToString();
                IDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                    c.Nom = reader["NomCasier"].ToString();
                }
            }
            finally
            {
                con.Close();
            }
        }

        public void Sauver(Casier c)
        {
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "UPDATE Casier SETNomCasier='" + c.Nom + "' WHERE IdCasier=" + c.Id.ToString();
                com.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        public void Supprimer(Casier c)
        {
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "DELETE FROM Casier WHERE IdCasier=" + c.Id.ToString();
                com.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        private Casier reader2Casier(IDataReader reader)
        {
            Casier c = new Casier();
            c.Id = Convert.ToInt32(reader["IdCasier"]);
            c.Nom = reader["NomCasier"].ToString();
            c.LargeurX = Convert.ToInt32(reader["Largeur_X"]);
            c.LargeurY = Convert.ToInt32(reader["Largeur_Y"]);
            return c;
        }
    }

}