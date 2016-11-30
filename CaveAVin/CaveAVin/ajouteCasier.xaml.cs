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
using System.Windows.Shapes;

namespace CaveAVin
{
    /// <summary>
    /// Logique d'interaction pour ajouteCasier.xaml
    /// </summary>
    public partial class ajouteCasier : Window
    {
        private int nbhauteurCasier;
        private int nblargeurCasier;
        private List<Metier.Casier> casiers;
        private Metier.Casier casier;

        public ajouteCasier()
        {
            InitializeComponent();
            nbhauteurCasier = 1;
            nblargeurCasier = 1;
            hauteurCasier.Text = nbhauteurCasier.ToString();
            largeurCasier.Text = nblargeurCasier.ToString();

            buttonMoinsHauteur.IsEnabled = false;
            buttonMoinsLargeur.IsEnabled = false;
        }

        private void buttonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void buttonPlusHauteur_Click(object sender, RoutedEventArgs e)
        {
            if (nbhauteurCasier == 1)
            {
                buttonPlusHauteur.IsEnabled = true;
            }
            nbhauteurCasier = nbhauteurCasier + 1;
            hauteurCasier.Text = nbhauteurCasier.ToString();
        }

        private void buttonMoinsHauteur_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Int32.Parse(hauteurCasier.Text) > 1)
                {
                    nbhauteurCasier = nbhauteurCasier - 1;
                    hauteurCasier.Text = nbhauteurCasier.ToString();
                    if (nbhauteurCasier == 1)
                    {
                        buttonMoinsHauteur.IsEnabled = false;
                    }else
                    {
                        buttonMoinsHauteur.IsEnabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("La hauteur du casier ne peux etre négatif ou null");
                    hauteurCasier.Text = nbhauteurCasier.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("La hauteur du casier doit être un entier");
            }
        }

        private void buttonPlusLargeur_Click(object sender, RoutedEventArgs e)
        {
            if (nblargeurCasier == 1)
            {
                buttonPlusLargeur.IsEnabled = true;
            }
            nblargeurCasier = nblargeurCasier + 1;
            largeurCasier.Text = nblargeurCasier.ToString();
        }

        private void buttonMoinsLargeur_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Int32.Parse(largeurCasier.Text) > 1)
                {
                    nblargeurCasier = nblargeurCasier - 1;
                    largeurCasier.Text = nblargeurCasier.ToString();
                    if (nblargeurCasier == 1)
                    {
                        buttonMoinsLargeur.IsEnabled = false;
                    }else
                    {
                        buttonMoinsLargeur.IsEnabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("La largeur du casier ne peux etre négatif ou null");
                    largeurCasier.Text = nblargeurCasier.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("La largeur du casier doit être un entier");
            }
        }

        private void buttonValider_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // test si tt les champs obligatoires sont remplies
                if ((nbhauteurCasier >= 1) && (nblargeurCasier >= 1) && (nomCasier.Text != ""))
                {
                    casiers.Add(casier);
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Les champs ne sont pas correctement remplie");
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void textBox_Copy_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
