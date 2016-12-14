using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public interface IMillesimeDAO
    {
        /// <summary>
        /// Ajoute le millesime dans la base
        /// </summary>
        /// <param name="p"></param>
        void Créer(Millesime p);

        /// <summary>
        /// Charge le millesime depuis la base
        /// </summary>
        /// <param name="p"></param>
        void Relire(Millesime p);

        /// <summary>
        /// Cherche le millesime à partir de son ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        Millesime Chercher(int ID);

        /// <summary>
        /// Sauve (met à jour) le millesime dans la base
        /// </summary>
        /// <param name="p"></param>
        void Sauver(Millesime p);

        /// <summary>
        /// Supprime le millesime de la base
        /// </summary>
        /// <param name="p"></param>
        void Supprimer(Millesime p);

        /// <summary>
        /// Liste tous les millesimes
        /// </summary>
        /// <returns></returns>
        Millesimes Lister();

    }
}
