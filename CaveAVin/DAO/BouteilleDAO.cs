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
    /// <summary>
    /// Implémentation du DAO produit en utilisant un SGBD
    /// </summary>
    /// <see cref="Bouteille"/> 
    public class BouteilleDAO : Metier.IBouteilleDAO
    {
        private IDbConnection con;
        /// <summary>
        /// Initialise l'objet DAO
        /// </summary>
        /// <param name="c">la connexion à utiliser</param>
        public BouteilleDAO(IDbConnection c)
        {
            Debug.Assert(c != null);
            con = c;
        }

        public Bouteille Chercher(int ID)
        {
            Bouteille b = null;

            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Bouteille WHERE IdBouteille=" + ID.ToString();
                IDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    b = reader2Bouteille(reader);
                }
            }
            finally
            {
                con.Close();
            }

            return b;
        }

        public void Créer(Bouteille b)
        {
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "INSERT INTO Bouteille(Texte,Bue,Position_X,Position_Y) VALUES('" + b.Texte + "','" + b.Bue.ToString() + "','" + b.PosX.ToString() + "','" + b.PosY.ToString() + "');";
                com.ExecuteNonQuery();
                com.CommandText = "SELECT LAST_INSERT_ID() FROM Bouteille;";
                IDataReader reader = com.ExecuteReader();
                int id = 1;
                if (reader.Read())
                    id = Convert.ToInt32(reader[0]);
                b.Id = id;
            }
            finally
            {
                con.Close();
            }
        }

        public Bouteilles Lister()
        {
            Bouteilles liste = new Bouteilles();
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Bouteille";
                IDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Bouteille b = reader2Bouteille(reader);
                    liste.Ajouter(b);
                }
            }
            finally
            {
                con.Close();
            }
            return liste;
        }

        public void Relire(Bouteille b)
        {
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Bouteille WHERE IdBouteille=" + b.Id.ToString();
                IDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                    b = reader2Bouteille(reader);
                }
            }
            finally
            {
                con.Close();
            }
        }

        public void Sauver(Bouteille b)
        {
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "UPDATE Bouteille SET Texte='" + b.Texte + "' WHERE IdBouteille=" + b.Id.ToString();
                com.ExecuteNonQuery();
                com.CommandText = "UPDATE Bouteille SET Bue='" + b.Bue.ToString() + "' WHERE IdBouteille=" + b.Id.ToString();
                com.ExecuteNonQuery();
                com.CommandText = "UPDATE Bouteille SET Position_X='" + b.PosX.ToString() + "' WHERE IdBouteille=" + b.Id.ToString();
                com.ExecuteNonQuery();
                com.CommandText = "UPDATE Bouteille SET Position_Y='" + b.PosY.ToString() + "' WHERE IdBouteille=" + b.Id.ToString();
                com.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        public void Supprimer(Bouteille b)
        {
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "DELETE FROM Bouteille WHERE IdBouteille=" + b.Id.ToString();
                com.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        private Bouteille reader2Bouteille(IDataReader reader)
        {
            Bouteille b = new Bouteille();
            b.Texte = reader["Texte"].ToString();
            b.Id = Convert.ToInt32(reader["IdBouteille"]);
            b.PosX= Convert.ToInt32(reader["Position_X"]);
            b.PosY= Convert.ToInt32(reader["Position_Y"]);
            b.Bue = Convert.ToBoolean(reader["Bue"]);
            return b;
        }

    }
}
