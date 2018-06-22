using Feriados.DBFeriadosTableAdapters;

namespace Feriados
{
    public class Vars
    {
        // Variables estáticas a usar en el programa:
        public static DBFeriados db;
        public static DBFeriados.tbFeriadosDataTable dt;
        public static tbFeriadosTableAdapter ta = new tbFeriadosTableAdapter();

        // Variables a usar en tabla:
        public static int nroIndice = 0;
        public static int cantRegistros = 0;
    }
}