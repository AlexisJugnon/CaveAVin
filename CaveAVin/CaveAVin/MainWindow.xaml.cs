﻿using MySql.Data.MySqlClient;
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

            // Gère les Grid à afficher au démarrage de l'application
            #region InitAffichage
            MainWindows.Visibility = Visibility.Visible;
            Accueil.Visibility = Visibility.Visible;
            affichBoute.Visibility = Visibility.Hidden;
            MultiCasier.Visibility = Visibility.Hidden;
            Faux_menu.Visibility = Visibility.Hidden;
            #endregion

            initBDD();

            initGridCasier();
            afficherBouteille();
        }

        /// <summary>
        /// Affiche le menu latéral
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            //récupération des coordonnées de la bouteille + select idBouteille en fonction des coordonnées et du casier
            int ligne = Int32.Parse(sender.ToString().Substring(32, 1));
            int col = Int32.Parse(sender.ToString().Substring(35, 1));

            int casier = nbasier;
            try
            {
                List<Metier.Bouteille> b = new List<Metier.Bouteille>();

                DAO.TypeDAO typ = new DAO.TypeDAO(DAO.BDD.Instance.Connexion);
                Metier.Types typs = typ.Lister();

                DAO.PaysDAO pay = new DAO.PaysDAO(DAO.BDD.Instance.Connexion);
                Metier.Pays2 payss = pay.Lister();

                DAO.RegionDAO reg = new DAO.RegionDAO(DAO.BDD.Instance.Connexion);
                //Metier.Regions regions = reg.Lister();

                DAO.AppelationDAO app = new DAO.AppelationDAO(DAO.BDD.Instance.Connexion);
                Metier.Appelations apps = app.Lister();

                DAO.ContenanceDAO cont = new DAO.ContenanceDAO(DAO.BDD.Instance.Connexion);
                Metier.Contenances conts = cont.Lister();

                ajouteBouteille f = new ajouteBouteille();
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

        #region affichageCasier

        private int nbasier = 1;
        private int nbCasier = 1;
        private Button[,] button;
        private Grid[] gestionCasier;
        private ColumnDefinition[] col1;
        private RowDefinition[] col2;

        /// <summary>
        /// Permet d'initialiser tous les casiers sous forme de grid pour pouvoir les afficher plus tard
        /// </summary>
        private void initGridCasier()
        {
            nbCasier = req.SelInt("Select Count(IdCasier) FROM Casier");
            gestionCasier = new Grid[nbCasier+1];
            
            int nbC, nbL;
            int HeightBoutton, WidthBoutton;


            for (int i = 1; i < nbCasier+1; i++)
            {
                gestionCasier[i] = new Grid();
                test.Children.Add(gestionCasier[i]);
                gestionCasier[i].Visibility = Visibility.Hidden;

                nbC = 0; nbL = 0;
                nbL = req.SelInt("Select Largeur_X FROM Casier where idCasier = " + i);
                nbC = req.SelInt("Select Largeur_Y FROM Casier where idCasier = " + i);

                WidthBoutton = (int)affBout.Width / nbC;
                HeightBoutton = (int)affBout.Height / nbL;

                col1 = new ColumnDefinition[nbC];
                col2 = new RowDefinition[nbL];

                for (int f = 0; f < nbC; f++)
                {
                    col1[f] = new ColumnDefinition();
                    col1[f].Width = GridLength.Auto;

                    gestionCasier[i].ColumnDefinitions.Add(col1[f]);
                }

                for (int f = 0; f < nbL; f++)
                {
                    col2[f] = new RowDefinition();
                    col2[f].Height = GridLength.Auto;

                    gestionCasier[i].RowDefinitions.Add(col2[f]);

                }
            #region GestionAffichBouteille
            //Gère l'affichage des bouteilles dans le casier
            string res;
            button = new Button[nbC + 1, nbL + 1];
            for (int k = 0; k < nbC; k++)
            {
                for (int j = 0; j < nbL; j++)
                {
                    button[k, j] = new Button();
                    
                    gestionCasier[i].Children.Add(button[k, j]);
                    button[k, j].Visibility = Visibility.Visible;
                        button[k, j].Content = k + ", " + j;
                        button[k, j].Width = WidthBoutton;
                        button[k, j].Height = HeightBoutton;
                    Grid.SetRow(button[k, j], k);
                    Grid.SetColumn(button[k, j], j);
                    res = "";
                    try
                    {
                        res = req.SelStr1("SELECT NomType From Type natural join Bouteille where idCasier =" + i +
                        " and Bouteille.Position_X = " + k + " and Bouteille.Position_Y = " + j + ";", "NomType");
                    }
                    catch
                    {

                    }

                    if (res == "Blanc")
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

            }

            #endregion
        }

        /// <summary>
        /// permet d'afficher le casier suivant ou précedent
        /// </summary>
        private void afficherBouteille()
        {
            // Permet de gérer l'affichage des flêches pour la gestion des casiers
            #region GestionFleche

            if (nbCasier > 1)
            {
                MultiCasier.Visibility = Visibility.Visible;
            }

            if (nbasier == 1)
            {
                casPrec.Visibility = Visibility.Hidden;
            }
            else
            {
                casPrec.Visibility = Visibility.Visible;
            }

            if (nbasier < nbCasier)
            {
                casSuiv.Visibility = Visibility.Visible;
            }
            else
            {
                casSuiv.Visibility = Visibility.Hidden;
            }
            #endregion

            lNomC.Content = req.SelStr1("Select NomCasier From Casier Where idCasier = " + nbasier, "NomCasier");

            gestionCasier[nbasier].Visibility = Visibility.Visible;

        }

        /// <summary>
        /// Permet de gérer le click sur une bouteille
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectionBouteille(Object sender, EventArgs e)
        {
            Decaler();
            
            //récupération des coordonnées de la bouteille + select idBouteille en fonction des coordonnées et du casier
            int ligne = Int32.Parse(sender.ToString().Substring(32,1));
            int col = Int32.Parse(sender.ToString().Substring(35, 1));

            int idBout = Int32.Parse(req.SelStr1("Select IdBouteille From Bouteille Where IdCasier = " + nbasier + " AND Position_X = " + col + " And Position_Y = " + ligne, "IdBouteille"));

            Console.WriteLine(idBout);
            displayFicheDetailBouteille(true);
        }

        /// <summary>
        /// Permet de gérer le click sur le bouton suivant dans l'affichage des casiers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void casSuiva(object sender, RoutedEventArgs e)
        {
            gestionCasier[nbasier].Visibility = Visibility.Hidden;
            nbasier++;
            afficherBouteille();
        }

        /// <summary>
        /// Permet de décaler l'affichage du casier pour pouvoir afficher la fiche bouteille
        /// </summary>
        private void Decaler()
        {
            aDecal.Margin = new Thickness(0, 30, 0, 50);
        }

        /// <summary>
        /// Permet de gérer le click sur le bouton précedent dans l'affichage des casiers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void casPrec_Click(object sender, RoutedEventArgs e)
        {
            gestionCasier[nbasier].Visibility = Visibility.Hidden;
            nbasier--;
            afficherBouteille();
        }

        #endregion

        #region Affichage Détail Bouteille

        // A refaire avec en passage en parametre num de la colonne et num de la ligne
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


        /// <summary>
        /// Affiche la page des casier et cache l'accueil lorsque on appuie sur le bouton du milieu de l'accueil
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_VoirCave_Click(object sender, RoutedEventArgs e)
        {
            Accueil.Visibility = Visibility.Hidden;
            affichBoute.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Affiche la page d'accueil et cache les casiers lorsque on appuie sur le 1er bouton du menu déroulant
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_Menu1_Click(object sender, RoutedEventArgs e)
        {
            Accueil.Visibility = Visibility.Visible;
            affichBoute.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// affiche le texte pour le saviez-vous
        /// </summary>
        /// <param name="c"></param>
        private void ChoixTexte(DAO.CitationDAO c)
        {

            Random r = new Random();
            TB_SaviezVous.Text = c.chercher(r.Next(1, 11));
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

            // crée une instance DAO.citation + l'affiche dans accueil
            DAO.CitationDAO daoCitation = new DAO.CitationDAO(DAO.BDD.Instance.Connexion);

            ChoixTexte(daoCitation);

            Metier.Casiers c = daoCasier.Lister();

            foreach (Metier.Casier ct in c.Lister())
            {
                Metier.Bouteilles bout = daoBouteille.Lister(ct);
                ct.Ajouter(bout);

                foreach (Metier.Bouteille b in bout.Lister())
                {
                    b.Casier = ct;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_Supprimer_Click(object sender, RoutedEventArgs e)
        {
            //int ligne = Int32.Parse(sender.ToString().Substring(32, 1));
            //int col = Int32.Parse(sender.ToString().Substring(35, 1));

            
            //DAO.BouteilleDAO b = new DAO.BouteilleDAO(DAO.BDD.Instance.Connexion);
            //b.RetirerBue(b);
        }
    }
}
