using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public interface Modification
    {
        #region opérations
        void Ajouter(Bouteille b);
        void Ajouter(Bouteilles b);
        void Supprimer(Bouteille b);
        Bouteille[] Lister();
        #endregion
    }
}
