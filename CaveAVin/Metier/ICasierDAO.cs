﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public interface ICasierDAO
    {
        /// <summary>
        /// Ajoute le casier dans la base
        /// </summary>
        /// <param name="p"></param>
        void Créer(Casier p);

        /// <summary>
        /// Charge le casier depuis la base
        /// </summary>
        /// <param name="p"></param>
        void Relire(Casier p);

        /// <summary>
        /// Cherche le casier à partir de son ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        Casier Chercher(int ID);

        /// <summary>
        /// Sauve (met à jour) le casier dans la base
        /// </summary>
        /// <param name="p"></param>
        void Sauver(Casier p);

        /// <summary>
        /// Supprime le casier de la base
        /// </summary>
        /// <param name="p"></param>
        void Supprimer(Casier p);

        /// <summary>
        /// Liste tous les casiers
        /// </summary>
        /// <returns></returns>
        Casiers Lister();

    }
}
