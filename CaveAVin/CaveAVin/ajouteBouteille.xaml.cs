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
        private List<Metier.Bouteille> bouteilles;
        private Metier.Bouteille bouteille;
        private int nbBouteille;
        public ajouteBouteille()
        {
            //bouteilles = b;
            bouteille = new Metier.Bouteille();
            nbBouteille = 1;

            InitializeComponent();
            textBox4.Text = nbBouteille.ToString();
            button.IsEnabled = false;

            // initialisation des listes dans les comboBox
            //int index = -1, i = 0;
            //foreach (Metier.Type c in typs.Lister())
            //{
            //    comboBox.Items.Add(c);
            //    if (c.Id == bouteille.IdType)
            //        index = i;
            //    ++i;
            //}
            //comboBox.SelectedIndex = index;

            ////index = -1;i = 0;
            ////foreach (Metier.Region c in regs.Lister())
            ////{
            ////    comboBox1.Items.Add(c);
            ////    if (c.Id == bouteille.IdRegion)
            ////        index = i;
            ////    ++i;
            ////}
            ////comboBox1.SelectedIndex = index;

            //index = -1; i = 0;
            //foreach (Metier.Appelation c in apps.Lister())
            //{
            //    comboBox2.Items.Add(c);
            //    if (c.Id == bouteille.IdAppelation)
            //        index = i;
            //    ++i;
            //}
            //comboBox2.SelectedIndex = index;

            //index = -1; i = 0;
            //foreach (Metier.Pays c in pays.Lister())
            //{
            //    comboBox3.Items.Add(c);
            //    if (c.Id == bouteille.Region.IdPays)
            //        index = i;
            //    ++i;
            //}
            //comboBox3.SelectedIndex = index;

            //index = -1; i = 0;
            //foreach (Metier.Contenance c in conts.Lister())
            //{
            //    comboBox4.Items.Add(c);
            //    if (c.Id == bouteille.IdContenance)
            //        index = i;
            //    ++i;
            //}
            //comboBox4.SelectedIndex = index;
        }

        // appuyer sur le bouton annuler
        private void annuler(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        // appuyer sur le bouton valider
        private void valider(object sender, RoutedEventArgs e)
        {
            try
            {
                // test si tt les champs obligatoires sont remplies
                if (nbBouteille >= 1 && textBox3.Text != "")
                {
                    bouteille.Type = comboBox.SelectedItem as Metier.Type;
                    bouteille.Region = comboBox1.SelectedItem as Metier.Region;
                    bouteille.Appelation = comboBox2.SelectedItem as Metier.Appelation;
                    bouteille.Region.Pays = comboBox3.SelectedItem as Metier.Pays;
                    bouteille.Contenance = comboBox4.SelectedItem as Metier.Contenance;

                    for (int i = 1; i <= nbBouteille; i++)
                    {
                        bouteilles.Add(bouteille);
                    }
                    DialogResult = true;
                }else
                {
                    MessageBox.Show("Les champs ne sont pas correctement remplies");
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        // appuyer sue le bouton plus (pour plus de bouteilles)
        private void plus(object sender, RoutedEventArgs e)
        {
            if (nbBouteille == 1)
            {
                button.IsEnabled = true;
            }
            nbBouteille = nbBouteille + 1;
            textBox4.Text = nbBouteille.ToString();
        }

        // appuyer sue le bouton moins (pour moins de bouteilles)
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
