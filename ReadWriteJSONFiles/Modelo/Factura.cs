using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadWriteJSONFiles.Modelo
{
    public class Factura
    {
        public int ID { get; set; }
        public DateTime Fecha { get; set; }
        public Cliente Cliente;
        public List<Detalle> ListaDetalles;




    }
}
