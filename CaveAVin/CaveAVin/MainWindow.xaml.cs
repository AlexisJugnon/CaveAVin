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
        private DAO.reqSQL req = new DAO.reqSQL(DAO.BDD.Instance.Connexion);
        public MainWindow()
        {
            InitializeComponent();

            MainWindows.Visibility = Visibility.Visible;
            Accueil.Visibility = Visibility.Visible;
            affichBoute.Visibility = Visibility.Hidden;
            MultiCasier.Visibility = Visibility.Hidden;
            Faux_menu.Visibility = Visibility.Hidden;

            initBDD();

            afficherBouteille();
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
                List<Metier.Bouteille> b = new List<Metier.Bouteille>();

                DAO.TypeDAO typ = new DAO.TypeDAO(DAO.BDD.Instance.Connexion);
                Metier.Types typs = typ.Lister();

                DAO.RegionDAO reg = new DAO.RegionDAO(DAO.BDD.Instance.Connexion);
                Metier.Regions regions = reg.Lister();

                DAO.AppelationDAO app = new DAO.AppelationDAO(DAO.BDD.Instance.Connexion);
                Metier.Appelations apps = app.Lister();

                DAO.PaysDAO pay = new DAO.PaysDAO(DAO.BDD.Instance.Connexion);
                Metier.Pays2 payss = pay.Lister();

                DAO.ContenanceDAO cont = new DAO.ContenanceDAO(DAO.BDD.Instance.Connexion);
                Metier.Contenances conts = cont.Lister();

                ajouteBouteille f = new ajouteBouteille(b, typs, regions, apps, payss, conts);
                if (f.ShowDialog() == true)
                {
                    DAO.BouteilleDAO daoBouteille = new DAO.BouteilleDAO(DAO.BDD.Instance.Connexion);
                    foreach (Metier.Bouteille bt in b)
                    {
                        daoBouteille.Créer(bt); // ajoute la bouteille dans la base                    
                        // et dans l'IHM
                    }
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
        private void initBDD()
        {
            DAO.BDD.Instance.Connexion.ConnectionString = "Database=WineFinder;DataSource=137.74.233.210;User Id=user; Password=user";
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
                }
            }
        }


        #region affichageCasier

        private int nbasier = 1;
        private Button[,] button;
        private ColumnDefinition[] col1;
        private RowDefinition[] col2;


        private void afficherBouteille()
        {
            int nbC = 0, nbL = 0;
            int nbCasier = req.SelInt("Select Count(IdCasier) FROM Casier");
            if (nbCasier > 1)
            {
                MultiCasier.Visibility = Visibility.Visible;
            }

            nbL = req.SelInt("Select Largeur_X FROM Casier");
            nbC = req.SelInt("Select Largeur_Y FROM Casier");

            int WidthBoutton = (int)affBout.Width / nbC;
            int HeightBoutton = (int)affBout.Height / nbL;

            #region GestionGrid
            col1 = new ColumnDefinition[nbC];
            col2 = new RowDefinition[nbL];
            for (int f = 0; f < nbC; f++)
            {
                col1[f] = new ColumnDefinition();
                col1[f].Width = GridLength.Auto;

                test.ColumnDefinitions.Add(col1[f]);
            }
            for (int f = 0; f < nbL; f++)
            {
                col2[f] = new RowDefinition();
                col2[f].Height = GridLength.Auto;

                test.RowDefinitions.Add(col2[f]);
            }

            #endregion

            #region GestionAffichBouteille
            string res;
            button = new Button[nbC+1, nbL+1];
            for (int k = 0; k < nbC; k++)
            {
                for (int j = 0; j < nbL; j++)
                {
                    button[k, j] = new Button();
                    test.Children.Add(button[k, j]);
                    button[k, j].Visibility = Visibility.Visible;
                    button[k, j].Width = WidthBoutton;
                    button[k, j].Height = HeightBoutton;
                    Grid.SetRow(button[k, j], k);
                    Grid.SetColumn(button[k, j], j);


                    res = req.SelStr1("SELECT NomType From Type natural join Bouteille where idCasier =" + nbasier + 
                        " and Bouteille.Position_X = "+ k +" and Bouteille.Position_Y = "+ j +";","NomType");
                     lNomC.Content = req.SelStr1("Select NomCasier From Casier Where idCasier = "+ nbasier,"NomCasier");

                    if(res == "Blanc")
                    {
                        var brush = new ImageBrush();
                        brush.ImageSource = new BitmapImage(new Uri("../../../CaveAVin/BlancCasier.png", UriKind.Relative));
                        button[k, j].Background = brush;
                        button[k, j].Click += selectionBouteille;
                    }
                    else if (res == "Rouge")
                    {
                        var brush = new ImageBrush();
                        brush.ImageSource = new BitmapImage(new Uri("../../../CaveAVin/RougeCasier.png", UriKind.Relative));
                        button[k, j].Background = brush;
                        button[k, j].Click += selectionBouteille;
                    }
                    else
                    {
                        var brush = new ImageBrush();
                        brush.ImageSource = new BitmapImage(new Uri("../../../CaveAVin/CaseVide.png", UriKind.Relative));
                        button[k, j].Background = brush;
                        button[k, j].Click += nouvelleBout;
                    }
                }
            }

            #endregion
        }

        private void selectionBouteille(Object sender, EventArgs e)
        {
            Decaler();
            displayFicheDetailBouteille(true);
        }

        private void casSuiva(object sender, RoutedEventArgs e)
        {
            nbasier++;
            afficherBouteille();
        }
        #endregion

        #region Affichage Détail Bouteille

        // A finir quand les cases du casier seront affiché
        private void afficherDetailBouteille(Metier.Bouteille bouteille)
        {
            if (bouteille != null)
            {
                lblAppelation.Content = bouteille.Appelation?.NomAppelation;
                lblCategorie.Content = bouteille.Type?.NomType;
                lblContenance.Content = bouteille.Contenance?.Valeur;
                lblCru.Content = bouteille.Cru?.NomCru;
                lblDomaine.Content = bouteille.Domaine?.NomDomaine;
                lblMillesime.Content = bouteille.Millesime?.NomMillesime;
                lblPays.Content = bouteille.Region?.Pays?.NomPays;
                lblRegion.Content = bouteille.Region?.NomRegion;
                lblVinification.Content = bouteille.Type_vinification?.NomVinif;
                displayFicheDetailBouteille(true);
            }
        } 

        private void displayFicheDetailBouteille(bool rendreVisible)
        {
            ficheDetailBouteille.Visibility = rendreVisible ? Visibility.Visible : Visibility.Hidden;
        }

        #endregion

        private void BT_VoirCave_Click(object sender, RoutedEventArgs e)
        {
            Accueil.Visibility = Visibility.Hidden;
            affichBoute.Visibility = Visibility.Visible;
        }

        private void Decaler()
        {
            aDecal.Margin = new Thickness(0, 30, 0, 50);
        }
    }
}
