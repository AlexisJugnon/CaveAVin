using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public interface IPaysDAO
    {
        /// <summary>
        /// Ajoute le pays dans la base
        /// </summary>
        /// <param name="p"></param>
        void Créer(Pays p);

        /// <summary>
        /// Charge le pays depuis la base
        /// </summary>
        /// <param name="p"></param>
        void Relire(Pays p);

        /// <summary>
        /// Cherche le pays à partir de son ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        Pays Chercher(int ID);

        /// <summary>
        /// Sauve (met à jour) le pays dans la base
        /// </summary>
        /// <param name="p"></param>
        void Sauver(Pays p);

        /// <summary>
        /// Supprime le pays de la base
        /// </summary>
        /// <param name="p"></param>
        void Supprimer(Pays p);

        /// <summary>
        /// Liste tous les payss
        /// </summary>
        /// <returns></returns>
        Pays2 Lister();

    }
}
