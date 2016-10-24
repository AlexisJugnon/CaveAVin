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
        /// Ajoute le appelation dans la base
        /// </summary>
        /// <param name="p"></param>
        void Créer(Appelation p);

        /// <summary>
        /// Charge le appelation depuis la base
        /// </summary>
        /// <param name="p"></param>
        void Relire(Appelation p);

        /// <summary>
        /// Cherche le appelation à partir de son ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        Appelation Chercher(int ID);

        /// <summary>
        /// Sauve (met à jour) le appelation dans la base
        /// </summary>
        /// <param name="p"></param>
        void Sauver(Appelation p);

        /// <summary>
        /// Supprime le appelation de la base
        /// </summary>
        /// <param name="p"></param>
        void Supprimer(Appelation p);

        /// <summary>
        /// Liste tous les appelations
        /// </summary>
        /// <returns></returns>
        Appelations Lister();

    }
}
