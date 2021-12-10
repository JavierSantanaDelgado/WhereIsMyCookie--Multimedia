using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MultimediaLab
{
    /// <summary>
    /// Lógica de interacción para Decision.xaml
    /// </summary>
    public partial class Decision : Window
    {
        public Decision()
        {
            InitializeComponent();
        }

        private void perdiste(object sender, RoutedEventArgs e)
        {
            VentanaBienvenida bienvenida = new VentanaBienvenida();
            this.Hide();
            bienvenida.ShowDialog();
            this.Close();
        }

        private void ganaste(object sender, RoutedEventArgs e)
        {
            btnCasero.Visibility = Visibility.Hidden;
            btnGorka.Visibility = Visibility.Hidden;
            btnNadie.Visibility = Visibility.Hidden;
            lblTituloCulpable.Visibility = Visibility.Hidden;
            lblGanador.Visibility = Visibility.Visible;
        }
    }
}
