using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public interface IDomaineDAO
    {
        /// <summary>
        /// Ajoute le domaine dans la base
        /// </summary>
        /// <param name="p"></param>
        void Créer(Domaine p);

        /// <summary>
        /// Charge le domaine depuis la base
        /// </summary>
        /// <param name="p"></param>
        void Relire(Domaine p);

        /// <summary>
        /// Cherche le domaine à partir de son ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        Domaine Chercher(int ID);

        /// <summary>
        /// Sauve (met à jour) le domaine dans la base
        /// </summary>
        /// <param name="p"></param>
        void Sauver(Domaine p);

        /// <summary>
        /// Supprime le domaine de la base
        /// </summary>
        /// <param name="p"></param>
        void Supprimer(Domaine p);

        /// <summary>
        /// Liste tous les domaines
        /// </summary>
        /// <returns></returns>
        Domaines Lister();

    }
}
