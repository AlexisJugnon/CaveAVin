using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public interface IContenanceDAO
    {
        /// <summary>
        /// Ajoute le contenance dans la base
        /// </summary>
        /// <param name="p"></param>
        void Créer(Contenance p);

        /// <summary>
        /// Charge le contenance depuis la base
        /// </summary>
        /// <param name="p"></param>
        void Relire(Contenance p);

        /// <summary>
        /// Cherche le contenance à partir de son ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        Contenance Chercher(int ID);

        /// <summary>
        /// Sauve (met à jour) le contenance dans la base
        /// </summary>
        /// <param name="p"></param>
        void Sauver(Contenance p);

        /// <summary>
        /// Supprime le contenance de la base
        /// </summary>
        /// <param name="p"></param>
        void Supprimer(Contenance p);

        /// <summary>
        /// Liste tous les contenances
        /// </summary>
        /// <returns></returns>
        Contenances Lister();

    }
}
