using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CaveAVin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            initBDD();
        }

        /// <summary>
        /// Initialise la connexion au SGBDR
        /// </summary>
        private void initBDD()
        {
            // paramètres de la connexion
            DAO.BDD.Instance.Connexion.ConnectionString =
                "Database=WineFinder;DataSource=137.74.233.210;User Id=user; Password=user";

            // crée les objets DAO. pour lire la base
            DAO.BouteilleDAO daoBouteille = new DAO.BouteilleDAO(DAO.BDD.Instance.Connexion);
            DAO.CasierDAO daoCasier = new DAO.CasierDAO(DAO.BDD.Instance.Connexion);

            Metier.Casiers c = daoCasier.Lister(); // cette ligne pose probleme
            foreach(Metier.Casier ct in c.Lister())
            {
                Metier.Bouteilles bout = daoBouteille.Lister(ct);
                ct.Ajouter(bout);
                foreach(Metier.Bouteille b in bout.Lister())
                {
                    b.Casier = ct;
                    listView.Items.Add(b.ToString());
                }
            }

        }
    }
}
