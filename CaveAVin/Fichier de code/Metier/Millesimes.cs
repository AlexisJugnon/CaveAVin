﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Millesimes
    {
        private List<Millesime> millesimes = new List<Millesime>();

        #region opérations

        /// <summary>
        /// Ajoute un millesime à la liste
        /// </summary>
        /// <param name="p"></param>
        /// <exception cref="Exception">Si le millesime existe déjà</exception>
        public void Ajouter(Millesime p)
        {
            if (millesimes.Contains(p))
                throw new Exception("Le millesime existe déjà");
            millesimes.Add(p);
        }

        public void Ajouter(Bouteille b)
        {
            b.Millesime.Ajouter(b);
        }

        public void Supprimer(Millesime p)
        {
            millesimes.Remove(p);
        }

        /// <summary>
        /// Fournit l'ensemble des millesimes
        /// </summary>
        /// <returns>un tableau des millesimes</returns>
        public Millesime[] Lister()
        {
            return millesimes.ToArray();
        }
        #endregion
    }
}
