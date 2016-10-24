using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Types
    {
        private List<Type> types = new List<Type>();

        #region opérations

        /// <summary>
        /// Ajoute un produit à la liste
        /// </summary>
        /// <param name="p"></param>
        /// <exception cref="Exception">Si le produit existe déjà</exception>
        public void Ajouter(Type p)
        {
            if (types.Contains(p))
                throw new Exception("Le produit existe déjà");
            types.Add(p);
        }

        public void Ajouter(Bouteille b) { }

        public void supprimer(Type p) { }
        /// <summary>
        /// Fournit l'ensemble des produits
        /// </summary>
        /// <returns>un tableau des produits</returns>
        public Type[] Lister()
        {
            return types.ToArray();
        }
        #endregion
    }
}
