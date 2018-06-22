using System;
using System.Data;
using System.Windows;

namespace Feriados
{
    public partial class VentanaAgregar : Window
    {
        public VentanaAgregar()
        {
            // Iniciando y colocando la ventana en el centro de la pantalla:
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        // Evento inicial:
        private void VentanaCargada(object sender, RoutedEventArgs e)
        {
            // Obteniendo el n° del último registro:
            PasarAlSiguiente();
        }

        // Métodos particulares:

        private void PasarAlSiguiente()
        {
            // Se muestra el n° del último registro a agregar en el label correspondiente
            // (Ej: si hay 10 registros, el registro a agregar seria el 11):
            lbUltimoId.Content = Vars.cantRegistros + 1;
        }

        private bool ChequearCampos()
        {
            // Si hay campos vacíos al agregar registro, se devuelve "false":
            if (txtNombre.Text.Equals("") || dpFecha.SelectedDate.Equals("") || txtDescripcion.Text.Equals(""))
                return false;
            // En caso contrario, si hay contenidos se devuelve "true":
            else
                return true;
        }

        // Eventos particulares:

        private void BotonAgregar(object sender, RoutedEventArgs e)
        {
            // Si hay contenidos en todos los campos, se prosigue:
            if (ChequearCampos())
            {
                // Ventana de confirmación para borrar registro seleccionado:
                if (MessageBoxResult.OK == MessageBox.Show("¿Desea agregar este registro?",
                                           "Confirmar acción",
                                           MessageBoxButton.OKCancel,
                                           MessageBoxImage.Question,
                                           MessageBoxResult.Cancel))
                {
                    // Si se hace click en "OK" entonces se procede a crear el registro.

                    // Creando variables:
                    int numero = Vars.cantRegistros + 1;
                    string nombre = txtNombre.Text;
                    string fecha = dpFecha.SelectedDate.Value.Date.ToShortDateString();
                    bool? inamovible = chkInamovible.IsChecked;
                    bool? religioso = chkReligioso.IsChecked;
                    string descripcion = txtDescripcion.Text;

                    try
                    {
                        // Creando variable datarow:
                        DataRow ultimo = Vars.dt.NewRow();

                        // Creando campos para el registro a agregar:
                        ultimo["id"] = numero;
                        ultimo["Nombre"] = nombre;
                        ultimo["Fecha"] = fecha;
                        ultimo["Inamovible"] = inamovible;
                        ultimo["Religioso"] = religioso;
                        ultimo["Descripcion"] = descripcion;
                        
                        // Agregando registro a la tabla:
                        Vars.dt.Rows.Add(ultimo);

                        // Guardando cambios en DB:
                        Vars.ta.Update(Vars.dt);
                   
                        // Borrando los campos:
                        BotonBorrar(sender, e);

                        // Mostrando mensaje en 'lbEstado':
                        lbEstado.Content = "Se ha AGREGADO con éxito un registro a la tabla.";

                        // Sumar 1 al contador de registros:
                        Vars.cantRegistros = Vars.dt.Rows.Count;

                        // Mostrar el sig. registro después del último en ventana:
                        PasarAlSiguiente();
                    }
                    catch(Exception error)
                    {
                        MessageBox.Show("MENSAJE:\n" + error.Message + "\n\nSTACKTRACE:\n" + error.StackTrace,
                            "Error al agregar registro",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    }                   
                }
            }
            else
            {
                // Caso contrario, se abre ventana emergente avisando:
                MessageBox.Show("Llene primero todos los campos antes de agregar un registro a la tabla.",
                                "Error al agregar registro:",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        private void BotonBorrar(object sender, RoutedEventArgs e)
        {
           // Chequeando si hay texto en alguno de los campos de la ventana:
            if (!txtNombre.Text.Equals("") || !txtDescripcion.Text.Equals("") || !dpFecha.Text.Equals(""))
            {
                // Borrando todos los campos y checkboxs, sin guardar nada:
                txtNombre.Text = "";
                txtDescripcion.Text = "";
                dpFecha.SelectedDate = null;
                chkInamovible.IsChecked = false;
                chkReligioso.IsChecked = false;

                // Mostrando mensaje por label de estado solo si contiene mensaje viejo:
                lbEstado.Content = "Campos BORRADOS.";
            }
        }

        private void BotonVolver(object sender, RoutedEventArgs e)
        {
            // Cerrando la ventana actual:
            this.Close();
        }
    }
}