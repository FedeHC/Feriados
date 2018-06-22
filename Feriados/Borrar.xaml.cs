using System;
using System.Windows;

namespace Feriados
{
    public partial class VentanaBorrar : Window
    {
        public VentanaBorrar()
        {
            // Iniciando y colocando la ventana en el centro de la pantalla:
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        // Evento inicial:
        private void VentanaCargada(object sender, RoutedEventArgs e)
        {
            // Para lograr mostrar datos de la DB en tabla:
            this.DataContext =Vars.dt.DefaultView;
        }

        // Eventos particulares:

        private void ClickDgFeriados(object sender, RoutedEventArgs e)
        {
            // Cambiar indice al de la fila elegida en la tabla:
            Vars.nroIndice = dgFeriadosBorrar.SelectedIndex;

            // Cambiar textbox correspondiente, con el valor corregido:
            txtNroRegistro.Text = (Vars.nroIndice + 1).ToString();
        }

        private void BotonBorrar(object sender, RoutedEventArgs e)
        {
            int nroRegistro = Vars.nroIndice;

            // Ventana de confirmación para borrar registro seleccionado:
            if (MessageBoxResult.OK == MessageBox.Show("¿Está seguro de querer borrar el registro n°" + (nroRegistro+1) + "?",
                            "Confirmar acción",
                            MessageBoxButton.OKCancel,
                            MessageBoxImage.Question,
                            MessageBoxResult.Cancel))
            {
                // Si se hace click en "OK" entonces se procede a ocultar el registro
                // (la última columna "Ocultar" se setea a "true"):
                try
                    {
                    // Borrando registro:
                    Vars.dt.Rows[nroRegistro].Delete();

                    // Guardando cambios en DB:
                    Vars.ta.Update(Vars.dt);

                    // Mostrando mensaje en 'lbEstado':
                    lbEstado.Content = "Se ha BORRADO con éxito un registro de la tabla.";

                    // Sumar 1 al contador de registros:
                    Vars.cantRegistros =Vars.dt.Rows.Count;
                }
                    catch (Exception error)
                {
                    MessageBox.Show("MENSAJE:\n" + error.Message + "\n\nSTACKTRACE:\n" + error.StackTrace,
                        "Error al borrar registro",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }

                // Mostrando mensaje en 'lbEstado':
                lbEstado.Content = "Se ha BORRADO con éxito un registro de la tabla.";

                // Se actualiza los datos de la tabla:
                this.DataContext =Vars.dt.DefaultView;
                dgFeriadosBorrar.SelectedIndex = 0;
                Vars.nroIndice = dgFeriadosBorrar.SelectedIndex;
                dgFeriadosBorrar.ScrollIntoView(dgFeriadosBorrar.SelectedIndex);

                // Y por último cambiar textbox correspondiente:
                txtNroRegistro.Text = (Vars.nroIndice + 1).ToString();
            }
        }

        private void BotonVolver(object sender, RoutedEventArgs e)
        {
            // Cerrando la ventana actual:
            this.Close();
        }
    }
}