using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public interface ICitationDAO
    {
        /// <summary>
        /// Cherche un texte pour le saviez-vous en fonction de l'identifiant donnée
        /// </summary>
        /// <param name="IDC">L'identifiant à rechercher</param>
        /// <returns>Le texte à afficher pour le Saviez-vous</returns>
        String chercher(int IDC);
    }
}
