using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadWriteJSONFiles.Modelo
{
    public class Producto
    {
        public string ID { get; set; }
        public string Nombre  { get; set; }
        public double Precio { get; set; }
        public double Impuesto { get; set; }
    }
}
