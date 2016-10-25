using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Type_vinifications
    {
        private List<Type_vinification> type_vinifications = new List<Type_vinification>();

        #region opérations

        /// <summary>
        /// Ajoute un type_vinification à la liste
        /// </summary>
        /// <param name="p"></param>
        /// <exception cref="Exception">Si le type_vinification existe déjà</exception>
        public void Ajouter(Type_vinification p)
        {
            if (type_vinifications.Contains(p))
                throw new Exception("Le type_vinification existe déjà");
            type_vinifications.Add(p);
        }

        public void Ajouter(Bouteille b)
        {
            b.Casier.Ajouter(b);
        }

        public void Supprimer(Type_vinification p)
        {
            type_vinifications.Remove(p);
        }

        /// <summary>
        /// Fournit l'ensemble des type_vinifications
        /// </summary>
        /// <returns>un tableau des type_vinifications</returns>
        public Type_vinification[] Lister()
        {
            return type_vinifications.ToArray();
        }
        #endregion
    }
}
