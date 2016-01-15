using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadWriteJSONFiles.Modelo
{
    public class Detalle
    {
        public int ID { get; set; }
        public Producto Producto;
        public double Cantidad { get; set; }
        public double Descuento { get; set; }

    }
}
