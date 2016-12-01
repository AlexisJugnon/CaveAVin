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


        public Bouteille Chercher(int ligne, int col)
        {
            Bouteille b = null;

            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Bouteille WHERE Position_X= '" + ligne.ToString() +"' AND Position_Y = '" + col.ToString()+"'";
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
                com.CommandText = "INSERT INTO Bouteille(Texte,Bue,Position_X,Position_Y,IdCasier,IdType,IdRegion,IdDomaine,IdContenance,IdCru,IdMillesime,IdVinif,IdAppelation) VALUES('" + b.Texte + "','" + b.Bue.ToString() + "','" + b.PosX.ToString() + "','" + b.PosY.ToString() + "','" + b.IdCasier.ToString() + "','" + b.IdType.ToString() + "','" + b.IdRegion.ToString() + "','" + b.IdDomaine.ToString() + "','" + b.IdContenance.ToString() + "','" + b.IdCru.ToString() + "','" + b.IdMillesime.ToString() + "','" + b.IdType_vinification.ToString() + "','" + b.IdAppelation.ToString() + "');";
                com.ExecuteNonQuery();
                com.CommandText = "SELECT LAST_INSERT_ID() FROM Bouteille;";
                IDataReader reader = com.ExecuteReader();
                if (reader.Read())
                    b.Id = Convert.ToInt32(reader[0]);
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

        public Bouteilles Lister(Casier c)
        {
            Bouteilles liste = new Bouteilles();
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Bouteille WHERE IdCasier=" + c.Id.ToString();
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

        public Bouteilles Lister(Appelation a)
        {
            Bouteilles liste = new Bouteilles();
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Bouteille WHERE IdAppelation=" +a.Id.ToString();
                IDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Bouteille b = reader2BouteilleComplet(reader);
                    liste.Ajouter(b);
                }
            }
            finally
            {
                con.Close();
            }
            return liste;
        }

        public Bouteilles Lister(Metier.Type t)
        {
            Bouteilles liste = new Bouteilles();
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Bouteille WHERE IdType="+t.Id.ToString();
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

        public Bouteilles Lister(Domaine d)
        {
            Bouteilles liste = new Bouteilles();
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Bouteille WHERE IdDomaine="+d.Id.ToString();
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

        public Bouteilles Lister(Region r)
        {
            Bouteilles liste = new Bouteilles();
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Bouteille WHERE IdRegion="+r.Id.ToString();
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

        public Bouteilles Lister(Contenance c)
        {
            Bouteilles liste = new Bouteilles();
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Bouteille WHERE IdContenance="+c.Id.ToString();
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

        public Bouteilles Lister(Cru c)
        {
            Bouteilles liste = new Bouteilles();
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Bouteille WHERE IdCru="+c.Id.ToString();
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

        public Bouteilles Lister(Millesime m)
        {
            Bouteilles liste = new Bouteilles();
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Bouteille WHERE IdMillesime="+m.Id.ToString();
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

        public Bouteilles Lister(Type_vinification tp)
        {
            Bouteilles liste = new Bouteilles();
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Bouteille WHERE IdVinif="+tp.Id.ToString();
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

        /// <summary>
        /// Met bue à 1 pour une bouteille précise
        /// </summary>
        /// <param name="p">La bouteille où il faut mettre la bouteille à bue</param>
        public void RetirerBue(Bouteille p)
        {
           
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "UPDATE Bouteille SET Bue = 1 WHERE IdBouteille=" + p.Id.ToString();
                com.ExecuteNonQuery();
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


        private Bouteille reader2BouteilleComplet(IDataReader reader)
        {
            Bouteille b = new Bouteille();
            return b;
        }

        //lblAppelation.Content = daoBouteille.;
        //    lblCategorie.Content = ;
        //    lblContenance.Content = ;
        //    lblCru.Content = ;
        //    lblDomaine.Content = ;
        //    lblMillesime.Content = ;
        //    lblPays.Content = ;
        //    lblRegion.Content = ;
        //    lblVinification.Content = ;

    }
}
