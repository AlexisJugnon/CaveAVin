using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public interface IAppelationDAO
    {

        /// <summary>
        /// Ajoute l'appelation dans la base
        /// </summary>
        /// <param name="p">l'appelation à ajouter dans la base de donées</param>
        void Créer(Appelation p);

        /// <summary>
        /// Charge l'appelation depuis la base
        /// </summary>
        /// <param name="p">L'appelation à recuperer à partir de la base de données si elle existe</param>
        void Relire(Appelation p);

        /// <summary>
        /// Cherche l'appelation à partir de son ID
        /// </summary>
        /// <param name="ID">l'identifieant de l'appelation recherchée</param>
        /// <returns>Retourne l'appelation correspondante à l'Id ou null si aucun résultat</returns>
        Appelation Chercher(int ID);

        /// <summary>
        /// Sauve (met à jour) l'appelation dans la base
        /// </summary>
        /// <param name="p">L'appelation à mettre à jour</param>
        void Sauver(Appelation p);

        /// <summary>
        /// Supprime l'appelation de la base
        /// </summary>
        /// <param name="p">L'appelation à retirer de la base de données</param>
        void Supprimer(Appelation p);

        /// <summary>
        /// Liste tous les appelations
        /// </summary>
        /// <returns></returns>
        Appelations Lister();

    }
}
