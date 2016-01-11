using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadWriteJSONFiles.Modelo
{
    class Detalle
    {
        public int ID { get; set; }
        public Producto Producto;
        public float Cantidad { get; set; }
        public float Descuento { get; set; }

    }
}
