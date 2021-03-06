﻿using System;
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

            if (con.State != ConnectionState.Open)
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

        public Bouteilles Chercher(Bouteille b)
        {
            Bouteilles liste = new Bouteilles();
            String IdType = null;
            if (b.IdType != 0) { IdType = b.IdType.ToString(); }
            String IdRegion = null;
            if (b.IdRegion != 0) { IdRegion = b.IdRegion.ToString(); }
            String IdDomaine = null;
            if (b.IdDomaine != 0) { IdDomaine = b.IdDomaine.ToString(); }
            String IdContenance = null;
            if (b.IdContenance != 0) { IdContenance = b.IdContenance.ToString(); }
            String IdCru = null;
            if (b.IdCru != 0) { IdCru = b.IdCru.ToString(); }
            String IdMillesime = null;
            if (b.IdMillesime != 0) { IdMillesime = b.IdMillesime.ToString(); }
            String IdType_vinification = null;
            if (b.IdType_vinification != 0) { IdType_vinification = b.IdType_vinification.ToString(); }
            String IdAppelation = null;
            if (b.IdAppelation != 0) { IdAppelation = b.IdAppelation.ToString(); }
            String IdPays = null;
            if (b.IdPays != 0) { IdPays = b.IdPays.ToString(); }

            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Bouteille LEFT JOIN Casier ON(Bouteille.IdCasier = Casier.idCasier) LEFT JOIN Type ON(Bouteille.IdType = Type.idType) LEFT JOIN Region ON(Bouteille.IdRegion = Region.IdRegion) LEFT JOIN Pays ON(Bouteille.IdPays = Pays.idPays) LEFT JOIN Domaine ON(Bouteille.IdPays = Pays.idPays) LEFT JOIN Contenance ON(Bouteille.IdContenance = Contenance.idContenance) LEFT JOIN Cru ON(Bouteille.IdCru = Cru.idCru) LEFT JOIN Millesime ON(Bouteille.IdMillesime = Millesime.idMillesime) LEFT JOIN Type_vinification ON(Bouteille.IdVinif = Type_vinification.IdVinif) LEFT JOIN Appelation ON(Bouteille.IdAppelation = Appelation.idAppelation)  Where ";
                if (b.IdType != 0) com.CommandText += " Bouteille.`IdType` = " + IdType+" AND ";
                if (b.IdRegion != 0) com.CommandText += " Bouteille.`IdRegion` = '" + IdRegion + "' AND ";
                if (b.IdDomaine != 0) com.CommandText += " Bouteille.`IdDomaine`= '" + IdDomaine + "' AND ";
                if (b.IdContenance != 0) com.CommandText += " Bouteille.`IdContenance` = '" + IdContenance + "' AND ";
                if (b.IdCru != 0) com.CommandText += " Bouteille.`IdCru` = '" + IdCru + "' AND ";
                if (b.IdMillesime != 0) com.CommandText += " Bouteille.`IdMillesime` = '" + IdMillesime + "' AND ";
                if (b.IdType_vinification != 0) com.CommandText += " Bouteille.`IdVinif` = '" + IdType + "' AND ";
                if (b.IdAppelation != 0) com.CommandText += " Bouteille.`IdAppelation` = '" + IdType_vinification + "' AND ";
                if (b.IdPays != 0) com.CommandText += " Bouteille.`IdPays` = '" + IdPays + "' AND ";
                com.CommandText += "1 = 1;";
                IDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Bouteille bo = reader2BouteilleRecherche(reader);
                    liste.Ajouter(bo);
                }
            }
            finally
            {
                con.Close();
            }
            return liste;
        }

        public Bouteille Chercher(int ligneIndex, int colonneIndex, int casier, int bue)
        {
            Bouteille b = null;

            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = String.Format("SELECT * FROM Bouteille WHERE Position_X={0} and Position_Y={1} and IdCasier={2} and bue={3}", ligneIndex, colonneIndex, casier, bue);
                IDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    b = reader2BouteilleFull(reader);
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
                String IdRegion = null;
                if (b.IdRegion != 0) { IdRegion = b.IdRegion.ToString(); }
                String IdDomaine = null;
                if (b.IdDomaine != 0) { IdDomaine = b.IdDomaine.ToString(); }
                String IdContenance = null;
                if (b.IdContenance != 0) { IdContenance = b.IdContenance.ToString(); }
                String IdCru = null;
                if (b.IdCru != 0) { IdCru = b.IdCru.ToString(); }
                String IdMillesime = null;
                if (b.IdMillesime != 0) { IdMillesime = b.IdMillesime.ToString(); }
                String IdType_vinification = null;
                if (b.IdType_vinification != 0) { IdType_vinification = b.IdType_vinification.ToString(); }
                String IdAppelation = null;
                if (b.IdAppelation != 0) { IdAppelation = b.IdAppelation.ToString(); }
                String IdPays = null;
                if (b.IdPays != 0) { IdPays = b.IdPays.ToString(); }


                IDbCommand com = con.CreateCommand();
                com.CommandText = "INSERT INTO `WineFinder`.`Bouteille` (`Texte`, `Bue`, `Position_X`, `Position_Y`, `IdCasier`, `IdType`";
                if (b.IdRegion != 0) com.CommandText += ", `IdRegion`";
                if (b.IdDomaine != 0) com.CommandText += ", `IdDomaine`";
                if (b.IdContenance != 0) com.CommandText += ", `IdContenance`";
                if (b.IdCru != 0) com.CommandText += ", `IdCru`";
                if (b.IdMillesime != 0) com.CommandText += ", `IdMillesime`";
                if (b.IdType_vinification != 0) com.CommandText += ", `IdVinif`";
                if (b.IdAppelation != 0) com.CommandText += ", `IdAppelation`";
                if (b.IdPays != 0) com.CommandText += ", `IdPays`";
                com.CommandText += ") VALUES ('" + b.Texte + "','" + b.Bue + "','" + b.PosX + "','" + b.PosY + "','" + b.IdCasier + "','" + b.IdType;
                if (b.IdRegion != 0) com.CommandText += "','" + IdRegion;
                if (b.IdDomaine != 0) com.CommandText += "','" + IdDomaine;
                if (b.IdContenance != 0) com.CommandText += "','" + IdContenance;
                if (b.IdCru != 0) com.CommandText += "','" + IdCru;
                if (b.IdMillesime != 0) com.CommandText += "','" + IdMillesime;
                if (b.IdType_vinification != 0) com.CommandText += "','" + IdType_vinification;
                if (b.IdAppelation != 0) com.CommandText += "','" + IdAppelation;
                if (b.IdPays != 0) com.CommandText += "','" + IdPays;
                com.CommandText += "');";
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

        public void Modifier(Bouteille b)
        {
            con.Open();

            try
            {
                String IdType = "NULL";
                if (b.IdType != 0) { IdType = b.IdType.ToString(); }
                String IdRegion = "NULL";
                if (b.IdRegion != 0) { IdRegion = b.IdRegion.ToString(); }
                String IdDomaine = "NULL";
                if (b.IdDomaine != 0) { IdDomaine = b.IdDomaine.ToString(); }
                String IdContenance = "NULL";
                if (b.IdContenance != 0) { IdContenance = b.IdContenance.ToString(); }
                String IdCru = "NULL";
                if (b.IdCru != 0) { IdCru = b.IdCru.ToString(); }
                String IdMillesime = "NULL";
                if (b.IdMillesime != 0) { IdMillesime = b.IdMillesime.ToString(); }
                String IdType_vinification = "NULL";
                if (b.IdType_vinification != 0) { IdType_vinification = b.IdType_vinification.ToString(); }
                String IdAppelation = "NULL";
                if (b.IdAppelation != 0) { IdAppelation = b.IdAppelation.ToString(); }
                String IdPays = "NULL";
                if (b.IdPays != 0) { IdPays = b.IdPays.ToString(); }

                IDbCommand com = con.CreateCommand();
                com.CommandText = String.Format("UPDATE `Bouteille` SET `IdType`={0},`IdRegion`={1},`idPays`={2},`IdDomaine`={3},`IdContenance`={4},`IdCru`={5},`IdMillesime`={6},`IdVinif`={7},`IdAppelation`={8} WHERE Position_X = {9} AND Position_Y = {10} AND IdCasier={11};", IdType, IdRegion, IdPays, IdDomaine, IdContenance, IdCru, IdMillesime, IdType_vinification, IdAppelation, b.PosX, b.PosY, b.IdCasier);
                com.ExecuteNonQuery();

            }
            finally
            {
                con.Close();
            }

                    
        }

        public void Deplacer(int ligne_act, int col_act, int cas_act, int ligne_pro, int col_pro, int cas_pro)
        {
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = String.Format("UPDATE `Bouteille` SET `Position_X`={0},`Position_Y`={1},`IdCasier`={2} WHERE Position_X = {3} AND Position_Y = {4} AND IdCasier={5};", ligne_pro, col_pro, cas_pro, ligne_act, col_act, cas_act);
                com.ExecuteNonQuery();
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
                com.CommandText = "SELECT * FROM Bouteille WHERE IdAppelation=" + a.Id.ToString();
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

        public Bouteilles Lister(Metier.Type t)
        {
            Bouteilles liste = new Bouteilles();
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Bouteille WHERE IdType=" + t.Id.ToString();
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
                com.CommandText = "SELECT * FROM Bouteille WHERE IdDomaine=" + d.Id.ToString();
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
                com.CommandText = "SELECT * FROM Bouteille WHERE IdRegion=" + r.Id.ToString();
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
                com.CommandText = "SELECT * FROM Bouteille WHERE IdContenance=" + c.Id.ToString();
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
                com.CommandText = "SELECT * FROM Bouteille WHERE IdCru=" + c.Id.ToString();
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
                com.CommandText = "SELECT * FROM Bouteille WHERE IdMillesime=" + m.Id.ToString();
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
                com.CommandText = "SELECT * FROM Bouteille WHERE IdVinif=" + tp.Id.ToString();
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

        public Bouteilles Lister(Pays p)
        {
            Bouteilles liste = new Bouteilles();
            con.Open();
            try
            {
                IDbCommand com = con.CreateCommand();
                com.CommandText = "SELECT * FROM Bouteille WHERE IdCasier=" + p.Id.ToString();
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
            b.PosX = Convert.ToInt32(reader["Position_X"]);
            b.PosY = Convert.ToInt32(reader["Position_Y"]);
            b.Bue = Convert.ToBoolean(reader["Bue"]);

            return b;
        }

        /// <summary>
        /// Converti un date reader en une bouteille pleinement intilialisé.
        /// </summary>
        /// <param name="reader"></param>
        /// <remarks>Ne marche que dans le cas d'une recherche sur un élément (méthode Chercher(...) )</remarks>
        /// <returns></returns>
        private Bouteille reader2BouteilleFull(IDataReader reader)
        {
            Bouteille bouteille = new Bouteille();
            bouteille.Texte = reader["Texte"].ToString();
            bouteille.Id = Convert.ToInt32(reader["IdBouteille"]);
            bouteille.PosX = Convert.ToInt32(reader["Position_X"]);
            bouteille.PosY = Convert.ToInt32(reader["Position_Y"]);
            bouteille.Bue = Convert.ToBoolean(reader["Bue"]);

            //On lit les ids des foreign key
            int idCasier = (Convert.ToInt32(reader["IdCasier"]));
            int idType = (Convert.ToInt32(reader["IdType"]));
            int idRegion = 0;
            int idDomaine = 0;
            int idContenance = 0;
            int idCru = 0;
            int idMillesime = 0;
            int idType_vinification = 0;
            int idPays = 0;
            int idAppelation = 0;
            try { idRegion = (Convert.ToInt32(reader["IdRegion"])); } catch { }
            try { idPays = (Convert.ToInt32(reader["idPays"])); } catch { }
            try { idDomaine = (Convert.ToInt32(reader["IdDomaine"])); } catch { }
            try { idContenance = (Convert.ToInt32(reader["IdContenance"])); } catch { }
            try { idCru = (Convert.ToInt32(reader["IdCru"])); } catch { }
            try { idMillesime = (Convert.ToInt32(reader["IdMillesime"])); } catch { }
            try { idType_vinification = (Convert.ToInt32(reader["IdVinif"])); } catch { }
            try { idAppelation = (Convert.ToInt32(reader["IdAppelation"])); } catch { }

            // Cloture du reader pour pouvoir utiliser les méthodes "Chercher". Sinon exception "Data reader already exist"
            reader.Close();

            //Instanciation des DAOS
            var casierDao = new CasierDAO(BDD.Instance.Connexion);
            var typeDao = new TypeDAO(BDD.Instance.Connexion);
            var regionDao = new RegionDAO(BDD.Instance.Connexion);
            var domaineDao = new DomaineDAO(BDD.Instance.Connexion);
            var contenanceDao = new ContenanceDAO(BDD.Instance.Connexion);
            var cruDao = new CruDAO(BDD.Instance.Connexion);
            var millesimeDao = new MillesimeDAO(BDD.Instance.Connexion);
            var vinificationDao = new Type_vinificationDAO(BDD.Instance.Connexion);
            var appelationDao = new AppelationDAO(BDD.Instance.Connexion);
            var paysDao = new PaysDAO(BDD.Instance.Connexion);

            //Population de la bouteille avec les objets liés
            bouteille.Casier = casierDao.Chercher(idCasier);
            bouteille.Type = typeDao.Chercher(idType);
            bouteille.Region = regionDao.Chercher(idRegion);
            bouteille.Domaine = domaineDao.Chercher(idDomaine);
            bouteille.Contenance = contenanceDao.Chercher(idContenance);
            bouteille.Cru = cruDao.Chercher(idCru);
            bouteille.Millesime = millesimeDao.Chercher(idMillesime);
            bouteille.Type_vinification = vinificationDao.Chercher(idType_vinification);
            bouteille.Appelation = appelationDao.Chercher(idAppelation);
            bouteille.Pays = paysDao.Chercher(idPays);
            return bouteille;
        }

        private Bouteille reader2BouteilleRecherche(IDataReader reader)
        {
            Bouteille bouteille = new Bouteille();
            bouteille.Texte = reader[1].ToString();
            bouteille.Id = Convert.ToInt32(reader[0]);
            bouteille.PosX = Convert.ToInt32(reader[3]);
            bouteille.PosY = Convert.ToInt32(reader[4]);
            bouteille.Bue = Convert.ToBoolean(reader[2]);

            Casier c = new Casier();
            c.Id = Convert.ToInt32(reader[5]);
            c.Nom = reader[16].ToString();
            bouteille.Casier = c;

            Metier.Type t = new Metier.Type();
            t.Id = Convert.ToInt32(reader[6]);
            t.NomType = reader[21].ToString();
            bouteille.Type = t;

            try
            {
                Region r = new Region();
                r.Id = Convert.ToInt32(reader[7]);
                r.NomRegion = reader[24].ToString();
                bouteille.Region = r;
            }
            catch { }

            try
            {
                Pays p = new Pays();
            p.Id = Convert.ToInt32(reader[8]);
            p.NomPays = reader[26].ToString();
            bouteille.Pays = p;
        }
            catch { }

            try
            {
                Domaine d = new Domaine();
            d.Id = Convert.ToInt32(reader[9]);
            d.NomDomaine = reader[28].ToString();
            bouteille.Domaine = d;
                        }
            catch { }

            try
            {
                Contenance co = new Contenance();
            co.Id = Convert.ToInt32(reader[10]);
            co.Valeur = Convert.ToInt32(reader[30]);
            bouteille.Contenance = co;
                }
            catch { }

            try
            {
                Cru cr = new Cru();
            cr.Id = Convert.ToInt32(reader[11]);
            cr.NomCru = reader[32].ToString();
            bouteille.Cru = cr;
                }
            catch { }

            try
            {
                Millesime m = new Millesime();
            m.Id = Convert.ToInt32(reader[12]);
            m.NomMillesime = reader[34].ToString();
            bouteille.Millesime = m;
                }
            catch { }

            try
            {
                Type_vinification tv = new Type_vinification();
            tv.Id = Convert.ToInt32(reader[13]);
            tv.NomVinif = reader[36].ToString();
            bouteille.Type_vinification = tv;
                }
            catch { }

            try
            {
                Appelation a = new Appelation();
            a.Id = Convert.ToInt32(reader[14]);
            a.NomAppelation = reader[38].ToString();
            bouteille.Appelation = a;
                }
            catch { }

            return bouteille;
        }

    }
}

