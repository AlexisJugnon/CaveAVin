using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public interface ICaveDAO
    {
        /// <summary>
        /// Ajoute le cave dans la base
        /// </summary>
        /// <param name="p"></param>
        void Créer(Cave p);

        /// <summary>
        /// Charge le cave depuis la base
        /// </summary>
        /// <param name="p"></param>
        void Relire(Cave p);

        /// <summary>
        /// Cherche le cave à partir de son ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        Cave Chercher(int ID);

        /// <summary>
        /// Sauve (met à jour) le cave dans la base
        /// </summary>
        /// <param name="p"></param>
        void Sauver(Cave p);

        /// <summary>
        /// Supprime le cave de la base
        /// </summary>
        /// <param name="p"></param>
        void Supprimer(Cave p);

        /// <summary>
        /// Liste tous les caves
        /// </summary>
        /// <returns></returns>
        Caves Lister();

    }
}
