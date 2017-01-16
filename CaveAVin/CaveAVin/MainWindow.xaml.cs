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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Attribut Général

        Boolean supprimer; // Passe à True après un clic sur le boutton ToutSupprimer

        List<Metier.Position> listeBouteilleAjout = new List<Metier.Position>(); //Liste contenant les emplacements des nouvelles bouteilles à créer

        List<Metier.Position> listeBouteilleSuppr = new List<Metier.Position>(); //Liste contenant les emplacements des bouteilles à supprimer

        private DAO.reqSQL req = new DAO.reqSQL(DAO.BDD.Instance.Connexion); //Permet d'utiliser certaines requêtes SQL facilement

        private int nCasierActuel = 0;

        private int nbCasierTotal = 0;

        private int nbCasierTotalActuel = 0;

        private List<Button[,]> listeBouton = new List<Button[,]>();

        private Button[,] button;

        private Grid[] gestionCasier;

        private int l_ligne;

        private int l_col;

        private int l_cas;

        private int l_ligne_dep;

        private int l_col_dep;

        private int l_cas_dep;

        private lancement sf;

        private bool casierSuppr = false;

        private bool casierModif = false;

        private bool lancer = false;

        private bool deplacer = false;

        private int ba_ligne = 0, ba_col = 0;

        private Metier.Position courante;

        #endregion

        /// <summary>
        /// Initialisation des différents affichage ainsi que de la Base de données et lancement du programme
        /// </summary>
        public MainWindow()
        {
            InitializeComponent(); // Initialise les différents composants graphiques

            // Gère les affichages à montrer lors du lancement du programme
            #region Initialisation Affichage

            MasquerEcran();
            AffichageAccueil.Visibility = Visibility.Visible;

            #endregion

            InitialiserBDD();

            supprimer = false;

            InitGridCasier(); //Création des différents casiers

            AfficherBouteille();

            courante = null;
        }



        /// <summary>
        /// Affiche le menu latéral lors d'un clic sur le bouton
        /// </summary>
        /// <param name="sender"> Objet émtteur de l'évenement </param>
        /// <param name="e"></param>
        private void AffichageMenu_Click(object sender, RoutedEventArgs e)
        {
            AffichageMenuLateral.Visibility = Visibility.Visible;
        }



        /// <summary>
        /// Cache le menu latéral lorsque l'on sort du cadre contenant les boutons
        /// </summary>
        /// <param name="sender"> Objet émtteur de l'évenement </param>
        /// <param name="e"></param>
        private void part(object sender, MouseEventArgs e)
        {
            AffichageMenuLateral.Visibility = Visibility.Hidden;
        }



        /// <summary>
        /// Affiche l'interface permettant de créer des bouteilles
        /// </summary>
        /// <param name="sender"> Objet émtteur de l'évenement </param>
        /// <param name="e"></param>
        private void AjoutBouteille_Click(object sender, RoutedEventArgs e)
        {
            AjoutBouteille();
        }

        /// <summary>
        /// Recalcul la taille des cases
        /// </summary>
        private void RecalculailleGrille()
        {
            // Si on a une grille
            if (gestionCasier.Any() && nCasierActuel > -1 && nCasierActuel < gestionCasier.Count())
            {
                // Obtention de la grille
                var grille = gestionCasier[nCasierActuel];

                // Récupération des hauteur et largeur d'affichage
                var hauteur = GridAffichageBouteille.ActualHeight;
                var largeur = GridAffichageBouteille.ActualWidth;

                // Calcul des hauteur et largeur maximal possible pour une case
                var hauteurMax = hauteur / (double)grille.RowDefinitions.Count;
                var largeurMax = largeur / (double)grille.ColumnDefinitions.Count;

                // On prend la plus petite valeur pour être sûr de ne pas dépasser de la zone d'affichage
                var taille = Math.Min(hauteurMax, largeurMax);

                // Récupération des cases
                var children = grille.Children
                                     .OfType<Button>();

                // Application de la nouvelle taille
                foreach (var child in children)
                {
                    child.Height = taille;
                    child.Width = taille;
                }
            }
        }

        /// <summary>
        /// Permet d'initialiser tous les casiers sous forme de grid pour pouvoir les afficher plus tard
        /// </summary>
        private void InitGridCasier()
        {
            DAO.CasierDAO daoCasier = new DAO.CasierDAO(DAO.BDD.Instance.Connexion);

            var casiers = daoCasier.Lister().Lister();

            nbCasierTotal = casiers.Count();

            if (lancer == false)
            {
                sf = new lancement();
                sf.Show();

                gestionCasier = new Grid[nbCasierTotal + 100];
            }
            int nombreColonne, nombreLigne;
            int HeightBoutton, WidthBoutton;

            var longueurMax = 648;
            var largeurMax = longueurMax;

            ColumnDefinition[] colonne;
            RowDefinition[] ligne;
            
            var imageBlanc = new BitmapImage(new Uri("../../../CaveAVin/Images/BlancCasier.png", UriKind.Relative));
            var imageRouge = new BitmapImage(new Uri("../../../CaveAVin/Images/RougeCasier.png", UriKind.Relative));
            var imageRose = new BitmapImage(new Uri("../../../CaveAVin/Images/RoséCasier.png", UriKind.Relative));
            var imagePetillant = new BitmapImage(new Uri("../../../CaveAVin/Images/PétillantCasier.png", UriKind.Relative));
            var imageVide = new BitmapImage(new Uri("../../../CaveAVin/Images/CaseVide.png", UriKind.Relative));

            for (int i = nbCasierTotalActuel; i < 2; i++)
            //for (int i = nbCasierTotalActuel; i < nbCasierTotal; i++)
            {
                var casier = casiers[i];
                gestionCasier[i] = new Grid();
                GridAffichageBouteille.Children.Add(gestionCasier[i]);
                gestionCasier[i].VerticalAlignment = VerticalAlignment.Center;
                gestionCasier[i].HorizontalAlignment = HorizontalAlignment.Center;
                gestionCasier[i].Visibility = Visibility.Hidden;

                nombreLigne = casier.LargeurX;
                nombreColonne = casier.LargeurY;

                //nombreLigne = req.SelInt("Select Largeur_X FROM Casier where idCasier = " + i);
                //nombreColonne = req.SelInt("Select Largeur_Y FROM Casier where idCasier = " + i);

                if (nombreColonne > nombreLigne)
                {
                    WidthBoutton = (int)((longueurMax)) / nombreColonne;
                    HeightBoutton = WidthBoutton;
                }
                else if (nombreColonne < nombreLigne)
                {
                    HeightBoutton = (int)((longueurMax)) / nombreLigne;
                    WidthBoutton = HeightBoutton;
                }
                else
                {
                    WidthBoutton = (int)((longueurMax)) / nombreColonne;
                    HeightBoutton = (int)((longueurMax)) / nombreLigne;
                }

                colonne = new ColumnDefinition[nombreColonne];
                ligne = new RowDefinition[nombreLigne];

                for (int f = 0; f < nombreColonne; f++)
                {
                    colonne[f] = new ColumnDefinition();
                    colonne[f].Width = GridLength.Auto;
                    gestionCasier[i].ColumnDefinitions.Add(colonne[f]);
                }

                for (int f = 0; f < nombreLigne; f++)
                {
                    ligne[f] = new RowDefinition();
                    ligne[f].Height = GridLength.Auto;

                    gestionCasier[i].RowDefinitions.Add(ligne[f]);
                }

                //Gère l'affichage des bouteilles dans le casier
                string res;
                button = new Button[nombreColonne, nombreLigne];
                for (int k = 0; k < nombreColonne; k++)
                {
                    for (int j = 0; j < nombreLigne; j++)
                    {
                        var buttonCourant = new Button();
                        button[k, j] = buttonCourant;

                        gestionCasier[i].Children.Add(button[k, j]);
                        button[k, j].Visibility = Visibility.Visible;
                        button[k, j].Content = k + ", " + j;
                        button[k, j].Width = WidthBoutton;
                        button[k, j].Height = HeightBoutton;

                        Grid.SetRow(button[k, j], j);
                        Grid.SetColumn(button[k, j], k);
                        res = "";
                        try
                        {
                            res = req.SelStr1("SELECT NomType From Type natural join Bouteille where idCasier =" + casier.Id +
                            " and Bouteille.Position_X = " + k + " and Bouteille.Position_Y = " + j + " and Bue = 0;", "NomType");
                        }
                        catch
                        {

                        }

                        var brush = new ImageBrush();


                        if (res == "Blanc")
                        {
                            brush.ImageSource = imageBlanc;
                            button[k, j].Background = brush;
                            button[k, j].Click += SelectionBouteille;
                        }
                        else if (res == "Rouge")
                        {
                            brush.ImageSource = imageRouge;
                            button[k, j].Background = brush;
                            button[k, j].Click += SelectionBouteille;
                        }
                        else if (res == "Rosé")
                        {
                            brush.ImageSource = imageRose;
                            button[k, j].Background = brush;
                            button[k, j].Click += SelectionBouteille;
                        }
                        else if (res == "Pétillant")
                        {
                            brush.ImageSource = imagePetillant;
                            button[k, j].Background = brush;
                            button[k, j].Click += SelectionBouteille;
                        }
                        else
                        {
                            brush.ImageSource = imageVide;
                            button[k, j].Background = brush;
                            button[k, j].Click += SelectBouteille;
                        }
                        if (j < 10)
                            button[k, j].Tag = k + ",0" + j;
                        else
                            button[k, j].Tag = k + "," + j;
                        button[k, j].Background = brush;
                    }
                }
                listeBouton.Add(button);
            }
            nbCasierTotalActuel = nbCasierTotal;
            if (lancer == false)
            {
                sf.Close();
                sf = null;
                lancer = true;
            }
        }



        /// <summary>
        /// Gestion du clic sur un emplacement vide
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectBouteille(object sender, RoutedEventArgs e)
        {
            float nb = float.Parse(((Button)sender).Tag.ToString());

            int ligne = (int) nb;

            float coln = (nb - ligne) * 100;
            int col = 0;
            if (coln < (int)coln + 0.5)
            {
                col = (int)coln;
            }
            else
            {
                col = (int)coln+1;
            }


            if (deplacer)
            {
                ((Button)listeBouton[nCasierActuel].GetValue(ligne, col)).Opacity = 0.5;
                l_ligne_dep = ligne;
                l_col_dep = col;
                l_cas_dep = nCasierActuel;

            }
            else
            {
                Metier.Position posi = new Metier.Position(ligne, col, nCasierActuel);

                bool existe = false;
                foreach (Metier.Position pos in listeBouteilleAjout)
                {
                    if (pos.X == ligne && pos.Y == col && pos.Casier == nCasierActuel)
                    {
                        existe = true;
                        listeBouteilleAjout.Remove(pos);
                        break;
                    }
                }
                if (existe)
                {
                    ((Button)listeBouton[nCasierActuel].GetValue(ligne, col)).Opacity = 1.0;

                }
                else
                {
                    posi.Casier = nCasierActuel;
                    listeBouteilleAjout.Add(posi);
                    ((Button)listeBouton[nCasierActuel].GetValue(ligne, col)).Opacity = 0.5;
                }
            }
        }



        /// <summary>
        /// permet d'afficher le casier suivant ou précedent
        /// </summary>
        private void AfficherBouteille()
        {
            // Permet de gérer l'affichage des flêches pour la gestion des casiers
            #region GestionFleche

            //if (nbCasierTotal > 1)
            //{
            //    MultiCasier.Visibility = Visibility.Visible;
            //}

            if (nCasierActuel == 0)
            {
                CasierPrecedent.Visibility = Visibility.Hidden;
            }
            else
            {
                CasierPrecedent.Visibility = Visibility.Visible;
            }

            if (nCasierActuel < nbCasierTotal-1)
            {
                CasierSuivant.Visibility = Visibility.Visible;
            }
            else
            {
                CasierSuivant.Visibility = Visibility.Hidden;
            }
            #endregion
            if (nbCasierTotal != 0)
            {
                lblNomCasier.Content = req.SelStr1("Select NomCasier From Casier Where idCasier = " + nCasierActuel, "NomCasier");
                gestionCasier[nCasierActuel].Visibility = Visibility.Visible;
                RecalculailleGrille();
            }

        }



        /// <summary>
        /// Permet de gérer le click sur une bouteille
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectionBouteille(Object sender, EventArgs e)
        {
            if (!supprimer)
            {
                l_ligne = Int32.Parse(sender.ToString().Substring(32, 1));
                l_col = Int32.Parse(sender.ToString().Substring(35, 1));

                ((Button)listeBouton[nCasierActuel].GetValue(ba_ligne, ba_col)).Opacity = 1.0 ;

                ((Button)listeBouton[nCasierActuel].GetValue(l_ligne, l_col)).Opacity = 0.5;

                ba_ligne = l_ligne; ba_col = l_col;

                Decaler();
                var button = (Button)sender;
                var textContent = button.Content.ToString();
                var coordonee = textContent.Split(',');

                Metier.Position posi = new Metier.Position(l_ligne, l_col);
                posi.Casier = nCasierActuel;

                var casierDao = new CasierDAO(DAO.BDD.Instance.Connexion);
                var bouteilleDao = new BouteilleDAO(DAO.BDD.Instance.Connexion);
                var casier = casierDao.Chercher(nCasierActuel);
                var bouteille = bouteilleDao.Chercher(posi.X, posi.Y, posi.Casier,0);
                AfficherDetailBouteille(bouteille);

            }
            else
            {      
                int ligne = Int32.Parse(sender.ToString().Substring(32, 1));
                int col = Int32.Parse(sender.ToString().Substring(35, 1));
                Metier.Position posi = new Metier.Position(ligne, col);

                bool existe = false;
                foreach (Metier.Position pos in listeBouteilleSuppr)
                {
                    if (pos.X == ligne && pos.Y == col  && pos.Casier== nCasierActuel)
                    {
                        existe = true;
                        listeBouteilleSuppr.Remove(pos);
                        break;
                    }
                }
                if (existe)
                {
                    ((Button)listeBouton[nCasierActuel].GetValue(ligne, col)).Opacity = 1.0;
                    

                }
                else
                {
                    posi.Casier = nCasierActuel;
                    listeBouteilleSuppr.Add(posi);
                    ((Button)listeBouton[nCasierActuel].GetValue(ligne, col)).Opacity = 0.5;


                }
            }
        }



        /// <summary>
        /// Permet de gérer le click sur le bouton suivant dans l'affichage des casiers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CasierSuivant_Click(object sender, RoutedEventArgs e)
        {
            gestionCasier[nCasierActuel].Visibility = Visibility.Hidden;
            nCasierActuel++;
            AfficherBouteille();
        }



        /// <summary>
        /// Permet de décaler l'affichage du casier pour pouvoir afficher la fiche bouteille
        /// </summary>
        private void Decaler()
        {
            var maxWidth = AffichageInterfaceCasier.ActualWidth;

            var tailleColBouton = 60;
            var tailleMinColPrincipale = GridAffichageBouteille.ActualHeight;
            var tailleColForm = Math.Min(maxWidth, 500);

            var ecranTailleMini = tailleColBouton * 2 + tailleMinColPrincipale + tailleColForm;

            if(ecranTailleMini < maxWidth)
            {
                gridCasierColPrecedent.Width = new GridLength(tailleColBouton);
                gridCasierColSuivant.Width = new GridLength(tailleColBouton);
            }
            else
            {
                gridCasierColPrecedent.Width = new GridLength(0);
                gridCasierColSuivant.Width = new GridLength(0);
                gridCasierColPrincipale.Width = new GridLength(0);
            }

            //GridAffichageBouteille.Margin = new Thickness(0, 30, 0, 50);
            gridCasierColForm.Width = new GridLength(tailleColForm);
        }



        /// <summary>
        /// Permet de réinitialiser l'affichage du casier
        /// </summary>
        private void ReDecaler()
        {
            //GridAffichageBouteille.Margin = new Thickness(300, 30, 0, 0);
            gridCasierColPrecedent.Width = new GridLength(100);
            gridCasierColSuivant.Width = new GridLength(100);
            gridCasierColPrincipale.Width = new GridLength(1, GridUnitType.Star);
            gridCasierColForm.Width = new GridLength(0);
        }



        /// <summary>
        /// Permet de gérer le click sur le bouton précedent dans l'affichage des casiers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CasierPrecedent_Click(object sender, RoutedEventArgs e)
        {
            gestionCasier[nCasierActuel].Visibility = Visibility.Hidden;
            nCasierActuel--;
            AfficherBouteille();
        }



        /// <summary>
        /// Affiche les détail d'une bouteille
        /// </summary>
        /// <param name="bouteille"></param>
        private void AfficherDetailBouteille(Metier.Bouteille bouteille)
        {
            lbl_Categorie.Items.Clear();
            lbl_Pays.Items.Clear();
            lbl_Region.Items.Clear();
            lbl_Domaine.Items.Clear();
            lbl_Cru.Items.Clear();
            lbl_Vinification.Items.Clear();
            lbl_Appelation.Items.Clear();

            DAO.TypeDAO daoTyp = new DAO.TypeDAO(DAO.BDD.Instance.Connexion);
            Metier.Types listeTyp = daoTyp.Lister();
            foreach (Metier.Type t in listeTyp.Lister())
            {
                lbl_Categorie.Items.Add(t.NomType);
            }

            DAO.PaysDAO daoPays = new DAO.PaysDAO(DAO.BDD.Instance.Connexion);
            Metier.Pays2 listePays = daoPays.Lister();
            foreach (Metier.Pays p in listePays.Lister())
            {
                lbl_Pays.Items.Add(p.NomPays);
            }

            DAO.RegionDAO daoReg = new DAO.RegionDAO(DAO.BDD.Instance.Connexion);
            Metier.Regions listeReg = daoReg.Lister();
            foreach (Metier.Region r in listeReg.Lister())
            {
                lbl_Region.Items.Add(r.NomRegion);
            }

            DAO.DomaineDAO daoDom = new DAO.DomaineDAO(DAO.BDD.Instance.Connexion);
            Metier.Domaines listeDom = daoDom.Lister();
            foreach (Metier.Domaine d in listeDom.Lister())
            {
                lbl_Domaine.Items.Add(d.NomDomaine);
            }

            DAO.CruDAO daoCru = new DAO.CruDAO(DAO.BDD.Instance.Connexion);
            Metier.Crus listeCru = daoCru.Lister();
            foreach (Metier.Cru c in listeCru.Lister())
            {
                lbl_Cru.Items.Add(c.NomCru);
            }

            DAO.Type_vinificationDAO daoVinif = new DAO.Type_vinificationDAO(DAO.BDD.Instance.Connexion);
            Metier.Type_vinifications listeVinif = daoVinif.Lister();
            foreach (Metier.Type_vinification v in listeVinif.Lister())
            {
                lbl_Vinification.Items.Add(v.NomVinif);
            }

            DAO.AppelationDAO daoApp = new DAO.AppelationDAO(DAO.BDD.Instance.Connexion);
            Metier.Appelations listeApp = daoApp.Lister();
            foreach (Metier.Appelation a in listeApp.Lister())
            {
                lbl_Appelation.Items.Add(a.NomAppelation);
            }

            if (bouteille != null)
            {
                lbl_Appelation.Text = bouteille.Appelation?.NomAppelation;
                lbl_Categorie.Text = bouteille.Type?.NomType;
                lbl_Contenance.Text = bouteille.Contenance?.Valeur.ToString();
                lbl_Cru.Text = bouteille.Cru?.NomCru;
                lbl_Domaine.Text = bouteille.Domaine?.NomDomaine;
                lbl_Millesime.Text = bouteille.Millesime?.NomMillesime;
                lbl_Pays.Text = bouteille.Pays?.NomPays;
                lbl_Region.Text = bouteille.Region?.NomRegion;
                lbl_Vinification.Text = bouteille.Type_vinification?.NomVinif;
                DisplayFicheDetailBouteille(true);
           }
        }



        /// <summary>
        /// Affiche ou masque la fiche du détail de la bouteille
        /// </summary>
        /// <param name="rendreVisible">Vrai si l'on doit afficher la fiche; sinon faux</param>
        private void DisplayFicheDetailBouteille(bool rendreVisible)
        {
            AffichageFicheAjoutBouteille.Visibility = Visibility.Hidden;
            AfficheDetailBouteille.Visibility = rendreVisible ? Visibility.Visible : Visibility.Hidden;
        }



        /// <summary>
        /// Affiche la page des casier et cache l'accueil lorsque on appuie sur le bouton du milieu de l'accueil
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_VoirCasier_Click(object sender, RoutedEventArgs e)
        {
            AffichageAccueil.Visibility = Visibility.Hidden;
            AffichageInterfaceCasier.Visibility = Visibility.Visible;
            GridAffichageBouteille.Visibility = Visibility.Visible;
            RecalculailleGrille();
        }



        /// <summary>
        /// Affiche la page d'accueil et cache les casiers lorsque on appuie sur le 1er bouton du menu déroulant
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_RetourAccueil_Click(object sender, RoutedEventArgs e)
        {
            MasquerEcran();
            AffichageAccueil.Visibility = Visibility.Visible;
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
        private void InitialiserBDD()
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
        /// Gère le clic sur le bouton supprimer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_Supprimer_Click(object sender, RoutedEventArgs e)
        {
            MasquerEcran();
            //AfficheDetailBouteille.Visibility = Visibility.Hidden;
            AjoutCommentaireSupression.Visibility = Visibility.Visible;
        }



        /// <summary>
        /// Affichage de la fenêtre pour ajouter un casier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_AjoutCasier_Click(object sender, RoutedEventArgs e)
        {
            MasquerEcran();
            //MultiCasier.Visibility = Visibility.Hidden;
            AffichageFicheAjoutBouteille.Visibility = Visibility.Hidden;
            AffichageMenuLateral.Visibility = Visibility.Hidden;
            AffichageAjoutCasier.Visibility = Visibility.Visible;
            
        }



        /// <summary>
        /// supprimer toutes les bouteilles selectionner ou lancer la selection des boutteiles a supprimer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToutSupprimer_Click(object sender, RoutedEventArgs e)
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
                    req.delete(ligne, col, cas, "");

                    ((Button)listeBouton[cas].GetValue(ligne, col)).Opacity = 1.0;

                    brush = new ImageBrush();
                    brush.ImageSource = new BitmapImage(new Uri("../../../CaveAVin/Images/CaseVide.png", UriKind.Relative));
                    ((Button)listeBouton[cas].GetValue(ligne, col)).Background = brush;
                    ((Button)listeBouton[cas].GetValue(ligne, col)).Click -= SelectionBouteille;
                    ((Button)listeBouton[cas].GetValue(ligne, col)).Click += SelectBouteille;
                }

                listeBouteilleSuppr.Clear();
                supprimer = false;
            }
            else
            {
                if (courante != null)
                    ((Button)listeBouton[courante.Casier - 1].GetValue(courante.X, courante.Y)).Opacity = 1.0;
                AfficheDetailBouteille.Visibility = Visibility.Hidden;
                ReDecaler();
                supprimer = true;
            }
        }



        /// <summary>
        /// Gère l'affichage de l'interface permettant d'ajouter des bouteilles
        /// </summary>
        private void AjoutBouteille()
        {
            Decaler();
            AffichageInterfaceCasier.Visibility = Visibility.Visible;
            AffichageFicheAjoutBouteille.Visibility = Visibility.Visible;
            AfficheDetailBouteille.Visibility = Visibility.Hidden;

            cb_Type_Ajout.Items.Clear();
            cb_Region_Ajout.Items.Clear();
            cb_Domaine_Ajout.Items.Clear();
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



        /// <summary>
        /// Gère l'ajout des bouteilles après validation de l'ajout
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValidationAjout_Click(object sender, RoutedEventArgs e)
        {
            ReDecaler();
            Metier.Type t;
            Metier.Pays p = null;
            Metier.Region r = null;
            Metier.Domaine d = null;
            Metier.Appelation a = null;
            Metier.Contenance c = null;
            Metier.Millesime m = null;
            Metier.Cru cru = null;
            Metier.Type_vinification tv = null;

            #region gestion combobox

                DAO.TypeDAO daoType = new DAO.TypeDAO(DAO.BDD.Instance.Connexion);
                string nomType = cb_Type_Ajout.Text;
                t = daoType.Chercher(cb_Type_Ajout.Text);
                if (t == null)
                {
                    daoType.Créer(nomType);
                    t = daoType.Chercher(cb_Type_Ajout.Text);
                }

            if (cb_Pays_Ajout.Text != "")
            {
                DAO.PaysDAO daoPays = new DAO.PaysDAO(DAO.BDD.Instance.Connexion);
                string nomPays = cb_Pays_Ajout.Text;
                p = daoPays.Chercher(cb_Pays_Ajout.Text);
                if (p == null)
                {
                    daoPays.Créer(nomPays);
                    p = daoPays.Chercher(cb_Pays_Ajout.Text);
                }
            }


            if (cb_Region_Ajout.Text != "")
            {
                DAO.RegionDAO daoReg = new DAO.RegionDAO(DAO.BDD.Instance.Connexion);
                string nomRegion = cb_Region_Ajout.Text;
                r = daoReg.Chercher(cb_Region_Ajout.Text);
                if (r == null)
                {
                    daoReg.Créer(nomRegion);
                    r = daoReg.Chercher(cb_Region_Ajout.Text);
                }
            }

            if (cb_Domaine_Ajout.Text != "")
            {
                DAO.DomaineDAO daoDom = new DAO.DomaineDAO(DAO.BDD.Instance.Connexion);
                string nomDomaine = cb_Domaine_Ajout.Text;
                d = daoDom.Chercher(cb_Domaine_Ajout.Text);
                if (d == null)
                {
                    daoDom.Créer(nomDomaine);
                    d = daoDom.Chercher(cb_Domaine_Ajout.Text);
                }
            }

            if (cb_Appelation_Ajout.Text != "")
            {
                DAO.AppelationDAO daoApp = new DAO.AppelationDAO(DAO.BDD.Instance.Connexion);
                string nomApp = cb_Appelation_Ajout.Text;
                a = daoApp.Chercher(cb_Appelation_Ajout.Text);
                if (a == null)
                {
                    daoApp.Créer(nomApp);
                    a = daoApp.Chercher(cb_Appelation_Ajout.Text);
                }
            }

            if (cb_Contenance_Ajout.Text != "")
            {
                DAO.ContenanceDAO daoCon = new DAO.ContenanceDAO(DAO.BDD.Instance.Connexion);
                int nomCon = Int32.Parse(cb_Contenance_Ajout.Text);
                c = daoCon.Chercher(nomCon, "");
                if (c == null)
                {
                    daoCon.Créer(nomCon, "");
                    c = daoCon.Chercher(nomCon, "");
                }
            }

            if (cb_Millesime_Ajout.Text != "")
            {
                DAO.MillesimeDAO daoMil = new DAO.MillesimeDAO(DAO.BDD.Instance.Connexion);
                int nomMil = Int32.Parse(cb_Millesime_Ajout.Text);
                m = daoMil.Chercher(nomMil, "");
                if (m == null)
                {
                    daoMil.Créer(nomMil);
                    m = daoMil.Chercher(nomMil, "");
                }
            }

            if (cb_Cru_Ajout.Text != "")
            {
                DAO.CruDAO daoCru = new DAO.CruDAO(DAO.BDD.Instance.Connexion);
                string nomCru = cb_Cru_Ajout.Text;
                cru = daoCru.Chercher(cb_Cru_Ajout.Text);
                if (cru == null)
                {
                    daoCru.Créer(nomCru);
                    cru = daoCru.Chercher(cb_Cru_Ajout.Text);
                }
            }

            if (cb_Vinification_Ajout.Text != "")
            {
                DAO.Type_vinificationDAO daoVinif = new DAO.Type_vinificationDAO(DAO.BDD.Instance.Connexion);
                string nomVinif = cb_Vinification_Ajout.Text;
                tv = daoVinif.Chercher(cb_Vinification_Ajout.Text);
                if (tv == null)
                {
                    daoVinif.Créer(nomVinif);
                    tv = daoVinif.Chercher(cb_Vinification_Ajout.Text);
                }
            }

            #endregion

            DAO.CasierDAO daoCas = new DAO.CasierDAO(DAO.BDD.Instance.Connexion);
            Metier.Casier cas;
            DAO.BouteilleDAO daoBou = new DAO.BouteilleDAO(DAO.BDD.Instance.Connexion);

            var brush = new ImageBrush();

            foreach (Metier.Position pos in listeBouteilleAjout)
            {
                cas = daoCas.Chercher(pos.Casier);
                    Metier.Bouteille b = new Metier.Bouteille(0, false, "", pos.X, pos.Y, cas, cru, m, c, d, r, tv, a, t,p);
                    daoBou.Créer(b);

                ((Button)listeBouton[pos.Casier].GetValue(pos.X, pos.Y)).Click -= SelectBouteille;
                ((Button)listeBouton[pos.Casier].GetValue(pos.X, pos.Y)).Opacity = 1.0;

                if (nomType == "Blanc")
                    {
                        brush.ImageSource = new BitmapImage(new Uri("../../../CaveAVin/Images/BlancCasier.png", UriKind.Relative));
                        ((Button)listeBouton[pos.Casier].GetValue(pos.X, pos.Y)).Background = brush;
                        ((Button)listeBouton[pos.Casier].GetValue(pos.X, pos.Y)).Click += SelectionBouteille;
                    }
                    else if (nomType == "Rouge")
                    {
                        brush.ImageSource = new BitmapImage(new Uri("../../../CaveAVin/Images/RougeCasier.png", UriKind.Relative));
                        ((Button)listeBouton[pos.Casier].GetValue(pos.X, pos.Y)).Background = brush;
                        ((Button)listeBouton[pos.Casier].GetValue(pos.X, pos.Y)).Click += SelectionBouteille;
                    }
                    else if (nomType == "Rosé")
                    {
                        brush.ImageSource = new BitmapImage(new Uri("../../../CaveAVin/Images/RoséCasier.png", UriKind.Relative));
                        ((Button)listeBouton[pos.Casier].GetValue(pos.X, pos.Y)).Background = brush;
                        ((Button)listeBouton[pos.Casier].GetValue(pos.X, pos.Y)).Click += SelectionBouteille;
                    }
                    else if (nomType == "Pétillant")
                    {
                        brush.ImageSource = new BitmapImage(new Uri("../../../CaveAVin/Images/PétillantCasier.png", UriKind.Relative));
                        ((Button)listeBouton[pos.Casier].GetValue(pos.X, pos.Y)).Background = brush;
                        ((Button)listeBouton[pos.Casier].GetValue(pos.X, pos.Y)).Click += SelectionBouteille;
                    }
                    else
                    {
                        brush.ImageSource = new BitmapImage(new Uri("../../../CaveAVin/Images/CaseVide.png", UriKind.Relative));
                        ((Button)listeBouton[pos.Casier].GetValue(pos.X, pos.Y)).Background = brush;
                        ((Button)listeBouton[pos.Casier].GetValue(pos.X, pos.Y)).Click += SelectBouteille;
                    }               
            }
            listeBouteilleAjout.Clear();

            AffichageFicheAjoutBouteille.Visibility = Visibility.Hidden;
        }



        /// <summary>
        /// Gère le clic sur le bouton rechercher
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_rechercher_Click(object sender, RoutedEventArgs e)
        {
            #region gère affichage
            MasquerEcran();
            
            //AffichageAccueil.Visibility = Visibility.Hidden;
            //AffichageInterfaceCasier.Visibility = Visibility.Hidden;
            //MultiCasier.Visibility = Visibility.Hidden;
           // AffichageMenuLateral.Visibility = Visibility.Hidden;
            //AffichageFicheAjoutBouteille.Visibility = Visibility.Hidden;
            AffichageRecherche.Visibility = Visibility.Visible;
            OptionRecherche.Visibility = Visibility.Visible;

            #endregion

            cb_Type_Recherche.Items.Clear();
            cb_Region_Recherche.Items.Clear();
            cb_Domaine_Recherche.Items.Clear();
            cb_Cru_Recherche.Items.Clear();
            cb_Vinification_Recherche.Items.Clear();
            cb_Appelation_Recherche.Items.Clear();

            DAO.TypeDAO daoTyp = new DAO.TypeDAO(DAO.BDD.Instance.Connexion);
            Metier.Types listeTyp = daoTyp.Lister();
            foreach (Metier.Type t in listeTyp.Lister())
            {
                cb_Type_Recherche.Items.Add(t.NomType);
            }
            cb_Type_Recherche.Items.Add("");

            DAO.PaysDAO daoPays = new DAO.PaysDAO(DAO.BDD.Instance.Connexion);
            Metier.Pays2 listePays = daoPays.Lister();
            foreach (Metier.Pays p in listePays.Lister())
            {
                cb_Pays_Recherche.Items.Add(p.NomPays);
            }
            cb_Pays_Recherche.Items.Add("");

            DAO.RegionDAO daoReg = new DAO.RegionDAO(DAO.BDD.Instance.Connexion);
            Metier.Regions listeReg = daoReg.Lister();
            foreach (Metier.Region r in listeReg.Lister())
            {
                cb_Region_Recherche.Items.Add(r.NomRegion);
            }
            cb_Region_Recherche.Items.Add("");

            DAO.DomaineDAO daoDom = new DAO.DomaineDAO(DAO.BDD.Instance.Connexion);
            Metier.Domaines listeDom = daoDom.Lister();
            foreach (Metier.Domaine d in listeDom.Lister())
            {
                cb_Domaine_Recherche.Items.Add(d.NomDomaine);
            }
            cb_Domaine_Recherche.Items.Add("");

            DAO.CruDAO daoCru = new DAO.CruDAO(DAO.BDD.Instance.Connexion);
            Metier.Crus listeCru = daoCru.Lister();
            foreach (Metier.Cru c in listeCru.Lister())
            {
                cb_Cru_Recherche.Items.Add(c.NomCru);
            }
            cb_Cru_Recherche.Items.Add("");

            DAO.Type_vinificationDAO daoVinif = new DAO.Type_vinificationDAO(DAO.BDD.Instance.Connexion);
            Metier.Type_vinifications listeVinif = daoVinif.Lister();
            foreach (Metier.Type_vinification v in listeVinif.Lister())
            {
                cb_Vinification_Recherche.Items.Add(v.NomVinif);
            }
            cb_Vinification_Recherche.Items.Add("");

            DAO.AppelationDAO daoApp = new DAO.AppelationDAO(DAO.BDD.Instance.Connexion);
            Metier.Appelations listeApp = daoApp.Lister();
            foreach (Metier.Appelation a in listeApp.Lister())
            {
                cb_Appelation_Recherche.Items.Add(a.NomAppelation);
            }
            cb_Appelation_Recherche.Items.Add("");

            DAO.ContenanceDAO daoCont = new DAO.ContenanceDAO(DAO.BDD.Instance.Connexion);
            Metier.Contenances listeCont = daoCont.Lister();
            foreach (Metier.Contenance c in listeCont.Lister())
            {
                cb_Contenance_Recherche.Items.Add(c.Valeur);
            }
            cb_Contenance_Recherche.Items.Add("");

            DAO.MillesimeDAO daoMil = new DAO.MillesimeDAO(DAO.BDD.Instance.Connexion);
            Metier.Millesimes listeMil = daoMil.Lister();
            foreach (Metier.Millesime m in listeMil.Lister())
            {
                cb_Millesime_Recherche.Items.Add(m.NomMillesime);
            }
            cb_Millesime_Recherche.Items.Add("");
        }



        /// <summary>
        /// Empeche la saisie de lettre dans le textbox de contenance
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_Contenance_Ajout_TextChanged(object sender, TextChangedEventArgs e)
        {
            int res;
            if (Int32.TryParse(cb_Contenance_Ajout.Text, out res))
            {

            }
            else
            {
                cb_Contenance_Ajout.Text = "";
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void validerAjoutComm_Click(object sender, RoutedEventArgs e)
        {
            AjoutCommentaireSupression.Visibility = Visibility.Hidden;
            AffichageInterfaceCasier.Visibility = Visibility.Visible;
            GridAffichageBouteille.Visibility = Visibility.Visible;
            ReDecaler();

            var brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri("../../../CaveAVin/Images/CaseVide.png", UriKind.Relative));
            ((Button)listeBouton[nCasierActuel].GetValue(l_ligne, l_col)).Background = brush;
            ((Button)listeBouton[nCasierActuel].GetValue(l_ligne, l_col)).Click -= SelectionBouteille;
            ((Button)listeBouton[nCasierActuel].GetValue(l_ligne, l_col)).Click += SelectBouteille;
            ((Button)listeBouton[nCasierActuel].GetValue(l_ligne, l_col)).Opacity = 1.0;

            req.delete(l_ligne, l_col, nCasierActuel, commentaireBouteille.Text);

            ((Button)listeBouton[nCasierActuel].GetValue(l_ligne, l_col)).Opacity = 1.0;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AjoutCasier_Click(object sender, RoutedEventArgs e)
        {
            DAO.CasierDAO daoCas = new DAO.CasierDAO(DAO.BDD.Instance.Connexion);
            Metier.Casier cas = new Metier.Casier();

            cas.Nom = nomCasierAjout.Text;
            cas.LargeurY = Int32.Parse(hauteurCasierAjout.Text);
            cas.LargeurX = Int32.Parse(largeurCasierAjout.Text);

            daoCas.Créer(cas);

            if (nbCasierTotal != 0)
            {
                gestionCasier[nCasierActuel].Visibility = Visibility.Hidden;
            }

            InitGridCasier();

            nCasierActuel = 0;
            AfficherBouteille();
            AffichageAjoutCasier.Visibility = Visibility.Hidden;
            AffichageAccueil.Visibility = Visibility.Visible;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void annulAjoutCasier_Click(object sender, RoutedEventArgs e)
        {
            AffichageAjoutCasier.Visibility = Visibility.Hidden;
            AffichageAccueil.Visibility = Visibility.Visible;
        }



        /// <summary>
        /// Permet de tester la saisie dans le champs largeur casier pour forcer la saisie de chiffre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void largeurCasierAjout_TextChanged(object sender, TextChangedEventArgs e)
        {
            int res;
            if(Int32.TryParse(largeurCasierAjout.Text,out res))
            {

            }
            else
            {
                largeurCasierAjout.Text = "";
            }

        }



        /// <summary>
        /// Permet de tester la saisie dans le champs hauteur casier pour forcer la saisie de chiffre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hauteurCasierAjout_TextChanged(object sender, TextChangedEventArgs e)
        {
            int res;
            if (Int32.TryParse(hauteurCasierAjout.Text, out res))
            {

            }
            else
            {
                hauteurCasierAjout.Text = "";
            }
        }



        /// <summary>
        /// Permet de tester la saisie dans le champs millesime pour forcer la saisie de chiffre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_Millesime_Ajout_TextChanged(object sender, TextChangedEventArgs e)
        {
            int res;
            if (Int32.TryParse(cb_Millesime_Ajout.Text, out res))
            {

            }
            else
            {
                cb_Millesime_Ajout.Text = "";
            }
        }



        /// <summary>
        /// Permet de fermer la fiche de detail d'une bouteille lorsqu'on appui sur le bouton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFermerFicheDetail_Click(object sender, EventArgs e)
        {
            DisplayFicheDetailBouteille(false);
            ((Button)listeBouton[nCasierActuel].GetValue(ba_ligne, ba_col)).Opacity = 1.0;
            ReDecaler();
        }



        /// <summary>
        /// 
        /// </summary>
        private void InitialiseListeCasier()
        {
            DAO.CasierDAO daoCasier = new DAO.CasierDAO(DAO.BDD.Instance.Connexion);
            grdListeCasier.Children.Clear();
            var casiers = daoCasier.Lister();
            //var nbLignes = (casiers.Lister().Length/3)+1;
            //grdListeCasier.RowDefinitions.Clear();

            //for (int i = 0; i < nbLignes; i++)
            //{
            //    RowDefinition rowDefinition = new RowDefinition();
            //    rowDefinition.Height = new GridLength(1, GridUnitType.Star);
            //    grdListeCasier.RowDefinitions.Add(rowDefinition);
            //}

            int index = 0;

            var converterColor = new BrushConverter();
            Brush couleur = (Brush)converterColor.ConvertFrom("#FF661141");
            //double largeurElement = grdListeCasier.Width / 3 - 20;
            //double hauteurElement = grdListeCasier.Height / nbLignes - 20;
            var taille = 200d;

            foreach (var casier in casiers.Lister())
            {
                int idColonne = index % 3;
                int idLigne = index / 3;
                

                var nomCasier = new Button();
                nomCasier.Click += CasierSelectione;
                nomCasier.Content = casier;
                nomCasier.Width = taille;
                nomCasier.Height = taille;
                nomCasier.Margin = new Thickness(10);
                nomCasier.VerticalAlignment = VerticalAlignment.Stretch;
                nomCasier.HorizontalAlignment = HorizontalAlignment.Stretch;
                nomCasier.HorizontalContentAlignment = HorizontalAlignment.Center;
                nomCasier.VerticalContentAlignment = VerticalAlignment.Center;
                nomCasier.FontSize = 30;
                nomCasier.FontFamily = new FontFamily("CalifornianFB");
                nomCasier.Foreground = Brushes.White;
                nomCasier.Background = couleur;
                //Grid.SetColumn(nomCasier, idColonne);
                //Grid.SetRow(nomCasier, idLigne);
                grdListeCasier.Children.Add(nomCasier);
                index++;
            }
        }



        /// <summary>
        /// 
        /// </summary>
        public void AfficherListeCasier()
        {
            MasquerEcran();

            InitialiseListeCasier();

            AffichageListeCasier.Visibility = Visibility.Visible;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_Menu3_Click(object sender, RoutedEventArgs e)
        {
            AfficherListeCasier();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CasierSelectione(object sender, RoutedEventArgs e)
        {
            var casierBouton = (Button)sender;
            var idColonne = Grid.GetColumn(casierBouton);
            var idLigne = Grid.GetRow(casierBouton);
            nCasierActuel = idLigne * 3 + idColonne;
            if (casierSuppr)
            {
                
            }
            else if (casierModif)
            {

            }
            else
            {
            AffichageListeCasier.Visibility = Visibility.Hidden;
            AffichageInterfaceCasier.Visibility = Visibility.Visible;
            GridAffichageBouteille.Visibility = Visibility.Visible;
           
            for (int i = 0; i < gestionCasier.Length; i++)
            {
                var casier = gestionCasier[i];

                    if (casier != null)
                {
                    casier.Visibility = Visibility.Hidden;
                }
            }

            var casierCourant = gestionCasier[nCasierActuel];
                if (casierCourant != null)
                {
                casierCourant.Visibility = Visibility.Visible;
            }
            AfficherBouteille();
        }
        }



        /// <summary>
        /// 
        /// </summary>
        private void MasquerEcran()
        {
            AffichageAccueil.Visibility = Visibility.Hidden;
            AffichageInterfaceCasier.Visibility = Visibility.Hidden;
            GridAffichageBouteille.Visibility = Visibility.Hidden;
            AffichageListeCasier.Visibility = Visibility.Hidden;
            AjoutCommentaireSupression.Visibility = Visibility.Hidden;
            AffichageFicheAjoutBouteille.Visibility = Visibility.Hidden;
            AffichageAjoutCasier.Visibility = Visibility.Hidden;
            AffichageRecherche.Visibility = Visibility.Hidden;
            AffichageMenuLateral.Visibility = Visibility.Hidden;
            AfficheDetailBouteille.Visibility = Visibility.Hidden;
            OptionRecherche.Visibility = Visibility.Hidden;
            Res_Recherche_Grid.Visibility = Visibility.Hidden;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_VoirCave_Click(object sender, RoutedEventArgs e)
        {
            AfficherListeCasier();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bt_GererCave_Click(object sender, RoutedEventArgs e)
        {
            MasquerEcran();
            //MultiCasier.Visibility = Visibility.Hidden;
            AffichageFicheAjoutBouteille.Visibility = Visibility.Hidden;
            AffichageAjoutCasier.Visibility = Visibility.Visible;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogo_Click(object sender, RoutedEventArgs e)
        {
            MasquerEcran();
            AffichageAccueil.Visibility = Visibility.Visible;
        }

        private void btnModifierFicheDetail_Click(object sender, RoutedEventArgs e)
        {
            Metier.Type t;
            Metier.Pays p = null;
            Metier.Region r = null;
            Metier.Domaine d = null;
            Metier.Appelation a = null;
            Metier.Contenance c = null;
            Metier.Millesime m = null;
            Metier.Cru cru = null;
            Metier.Type_vinification tv = null;

            #region gestion combobox

            DAO.TypeDAO daoType = new DAO.TypeDAO(DAO.BDD.Instance.Connexion);
            string nomType = lbl_Categorie.Text;
            t = daoType.Chercher(lbl_Categorie.Text);
            if (t == null)
            {
                daoType.Créer(nomType);
                t = daoType.Chercher(lbl_Categorie.Text);
            }

            if (lbl_Pays.Text != "")
            {
                DAO.PaysDAO daoPays = new DAO.PaysDAO(DAO.BDD.Instance.Connexion);
                string nomPays = lbl_Pays.Text;
                p = daoPays.Chercher(lbl_Pays.Text);
                if (p == null)
                {
                    daoPays.Créer(nomPays);
                    p = daoPays.Chercher(lbl_Pays.Text);
                }
            }


            if (lbl_Region.Text != "")
            {
                DAO.RegionDAO daoReg = new DAO.RegionDAO(DAO.BDD.Instance.Connexion);
                string nomRegion = lbl_Region.Text;
                r = daoReg.Chercher(lbl_Region.Text);
                if (r == null)
                {
                    daoReg.Créer(nomRegion);
                    r = daoReg.Chercher(lbl_Region.Text);
                }
            }

            if (lbl_Domaine.Text != "")
            {
                DAO.DomaineDAO daoDom = new DAO.DomaineDAO(DAO.BDD.Instance.Connexion);
                string nomDomaine = lbl_Domaine.Text;
                d = daoDom.Chercher(lbl_Domaine.Text);
                if (d == null)
                {
                    daoDom.Créer(nomDomaine);
                    d = daoDom.Chercher(lbl_Domaine.Text);
                }
            }

            if (lbl_Appelation.Text != "")
            {
                DAO.AppelationDAO daoApp = new DAO.AppelationDAO(DAO.BDD.Instance.Connexion);
                string nomApp = lbl_Appelation.Text;
                a = daoApp.Chercher(lbl_Appelation.Text);
                if (a == null)
                {
                    daoApp.Créer(nomApp);
                    a = daoApp.Chercher(lbl_Appelation.Text);
                }
            }

            if ((lbl_Contenance.Text != "") && (lbl_Contenance.Text != null))
            {
                DAO.ContenanceDAO daoCon = new DAO.ContenanceDAO(DAO.BDD.Instance.Connexion);
                int nomCon = Int32.Parse(lbl_Contenance.Text);
                c = daoCon.Chercher(nomCon, "");
                if (c == null)
                {
                    daoCon.Créer(nomCon, "");
                    c = daoCon.Chercher(nomCon, "");
                }
            }

            if ((lbl_Millesime.Text != "")&& (lbl_Millesime.Text != null))
            {
                DAO.MillesimeDAO daoMil = new DAO.MillesimeDAO(DAO.BDD.Instance.Connexion);
                int nomMil = Int32.Parse(lbl_Millesime.Text);
                m = daoMil.Chercher(nomMil, "");
                if (m == null)
                {
                    daoMil.Créer(nomMil);
                    m = daoMil.Chercher(nomMil, "");
                }
            }

            if (lbl_Cru.Text != "")
            {
                DAO.CruDAO daoCru = new DAO.CruDAO(DAO.BDD.Instance.Connexion);
                string nomCru = lbl_Cru.Text;
                cru = daoCru.Chercher(lbl_Cru.Text);
                if (cru == null)
                {
                    daoCru.Créer(nomCru);
                    cru = daoCru.Chercher(lbl_Cru.Text);
                }
            }

            if (lbl_Vinification.Text != "")
            {
                DAO.Type_vinificationDAO daoVinif = new DAO.Type_vinificationDAO(DAO.BDD.Instance.Connexion);
                string nomVinif = lbl_Vinification.Text;
                tv = daoVinif.Chercher(lbl_Vinification.Text);
                if (tv == null)
                {
                    daoVinif.Créer(nomVinif);
                    tv = daoVinif.Chercher(lbl_Vinification.Text);
                }
            }

            #endregion

            DAO.CasierDAO daoCas = new DAO.CasierDAO(DAO.BDD.Instance.Connexion);
            Metier.Casier cas;
            DAO.BouteilleDAO daoBou = new DAO.BouteilleDAO(DAO.BDD.Instance.Connexion);

            var brush = new ImageBrush();

            cas = daoCas.Chercher(nCasierActuel);
            Metier.Bouteille b = new Metier.Bouteille(1, false, "", l_ligne, l_col, cas, cru, m, c, d, r, tv, a, t, p);
            daoBou.Modifier(b);
            }

        private void btnDéplacerFicheDetail_Click(object sender, RoutedEventArgs e)
        {
            if(deplacer == false)
            {
                deplacer = true;
            }
            else
            {
                var brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri("../../../CaveAVin/Images/CaseVide.png", UriKind.Relative));
                ((Button)listeBouton[l_cas].GetValue(l_ligne, l_col)).Background = brush;
                ((Button)listeBouton[l_cas].GetValue(l_ligne, l_col)).Click -= SelectionBouteille;
                ((Button)listeBouton[l_cas].GetValue(l_ligne, l_col)).Click += SelectBouteille;
                ((Button)listeBouton[l_cas].GetValue(l_ligne, l_col)).Opacity = 1.0;

                var brush2 = new ImageBrush();
                brush2.ImageSource = new BitmapImage(new Uri("../../../CaveAVin/Images/BlancCasier.png", UriKind.Relative));
                ((Button)listeBouton[l_cas_dep].GetValue(l_ligne_dep, l_col_dep)).Background = brush2;
                ((Button)listeBouton[l_cas_dep].GetValue(l_ligne_dep, l_col_dep)).Click += SelectionBouteille;
                ((Button)listeBouton[l_cas_dep].GetValue(l_ligne_dep, l_col_dep)).Click -= SelectBouteille;
                ((Button)listeBouton[l_cas_dep].GetValue(l_ligne_dep, l_col_dep)).Opacity = 0.5;

                DAO.BouteilleDAO daoBout = new DAO.BouteilleDAO(DAO.BDD.Instance.Connexion);
                daoBout.Deplacer(l_ligne,l_col,l_cas,l_ligne_dep,l_col_dep,l_cas_dep);
                l_ligne = l_ligne_dep;
                l_col = l_col_dep;
                l_cas = l_cas_dep;
                deplacer = false;
            }
        }

        private void btnFermerFicheDetail_Click(object sender, RoutedEventArgs e)
        {
            ReDecaler();
            AfficheDetailBouteille.Visibility = Visibility.Hidden;
            ((Button)listeBouton[l_cas].GetValue(l_ligne, l_col)).Opacity = 1.0;
        }

        private void btnFermerFicheAjout_Click(object sender, RoutedEventArgs e)
        {
            ReDecaler();
            AffichageFicheAjoutBouteille.Visibility = Visibility.Hidden;
            ((Button)listeBouton[l_cas].GetValue(l_ligne, l_col)).Opacity = 1.0;
        }

        private void btnModifierCasier_Click(object sender, RoutedEventArgs e)
        {
            if(!casierModif)
            {
                casierModif = true;
            }
            else
            {

            }
        }

        private void BT_Supprimer_Casier_Click(object sender, RoutedEventArgs e)
        {
            if(!casierSuppr)
            {
                casierSuppr = true;
            }
            else
            {

            }
        }

        private void AffichagePrincipal_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RecalculailleGrille();
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValidationRecherche_Click(object sender, RoutedEventArgs e)
        {
            #region gère affichage

            MasquerEcran();
            AffichageRecherche.Visibility = Visibility.Visible;
            AffichageRecherche.Visibility = Visibility.Visible;
            Res_Recherche_Grid.Visibility = Visibility.Visible;

            #endregion

            Metier.Type t;
            Metier.Pays p = null;
            Metier.Region r = null;
            Metier.Domaine d = null;
            Metier.Appelation a = null;
            Metier.Contenance c = null;
            Metier.Millesime m = null;
            Metier.Cru cru = null;
            Metier.Type_vinification tv = null;

            #region gestion combobox

            DAO.TypeDAO daoType = new DAO.TypeDAO(DAO.BDD.Instance.Connexion);
            string nomType = lbl_Categorie.Text;
            t = daoType.Chercher(lbl_Categorie.Text);
            if (t == null)
            {
                daoType.Créer(nomType);
                t = daoType.Chercher(lbl_Categorie.Text);
            }

            if (lbl_Pays.Text != "")
            {
                DAO.PaysDAO daoPays = new DAO.PaysDAO(DAO.BDD.Instance.Connexion);
                string nomPays = lbl_Pays.Text;
                p = daoPays.Chercher(lbl_Pays.Text);
                if (p == null)
                {
                    daoPays.Créer(nomPays);
                    p = daoPays.Chercher(lbl_Pays.Text);
                }
            }


            if (lbl_Region.Text != "")
            {
                DAO.RegionDAO daoReg = new DAO.RegionDAO(DAO.BDD.Instance.Connexion);
                string nomRegion = lbl_Region.Text;
                r = daoReg.Chercher(lbl_Region.Text);
                if (r == null)
                {
                    daoReg.Créer(nomRegion);
                    r = daoReg.Chercher(lbl_Region.Text);
                }
            }

            if (lbl_Domaine.Text != "")
            {
                DAO.DomaineDAO daoDom = new DAO.DomaineDAO(DAO.BDD.Instance.Connexion);
                string nomDomaine = lbl_Domaine.Text;
                d = daoDom.Chercher(lbl_Domaine.Text);
                if (d == null)
                {
                    daoDom.Créer(nomDomaine);
                    d = daoDom.Chercher(lbl_Domaine.Text);
                }
            }

            if (lbl_Appelation.Text != "")
            {
                DAO.AppelationDAO daoApp = new DAO.AppelationDAO(DAO.BDD.Instance.Connexion);
                string nomApp = lbl_Appelation.Text;
                a = daoApp.Chercher(lbl_Appelation.Text);
                if (a == null)
                {
                    daoApp.Créer(nomApp);
                    a = daoApp.Chercher(lbl_Appelation.Text);
                }
            }

            if ((lbl_Contenance.Text != "") && (lbl_Contenance.Text != null))
            {
                DAO.ContenanceDAO daoCon = new DAO.ContenanceDAO(DAO.BDD.Instance.Connexion);
                int nomCon = Int32.Parse(lbl_Contenance.Text);
                c = daoCon.Chercher(nomCon, "");
                if (c == null)
                {
                    daoCon.Créer(nomCon, "");
                    c = daoCon.Chercher(nomCon, "");
                }
            }

            if ((lbl_Millesime.Text != "") && (lbl_Millesime.Text != null))
            {
                DAO.MillesimeDAO daoMil = new DAO.MillesimeDAO(DAO.BDD.Instance.Connexion);
                int nomMil = Int32.Parse(lbl_Millesime.Text);
                m = daoMil.Chercher(nomMil, "");
                if (m == null)
                {
                    daoMil.Créer(nomMil);
                    m = daoMil.Chercher(nomMil, "");
                }
            }

            if (lbl_Cru.Text != "")
            {
                DAO.CruDAO daoCru = new DAO.CruDAO(DAO.BDD.Instance.Connexion);
                string nomCru = lbl_Cru.Text;
                cru = daoCru.Chercher(lbl_Cru.Text);
                if (cru == null)
                {
                    daoCru.Créer(nomCru);
                    cru = daoCru.Chercher(lbl_Cru.Text);
                }
            }

            if (lbl_Vinification.Text != "")
            {
                DAO.Type_vinificationDAO daoVinif = new DAO.Type_vinificationDAO(DAO.BDD.Instance.Connexion);
                string nomVinif = lbl_Vinification.Text;
                tv = daoVinif.Chercher(lbl_Vinification.Text);
                if (tv == null)
                {
                    daoVinif.Créer(nomVinif);
                    tv = daoVinif.Chercher(lbl_Vinification.Text);
                }
            }

            #endregion

            DAO.CasierDAO daoCas = new DAO.CasierDAO(DAO.BDD.Instance.Connexion);
            Metier.Casier cas;
            DAO.BouteilleDAO daoBou = new DAO.BouteilleDAO(DAO.BDD.Instance.Connexion);

            var brush = new ImageBrush();

            cas = daoCas.Chercher(nCasierActuel);
            Metier.Bouteille b = new Metier.Bouteille(1, false, "", l_ligne, l_col, cas, cru, m, c, d, r, tv, a, t, p);
            Metier.Bouteilles bs = daoBou.Chercher(b);
            //foreach ( Metier.Bouteille x in bs )
            //{

            //}
        }
    }
}

