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
        /// <param name="p">La bouteille à ajouter</param>
    void Créer(Bouteille p);

    /// <summary>
    /// Charge le bouteille depuis la base
    /// </summary>
    /// <param name="p">La bouteille</param>
    void Relire(Bouteille p);

        /// <summary>
        /// Cherche la bouteille à partir de son ID
        /// </summary>
        /// <param name="ID">l'identifiant</param>
        /// <returns>La bouteille chargée, ou null si inexistante</returns>
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
        /// <returns>Les Bouteilles existantes</returns>
    Bouteilles Lister();

    Bouteilles Lister(Casier c);
        Bouteilles Lister(Appelation a);
        Bouteilles Lister(Contenance c);
        Bouteilles Lister(Cru c);
        Bouteilles Lister(Domaine d);
        Bouteilles Lister(Millesime m);
        Bouteilles Lister(Region r);
        Bouteilles Lister(Type t);
        Bouteilles Lister(Type_vinification tp);

}
}
