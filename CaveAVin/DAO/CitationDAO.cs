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
    public class CitationDAO : Metier.ICitationDAO
    {
        private IDbConnection con;

        public CitationDAO(IDbConnection c)
        {
            Debug.Assert(c != null);
            con = c;
        }
        /// <summary>
        /// Permet de chercher un texte pour le saviez-vous à partir d'un identifiant
        /// </summary>
        /// <param name="IDC"></param>
        /// <returns></returns>
        public string chercher(int IDC)
        {
            String texte = "";
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT textC FROM Citation WHERE IdC=" + IDC.ToString();
                IDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    texte = reader2Citation(reader);
                }
            }
            finally
            {
                con.Close();
            }
            return texte;
        }

        /// <summary>
        /// récupère les données contenu après l'éxécution de la requète
        /// </summary>
        /// <param name="reader">Variable contenant le résultat de la requète</param>
        /// <returns>Le string contenant le texte sortie de la BDD</returns>
        private string reader2Citation(IDataReader reader)
        {
            return reader["TEXTC"].ToString();            
        }
    }
}
