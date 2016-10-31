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
    class TypeDAO : Metier.ITypeDAO
    {
        private const string SQL_SELECT_BY_ID = "SELECT * FROM Type WHERE IdType={0}";
        private const string SQL_SELECT_LAST_INSERT_ID = "SELECT LAST_INSERT_ID() FROM Type;";
        private const string SQL_SELECT_ALL = "SELECT * FROM Type";
        private const string SQL_INSERT = "INSERT INTO Type(NomType) VALUES('{0}');";
        private const string SQL_UPDATE = "UPDATE Type SET NomType='{0}' WHERE IdType={1}";
        private const string SQL_DELETE = "DELETE FROM Type WHERE IdType={0}";

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

        public Metier.Type Chercher(int Id)
        {
            Metier.Type type = null;

            IDataReader reader = ExecuteQuery(String.Format(SQL_SELECT_BY_ID, Id));

            if (reader != null && reader.Read())
            {
                type = reader2Type(reader);
            }

            return type;
        }

        public void Créer(Metier.Type type)
        {
            if (type != null)
            {
                ExecuteCommand(String.Format(SQL_INSERT, type.NomType));

                var reader = ExecuteQuery(SQL_SELECT_LAST_INSERT_ID);

                int id = 1;
                if (reader != null && reader.Read())
                {
                    Int32.TryParse(reader[0].ToString(), out id);
                }
                type.Id = id;
            }
        }

        public Types Lister()
        {
            var types = new Types();

            var reader = ExecuteQuery(SQL_SELECT_ALL);

            while (reader != null && reader.Read())
            {
                var type = reader2Type(reader);
                types.Ajouter(type);
            }

            return types;
        }

        public void Relire(Metier.Type type)
        {
            var reader = ExecuteQuery(String.Format(SQL_SELECT_BY_ID, type.Id));

            if (reader != null && reader.Read())
            {
                type = reader2Type(reader);
            }
        }

        public void Sauver(Metier.Type type)
        {
            ExecuteCommand(String.Format(SQL_UPDATE, type.NomType, type.Id));
        }

        public void Supprimer(Metier.Type type)
        {
            ExecuteCommand(String.Format(SQL_DELETE, type.Id));
        }
        private Metier.Type reader2Type(IDataReader reader)
        {
            var type = new Metier.Type();
            int id;
            if (Int32.TryParse(reader["IdType"].ToString(), out id))
            {
                type.Id = id;
            }
            type.NomType = reader["NomType"].ToString();
            return type;
        }

        private IDataReader ExecuteQuery(string sql)
        {
            IDataReader reader = null;

            if (con != null)
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                try
                {
                    var command = con.CreateCommand();
                    command.CommandText = sql;
                    reader = command.ExecuteReader();
                }
                finally
                {
                    con.Close();
                }
            }

            return reader;
        }

        private void ExecuteCommand(string sql)
        {
            if (con != null)
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }

                try
                {
                    var command = con.CreateCommand();
                    command.CommandText = sql;
                    command.ExecuteNonQuery();
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
}
