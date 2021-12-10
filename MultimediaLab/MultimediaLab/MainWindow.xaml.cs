using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MultimediaLab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        String posicionAnterior = "";
        String posicionActual = "Cama";
        String rutaVideo = @"D:/4Curso/Multimedia/TrabajoPractico/MultimediaLab/MultimediaLab/vid/";
        String rutaImagen = @"D:/4Curso/Multimedia/TrabajoPractico/MultimediaLab/MultimediaLab/img/";
        String[] Habitacion = new String[3] { "Papelera", "Ordenador", "Plato" };
        String[] Cocina = new String[3] { "Mesa", "Armario", "Microondas" };
        String Salon = "Casero";
        String Companero = "Escritorio";
        Storyboard sbDespertar;
        Boolean llave = false;
        Boolean hab = true;
        Boolean cocina = true;
        Boolean compa = true;
        public MainWindow()
        {
            /*VentanaBienvenida pantallaInicio = new VentanaBienvenida(this);
            pantallaInicio.ShowDialog();*/
            InitializeComponent();
            
             
            todosDisenable();
            sbDespertar = (Storyboard)this.Resources["Despertar"];
            sbDespertar.Completed += new EventHandler(animacionterminada);
            sbDespertar.Begin();


            Canvas canvas = new Canvas();
            MediaElement video = new MediaElement();
            desactivacionBotones();
            comprobarEstadoHabitacion();
            

        }


        private void MovimientoPersonaje(object sender, RoutedEventArgs e)
        {
            Button boton = sender as Button;
            posicionAnterior = posicionActual;
            posicionActual = boton.Name.Substring(3);
            boton.Visibility = Visibility.Hidden;
            imgInteraccion.Visibility = Visibility.Hidden;
            ImageBrush myBrush = new ImageBrush();
            Image image = new Image();
            switch (posicionActual)
            {
                case "Habitacion":
                    btnPasilloH.Visibility = Visibility.Visible;
                    desactivacionBotones();
                    comprobarEstadoHabitacion();
                    break;
                case "PasilloH":
                    desactivacionBotones();
                    comprobarEstadoPasillo();
                    break;
                case "PasilloC":
                    desactivacionBotones();
                    comprobarEstadoPasillo();
                    break;
                case "PasilloS":
                    desactivacionBotones();
                    comprobarEstadoPasillo();
                    break;
                case "PasilloG":
                    desactivacionBotones();
                    comprobarEstadoPasillo();
                    break;
                case "Cocina":
                    desactivacionBotones();
                    comprobarEstadoCocina();
                    break;
                case "Salon":
                    desactivacionBotones();
                    comprobarEstadoSalon();
                    break;
                case "Companero":
                    canvas.Visibility = Visibility.Hidden;
                    if (!posicionActual.Equals(posicionAnterior) || (posicionAnterior.Equals("PasilloG") && posicionActual.Equals("Companero")))
                    {
                        if (posicionAnterior.Equals("PasilloG") && posicionActual.Equals("Companero"))
                        {
                            canvas.Visibility = Visibility.Visible;
                            image.Source = new BitmapImage(new Uri(rutaImagen + "PasilloCCompaneroB.png", UriKind.Absolute));
                            myBrush.ImageSource = image.Source;
                            canvas.Background = myBrush;
                            desactivacionBotones();
                            comprobarEstadoPasillo();
                        }
                        else
                        {
                            if (llave)
                            {
                                video.Source = new Uri(rutaVideo + posicionAnterior + posicionActual + ".mkv", UriKind.RelativeOrAbsolute);
                                desactivacionBotones();
                                comprobarEstadoCompanero();
                                todosDisenable();
                            }
                            else
                            {
                                video.Source = new Uri(rutaVideo + posicionAnterior + posicionActual + "B.mkv", UriKind.RelativeOrAbsolute);
                                todosDisenable();
                            }
                            video.Visibility = Visibility.Visible;
                            video.Play();
                        }
                        }
                    else
                    {
                        canvas.Visibility = Visibility.Visible;
                        image.Source = new BitmapImage(new Uri(rutaImagen + "PasilloCCompaneroB.png", UriKind.Absolute));
                        myBrush.ImageSource = image.Source;
                        canvas.Background = myBrush;
                        desactivacionBotones();
                        comprobarEstadoPasillo();
                    }
                    break;

            }
            if (!posicionActual.Equals("Companero"))
            {
                canvas.Visibility = Visibility.Hidden;
                video.Source = new Uri(rutaVideo + posicionAnterior + posicionActual + ".mkv", UriKind.RelativeOrAbsolute);
                video.Visibility = Visibility.Visible;
                video.Play();
                todosDisenable();
            }
            
        }

        private void comprobarEstadoCompanero()
        {
            btnPasilloG.Visibility = Visibility.Visible;
            if (posicionActual.Equals("Escritorio"))
                Companero = "";
            if (Companero.Equals("Escritorio"))
                btnEscritorio.Visibility = Visibility.Visible;
        }

        private void comprobarEstadoSalon()
        {
            btnPasilloS.Visibility = Visibility.Visible;
            if (posicionActual.Equals("Casero"))
                Salon = "";
            if (Salon.Equals("Casero"))
                btnCasero.Visibility = Visibility.Visible;
        }

        private void comprobarEstadoCocina()
        {
            btnPasilloC.Visibility = Visibility.Visible;
            switch (posicionActual)
            {
                case "Mesa":
                    Cocina[0] = "";
                    break;
                case "Armario":
                    Cocina[1] = "";
                    break;
                case "Microondas":
                    Cocina[2] = "";
                    break;
            }
            foreach (String boton in Cocina)
            {
                switch (boton)
                {
                    case "Mesa":
                        btnMesa.Visibility = Visibility.Visible;
                        break;
                    case "Armario":
                        btnArmario.Visibility = Visibility.Visible;
                        break;
                    case "Microondas":
                        btnMicroondas.Visibility = Visibility.Visible;
                        break;
                }
            }
        }

        private void comprobarEstadoPasillo()
        {
            btnHabitacion.Visibility = Visibility.Visible;
            btnSalon.Visibility = Visibility.Visible;
            btnCompanero.Visibility = Visibility.Visible;
            btnCocina.Visibility = Visibility.Visible;
        }

        private void desactivacionBotones()
        {
            btnHabitacion.Visibility = Visibility.Hidden;
            btnOrdenador.Visibility = Visibility.Hidden;
            btnPapelera.Visibility = Visibility.Hidden;
            btnPasilloH.Visibility = Visibility.Hidden;
            btnPlato.Visibility = Visibility.Hidden;

            btnCocina.Visibility = Visibility.Hidden;
            btnCompanero.Visibility = Visibility.Hidden;
            btnSalon.Visibility = Visibility.Hidden;

            btnArmario.Visibility = Visibility.Hidden;
            btnMesa.Visibility = Visibility.Hidden;
            btnMicroondas.Visibility = Visibility.Hidden;
            btnPasilloC.Visibility = Visibility.Hidden;

            btnCasero.Visibility = Visibility.Hidden;
            btnPasilloS.Visibility = Visibility.Hidden;

            btnEscritorio.Visibility = Visibility.Hidden;
            btnPasilloG.Visibility = Visibility.Hidden;
        }

        private void comprobarEstadoHabitacion()
        {
            btnPasilloH.Visibility = Visibility.Visible;
            Debug.WriteLine(posicionActual);
            switch (posicionActual)
            {
                case "Papelera":
                    Habitacion[0] = "";
                    break;
                case "Ordenador":
                    Habitacion[1] = "";
                    break;
                case "Plato":
                    Habitacion[2] = "";
                    break;
            }
            foreach (String boton in Habitacion)
            {
                switch (boton)
                {
                    case "Papelera":
                        btnPapelera.Visibility = Visibility.Visible;
                        break;
                    case "Ordenador":
                        btnOrdenador.Visibility = Visibility.Visible;
                        break;
                    case "Plato":
                        btnPlato.Visibility = Visibility.Visible;
                        break;
                }
            }
        }

        private void videoTerminado(object sender, RoutedEventArgs e)
        {
            video.Visibility = Visibility.Hidden;
            canvas.Visibility = Visibility.Visible;
            ImageBrush myBrush = new ImageBrush();
            Image image = new Image();
            if(posicionActual.Equals("Companero") && !llave)
            {
                image.Source = new BitmapImage(new Uri(rutaImagen+"PasilloCCompaneroB.png", UriKind.RelativeOrAbsolute));
                Debug.WriteLine(posicionActual);
                cargarAnimacion(posicionActual);
                myBrush.ImageSource = image.Source;
                canvas.Background = myBrush;
            }
            else if (posicionActual.Equals("Companero") && llave)
            {
                MessageBoxResult result3 = MessageBox.Show("¿Desea abrir la puerta?", "Decisión", MessageBoxButton.YesNo);
                switch (result3)
                {
                    case MessageBoxResult.Yes:
                        image.Source = new BitmapImage(new Uri(rutaImagen + posicionActual + ".png", UriKind.Absolute));
                        myBrush.ImageSource = image.Source;
                        canvas.Background = myBrush;
                        desactivacionBotones();
                        comprobarEstadoCompanero();
                        cargarAnimacion(posicionActual);
                        myBrush.ImageSource = image.Source;
                        canvas.Background = myBrush;
                        break;
                    case MessageBoxResult.No:
                        ImageBrush myBrush1 = new ImageBrush();
                        Image image1 = new Image();
                        image1.Source = new BitmapImage(new Uri(rutaImagen + "PasilloCCompaneroB.png", UriKind.Absolute));
                        desactivacionBotones();
                        comprobarEstadoPasillo();
                        todosEnable();
                        myBrush1.ImageSource = image1.Source;
                        canvas.Background = myBrush1;
                        break;
                }
            }
            else
            {
                image.Source = new BitmapImage(new Uri(rutaImagen + posicionActual + ".png", UriKind.Absolute));
                cargarAnimacion(posicionActual);
                myBrush.ImageSource = image.Source;
                canvas.Background = myBrush;
            }         
        }

        private void todosEnable()
        {
            btnArmario.IsEnabled = true;
            btnCasero.IsEnabled = true;
            btnCocina.IsEnabled = true;
            btnCompanero.IsEnabled = true;
            btnEscritorio.IsEnabled = true;
            btnHabitacion.IsEnabled = true;
            btnMesa.IsEnabled = true;
            btnMicroondas.IsEnabled = true;
            btnPapelera.IsEnabled = true;
            btnOrdenador.IsEnabled = true;
            btnPasilloC.IsEnabled = true;
            btnPasilloG.IsEnabled = true;
            btnPasilloH.IsEnabled = true;
            btnPasilloS.IsEnabled = true;
            btnPlato.IsEnabled = true;
            btnSalon.IsEnabled = true;


        }
        private void todosDisenable()
        {
            btnArmario.IsEnabled = false;
            btnCasero.IsEnabled = false;
            btnCocina.IsEnabled = false;
            btnCompanero.IsEnabled = false;
            btnEscritorio.IsEnabled = false;
            btnHabitacion.IsEnabled = false;
            btnMesa.IsEnabled = false;
            btnMicroondas.IsEnabled = false;
            btnPapelera.IsEnabled = false;
            btnOrdenador.IsEnabled = false;
            btnPasilloC.IsEnabled = false;
            btnPasilloG.IsEnabled = false;
            btnPasilloH.IsEnabled = false;
            btnPasilloS.IsEnabled = false;
            btnPlato.IsEnabled = false;
            btnSalon.IsEnabled = false;


        }

        private void cargarAnimacion(String posicionActual)
        {
            Storyboard aux;
            if (!posicionActual.Equals("PasilloG") && !posicionActual.Equals("PasilloC") && !posicionActual.Equals("Salon") && !posicionActual.Equals("PasilloS") && !posicionActual.Equals("Habitacion"))
            {

                if (posicionActual.Equals("Companero") && !llave)
                {
                    Bocadillo.Visibility = Visibility.Visible;
                    imgPersonaje.Visibility = Visibility.Visible;
                    textoBocadillo.Visibility = Visibility.Visible;
                    aux = (Storyboard)this.Resources["Cerrado"];
                    aux.Completed += new EventHandler(animacionterminada);
                    aux.Begin();
                }
                else
                {
                    if (posicionActual.Equals("Casero"))
                    {
                        imgPersonaje_Copy.Visibility = Visibility.Visible;
                    }
                    if (posicionActual.Equals("PasilloH"))
                    {
                        if (hab)
                        {
                            hab = false;
                            ponerComunicacion();

                        }
                        else
                        {
                            todosEnable();
                        }

                    }
                    else if (posicionActual.Equals("Cocina"))
                    {
                        if (cocina)
                        {
                            cocina = false;
                            ponerComunicacion();
                        }
                        else
                        {
                            todosEnable();
                        }
                    }
                    else if (posicionActual.Equals("Companero"))
                    {
                        if (compa)
                        {
                            compa = false;
                            ponerComunicacion();
                        }
                        else
                        {
                            todosEnable();
                        }
                    }
                    else
                    {
                        
                        ponerComunicacion();

                    }

                }


            }
            else
            {
                todosEnable();
            }     
        }
        private void animacionterminada(object sender, EventArgs e)
        {
            Storyboard segunda = (Storyboard)this.Resources["segundaConversacion"]; ;
            Storyboard primera = (Storyboard)this.Resources["Ordenador1"]; ;
            todosEnable();
            Bocadillo.Visibility = Visibility.Hidden;
            imgPersonaje.Visibility = Visibility.Hidden;
            textoBocadillo.Visibility = Visibility.Hidden;
            primera.Completed += new EventHandler(animacionEspecial);
            segunda.Completed += new EventHandler(decisionFinal);
            primera.Begin();
            primera.Pause();
            switch (posicionActual)
            {
                case "Ordenador":
                    MessageBoxResult result = MessageBox.Show("¿Desea abrir el correo?", "Decision", MessageBoxButton.YesNo);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            Bocadillo.Visibility = Visibility.Visible;
                            imgPersonaje.Visibility = Visibility.Visible;
                            textoBocadillo.Visibility = Visibility.Visible;

                            primera.Resume();
                            

                            imgInteraccion.Visibility = Visibility.Visible;
                            imgInteraccion.Source = new BitmapImage(new Uri(rutaImagen+"correo.png", UriKind.Absolute));
                            break;
                        case MessageBoxResult.No:
                            break;
                    }
                    break;
                case "Papelera":
                    imgInteraccion.Visibility = Visibility.Visible;
                    imgInteraccion.Source = new BitmapImage(new Uri(rutaImagen + "imgpapelera.png", UriKind.Absolute));
                    imgInteraccion.Stretch = Stretch.Uniform;
                    break;
                case "Plato":
                    imgInteraccion.Visibility = Visibility.Visible;
                    imgInteraccion.Source = new BitmapImage(new Uri(rutaImagen + "imgplato.png", UriKind.Absolute));
                    imgInteraccion.Stretch = Stretch.Uniform;
                    break;
                case "Armario":
                    imgInteraccion.Visibility = Visibility.Visible;
                    imgInteraccion.Source = new BitmapImage(new Uri(rutaImagen + "armarioLleno.png", UriKind.Absolute));
                    imgInteraccion.Stretch = Stretch.Uniform;
                    break;
                case "Microondas":
                    imgInteraccion.Visibility = Visibility.Visible;
                    imgInteraccion.Source = new BitmapImage(new Uri(rutaImagen + "imgmicrondas.png", UriKind.Absolute));
                    imgInteraccion.Stretch = Stretch.Uniform;
                    break;
                case "Mesa":
                    MessageBoxResult result2 = MessageBox.Show("¿Desea coger la llave?", "Decision", MessageBoxButton.YesNo);
                    switch (result2)
                    {
                        case MessageBoxResult.Yes:
                            imgInventario.Visibility = Visibility.Visible;
                            imgInventario.Source = new BitmapImage(new Uri(rutaImagen + "llave.png", UriKind.Absolute));
                            imgInventario.Stretch = Stretch.Uniform;
                            llave = true;
                            break;
                        case MessageBoxResult.No:
                            break;
                    }
                    break;
                case "Escritorio":
                    imgInteraccion.Visibility = Visibility.Visible;
                    imgInteraccion.Source = new BitmapImage(new Uri(rutaImagen + "Pagina.png", UriKind.Absolute));
                    imgInteraccion.Stretch = Stretch.Uniform;
                    break;
                case "Casero":
                    MessageBoxResult result1 = MessageBox.Show("¿Es gorka el culpable?", "Decision", MessageBoxButton.YesNo);
                    switch (result1)
                    {
                        case MessageBoxResult.Yes:
                            VentanaBienvenida final = new VentanaBienvenida();
                            this.Hide();
                            final.ShowDialog();
                            this.Close();
                            break;
                        case MessageBoxResult.No:
                            Bocadillo.Visibility = Visibility.Visible;
                            imgPersonaje.Visibility = Visibility.Visible;
                            textoBocadillo.Visibility = Visibility.Visible;
                            segunda.Begin();
                            break;           
                    }
                    break;
            }


        }

        private void decisionFinal(object sender, EventArgs e)
        {
            Decision final = new Decision();
            this.Hide();
            final.ShowDialog();
            this.Close();
        }

        private void animacionEspecial(object sender, EventArgs e)
        {
            Bocadillo.Visibility = Visibility.Hidden;
            imgPersonaje.Visibility = Visibility.Hidden;
            textoBocadillo.Visibility = Visibility.Hidden;
            imgPersonaje_Copy.Visibility = Visibility.Hidden;
        }
        private void mouseMover(object sender, MouseEventArgs e)
        {
            System.Windows.Point position = e.GetPosition(this);
            double pXIzq = position.X / 5;
            double pYIqz = position.Y / 5;

            double pXDer = position.X / 5;
            double pYDer = position.Y / 5;

            // Sets the Height/Width of the circle to the mouse coordinates.
            if (pYIqz > 108)
            {
                pYIqz = 108;
            }
            if (pXIzq > 177)
            {
                pXIzq = 177;
            }
            if (pYIqz < 98)
            {
                pYIqz = 98;
            }
            if (pXIzq < 170)
            {
                pXIzq = 170;
            }


            if (pYDer > 108)
            {
                pYDer = 108;
            }
            if (pXDer > 210)
            {
                pXDer = 210;
            }
            if (pYDer < 98)
            {
                pYDer = 98;
            }
            if (pXDer < 200)
            {
                pXDer = 200;
            }
            ojoIzq.SetValue(Canvas.TopProperty, pYIqz);
            ojoIzq.SetValue(Canvas.LeftProperty, pXIzq);
            ojoDer.SetValue(Canvas.TopProperty, pYDer);
            ojoDer.SetValue(Canvas.LeftProperty, pXDer);
        }
        private void ponerComunicacion()
        {
            Storyboard aux;
            Bocadillo.Visibility = Visibility.Visible;
            imgPersonaje.Visibility = Visibility.Visible;
            textoBocadillo.Visibility = Visibility.Visible;
            aux = (Storyboard)this.Resources[posicionActual];
            aux.Completed += new EventHandler(animacionterminada);
            aux.Begin();
        }
    }
}
