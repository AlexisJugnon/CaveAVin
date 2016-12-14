using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public interface ITypeDAO
    {

        #region Fonctions
        /// <summary>
        /// Ajoute le type dans la base
        /// </summary>
        /// <param name="p">type à ajouter</param>
        void Créer(string nom);

        /// <summary>
        /// Charge le type depuis la base
        /// </summary>
        /// <param name="p">type à charger</param>
        void Relire(Type p);

        /// <summary>
        /// Cherche le type à partir de son ID
        /// </summary>
        /// <param name="ID">Identifiant du type à chercher</param>
        /// <returns></returns>
        Type Chercher(int ID);

        /// <summary>
        /// Sauve (met à jour) le type dans la base
        /// </summary>
        /// <param name="p">Type à sauver/mettre à jour</param>
        void Sauver(Type p);

        /// <summary>
        /// Supprime le type de la base
        /// </summary>
        /// <param name="p">Type à supprimer</param>
        void Supprimer(Type p);

        /// <summary>
        /// Liste tous les types
        /// </summary>
        /// <returns>la liste des types</returns>
        Types Lister();
        #endregion
    }
}
