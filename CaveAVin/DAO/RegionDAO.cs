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
    public class RegionDAO:Metier.IRegionDAO
    {
        private IDbConnection con;
        /// <summary>
        /// Initialise l'objet DAO
        /// </summary>
        /// <param name="c">la connexion à utiliser</param>
        public RegionDAO(IDbConnection c)
        {
            Debug.Assert(c != null);
            con = c;
        }

        /// <summary>
        /// chercher une région en fonction de son identifiant
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Region Chercher(string nom)
        {
            Region r = null;

            if (con.State != ConnectionState.Open)
                con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Region WHERE NomRegion='" + nom + "'";
                IDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    r = reader2Region(reader);
                }
            }
            finally
            {
                con.Close();
            }

            return r;

        }

        /// <summary>
        /// chercher une région en fonction de son identifiant
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Region Chercher(int ID)
        {
            Region r = null;

            if (con.State != ConnectionState.Open)
                con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Region WHERE IdRegion=" + ID.ToString();
                IDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    r = reader2Region(reader);
                }
            }
            finally
            {
                con.Close();
            }

            return r;

        }

        /// <summary>
        /// Créé une nouvelle région dans la base de données
        /// </summary>
        /// <param name="p">Région à créér</param>
        public void Créer(Region p)
        {
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "INSERT INTO Region(NomRegion, IdPays) VALUES('" + p.NomRegion + "', "+ p.Pays.Id +");";
                com.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        /// <summary>
        /// Créé une nouvelle région dans la base de données
        /// </summary>
        /// <param name="p">Région à créér</param>
        public void Créer(string nom, string nomPays)
        {
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "INSERT INTO Region(NomRegion, NomPays) VALUES('" + nom + "', " + nomPays + ");";
                com.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }


        /// <summary>
        /// liste toutes les régions de la base de données
        /// </summary>
        /// <returns></returns>
        public Regions Lister()
        {
            Regions liste = new Regions();
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Region";
                IDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Region r = reader2Region(reader);
                    liste.Ajouter(r);
                }
            }
            finally
            {
                con.Close();
            }
            return liste;
        }

        public Regions Lister(Pays c)
        {
            Regions liste = new Regions();
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Regions WHERE IdPays=" + c.Id.ToString();
                IDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Region cd = reader2Region(reader);
                    liste.Ajouter(cd);
                }
            }
            finally
            {
                con.Close();
            }
            return liste;
        }

        /// <summary>
        /// Charge la région entré en paramètre
        /// </summary>
        /// <param name="p"></param>
        public void Relire(Region p)
        {
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Region WHERE IdRegion=" + p.Id.ToString();
                IDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                    p.NomRegion = reader["NomRegion"].ToString();
                }
            }
            finally
            {
                con.Close();
            }
        }

        /// <summary>
        /// met à jour la région entré en paramètre
        /// </summary>
        /// <param name="p"></param>
        public void Sauver(Region p)
        {
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "UPDATE Region SET NomRegion='" + p.NomRegion + "', IdPays = " + p.Pays.Id + " WHERE IdRegion=" + p.Id.ToString();
                com.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        /// <summary>
        /// Supprime la erégion de la base de données
        /// </summary>
        /// <param name="p"></param>
        public void Supprimer(Region p)
        {
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "DELETE FROM Region WHERE IdRegion=" + p.Id.ToString();
                com.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        /// <summary>
        /// Initialise Région avec les données sortie de la base de donnée
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private Region reader2Region(IDataReader reader)
        {
            Region r = new Region();
            r.Id = Convert.ToInt32(reader["IdRegion"]);
            r.NomRegion = reader["NomRegion"].ToString();
#warning Null ref : le pays n'est pas initialisé.
            //r.Pays.Id = Convert.ToInt32(reader["IdPays"]);
            return r;
        }
    }
}
