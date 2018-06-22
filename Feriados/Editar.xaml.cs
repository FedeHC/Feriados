using System;
using System.Windows;

namespace Feriados
{
    public partial class VentanaEditar : Window
    {
        public VentanaEditar()
        {
            // Iniciando y colocando la ventana en el centro de la pantalla:
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        // Evento inicial:
        private void VentanaCargada(object sender, RoutedEventArgs e)
        {
            // Para lograr mostrar datos de la DB en tabla:
            this.DataContext = Vars.dt.DefaultView;
        }

        // Eventos particulares:

        private void BotonGuardar(object sender, RoutedEventArgs e)
        {
            // Actualizando datagrid:
            dgFeriadosEdit.Items.Refresh();

            // Actualizando DB:
            try
            {
                // Guardando cambios en DB:
                Vars.ta.Update(Vars.dt);
            }
            catch (Exception error)
            {
                MessageBox.Show("MENSAJE:\n" + error.Message + "\n\nSTACKTRACE:\n" + error.StackTrace,
                    "Error al editar registro",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

            // Mostrando mensaje en 'lbEstado':
            lbEstado.Content = "Los últimos cambios en la tabla han sido GUARDADOS.";
        }

        private void BotonVolver(object sender, RoutedEventArgs e)
        {
            // Cerrando la ventana actual:
            this.Close();
        }
    }
}