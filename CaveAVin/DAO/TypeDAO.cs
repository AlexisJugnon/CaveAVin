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
    public class TypeDAO : Metier.ITypeDAO
    {
        private IDbConnection con;

        /// <summary>
        /// Initialise l'objet DAO
        /// </summary>
        /// <param name="c">la connexion à utiliser</param>
        public TypeDAO(IDbConnection c)
        {
            Debug.Assert(c != null);
            con = c;
        }

        /// <summary>
        /// Cherche un type en fonction de son id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Metier.Type Chercher(int ID)
        {
            Metier.Type c = null;

            if (con.State != ConnectionState.Open)
                con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Type WHERE IdType=" + ID.ToString();
                IDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    c = reader2Type(reader);
                }
            }
            finally
            {
                con.Close();
            }

            return c;
        }

        /// <summary>
        /// Insert un nouveau type
        /// </summary>
        /// <param name="p">Type à insérer en base</param>
        public void Créer(Metier.Type p)
        {
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "INSERT INTO Type(NomType) VALUES('" + p.NomType + "');";
                com.ExecuteNonQuery();
                com.CommandText = "SELECT LAST_INSERT_ID() FROM Type;";
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

        /// <summary>
        /// Liste tous les types de la base
        /// </summary>
        /// <returns>Types contenu en base</returns>
        public Types Lister()
        {
            Types liste = new Types();
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Type";
                IDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Metier.Type d = reader2Type(reader);
                    liste.Ajouter(d);
                }
            }
            finally
            {
                con.Close();
            }
            return liste;
        }

        /// <summary>
        /// Reprend les données d'un type en base
        /// </summary>
        /// <param name="p">Type à reprendre</param>
        public void Relire(Metier.Type p)
        {
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Type WHERE IdType=" + p.Id.ToString();
                IDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                    p.NomType = reader["NomType"].ToString();
                }
            }
            finally
            {
                con.Close();
            }
        }

        /// <summary>
        /// Sauvegarde un type déjà existant en base
        /// </summary>
        /// <param name="p">Type à sauvegarder</param>
        public void Sauver(Metier.Type p)
        {
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "UPDATE Type SET NomType='" + p.NomType + "' WHERE IdType=" + p.Id.ToString();
                com.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        /// <summary>
        /// Retire un type de la base
        /// </summary>
        /// <param name="p">Type à supprimer</param>
        public void Supprimer(Metier.Type p)
        {
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "DELETE FROM Type WHERE IdType=" + p.Id.ToString();
                com.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        /// <summary>
        /// Converti une ligne de retour de requête en un type
        /// </summary>
        /// <param name="reader">Reader representant une ligne de réponse</param>
        /// <returns>Type initialisé</returns>
        private Metier.Type reader2Type(IDataReader reader)
        {
            Metier.Type d = new Metier.Type();
            d.Id = Convert.ToInt32(reader["IdType"]);
            d.NomType = reader["NomType"].ToString();
            return d;
        }
    }
}

