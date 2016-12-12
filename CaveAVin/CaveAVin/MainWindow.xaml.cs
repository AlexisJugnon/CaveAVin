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

        private int nCasierActuel = 1;

        private int nbCasierTotal = 1;

        private List<Button[,]> listeBouton = new List<Button[,]>();

        private Button[,] button;

        private Grid[] gestionCasier;

        private int l_ligne;

        private int l_col;

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

            AffichagePrincipal.Visibility = Visibility.Visible;
            AffichageAccueil.Visibility = Visibility.Visible;
            AffichageInterfaceCasier.Visibility = Visibility.Hidden;
            MultiCasier.Visibility = Visibility.Hidden;
            AffichageMenuLateral.Visibility = Visibility.Hidden;
            AffichageFicheAjoutBouteille.Visibility = Visibility.Hidden;
            AffichageRecherche.Visibility = Visibility.Hidden;

            #endregion

            InitialiserBDD();

            supprimer = false;

            initGridCasier(); //Création des différents casiers

            afficherBouteille();

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
        /// Permet d'initialiser tous les casiers sous forme de grid pour pouvoir les afficher plus tard
        /// </summary>
        private void initGridCasier()
        {
            nbCasierTotal = req.SelInt("Select Count(IdCasier) FROM Casier");
            gestionCasier = new Grid[nbCasierTotal + 1];

            int nombreColonne, nombreLigne;
            ColumnDefinition[] colonne;

            RowDefinition[] ligne;
            int HeightBoutton, WidthBoutton;


            for (int i = 1; i < nbCasierTotal + 1; i++)
            {
                gestionCasier[i] = new Grid();
                GridAffichageBouteille.Children.Add(gestionCasier[i]);
                gestionCasier[i].Visibility = Visibility.Hidden;

                nombreColonne = 0; nombreLigne = 0;
                nombreLigne = req.SelInt("Select Largeur_X FROM Casier where idCasier = " + i);
                nombreColonne = req.SelInt("Select Largeur_Y FROM Casier where idCasier = " + i);

                WidthBoutton = (int)AffichageBordureCasier.Width / nombreColonne;
                HeightBoutton = (int)AffichageBordureCasier.Height / nombreLigne;

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
                #region GestionAffichBouteille
                //Gère l'affichage des bouteilles dans le casier
                string res;
                button = new Button[nombreColonne, nombreLigne];
                for (int k = 0; k < nombreColonne; k++)
                {
                    for (int j = 0; j < nombreLigne; j++)
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
                listeBouton.Add(button);
            }

            #endregion
        }

        /// <summary>
        /// Gestion du clic sur un emplacement vide
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
                if (pos.X == ligne && pos.Y==col && pos.Casier== nCasierActuel)
                {
                    existe = true;
                    listeBouteilleAjout.Remove(pos);
                    break;
                }
            }
            if (existe)
            {            
                ((Button)listeBouton[nCasierActuel - 1].GetValue(ligne, col)).Opacity = 1.0;

            }
            else
            {
                posi.Casier = nCasierActuel;
                listeBouteilleAjout.Add(posi);
                ((Button)listeBouton[nCasierActuel - 1].GetValue(ligne, col)).Opacity = 0.5;

            }
        }

        /// <summary>
        /// permet d'afficher le casier suivant ou précedent
        /// </summary>
        private void afficherBouteille()
        {
            // Permet de gérer l'affichage des flêches pour la gestion des casiers
            #region GestionFleche

            if (nbCasierTotal > 1)
            {
                MultiCasier.Visibility = Visibility.Visible;
            }

            if (nCasierActuel == 1)
            {
                CasierPrecedent.Visibility = Visibility.Hidden;
            }
            else
            {
                CasierPrecedent.Visibility = Visibility.Visible;
            }

            if (nCasierActuel < nbCasierTotal)
            {
                CasierSuivant.Visibility = Visibility.Visible;
            }
            else
            {
                CasierSuivant.Visibility = Visibility.Hidden;
            }
            #endregion

            lblNomCasier.Content = req.SelStr1("Select NomCasier From Casier Where idCasier = " + nCasierActuel, "NomCasier");

            gestionCasier[nCasierActuel].Visibility = Visibility.Visible;

        }

        /// <summary>
        /// Permet de gérer le click sur une bouteille
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectionBouteille(Object sender, EventArgs e)
        {
            if (!supprimer)
            {
                if (courante != null)
                    ((Button)listeBouton[courante.Casier - 1].GetValue(courante.X, courante.Y)).Opacity = 1.0;

                l_ligne = Int32.Parse(sender.ToString().Substring(32, 1));
                l_col = Int32.Parse(sender.ToString().Substring(35, 1));

                courante = new Metier.Position(l_ligne,l_col);
                courante.Casier = nCasierActuel; 

                Decaler();
                var button = (Button)sender;
                var textContent = button.Content.ToString();
                var coordonee = textContent.Split(',');

                    Metier.Position posi = new Metier.Position(l_ligne, l_col);
                    posi.Casier = nCasierActuel;
            
                var casierDao = new CasierDAO(DAO.BDD.Instance.Connexion);
                var bouteilleDao = new BouteilleDAO(DAO.BDD.Instance.Connexion);
                var casier = casierDao.Chercher(nCasierActuel);
                var bouteille = bouteilleDao.Chercher(posi.X, posi.Y, posi.Casier);
                ((Button)listeBouton[nCasierActuel - 1].GetValue(l_ligne, l_col)).Opacity = 0.5;
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
                    ((Button)listeBouton[nCasierActuel - 1].GetValue(ligne, col)).Opacity = 1.0;
                    

                }
                else
                {
                    posi.Casier = nCasierActuel;
                    listeBouteilleSuppr.Add(posi);
                    ((Button)listeBouton[nCasierActuel - 1].GetValue(ligne, col)).Opacity = 0.5;


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
            afficherBouteille();
        }

        /// <summary>
        /// Permet de décaler l'affichage du casier pour pouvoir afficher la fiche bouteille
        /// </summary>
        private void Decaler()
        {
            AffichageCasier.Margin = new Thickness(0, 30, 0, 50);
        }

        private void ReDecaler()
        {
            AffichageCasier.Margin = new Thickness(50, 50, 50, 50);
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
            afficherBouteille();
        }

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
                lbl_Appelation.Content = bouteille.Appelation?.NomAppelation;
                lbl_Categorie.Content = bouteille.Type?.NomType;
                lbl_Contenance.Content = bouteille.Contenance?.Valeur;
                lbl_Cru.Content = bouteille.Cru?.NomCru;
                lbl_Domaine.Content = bouteille.Domaine?.NomDomaine;
                lbl_Millesime.Content = bouteille.Millesime?.NomMillesime;
                lbl_Pays.Content = bouteille.Region?.Pays?.NomPays;
                lbl_Region.Content = bouteille.Region?.NomRegion;
                lbl_Vinification.Content = bouteille.Type_vinification?.NomVinif;
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

        #endregion


        /// <summary>
        /// Affiche la page des casier et cache l'accueil lorsque on appuie sur le bouton du milieu de l'accueil
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_VoirCasier_Click(object sender, RoutedEventArgs e)
        {
            AffichageAccueil.Visibility = Visibility.Hidden;
            AffichageInterfaceCasier.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Affiche la page d'accueil et cache les casiers lorsque on appuie sur le 1er bouton du menu déroulant
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_RetourAccueil_Click(object sender, RoutedEventArgs e)
        {
            AffichageAccueil.Visibility = Visibility.Visible;
            AffichageInterfaceCasier.Visibility = Visibility.Hidden;
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
           
            var brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri("../../../CaveAVin/CaseVide.png", UriKind.Relative));
            ((Button)listeBouton[nCasierActuel - 1].GetValue(l_ligne, l_col)).Background = brush;
            ((Button)listeBouton[nCasierActuel - 1].GetValue(l_ligne, l_col)).Click -= selectionBouteille;
            ((Button)listeBouton[nCasierActuel - 1].GetValue(l_ligne, l_col)).Click += selectBouteille;
            ((Button)listeBouton[nCasierActuel - 1].GetValue(l_ligne, l_col)).Opacity = 1.0;
            req.delete(l_ligne, l_col, nCasierActuel);
            ((Button)listeBouton[nCasierActuel - 1].GetValue(l_ligne, l_col)).Opacity = 1.0;

            AfficheDetailBouteille.Visibility = Visibility.Hidden;
            ReDecaler();
        }

        /// <summary>
        /// Affichage de la fenêtre pour ajouter un casier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_AjoutCasier_Click(object sender, RoutedEventArgs e)
        {
            ajouteCasier addCasier = new ajouteCasier();
            addCasier.Visibility = Visibility.Visible;
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
                    req.delete(ligne, col, cas);

                    ((Button)listeBouton[cas - 1].GetValue(ligne, col)).Opacity = 1.0;

                    brush = new ImageBrush();
                    brush.ImageSource = new BitmapImage(new Uri("../../../CaveAVin/CaseVide.png", UriKind.Relative));
                    ((Button)listeBouton[cas-1].GetValue(ligne, col)).Background = brush;
                    ((Button)listeBouton[cas-1].GetValue(ligne, col)).Click -= selectionBouteille;
                    ((Button)listeBouton[cas-1].GetValue(ligne, col)).Click += selectBouteille;
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

        /// <summary>
        /// Gère l'ajout des bouteilles après validation de l'ajout
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValidationAjout_Click(object sender, RoutedEventArgs e)
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
            t = daoType.Chercher(cb_Type_Ajout.Text);
            if (t == null)
            {
                daoType.Créer(nomType);
                t = daoType.Chercher(cb_Type_Ajout.Text);
            }


            DAO.PaysDAO daoPays = new DAO.PaysDAO(DAO.BDD.Instance.Connexion);
            string nomPays = cb_Pays_Ajout.Text;
            p = daoPays.Chercher(cb_Pays_Ajout.Text);
            if (p == null)
            {
                daoPays.Créer(nomPays);
                p = daoPays.Chercher(cb_Pays_Ajout.Text);
            }

            DAO.RegionDAO daoReg = new DAO.RegionDAO(DAO.BDD.Instance.Connexion);
            string nomRegion = cb_Region_Ajout.Text;
            r = daoReg.Chercher(cb_Region_Ajout.Text);
            if (r == null)
            {
                daoReg.Créer(nomRegion, nomPays);
                r = daoReg.Chercher(cb_Region_Ajout.Text);
            }

            DAO.DomaineDAO daoDom = new DAO.DomaineDAO(DAO.BDD.Instance.Connexion);
            string nomDomaine = cb_Domaine_Ajout.Text;
            d = daoDom.Chercher(cb_Domaine_Ajout.Text);
            if (d == null)
            {
                daoDom.Créer(nomDomaine);
                d = daoDom.Chercher(cb_Domaine_Ajout.Text);
            }

            DAO.AppelationDAO daoApp = new DAO.AppelationDAO(DAO.BDD.Instance.Connexion);
            string nomApp = cb_Appelation_Ajout.Text;
            a = daoApp.Chercher(cb_Appelation_Ajout.Text);
            if(a == null)
            {
                daoApp.Créer(nomApp);
                a = daoApp.Chercher(cb_Appelation_Ajout.Text);
            }

            DAO.ContenanceDAO daoCon = new DAO.ContenanceDAO(DAO.BDD.Instance.Connexion);
            int nomCon = Int32.Parse(cb_Contenance_Ajout.Text);
            c = daoCon.Chercher(nomCon,"");
            if(c == null)
            { 
                daoCon.Créer(nomCon,"");
                c = daoCon.Chercher(nomCon,"");
            }

            DAO.MillesimeDAO daoMil = new DAO.MillesimeDAO(DAO.BDD.Instance.Connexion);
            string nomMil = cb_Millesime_Ajout.Text;
            m = daoMil.Chercher(cb_Millesime_Ajout.Text);
            if(m == null)
            {
                daoMil.Créer(nomMil);
                m = daoMil.Chercher(cb_Millesime_Ajout.Text);
            }

            DAO.CruDAO daoCru = new DAO.CruDAO(DAO.BDD.Instance.Connexion);
            string nomCru = cb_Cru_Ajout.Text;
            cru = daoCru.Chercher(cb_Cru_Ajout.Text);
            if(cru == null)
            {
                daoCru.Créer(nomCru);
                cru = daoCru.Chercher(cb_Cru_Ajout.Text);
            }

            DAO.Type_vinificationDAO daoVinif = new DAO.Type_vinificationDAO(DAO.BDD.Instance.Connexion);
            string nomVinif = cb_Vinification_Ajout.Text;
            tv = daoVinif.Chercher(cb_Vinification_Ajout.Text);
            if(tv == null)
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
                    ((Button)listeBouton[nCasierActuel - 1].GetValue(pos.X, pos.Y)).Background = brush;
                    ((Button)listeBouton[nCasierActuel - 1].GetValue(pos.X, pos.Y)).Click -= selectBouteille;
                    ((Button)listeBouton[nCasierActuel - 1].GetValue(pos.X, pos.Y)).Click += selectionBouteille;
                    ((Button)listeBouton[nCasierActuel - 1].GetValue(pos.X, pos.Y)).Opacity = 1.0;

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

            AffichagePrincipal.Visibility = Visibility.Visible;
            AffichageRecherche.Visibility = Visibility.Visible;
            AffichageAccueil.Visibility = Visibility.Hidden;
            AffichageInterfaceCasier.Visibility = Visibility.Hidden;
            MultiCasier.Visibility = Visibility.Hidden;
            AffichageMenuLateral.Visibility = Visibility.Hidden;
            AffichageFicheAjoutBouteille.Visibility = Visibility.Hidden;

            #endregion
        }
    }
}
