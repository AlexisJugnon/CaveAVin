using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metier
{
    public class Bouteille
    {
        #region attributs
        private int id;
        private Boolean bue;
        private string texte;
        private int posX;
        private int posY;
        private Casier casier;
        private Cru cru;
        private Millesime millesime;
        private Contenance contenance;
        private Domaine domaine;
        private Region region;
        private Type_vinification type_vinification;
        private Appelation appelation;
        private Type type;
        #endregion

        #region propriétés
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public Boolean Bue
        {
            get
            {
                return bue;
            }
            set
            {
                bue = value;
            }
        }
        public string Texte
        {
            get
            {
                return texte;
            }
            set
            {
                texte = value;
            }
        }
        public int PosX
        {
            get
            {
                return posX;
            }
            set
            {
                posX = value;
            }
        }
        public int PosY
        {
            get
            {
                return posY;
            }
            set
            {
                posY = value;
            }
        }

        public Casier Casier
        {
            get
            {
                return casier;
            }

            set
            {
                casier = value;
            }
        }

        public Cru Cru
        {
            get
            {
                return cru;
            }

            set
            {
                cru = value;
            }
        }

        public Millesime Millesime
        {
            get
            {
                return millesime;
            }

            set
            {
                millesime = value;
            }
        }

        public Contenance Contenance
        {
            get
            {
                return contenance;
            }

            set
            {
                contenance = value;
            }
        }

        public Domaine Domaine
        {
            get
            {
                return domaine;
            }

            set
            {
                domaine = value;
            }
        }

        public Region Region
        {
            get
            {
                return region;
            }

            set
            {
                region = value;
            }
        }

        public Type_vinification Type_vinification
        {
            get
            {
                return type_vinification;
            }

            set
            {
                type_vinification = value;
            }
        }

        public Appelation Appelation
        {
            get
            {
                return appelation;
            }

            set
            {
                appelation = value;
            }
        }

        public Type Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        public int IdAppelation {
            get
            {
                int id = 0;
                if (appelation != null)
                    id = appelation.Id;
                return id;
            }
        }

        public int IdCasier {
            get
            {
                int id = 0;
                if (casier != null)
                    id = casier.Id;
                return id;
            }
        }

        public int IdContenance {
            get
            {
                int id = 0;
                if (contenance != null)
                    id = contenance.Id;
                return id;
            }
        }

        public int IdCru {
            get
            {
                int id = 0;
                if (cru != null)
                    id = cru.Id;
                return id;
            }
        }

        public int IdDomaine {
            get
            {
                int id = 0;
                if (domaine != null)
                    id = domaine.Id;
                return id;
            }
        }

        public int IdMillesime {
            get
            {
                int id = 0;
                if (millesime != null)
                    id = millesime.Id;
                return id;
            }
        }

        public int IdRegion {
            get
            {
                int id = 0;
                if (region != null)
                    id = region.Id;
                return id;
            }
        }

        public int IdType {
            get
            {
                int id = 0;
                if (type != null)
                    id = type.Id;
                return id;
            }
        }

        public int IdType_vinification {
            get
            {
                int id = 0;
                if (type_vinification != null)
                    id = type_vinification.Id;
                return id;
            }
        }

        #endregion
    }
}
