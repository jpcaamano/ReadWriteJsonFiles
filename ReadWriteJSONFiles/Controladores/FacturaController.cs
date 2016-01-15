using Newtonsoft.Json;
using ReadWriteJSONFiles.Modelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadWriteJSONFiles.Controladores
{
    public static class FacturaController
    {
        public static List<Factura> JSONListadoDeFacturas()
        {
            try
            {
                string cadenaFacturas = LeerDatosDeFacturas();
                var listadoFacturas = JsonConvert.DeserializeObject<List<Factura>>(cadenaFacturas) ?? new List<Factura>();
                return listadoFacturas;
            }
            catch (Exception e)
            {
                Exception baseException = e.GetBaseException();
                Console.WriteLine("Error: {0}, Message: {1}", e.Message, baseException.Message);
                return null;
            }
        }
        public static int ListadoDeFacturasJSON(List<Factura> listadoFacturas)
        {
            try
            {
                string cadena = JsonConvert.SerializeObject(listadoFacturas);
                return EscribirDatosDeFacturas(cadena);
            }
            catch (Exception e)
            {
                Exception baseException = e.GetBaseException();
                Console.WriteLine("Error: {0}, Message: {1}", e.Message, baseException.Message);
                return 0;
            }
        }
        private static string LeerDatosDeFacturas()
        {
            try
            {
                string cadena = File.ReadAllText(@"C:\Test\Facturas.json");
                return cadena;
            }
            catch (Exception e)
            {
                Exception baseException = e.GetBaseException();
                Console.WriteLine("Error: {0}, Message: {1}", e.Message, baseException.Message);
                return null;
            }
        }
        private static int EscribirDatosDeFacturas(string cadena)
        {
            try
            {
                //StreamWriter file = new StreamWriter(@"C:\Test\Facturas.json");
                //file.WriteAsync(cadena);
                File.WriteAllText(@"C:\Test\Facturas.json", cadena);
                return 1;
            }
            catch (Exception e)
            {
                Exception baseException = e.GetBaseException();
                Console.WriteLine("Error: {0}, Message: {1}", e.Message, baseException.Message);
                return 0;
            }
        }
    }
}
