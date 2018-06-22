using System.Data;
using System.Windows;

namespace Feriados
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            // Iniciando y colocando la ventana en el centro de la pantalla:
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        // Evento inicial:
        private void VentanaCargada(object sender, RoutedEventArgs e)
        {
            // DB:
            Vars.db = new DBFeriados();
            Vars.dt = new DBFeriados.tbFeriadosDataTable();
            Vars.ta.Fill(Vars.dt);
            DataContext = Vars.dt.DefaultView;

            // Obteniendo la cantidad de registros de la tabla:
            Vars.cantRegistros = Vars.dt.Rows.Count;

            // Mostrar datos del 1er registro de DataTable en ventana principal:
            MostrarDatos(Vars.dt.Rows[Vars.nroIndice]);
        }

        // Método particular:
        private void MostrarDatos(DataRow dr)
        {
            // Poner en foco la fila seleccionada en tabla:
            dgFeriados.SelectedIndex = Vars.nroIndice;

            // Nro. (id):
            txtNro.Text = dr["id"].ToString();

            // Nombre:
            txtNombre.Text = dr["Nombre"].ToString();

            // Fecha:
            txtFecha.Text = dr["Fecha"].ToString();

            // ¿Inamovible?:
            if (dr["Inamovible"].ToString().Equals("True"))
                chkInamovible.IsChecked = true;
            else
                chkInamovible.IsChecked = false;

            // ¿Religioso?:
            if (dr["Religioso"].ToString().Equals("True"))
                chkReligioso.IsChecked = true;
            else
                chkReligioso.IsChecked = false;

            // Detalles:
            txtDescripcion.Text = dr["Descripcion"].ToString();

            // Seleccionar fila en datagrid:
            dgFeriados.ScrollIntoView(dgFeriados.SelectedItem);
        }

        // Eventos particulares:

        private void EnterTxtNro(object sender, System.Windows.Input.KeyEventArgs e)
        {
            // Si se presiona enter en 'txtNro' se ejecuta lo de 'BotonIrNro':
            if (e.Key == System.Windows.Input.Key.Enter)
                BotonIrNro(sender, e);
        }

        private void BotonIrNro(object sender, RoutedEventArgs e)
        {
            // Obteniendo contenido de 'txtNro':
            string contenido = txtNro.Text;

            // [Si hubo problemas]

            // Si no se puede parsear se sale sin cambios:
            if (!int.TryParse(contenido, out Vars.nroIndice))
                return;

            // Si el nro. parseado es cero, negativo o mayor a cantRegistros,
            // se sale también sin cambios:
            if (Vars.nroIndice <= 0 || Vars.nroIndice > Vars.cantRegistros)
                return;

            // [Si NO hubo problemas]

            // Restar 1 a nroIndice para compensar:
            Vars.nroIndice--;

            // Actualizar datos en ventana con el nuevo indice cambiado::
            MostrarDatos(Vars.dt.Rows[Vars.nroIndice]);
        }

        private void BotonPrimero(object sender, RoutedEventArgs e)
        {
            // Se vuelve al primer nro./id:
            Vars.nroIndice = 0;

            // Se actualiza los datos mostrados en la ventana:
            MostrarDatos(Vars.dt.Rows[Vars.nroIndice]);
        }

        private void BotonAtras(object sender, RoutedEventArgs e)
        {
            // Si el usuario NO está en el 1er indice:
            if (Vars.nroIndice != 0)
                Vars.nroIndice--;
            // Si lo está, se vuelve al último indice:
            else
                Vars.nroIndice = Vars.cantRegistros - 1;

            // Se actualiza los datos mostrados en la ventana:
            MostrarDatos(Vars.dt.Rows[Vars.nroIndice]);
        }

        private void BotonSiguiente(object sender, RoutedEventArgs e)
        {
            // Si el usuario NO está en el último indice:
            if (Vars.nroIndice != Vars.cantRegistros - 1)
            {
                Vars.nroIndice++;
            }
            // Si lo está, se vuelve al primer indice:
            else
                Vars.nroIndice = 0;

            // Se actualiza los datos mostrados en la ventana:
            MostrarDatos(Vars.dt.Rows[Vars.nroIndice]);
        }

        private void BotonUltimo(object sender, RoutedEventArgs e)
        {
            // Se vuelve al último id:
            Vars.nroIndice = Vars.cantRegistros - 1;

            // Se actualiza los datos mostrados en la ventana:
            MostrarDatos(Vars.dt.Rows[Vars.nroIndice]);
        }

        private void EnterTxtBuscar(object sender, System.Windows.Input.KeyEventArgs e)
        {
            // Si se presiona enter en 'txtBuscar' se ejecuta lo de 'botonIr':
            if (e.Key == System.Windows.Input.Key.Enter)
                BotonIrBuscar(sender, e);
        }

        private void BotonIrBuscar(object sender, RoutedEventArgs e)
        {
            // Al hacer click en el botón ir, lo escrito en el campo 'txtBuscar' se busca
            // en la columna "Nombre" del DataGrid hasta dar con la 1ra coincidencia:
            string palabra = txtBuscar.Text;

            // Si se escribió alguna palabra en el textbox se procede a buscar:
            if (!palabra.Equals(""))
            {
                palabra = palabra.ToLower();
                for (int c = 0; c < Vars.dt.Rows.Count; c++)
                {
                    // Obteniendo el nombre del feriado de la fila actual
                    // y se lo pasa a minusculas:
                    string celda = (string)Vars.dt.Rows[c]["Nombre"];
                    celda = celda.ToLower();

                    // Si hay coincidencia, saltar a esa fila de la tabla:
                    if (celda.Contains(palabra))
                    {
                        // Se actualiza los datos con nuevo valor:
                        MostrarDatos(Vars.dt.Rows[c]);
                        dgFeriados.SelectedIndex = c;
                        dgFeriados.ScrollIntoView(dgFeriados.SelectedItem);

                        // Cortamos la búsqueda en ese resultado encontrado:
                        break;
                    }
                }
            }
        }

        private void ClickDgFeriados(object sender, RoutedEventArgs e)
        {
            // Cambiar indice al de la fila elegida en la tabla:
            Vars.nroIndice = dgFeriados.SelectedIndex;

            // Actualizar datos en ventana con el nuevo indice cambiado::
            MostrarDatos(Vars.dt.Rows[Vars.nroIndice]);
        }

        private void BotonAgregar(object sender, RoutedEventArgs e)
        {
            // Abriendo nueva ventana de "Agregar":
            VentanaAgregar vntAgregar = new VentanaAgregar();
            vntAgregar.Show();
        }

        private void BotonBorrar(object sender, RoutedEventArgs e)
        {
            // Abriendo nueva ventana de "Borrar":
            VentanaBorrar vntBorrar = new VentanaBorrar();
            vntBorrar.Show();
        }

        private void BotonEditar(object sender, RoutedEventArgs e)
        {
            // Abriendo nueva ventana de "Editar":
            VentanaEditar vntEditar = new VentanaEditar();
            vntEditar.Show();
        }

        private void VentanaFoco(object sender, System.EventArgs e)
        {
            // Al tener foco nuevamente en la ventana, actualizamos la DataGrid
            // para ver los cambios hechos desde la ventana Editar:
            DataContext = Vars.dt.DefaultView;
        }
    }
}