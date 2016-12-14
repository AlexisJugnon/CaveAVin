using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public interface IRegionDAO
    {
        /// <summary>
        /// Ajoute le region dans la base
        /// </summary>
        /// <param name="p"></param>
        void Créer(Region p);

        /// <summary>
        /// Charge le region depuis la base
        /// </summary>
        /// <param name="p"></param>
        void Relire(Region p);

        /// <summary>
        /// Cherche le region à partir de son ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        Region Chercher(int ID);

        /// <summary>
        /// Sauve (met à jour) le region dans la base
        /// </summary>
        /// <param name="p"></param>
        void Sauver(Region p);

        /// <summary>
        /// Supprime le region de la base
        /// </summary>
        /// <param name="p"></param>
        void Supprimer(Region p);

        /// <summary>
        /// Liste tous les regions
        /// </summary>
        /// <returns></returns>
        Regions Lister();

    }
}
