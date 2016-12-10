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
    public class Type_vinificationDAO : Metier.IType_vinificationDAO
    {
        private IDbConnection con;
        /// <summary>
        /// Initialise l'objet DAO
        /// </summary>
        /// <param name="c">la connexion à utiliser</param>
       
        public Type_vinificationDAO(IDbConnection c)
        {
            Debug.Assert(c != null);
            con = c;
        }
        /// <summary>
        /// chercher un type_vignifaication en fonction de son ID
        /// </summary>
        /// <param name="ID">Identifiant à rechercher</param>
        /// <returns></returns>
        public Type_vinification Chercher(int ID)
        {
            Type_vinification t = null;
            try
            {
                con.Open();
                IDbCommand com = con.CreateCommand();
                com.CommandText ="SELECT idVinif FROM Type_Vinication WHERE IdVinif = " + ID.ToString();
                IDataReader reader = com.ExecuteReader();
                if(reader.Read())
                {
                    t = reader2Type_vinif(reader);
                }
            }
            finally
            {
                con.Close();
            }
            return t;
        }

        /// <summary>
        /// Créé un noveau type_vignification
        /// </summary>
        /// <param name="p"></param>
        public void Créer(Type_vinification p)
        {
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "INSERT INTO Type_vignification(NomVinif) VALUES('" + p.NomVinif + "');";
                com.ExecuteNonQuery();
                com.CommandText = "SELECT LAST_INSERT_ID() FROM Type_vignification;";
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
        /// liste tous les types_vignification
        /// </summary>
        /// <returns></returns>
        public Type_vinifications Lister()
        {
            Type_vinifications liste = new Type_vinifications();
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Type_vinification";
                IDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                     Type_vinification t = reader2Type_vinif(reader);
                    liste.Ajouter(t);
                }
            }
            finally
            {
                con.Close();
            }
            return liste;
        }

        /// <summary>
        /// charge le type_vignification en question
        /// </summary>
        /// <param name="p"></param>
        public void Relire(Type_vinification p)
        {
            {
                con.Open();
                try
                {
                    IDbCommand com = con.CreateCommand();
                    com.CommandText = "SELECT * FROM Type_vignification WHERE IdVinif=" + p.Id.ToString();
                    IDataReader reader = com.ExecuteReader();
                    if (reader.Read())
                    {
                        p.NomVinif = reader["NomVinif"].ToString();
                    }
                }
                finally
                {
                    con.Close();
                }
            }
        }

        /// <summary>
        /// met à jour les données d'un type_vignification
        /// </summary>
        /// <param name="p"></param>
        public void Sauver(Type_vinification p)
        {
            {
                con.Open();
                try
                {
                    IDbCommand com = con.CreateCommand();
                    com.CommandText = "UPDATE Type_vignification SET NomVinif='" + p.NomVinif + "' WHERE IdVinif=" + p.Id.ToString();
                    com.ExecuteNonQuery();
                }
                finally
                {
                    con.Close();
                }
            }
        }

        /// <summary>
        /// Supprime de la base un type_vignification
        /// </summary>
        /// <param name="p"></param>
        public void Supprimer(Type_vinification p)
        {
            {
                con.Open();
                try
                {
                    IDbCommand com = con.CreateCommand();
                    com.CommandText = "DELETE FROM Type_vignification WHERE IdVinif=" + p.Id.ToString();
                    com.ExecuteNonQuery();
                }
                finally
                {
                    con.Close();
                }
            }
        }

        /// <summary>
        /// Lit les données capturé par les requêtes
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private Type_vinification reader2Type_vinif(IDataReader reader)
        {
            {
                Type_vinification t = new Type_vinification();
                t.Id = Convert.ToInt32(reader["IdVinif"]);
                t.NomVinif = reader["NomVinif"].ToString();
                
                return t;
            }
        }
    }
}
