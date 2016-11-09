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


    ///Noms des différents éléments graphiques composant le MainWindows
    ///
    ///Le grid global de la fenêtre s'appelle MainWindows
    ///
    ///         Pour l'accueil
    /// On a le bouton rechercher qui s'appelle BT_rechercher
    /// 
    /// on a le menu cliquable constitué de deux éléments.
    ///    - Le premier est la base de l'objet menu appellé BaseMenu
    ///    - Le deuxième est le menu cliquable appelé Menu
    ///    
    ///On a le sous menu qui est apparait quand on clique sur l'objet Menu.
    ///Ce dernier s'appelle FauxMenu
    ///
    ///Le sous menu est constitué de deux boutons tests s'appellant button1 et button2       ILS DEVRONT ETRE RENOMME PLUS TARD
    ///
    ///Le textBox pour le saviez-bout s'appelle TB_SaviezVous. De plus, il est multiligne et 
    ///inclus une scroll bar quand le nombre de ligne dépassent la taille du textBox et l'utilisateur ne peut écrire dedans
    ///
    ///le grid qui contient les objets de l'accueil s'appelle Accueil




    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           
            //initBDD();
        }

   
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Faux_menu.Visibility = Visibility;
           
        }

        
        /// <summary>
        /// Cache le "sous_menu" quand on quitte les boutons le constituant.
        /// ATTENTION : Il faut que les boutons soient collées sinon l'objet disparaitra après avoir quitter le bouton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void part(object sender, MouseEventArgs e)
        {
            Faux_menu.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Cache les éléments à accueil et affiche les éléments d'ajouter bouteille
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_VoirBouteille_Click(object sender, RoutedEventArgs e)
        {
            Accueil.Visibility = Visibility.Hidden;
            nouvelleBout(sender, e);
        }

        /// <summary>
        /// Crée une nouvelle bouteille
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nouvelleBout(object sender, RoutedEventArgs e)
        {
            try
            {
                Metier.Bouteille b = new Metier.Bouteille();

                DAO.RegionDAO reg = new DAO.RegionDAO(DAO.BDD.Instance.Connexion);
                Metier.Regions regions = reg.Lister();

                DAO.AppelationDAO app = new DAO.AppelationDAO(DAO.BDD.Instance.Connexion);
                Metier.Appelations apps = app.Lister();

                DAO.PaysDAO pay = new DAO.PaysDAO(DAO.BDD.Instance.Connexion);
                Metier.Pays2 payss = pay.Lister();

                DAO.ContenanceDAO cont = new DAO.ContenanceDAO(DAO.BDD.Instance.Connexion);
                Metier.Contenances conts = cont.Lister();

                ajouteBouteille f = new ajouteBouteille(b, regions,apps,payss,conts);
                if (f.ShowDialog() == true)
                {
                    DAO.BouteilleDAO daoBouteille = new DAO.BouteilleDAO(DAO.BDD.Instance.Connexion);
                    daoBouteille.Créer(b); // ajoute la bouteille dans la base                    
                    //; // et dans l'IHM
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        /// <summary>
        /// Initialise la connexion au SGBDR
        /// </summary>
        /*private void initBDD()
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

        }*/
    }
}
