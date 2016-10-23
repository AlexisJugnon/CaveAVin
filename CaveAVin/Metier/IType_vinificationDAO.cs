using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public interface IType_vinificationDAO
    {
        /// <summary>
        /// Ajoute le type_vinification dans la base
        /// </summary>
        /// <param name="p"></param>
        void Créer(Type_vinification p);

        /// <summary>
        /// Charge le type_vinification depuis la base
        /// </summary>
        /// <param name="p"></param>
        void Relire(Type_vinification p);

        /// <summary>
        /// Cherche le type_vinification à partir de son ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        Type_vinification Chercher(int ID);

        /// <summary>
        /// Sauve (met à jour) le type_vinification dans la base
        /// </summary>
        /// <param name="p"></param>
        void Sauver(Type_vinification p);

        /// <summary>
        /// Supprime le type_vinification de la base
        /// </summary>
        /// <param name="p"></param>
        void Supprimer(Type_vinification p);

        /// <summary>
        /// Liste tous les type_vinifications
        /// </summary>
        /// <returns></returns>
        Type_vinifications Lister();

    }
}
