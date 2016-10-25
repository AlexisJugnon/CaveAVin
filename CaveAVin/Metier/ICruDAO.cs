using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Metier
{
    public interface ICruDAO 
    {
        /// <summary>
        /// Ajoute le cru dans la base
        /// </summary>
        /// <param name="p">Le cru à ajouter</param>
        void Créer(Cru p);

        /// <summary>
        /// Charge le cru depuis la base
        /// </summary>
        /// <param name="p">Le cru à charger</param>
        void Relire(Cru p);

        /// <summary>
        /// Cherche le cru à partir de son ID
        /// </summary>
        /// <param name="ID">L'id du cru à chercher</param>
        /// <returns>Le cru à chercher ou null si le cru est inexistant</returns>
        Cru Chercher(int ID);

        /// <summary>
        /// Sauve (met à jour) le cru dans la base
        /// </summary>
        /// <param name="p">Le cru à mettre à jour</param>
        void Sauver(Cru p);

        /// <summary>
        /// Supprime le cru de la base
        /// </summary>
        /// <param name="p">Le cru à retirer</param>
        void Supprimer(Cru p);

        /// <summary>
        /// Liste tous les crus
        /// </summary>
        /// <returns>La liste de cru</returns>
        Crus Lister();

    }
}
