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
    /// Lógica de interacción para VentanaBienvenida.xaml
    /// </summary>
    public partial class VentanaBienvenida : Window
    {
        public VentanaBienvenida()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            
        }
    }
}
