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
    public static class ClienteController
    {
        public static List<Cliente> JSONListadoDeClientes()
        {
            try
            {
                string cadenaClientes = LeerDatosDeCliente();
                var listadoClientes = JsonConvert.DeserializeObject<List<Cliente>>(cadenaClientes) ?? new List<Cliente>();
                return listadoClientes;
            }
            catch (Exception e)
            {
                Exception baseException = e.GetBaseException();
                Console.WriteLine("Error: {0}, Message: {1}", e.Message, baseException.Message);
                return null;
            }
        }
        public static int ListadoDeClientesJSON(List<Cliente> listadoClientes)
        {
            try
            {
                string cadena = JsonConvert.SerializeObject(listadoClientes);
                return EscribirDatosDeCliente(cadena);
            }
            catch (Exception e)
            {
                Exception baseException = e.GetBaseException();
                Console.WriteLine("Error: {0}, Message: {1}", e.Message, baseException.Message);
                return 0;
            }
        }
        private static string LeerDatosDeCliente()
        {
            try
            {
                string cadena = File.ReadAllText(@"C:\Test\Clientes.json");
                return cadena;
            }
            catch (Exception e)
            {
                Exception baseException = e.GetBaseException();
                Console.WriteLine("Error: {0}, Message: {1}", e.Message, baseException.Message);
                return null;
            }
        }
        private static int EscribirDatosDeCliente(string cadena)
        {
            try
            {
                //StreamWriter file = new StreamWriter(@"C:\Test\Clientes.json");
                //file.Write(cadena);
                File.WriteAllText(@"C:\Test\Clientes.json", cadena);
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
