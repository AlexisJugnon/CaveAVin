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
        /// <param name="p">Le produit à ajouter</param>
        /// <exception cref="Exception">Si le produit existe déjà</exception>
        public void Ajouter(Type p)
        {
            if (types.Contains(p))
                throw new Exception("Le produit existe déjà");
            types.Add(p);
        }

        /// <summary>
        /// Ajoute le type d'une bouteille à la liste de type
        /// </summary>
        /// <param name="b">bouteille à ajouter</param>
        public void Ajouter(Bouteille b)
        {
            b.Type.Ajouter(b);
        }

        /// <summary>
        /// Supprime un type de la liste de type
        /// </summary>
        /// <param name="p">type à supprimer</param>
        public void supprimer(Type p)
        {
            types.Remove(p);
        }


        ///<summary>
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
