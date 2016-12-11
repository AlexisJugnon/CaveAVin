using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        #region attributAjoutSuppr
        Boolean supprimer; // faux si on a cliquer sur le bouton supprimer (pour la selection des bouteilles à supprimer)
        List<Metier.Position> listeBouteilleAjout = new List<Metier.Position>(); //liste  des emplacements ou new bouteille
        List<Metier.Position> listeBouteilleSuppr = new List<Metier.Position>();
        #endregion

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
            ajoutB.Visibility = Visibility.Hidden;
            #endregion

            initBDD();
            supprimer = false;
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
            initAjoutBouteille();
        }

        #region affichageCasier

        private int nbasier = 1;
        private int nbCasier = 1;
        private List<Button[,]> but = new List<Button[,]>();
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
            gestionCasier = new Grid[nbCasier + 1];

            int nbC, nbL;
            int HeightBoutton, WidthBoutton;


            for (int i = 1; i < nbCasier + 1; i++)
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
                button = new Button[nbC, nbL];
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
                            " and Bouteille.Position_X = " + k + " and Bouteille.Position_Y = " + j + " and Bue = 0;", "NomType");
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
                        else if (res == "Rosé")
                        {
                            var brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../../CaveAVin/RoséCasier.png", UriKind.Relative));
                            button[k, j].Background = brush;
                            button[k, j].Click += selectionBouteille;
                        }
                        else if (res == "Pétillant")
                        {
                            var brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../../CaveAVin/PétillantCasier.png", UriKind.Relative));
                            button[k, j].Background = brush;
                            button[k, j].Click += selectionBouteille;
                        }
                        else
                        {
                            var brush = new ImageBrush();
                            brush.ImageSource = new BitmapImage(new Uri("../../../CaveAVin/CaseVide.png", UriKind.Relative));
                            button[k, j].Background = brush;
                            button[k, j].Click += selectBouteille;
                        }
                    }
                }
                but.Add(button);
            }

            #endregion
        }

        /// <summary>
        /// selection d'un emplacement d'un casier vide
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectBouteille(object sender, RoutedEventArgs e)
        {
            int ligne = Int32.Parse(sender.ToString().Substring(32, 1));
            int col = Int32.Parse(sender.ToString().Substring(35, 1));
            Metier.Position posi = new Metier.Position(ligne, col);

            bool existe = false;
            foreach(Metier.Position pos in listeBouteilleAjout)
            {
                if (pos.X == ligne && pos.Y==col && pos.Casier==nbasier)
                {
                    existe = true;
                    listeBouteilleAjout.Remove(pos);
                    break;
                }
            }
            if (existe)
            {            
                ((Button)but[nbasier - 1].GetValue(ligne, col)).Opacity = 1.0;

            }
            else
            {
                posi.Casier = nbasier;
                listeBouteilleAjout.Add(posi);
                ((Button)but[nbasier - 1].GetValue(ligne, col)).Opacity = 0.5;

            }
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

        private int l_ligne;
        private int l_col;
        /// <summary>
        /// Permet de gérer le click sur une bouteille
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectionBouteille(Object sender, EventArgs e)
        {
            if (!supprimer)
            {
                l_ligne = Int32.Parse(sender.ToString().Substring(32, 1));
                l_col = Int32.Parse(sender.ToString().Substring(35, 1));


                Decaler();
                var button = (Button)sender;
                var textContent = button.Content.ToString();
                var coordonee = textContent.Split(',');

                    Metier.Position posi = new Metier.Position(l_ligne, l_col);
                    posi.Casier = nbasier;

// if (Int32.TryParse(coordonee[0], out ligneIndex) && Int32.TryParse(coordonee[1], out colonneIndex))
//{
                var casierDao = new CasierDAO(DAO.BDD.Instance.Connexion);
                var bouteilleDao = new BouteilleDAO(DAO.BDD.Instance.Connexion);
                var casier = casierDao.Chercher(nbasier);
                var bouteille = bouteilleDao.Chercher(posi.X, posi.Y, posi.Casier);
                Console.WriteLine(bouteille.IdDomaine);
                    AfficherDetailBouteille(bouteille);
// }
            }
            else
            {
                int ligne = Int32.Parse(sender.ToString().Substring(32, 1));
                int col = Int32.Parse(sender.ToString().Substring(35, 1));
                Metier.Position posi = new Metier.Position(ligne, col);

                bool existe = false;
                foreach (Metier.Position pos in listeBouteilleSuppr)
                {
                    if (pos.X == ligne && pos.Y == col  && pos.Casier==nbasier)
                    {
                        existe = true;
                        listeBouteilleSuppr.Remove(pos);
                        break;
                    }
                }
                if (existe)
                {
                    ((Button)but[nbasier - 1].GetValue(ligne, col)).Opacity = 1.0;
                    

                }
                else
                {
                    posi.Casier = nbasier;
                    listeBouteilleSuppr.Add(posi);
                    ((Button)but[nbasier - 1].GetValue(ligne, col)).Opacity = 0.5;


                }
            }
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
        /// <summary>
        /// Affiche les détail d'une bouteille
        /// </summary>
        /// <param name="bouteille"></param>
        private void AfficherDetailBouteille(Metier.Bouteille bouteille)
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
                DisplayFicheDetailBouteille(true);
            }
        }

        /// <summary>
        /// Affiche ou masque la fiche du détail de la bouteille
        /// </summary>
        /// <param name="rendreVisible">Vrai si l'on doit afficher la fiche; sinon faux</param>
        private void DisplayFicheDetailBouteille(bool rendreVisible)
        {
            ajoutB.Visibility = Visibility.Hidden;
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

            ((Button)but[nbasier - 1].GetValue(l_ligne, l_col)).Opacity = 1.0;

            var brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri("../../../CaveAVin/CaseVide.png", UriKind.Relative));
            ((Button)but[nbasier-1].GetValue(l_ligne, l_col)).Background = brush;
            ((Button)but[nbasier - 1].GetValue(l_ligne, l_col)).Click += selectBouteille;

            req.delete(l_ligne, l_col, nbasier);
        }

        /// <summary>
        /// Affichage de la fenêtre pour ajouter un casier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_Menu2_Click(object sender, RoutedEventArgs e)
        {
            ajouteCasier addCasier = new ajouteCasier();
            addCasier.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// supprimer toutes les bouteilles selectionner ou lancer la selection des boutteiles a supprimer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void supprimerAll(object sender, RoutedEventArgs e)
        {
            if (supprimer)
            {
                int ligne;
                int col;
                int cas;
                var brush = new ImageBrush();
                foreach (Metier.Position pos in listeBouteilleSuppr)
                {
                    ligne = pos.X;
                    col = pos.Y;
                    cas = pos.Casier;                 
                    req.delete(ligne, col, cas);

                    ((Button)but[cas - 1].GetValue(ligne, col)).Opacity = 1.0;

                    brush = new ImageBrush();
                    brush.ImageSource = new BitmapImage(new Uri("../../../CaveAVin/CaseVide.png", UriKind.Relative));
                    ((Button)but[cas-1].GetValue(ligne, col)).Background = brush;
                    ((Button)but[cas-1].GetValue(ligne, col)).Click -= selectionBouteille;
                    ((Button)but[cas-1].GetValue(ligne, col)).Click += selectBouteille;
                }

                listeBouteilleSuppr.Clear();
                supprimer = false;
            }
            else
            {
                supprimer = true;
            }
        }



        private void initAjoutBouteille()
        {
            Decaler();
            MainWindows.Visibility = Visibility.Visible;
            Accueil.Visibility = Visibility.Hidden;
            affichBoute.Visibility = Visibility.Visible;
            MultiCasier.Visibility = Visibility.Hidden;
            Faux_menu.Visibility = Visibility.Hidden;
            ajoutB.Visibility = Visibility.Visible;
            ficheDetailBouteille.Visibility = Visibility.Hidden;

            cb_Type_Ajout.Items.Clear();
            cb_Region_Ajout.Items.Clear();
            cb_Domaine_Ajout.Items.Clear();
            cb_Contenance_Ajout.Items.Clear();
            cb_Millesime_Ajout.Items.Clear();
            cb_Cru_Ajout.Items.Clear();
            cb_Vinification_Ajout.Items.Clear();
            cb_Appelation_Ajout.Items.Clear();

            DAO.TypeDAO daoTyp = new DAO.TypeDAO(DAO.BDD.Instance.Connexion);
            Metier.Types listeTyp = daoTyp.Lister();
            foreach (Metier.Type t in listeTyp.Lister())
            {
                cb_Type_Ajout.Items.Add(t.NomType);
            }

            DAO.PaysDAO daoPays = new DAO.PaysDAO(DAO.BDD.Instance.Connexion);
            Metier.Pays2 listePays = daoPays.Lister();
            foreach (Metier.Pays p in listePays.Lister())
            {
                cb_Pays_Ajout.Items.Add(p.NomPays);
            }

            DAO.RegionDAO daoReg = new DAO.RegionDAO(DAO.BDD.Instance.Connexion);
            Metier.Regions listeReg = daoReg.Lister();
            foreach (Metier.Region r in listeReg.Lister())
            {
                cb_Region_Ajout.Items.Add(r.NomRegion);
            }

            DAO.DomaineDAO daoDom = new DAO.DomaineDAO(DAO.BDD.Instance.Connexion);
            Metier.Domaines listeDom = daoDom.Lister();
            foreach (Metier.Domaine d in listeDom.Lister())
            {
                cb_Domaine_Ajout.Items.Add(d.NomDomaine);
            }

            DAO.ContenanceDAO daoCon = new DAO.ContenanceDAO(DAO.BDD.Instance.Connexion);
            Metier.Contenances listeCon = daoCon.Lister();
            foreach (Metier.Contenance c in listeCon.Lister())
            {
                cb_Contenance_Ajout.Items.Add(c.Valeur);
            }

            DAO.MillesimeDAO daoMil = new DAO.MillesimeDAO(DAO.BDD.Instance.Connexion);
            Metier.Millesimes listeMil = daoMil.Lister();
            foreach (Metier.Millesime m in listeMil.Lister())
            {
                cb_Millesime_Ajout.Items.Add(m.NomMillesime);
            }

            DAO.CruDAO daoCru = new DAO.CruDAO(DAO.BDD.Instance.Connexion);
            Metier.Crus listeCru = daoCru.Lister();
            foreach (Metier.Cru c in listeCru.Lister())
            {
                cb_Cru_Ajout.Items.Add(c.NomCru);
            }

            DAO.Type_vinificationDAO daoVinif = new DAO.Type_vinificationDAO(DAO.BDD.Instance.Connexion);
            Metier.Type_vinifications listeVinif = daoVinif.Lister();
            foreach (Metier.Type_vinification v in listeVinif.Lister())
            {
                cb_Vinification_Ajout.Items.Add(v.NomVinif);
            }

            DAO.AppelationDAO daoApp = new DAO.AppelationDAO(DAO.BDD.Instance.Connexion);
            Metier.Appelations listeApp = daoApp.Lister();
            foreach (Metier.Appelation a in listeApp.Lister())
            {
                cb_Appelation_Ajout.Items.Add(a.NomAppelation);
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Metier.Type t;
            Metier.Pays p;
            Metier.Region r;
            Metier.Domaine d;
            Metier.Appelation a;
            Metier.Contenance c;
            Metier.Millesime m;
            Metier.Cru cru;
            Metier.Type_vinification tv;

            #region gestion combobox
            DAO.TypeDAO daoType = new DAO.TypeDAO(DAO.BDD.Instance.Connexion);
            string nomType = cb_Type_Ajout.Text;
            try
            {
                t = daoType.Chercher(cb_Type_Ajout.Text);
            }
            catch(Exception ex)
            {
                daoType.Créer(nomType);
                t = daoType.Chercher(cb_Type_Ajout.Text);
            }

            DAO.PaysDAO daoPays = new DAO.PaysDAO(DAO.BDD.Instance.Connexion);
            string nomPays = cb_Pays_Ajout.Text;
            try
            {
                p = daoPays.Chercher(cb_Pays_Ajout.Text);
            }
            catch (Exception ex)
            {

                daoPays.Créer(nomPays);
                p = daoPays.Chercher(cb_Pays_Ajout.Text);
            }

            DAO.RegionDAO daoReg = new DAO.RegionDAO(DAO.BDD.Instance.Connexion);
            string nomRegion = cb_Region_Ajout.Text;
            try
            {
                r = daoReg.Chercher(cb_Region_Ajout.Text);
            }
            catch (Exception ex)
            {
                daoReg.Créer(nomRegion, nomPays);
                r = daoReg.Chercher(cb_Region_Ajout.Text);
            }

            DAO.DomaineDAO daoDom = new DAO.DomaineDAO(DAO.BDD.Instance.Connexion);
            string nomDomaine = cb_Domaine_Ajout.Text;
            try
            {
                d = daoDom.Chercher(cb_Domaine_Ajout.Text);
            }
            catch (Exception ex)
            {
                daoDom.Créer(nomDomaine);
                d = daoDom.Chercher(cb_Domaine_Ajout.Text);
            }

            DAO.AppelationDAO daoApp = new DAO.AppelationDAO(DAO.BDD.Instance.Connexion);
            string nomApp = cb_Appelation_Ajout.Text;
            try
            {
                a = daoApp.Chercher(cb_Appelation_Ajout.Text);
            }
            catch (Exception ex)
            {
                daoApp.Créer(nomApp);
                a = daoApp.Chercher(cb_Appelation_Ajout.Text);
            }

            DAO.ContenanceDAO daoCon = new DAO.ContenanceDAO(DAO.BDD.Instance.Connexion);
            int nomCon = Int32.Parse(cb_Contenance_Ajout.Text);
            try
            {
                c = daoCon.Chercher(nomCon,"");
            }
            catch (Exception ex)
            {

                daoCon.Créer(nomCon,"");
                c = daoCon.Chercher(nomCon,"");
            }

            DAO.MillesimeDAO daoMil = new DAO.MillesimeDAO(DAO.BDD.Instance.Connexion);
            string nomMil = cb_Millesime_Ajout.Text;
            try
            {
                m = daoMil.Chercher(cb_Millesime_Ajout.Text);
            }
            catch (Exception ex)
            {
                daoMil.Créer(nomMil);
                m = daoMil.Chercher(cb_Millesime_Ajout.Text);
            }

            DAO.CruDAO daoCru = new DAO.CruDAO(DAO.BDD.Instance.Connexion);
            string nomCru = cb_Cru_Ajout.Text;
            try
            {
                cru = daoCru.Chercher(cb_Cru_Ajout.Text);
            }
            catch (Exception ex)
            {
                daoCru.Créer(nomCru);
                cru = daoCru.Chercher(cb_Cru_Ajout.Text);
            }

            DAO.Type_vinificationDAO daoVinif = new DAO.Type_vinificationDAO(DAO.BDD.Instance.Connexion);
            string nomVinif = cb_Vinification_Ajout.Text;
            try
            {
                tv = daoVinif.Chercher(cb_Vinification_Ajout.Text);
            }
            catch (Exception ex)
            {
                daoVinif.Créer(nomVinif);
                tv = daoVinif.Chercher(cb_Vinification_Ajout.Text);
            }

            #endregion

            DAO.CasierDAO daoCas = new DAO.CasierDAO(DAO.BDD.Instance.Connexion);
            Metier.Casier cas;
            DAO.BouteilleDAO daoBou = new DAO.BouteilleDAO(DAO.BDD.Instance.Connexion);

            var brush = new ImageBrush();

            foreach (Metier.Position pos in listeBouteilleAjout)
            {
                cas = daoCas.Chercher(pos.Casier);
                if (cas != null)
                {
                    Metier.Bouteille b = new Metier.Bouteille(0, false, "", pos.X, pos.Y, cas, cru, m, c, d, r, tv, a, t);
                    daoBou.Créer(b);

                    if (nomType == "Blanc")
                    {
                        brush.ImageSource = new BitmapImage(new Uri("../../../CaveAVin/BlancCasier.png", UriKind.Relative));
                    }
                    else if (nomType == "Rouge")
                    {
                        brush.ImageSource = new BitmapImage(new Uri("../../../CaveAVin/RougeCasier.png", UriKind.Relative));
                    }
                    else if (nomType == "Rosé")
                    {
                        brush.ImageSource = new BitmapImage(new Uri("../../../CaveAVin/RoséCasier.png", UriKind.Relative));
                    }
                    else if (nomType == "Pétillant")
                    {
                        brush.ImageSource = new BitmapImage(new Uri("../../../CaveAVin/PétillantCasier.png", UriKind.Relative));
                    }
                    else
                    {
                        brush.ImageSource = new BitmapImage(new Uri("../../../CaveAVin/CaseVide.png", UriKind.Relative));
                    }
                    ((Button)but[nbasier - 1].GetValue(pos.X, pos.Y)).Background = brush;
                    ((Button)but[nbasier - 1].GetValue(pos.X, pos.Y)).Click += selectionBouteille;

                }
            }

            ajoutB.Visibility = Visibility.Hidden;
        }
    }
}
