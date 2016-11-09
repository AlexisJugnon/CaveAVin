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
    /// Logique d'interaction pour ajouteBouteille.xaml
    /// </summary>
    public partial class ajouteBouteille : Window
    {
        private Metier.Bouteille bouteille;
        private int nbBouteille;
        public ajouteBouteille(Metier.Bouteille b,Metier.Regions regs,Metier.Appelations apps,Metier.Pays2 pays,Metier.Contenances conts)
        {
            bouteille = b;
            nbBouteille = 1;
            InitializeComponent();
            textBox4.Text = nbBouteille.ToString();
            button.IsEnabled = false;
        }

        private void annuler(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void valider(object sender, RoutedEventArgs e)
        {
            try
            {
                
                
                DialogResult = true;
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        private void plus(object sender, RoutedEventArgs e)
        {
            if (nbBouteille == 1)
            {
                button.IsEnabled = true;
            }
            nbBouteille = nbBouteille + 1;
            textBox4.Text = nbBouteille.ToString();
        }

        private void moins(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Int32.Parse(textBox4.Text) > 1 )
                {
                    nbBouteille = nbBouteille - 1;                
                    textBox4.Text = nbBouteille.ToString();
                    if (nbBouteille == 1)
                    {
                        button.IsEnabled = false;
                    }
                }
                else
                {
                    MessageBox.Show("Le nombre de Bouteille ne peux etre négatif ou null");
                    textBox4.Text = nbBouteille.ToString();
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Le nombre de bouteille doit être un entier");
            }
        }
    }
}
