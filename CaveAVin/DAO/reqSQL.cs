using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metier;
using System.Data;
using System.Diagnostics;
using MySql.Data.MySqlClient;

namespace DAO
{
    /// <summary>
    /// Implémentation du DAO produit en utilisant un SGBD
    /// </summary>
    /// <see cref="Bouteille"/> 
    public class reqSQL
    {
        private IDbConnection con;
        /// <summary>
        /// Initialise l'objet DAO
        /// </summary>
        /// <param name="c">la connexion à utiliser</param>
        public reqSQL(IDbConnection c)
        {
            Debug.Assert(c != null);
            con = c;
        }

        public int SelInt(string req)
        {
            int val = 0;
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = req;
                val = int.Parse(com.ExecuteScalar() + ""); ;

            }
            finally
            {
                con.Close();
            }

            return val;
        }

        public string SelStr1(string req, string read)
        {
            string res = "";
            {
                con.Open();
                try
                {
                    IDbCommand com = con.CreateCommand();
                    com.CommandText = req;
                    using (IDataReader reader = com.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            res = reader[read].ToString();
                        }
                    }
                }
                finally
                {
                    con.Close();
                }
            }
        
            return res;
        }

        public void delete(int ligne, int col, int nbasier)
        {
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "DELETE FROM Bouteille WHERE Position_X = '" + ligne.ToString() + "' AND Position_Y = '" + col.ToString() + "' AND idCasier = '" + nbasier.ToString()+"'";
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
            b.PosX = Convert.ToInt32(reader["Position_X"]);
            b.PosY = Convert.ToInt32(reader["Position_Y"]);
            b.Bue = Convert.ToBoolean(reader["Bue"]);
            return b;
        }

    }
}
