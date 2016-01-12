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
    public static class ProductoController
    {
        public static List<Producto> JSONListadoDeProductos()
        {
            try
            {
                string cadenaProductos = LeerDatosDeProductos();
                var listadoProductos = JsonConvert.DeserializeObject<List<Producto>>(cadenaProductos) ?? new List<Producto>();
                return listadoProductos;
            }
            catch (Exception e)
            {
                Exception baseException = e.GetBaseException();
                Console.WriteLine("Error: {0}, Message: {1}", e.Message, baseException.Message);
                return null;
            }
        }
        public static int ListadoDeProductosJSON(List<Producto> listadoProductos)
        {
            try
            {
                string cadena = JsonConvert.SerializeObject(listadoProductos);
                return EscribirDatosDeProductos(cadena);
            }
            catch (Exception e)
            {
                Exception baseException = e.GetBaseException();
                Console.WriteLine("Error: {0}, Message: {1}", e.Message, baseException.Message);
                return 0;
            }
        }
        private static string LeerDatosDeProductos()
        {
            try
            {
                string cadena = File.ReadAllText(@"C:\Test\Productos.JSON");
                return cadena;
            }
            catch (Exception e)
            {
                Exception baseException = e.GetBaseException();
                Console.WriteLine("Error: {0}, Message: {1}", e.Message, baseException.Message);
                return null;
            }
        }
        private static int EscribirDatosDeProductos(string cadena)
        {
            try
            {
                StreamWriter file = new StreamWriter(@"C:\Test\Productos.JSON");
                file.WriteAsync(cadena);
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
