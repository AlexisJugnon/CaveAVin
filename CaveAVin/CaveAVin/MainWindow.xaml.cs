﻿using System;
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

                ajouteBouteille f = new ajouteBouteille(b,typs, regions,apps,payss,conts);
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

        private void afficherBouteille()
        {
            int nbC = 0, nbL = 0;
            int nbCasier = req.SelInt("Select Count(IdCasier) FROM Casier");
            if (nbCasier <= 1)
            {
                MultiCasier.Visibility = Visibility.Hidden;
            }

            for (int i = 0; i < nbCasier; i++)
            {
                nbL = req.SelInt("Select Largeur_X FROM Casier");
                nbC = req.SelInt("Select Largeur_Y FROM Casier");

                int WidthBoutton = (int)affBout.Width / nbC;
                int HeightBoutton = (int)affBout.Height / nbL;

                string res;
                Button[,] button = new Button[nbC+1, nbL+1];
                for (int k = 1; k <= nbC; k++)
                {
                    for (int j = 1; j <= nbL; j++)
                    {
                        button[k, j] = new Button();
                        button[k, j].Visibility = Visibility.Visible;
                        button[k, j].Width = WidthBoutton;
                        button[k, j].Height = HeightBoutton;

                        res = req.SelStr1("SELECT NomType From Type natural join Bouteille where idCasier =" + i + 
                            " and Bouteille.Position_X = "+ k%nbC +" and Bouteille.Position_Y = "+ j%nbL +";","NomType");
                        label.Content = res;

                        if(res == "blanc")
                        {
                            button[k, j].Content = "Blanc.png";
                        }
                    }
                }
            }
        }

       
    }
}
