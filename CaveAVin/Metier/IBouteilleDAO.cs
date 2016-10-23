using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public interface IBouteilleDAO
    { 
        /// <summary>
        /// Ajoute le bouteille dans la base
        /// </summary>
        /// <param name="p"></param>
    void Créer(Bouteille p);

    /// <summary>
    /// Charge le bouteille depuis la base
    /// </summary>
    /// <param name="p"></param>
    void Relire(Bouteille p);

        /// <summary>
        /// Cherche la bouteille à partir de son ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
    Bouteille Chercher(int ID);

    /// <summary>
    /// Sauve (met à jour) la bouteille dans la base
    /// </summary>
    /// <param name="p"></param>
    void Sauver(Bouteille p);

    /// <summary>
    /// Supprime la bouteille de la base
    /// </summary>
    /// <param name="p"></param>
    void Supprimer(Bouteille p);

        /// <summary>
        /// Liste toute les bouteilles
        /// </summary>
        /// <returns></returns>
    Bouteille Lister();

}
}
